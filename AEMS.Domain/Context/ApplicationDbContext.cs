using IMS.Domain.Base;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.Domain.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, AppRole, Guid>
{
    public IEnumerable<object> invoice;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<AppRoleClaim> AppRoleClaims { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrganizationUser> OrganizationUsers { get; set; }
    public DbSet<OrganizationSetting> OrganizationSettings { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<BranchSetting> BranchSettings { get; set; }
    public DbSet<Level> Levels { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }
    public DbSet<StockReorder> StockReorders { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeManagement> EmployeeManagements { get; set; }
    public DbSet<ProjectTarget> ProjectTargets { get; set; }    
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CapitalAccount> CapitalAccounts { get; set; }   
    public DbSet<Liabilities> Liabilities { get; set; }
    public DbSet<Assets> Assets { get; set; }   
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Revenue> Revenues {  get; set; }   
    public DbSet<Description> Descriptions { get; set; } 
    public DbSet<Stuff> Stuffs { get; set; }
    public DbSet<BlendRatio> BlendRatio { get; set; }
    public DbSet<WrapYarnType> WrapYarnTypes { get; set; }
    public DbSet<WeftYarnType> WeftYarnTypes { get; set; }
    public DbSet<Weaves> Weaves { get; set; }
    public DbSet<PickInsertion> PickInsertion { get; set; }
    public DbSet<Final> Finals { get; set; }    
    public DbSet<Selvege> Selveges { get; set; }
    public DbSet<SelvegeWeaves> SelvegeWeaves { get; set; }
    public DbSet<SelvegeWidth> SelvegeWidths { get; set; }
    public DbSet<Peicelength> Peicelengths { get; set; }
    public DbSet<Packing> Packings { get; set; }
    public DbSet<FabricType> FabricTypes { get; set; }
    public DbSet<EndUse> EndUses { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Buyer> Buyers { get; set; }  
    public DbSet<CommisionType> CommisionTypes { get; set; }
    public DbSet<DeliveryTerm> DeliveryTerms { get; set; }
    public DbSet<PaymentTerm> PaymentTerms { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<UnitofMeasure> UnitofMeasures { get; set; }
    public DbSet<GeneralSaleTextType> GeneralSaleTexts { get; set; }
    public DbSet<Contract> contracts { get; set; }
    public DbSet<DispatchNote> DispatchNotes { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InspectionNote> InspectionNotes { get; set; }
    public DbSet<SelvegeThickness>  SelvegeThicknesses { get; set; }
    public DbSet<InductionThread> InductionThreads { get; set; }
    public DbSet<Gsm> Gsms { get; set; }
    public DbSet<TransporterCompany> TransporterCompanies { get; set; }
  /*  public DbSet<CommisionInvoice> CommisionInvoice { get; set; }*/
    public object ConversionContractRows { get; set; }
    public object RelatedInvoices { get; set; }
    public object RelatedInvoiceContracts { get; set; }

    /*ABL Software*/
    public DbSet<Equality> Equality { get; set; }
    public DbSet<AblLiabilities> AblLiabilities { get; set; }
    public DbSet<AblExpense> AblExpense { get; set; }
    public DbSet<AblRevenue> AblRevenue { get; set; }
    public DbSet<AblAssests> AblAssests { get; set; }
    public DbSet<Party> Party { get; set; } 
    public DbSet<Transporter> Transporters { get; set; }
    public DbSet<Vendor> Vendor { get; set; }
    public DbSet<Brooker> Brooker { get; set; }
    public DbSet<BusinessAssociate> BusinessAssociate { get; set; }
    public DbSet<Munshyana> Munshyana { get; set; }
    public DbSet<SalesTax> SalesTax { get; set; }
    public DbSet<BookingOrder> BookingOrder { get; set; }
    public DbSet<Consignment> Consignment { get; set; }
    public DbSet<Charges> Charges { get; set; }
    public DbSet<Receipt> Receipt { get; set; }


    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        // var userId = HttpContextAccessor.HttpContext.GetUserId();
        ChangeTracker.DetectChanges();
        var added = ChangeTracker.Entries().Where(t => t.State == EntityState.Added).Select(f => f.Entity)
            .ToArray();

        foreach (var entity in added)
        {

            switch (entity)
            {
                case IGeneralBase addedEntity:
                    addedEntity.CreatedDateTime = DateTime.UtcNow;
                    addedEntity.IsDeleted = false;
                    addedEntity.IsActive = true;
                    //addedEntity.CreatedById = userId;
                    break;
                case IMinBase minActiveBaseEntity:
                    //minActiveBaseEntity.CreatedById = userId;
                    minActiveBaseEntity.IsActive = true;
                    minActiveBaseEntity.CreatedDateTime = DateTime.UtcNow;
                    break;
                    //case IMinBase mBAddedEntity:
                    //    mBAddedEntity.CreatedDate = DateTime.UtcNow;
                    //    break;
            }
        }

        var modified = ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Modified)
            .Select(t => t.Entity)
            .ToArray();

        foreach (var entity in modified)
        {
            if (entity is not IGeneralBase gBModifiedEntity) continue;
            gBModifiedEntity.ModifiedDateTime = DateTime.UtcNow;
            //gBModifiedEntity.ModifiedById = userId;
        }
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        //var userId = HttpContextAccessor.HttpContext.GetUserId();
        ChangeTracker.DetectChanges();
        var added = ChangeTracker.Entries().Where(t => t.State == EntityState.Added).Select(f => f.Entity)
            .ToArray();

        foreach (var entity in added)
        {
            switch (entity)
            {
                case IGeneralBase addedEntity:
                    addedEntity.CreatedDateTime = DateTime.UtcNow;
                    addedEntity.IsDeleted = false;
                    addedEntity.IsActive = true;
                    //addedEntity.CreatedById = userId;
                    break;
                case IMinBase minActiveBaseEntity:
                    //minActiveBaseEntity.CreatedById = userId;
                    minActiveBaseEntity.IsActive = true;
                    minActiveBaseEntity.CreatedDateTime = DateTime.UtcNow;
                    break;
                    //case IMinBase mBAddedEntity:
                    //    mBAddedEntity.CreatedDate = DateTime.UtcNow;
                    //    break;
            }
        }

        var modified = ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Modified)
            .Select(t => t.Entity)
            .ToArray();

        foreach (var entity in modified)
        {
            if (entity is not IGeneralBase gBModifiedEntity) continue;
            gBModifiedEntity.ModifiedDateTime = DateTime.UtcNow;
            //gBModifiedEntity.ModifiedById = userId;
        }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict; // or DeleteBehavior.NoAction
        }

        modelBuilder.Entity<OrganizationUser>()
            .HasKey(o => new { o.OrganizationId, o.UserId });

        modelBuilder.Entity<OrganizationUser>()
            .HasKey(a => new { a.UserId, a.OrganizationId });

        modelBuilder.Entity<Level>().HasOne(l => l.Parent)
                  .WithMany()
                  .HasForeignKey(l => l.ParentId)
                  .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BranchSetting>().HasOne(l => l.Branch)
                  .WithOne()
                  .HasForeignKey<BranchSetting>(bs => bs.BranchId);


        modelBuilder.Seed();
    }
}
