using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IMS.DataAccess.Repositories
{
    public interface IEmployeeManagementRepository
    {
    }
    public class EmployeeManagementRepository : BaseRepository<EmployeeManagement>, IEmployeeManagementRepository
    {
        public EmployeeManagementRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        
    }
}