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
        builder.Services.AddTransient<IPaymentService, PaymentService>();
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
        builder.Services.AddTransient<ILiabilitiesService, LiabilitiesService>(); 
        builder.Services.AddTransient<IAssetsService, AssetsService>();
        builder.Services.AddTransient<IExpenseService, ExpenseService>();
        builder.Services.AddTransient<IRevenueService, RevenueService>();   
        builder.Services.AddTransient<IDescriptionService, DescriptionService>();
        builder.Services.AddTransient<IStuffService,StuffService>();
        builder.Services.AddTransient<IBlendRatioService, BlendRatioService>();
        builder.Services.AddTransient<IWeftYarnTypeService, WeftYarnTypeService>();
        builder.Services.AddTransient<IWeavesService, WeavesService>();
        builder.Services.AddTransient<IPickInsertionService, PickInsertionService>();
        builder.Services.AddTransient<IFinalService, FinalService>();
        builder.Services.AddTransient<ISelvegeService,SelvegeService>();
        builder.Services.AddTransient<ISelvegeWeavesService, SelvegeWeavesService>();
        builder.Services.AddTransient<ISelvegeWidthService, SelvegeWidthService>();
        builder.Services.AddTransient<IPeicelengthService, PeicelengthService>();
        builder.Services.AddTransient<IPackingService, PackingService>();
        builder.Services.AddTransient<IFabricTypeService, FabricTypeService>();
        builder.Services.AddTransient<IEndUseService, EndUseService>();
        builder.Services.AddTransient<ISellerService, SellerService>();
        builder.Services.AddTransient<IBuyerService, BuyerService>();
        builder.Services.AddTransient<IWrapYarnTypeService, WrapYarnTypeService>();
        builder.Services.AddTransient<ICommisionTypeService, CommisionTypeService>();
        builder.Services.AddTransient<IDeliveryTermService, DeliveryTermService>();
        builder.Services.AddTransient<IPaymentTermService, PaymentTermService>();
        builder.Services.AddTransient<IUnitofMeasureService, UnitofMeasureService>();
        builder.Services.AddTransient<IGeneralSaleTextTypeService, GeneralSaleTextTypeService>();
        builder.Services.AddTransient<IContractService, ContractService>();
        builder.Services.AddTransient<IDispatchNoteService, DispatchNoteService>();
        builder.Services.AddTransient<IInvoiceService, InvoiceService>();
        builder.Services.AddTransient<IInspectionNoteService, InspectionNoteService>();
        builder.Services.AddTransient<ISelvegeThicknessService, SelvegeThicknessService>();
        builder.Services.AddTransient<IInductionThreadService, InductionThreadService>();
        builder.Services.AddTransient<IGsmService, GsmService>();
        builder.Services.AddTransient<ITransporterCompanyService, TransporterCompanyService>();

        /* ABL SOFTWARE  */
        builder.Services.AddTransient<IEqualityService, EqualityService>();
        builder.Services.AddTransient<IAblLiabilitiesService, AblLiabilitiesService>();
        builder.Services.AddTransient<IAblRevenueService, AblRevenueService>();
        builder.Services.AddTransient<IAblExpenseService, AblExpenseService>();
        builder.Services.AddTransient<IAblAssestsService, AblAssestsService>();
        builder.Services.AddTransient<IPartyService, PartyService>();
        builder.Services.AddTransient<ITransporterService, TransporterService>();
        builder.Services.AddTransient<IVendorService , VendorService>();
        builder.Services.AddTransient<IBrookerService, BrookerService>();
        builder.Services.AddTransient<IBusinessAssociateService, BusinessAssociateService>();



        #endregion

        builder.Services.AddIdentity<ApplicationUser, AppRole>().AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

    }
}

