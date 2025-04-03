using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Base;
using IMS.Domain.Context;
using IMS.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IMS.Business.Services
{
    public interface IAccountIdService
       : IBaseService<AccountIdReq, AccountIdRes, AccountId>

    {
        Task<Response<Guid>> CreateAccount(AccountIdReq request);
        Task<Response<List<AccountIdRes>>> GetHierarchy();
        Task<Response<List<AccountIdRes>>> GetByParent(Guid parentId);
       
        Task<Response<List<AccountIdRes>>> SearchAccounts(string searchTerm);


    }

    public class AccountIdService : BaseService<AccountIdReq, AccountIdRes,AccountIdRepository, AccountId>,IAccountIdService
    {
        
        private readonly ApplicationDbContext _context;

        public AccountIdService(IUnitOfWork unitOfWork, ApplicationDbContext context) :base(unitOfWork)
        {
            
            _context = context;
        }

        public async Task<Response<Guid>> CreateAccount(AccountIdReq request)
        {
            try
            {
                AccountId parentAccount = null;
                if (request.ParentAccountId.HasValue)
                {
                    parentAccount = await _context.AccountIds
                        .FirstOrDefaultAsync(p => p.Id == request.ParentAccountId.Value);
                    request.AccountType = parentAccount.AccountType;
                }

                var entity = request.Adapt<AccountId>();
                entity.Listid = await GenerateListId(request, parentAccount);
                
                await Repository.Add(entity);
                await _context.SaveChangesAsync();

                return new Response<Guid>
                {
                    Data = entity.Id.Value,
                    StatusMessage = "Account created successfully",
                    
                };
            }
            catch (Exception ex)
            {
                return new Response<Guid>
                {
                    StatusMessage = ex.Message,
                   
                };
            }
        }

        public async Task<string> GenerateListId(AccountIdReq request, AccountId parentAccount)
        {
            if (parentAccount == null)
            {
                // Top-level account
                var topAccounts = await _context.AccountIds
                    .Where(a => a.ParentAccountId == null && a.AccountType == request.AccountType)
                    .ToListAsync();

                return $"{(int)request.AccountType}.{(topAccounts.Count + 1):D2}";
            }

            // Calculate depth based on parent's Listid parts
            var parentParts = parentAccount.Listid.Split('.');
            int depth = parentParts.Length;

            // Get existing siblings
            var siblings = await _context.AccountIds
                .Where(a => a.ParentAccountId == parentAccount.Id)
                .OrderBy(a => a.Listid)
                .ToListAsync();

            // Determine last sequence number
            int lastNumber = siblings.Count > 0
                ? int.Parse(siblings.Last().Listid.Split('.').Last())
                : 0;

            // Format based on depth
            string format = depth switch
            {
                1 => "D2",  // Parent is top-level (e.g., "1")
                2 => "D2",  // Parent is "1.01"
                3 => "D3",  // Parent is "1.01.01"
                4 => "D4",  // Parent is "1.01.01.001"
                _ => $"D{depth}"  // Deeper levels
            };

            return $"{parentAccount.Listid}.{(lastNumber + 1).ToString(format)}";
        }

        public async Task<Response<List<AccountIdRes>>> GetHierarchy()
        {
            try
            {
                var hierarchy = await Repository.GetAllHierarchy();
                return new Response<List<AccountIdRes>>
                {
                    Data = MapHierarchy(hierarchy),
                   
                };
            }
            catch (Exception ex)
            {
                return new Response<List<AccountIdRes>>
                {
                    StatusMessage = ex.Message,
                   
                };
            }
        }

        public List<AccountIdRes> MapHierarchy(IList<AccountId> accounts)
        {
            AccountIdRes MapNode(AccountId account)
            {
                return new AccountIdRes
                {
                    Id = account.Id,
                    Listid = account.Listid,
                    Description = account.Description,
                    AccountType = account.AccountType,
                    ParentAccountId = account.ParentAccountId,
                    Children = account.Children
                        .OrderBy(c => c.Listid)
                        .Select(MapNode)
                        .ToList()
                };
            }

            return accounts.Select(MapNode).ToList();
        }

        public async Task<Response<List<AccountIdRes>>> GetByParent(Guid parentId)
        {
            try
            {
                var accounts = await Repository.GetByParent(parentId);
                return new Response<List<AccountIdRes>>
                {
                    Data = accounts.Adapt<List<AccountIdRes>>(),
                   
                };
            }
            catch (Exception ex)
            {
                return new Response<List<AccountIdRes>>
                {
                    StatusMessage = ex.Message,
                    
                };
            }
        }


        public async Task<Response<List<AccountIdRes>>> SearchAccounts(string searchTerm)
        {
            try
            {
                var accounts = await _context.AccountIds
                    .Where(a => a.Description.Contains(searchTerm))
                    .OrderBy(a => a.Listid)
                    .ProjectToType<AccountIdRes>()
                    .ToListAsync();

                return new Response<List<AccountIdRes>>
                {
                    Data = accounts,
                   
                    StatusMessage = accounts.Count > 0
                        ? "Accounts found"
                        : "No matching accounts found"
                };
            }
            catch (Exception ex)
            {
                return new Response<List<AccountIdRes>>
                {
                   
                    StatusMessage = ex.Message
                };
            }
        }

      

      

    }
}