using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using IMS.Domain.Utilities;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace IMS.Business.Services;

public interface IBranchService : IBaseService<BranchReq, BranchRes, Branch>
{
    Task<Response<IList<BranchRes>>> GetAll(Pagination pagination, bool onlyusers);

    #region BranchSetting
    Task<Response<BranchSettingRes>> SaveSetting(BranchSettingReq req);
    Task<Response<BranchSettingRes>> GetSetting(Guid id);
    Task<MemoryStream> GetLogo(Guid id);
    #endregion
}
public class BranchService : BaseService<BranchReq, BranchRes, BranchRepository, Branch>, IBranchService
{

    private readonly IWebHostEnvironment _environment;
    private readonly string _absoluteFilePath = $"\\Media\\BranchSettings\\";
    public BranchService(IUnitOfWork unitOfWork, IWebHostEnvironment environment) : base(unitOfWork)
    {
        _environment = environment;
    }

    public async Task<Response<IList<BranchRes>>> GetAll(Pagination pagination, bool onlyusers = true)
    {
        try
        {
            var (pag, data) = await Repository.GetAll(pagination, onlyusers);

            var res = data.Adapt<List<BranchRes>>();

            return new Response<IList<BranchRes>>
            {
                Data = res,
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<BranchRes>>
            {
                StatusMessage = e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    #region BranchSetting
    public async Task<Response<BranchSettingRes>> SaveSetting(BranchSettingReq req)
    {
        var trans = await UnitOfWork.BeginTransactionAsync();
        try
        {
            BranchSetting entity = req.Adapt<BranchSetting>();
            BranchSetting? existing = await UnitOfWork._context.BranchSettings.FirstOrDefaultAsync(x => x.BranchId == entity.BranchId);

            // Add Attachment if user provide a file.                
            Attachment? attachment = null;
            if (req.RptHeaderLogo != null)
            {
                string fileName = $"--{Guid.NewGuid()}{req.RptHeaderLogo.FileName}";
                string rootPath = _environment.ContentRootPath + _absoluteFilePath;
                if (req.RptHeaderLogo.Length > 0)
                {
                    if (!Directory.Exists(rootPath))
                    {
                        Directory.CreateDirectory(rootPath);
                    }
                    using FileStream fileStream = File.Create(rootPath + fileName);
                    await req.RptHeaderLogo.CopyToAsync(fileStream);
                    attachment = new Attachment
                    {
                        Path = _absoluteFilePath + fileName,
                        Schema = nameof(BranchSetting),
                        Extension = Path.GetExtension(req.RptHeaderLogo.FileName),
                        Size = long.Parse(req.RptHeaderLogo.Length.ToString() ?? "0"),
                    };
                }
            }

            if (existing == null)
            {
                BranchSetting setting = req.Adapt<BranchSetting>();
                if (attachment != null)
                {
                    setting.RptHeaderLogo = attachment;
                }
                await UnitOfWork._context.BranchSettings.AddAsync(setting);
                await UnitOfWork.SaveAsync();
                await UnitOfWork.CommitTransactionAsync(trans);
                return new Response<BranchSettingRes>
                {
                    Data = setting.Adapt<BranchSettingRes>(),
                    StatusCode = HttpStatusCode.Created,
                    StatusMessage = "Created successfully"
                };
            }
            if (attachment != null)
            {
                await UnitOfWork._context.Attachments.AddAsync(attachment);
                await UnitOfWork.SaveAsync();

                entity.RptHeaderLogoId = attachment.Id;
            }
            UnitOfWork._context.Entry(existing).CurrentValues.SetValues(entity);
            UnitOfWork._context.Entry(existing).State = EntityState.Modified;
            await UnitOfWork.SaveAsync();
            await UnitOfWork.CommitTransactionAsync(trans);
            return new Response<BranchSettingRes>
            {
                Data = existing.Adapt<BranchSettingRes>(),
                StatusMessage = "Updated successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            await UnitOfWork.RollBackTransactionAsync(trans);
            return new Response<BranchSettingRes>
            {
                StatusMessage = e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<BranchSettingRes>> GetSetting(Guid id)
    {
        try
        {
            BranchSetting? existing = await UnitOfWork._context.BranchSettings.Include(a => a.RptHeaderLogo).FirstOrDefaultAsync(x => x.BranchId == id);
            if (existing == null)
            {
                return new Response<BranchSettingRes>
                {
                    StatusCode = HttpStatusCode.NoContent,
                    StatusMessage = "Not found."
                };
            }
            return new Response<BranchSettingRes>
            {
                Data = existing.Adapt<BranchSettingRes>(),
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<BranchSettingRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<MemoryStream> GetLogo(Guid id)
    {
        FileStreamResult fileStreamResult = null;
        var s = await UnitOfWork._context.Attachments.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (s != null)
        {
            var stream = File.Open(_environment.ContentRootPath + s.Path, FileMode.Open, FileAccess.Read);
            fileStreamResult = new FileStreamResult(stream, MimeTypes.MimeTypeMap.GetMimeType(s.Extension));
        }

        var memoryStream = new MemoryStream();
        fileStreamResult.FileStream.CopyTo(memoryStream);
        return memoryStream;
    }
    #endregion

}