using IMS.API.Utilities.Auth;
using IMS.Business.Services;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IMS.API.Utilities;

public static class ServicesInjector
{
    public static void InjectAllServices(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        var services = builder.Services;



        #region For Identity Configurations
        services.AddDbContextFactory<ApplicationDbContext>(optionsBuilder =>
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("AEMSConnection")));
        #endregion

        // Other service registrations
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAEMSAuthentication();
        builder.Services.AddAEMSAuthorization();


        #region For Repositories DI
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        #region For Services DI
        builder.Services.AddTransient<IAccountService, AccountService>();
        builder.Services.AddTransient<IOrganizationService, OrganizationService>();
        builder.Services.AddTransient<IAddressService, AddressService>();
        builder.Services.AddTransient<IBranchService, BranchService>();
        builder.Services.AddTransient<IRoleService, RoleService>();
        builder.Services.AddTransient<IUsersService, UsersService>();
        builder.Services.AddTransient<ILevelService, LevelService>();
        builder.Services.AddTransient<IStockService, StockService>();
        builder.Services.AddTransient<IStockMovementService, StockMovementService>();
        builder.Services.AddTransient<IStockReorderService, StockReorderService>();
        builder.Services.AddTransient<IPriceService, PriceService>();
        builder.Services.AddTransient<IBrandService, BrandService>();
        builder.Services.AddTransient<ICategoryService, CategoryService>();
        builder.Services.AddTransient<IProductService, ProductService>();
        builder.Services.AddTransient<IDepartmentService, DepartmentService>();
        builder.Services.AddTransient<IUnitService, UnitService>();
        builder.Services.AddTransient<IEmployeeService, EmployeeService>();
        builder.Services.AddTransient<IEmployeeManagementService, EmployeeManagementService>();
        builder.Services.AddTransient<IProjectTargetService, ProjectTargetService>();
        builder.Services.AddTransient<ISupplierService, SupplierService>();
        builder.Services.AddTransient<ICustomerService, CustomerService>(); 
        builder.Services.AddTransient<ICapitalAccountService, CapitalAccountService>(); 
        #endregion

        builder.Services.AddIdentity<ApplicationUser, AppRole>().AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

    }
}

