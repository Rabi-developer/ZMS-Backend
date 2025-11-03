using HMS.Domain.Context;
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
    public DbSet<RelatedConsignment> RelatedConsignments { get; set; }
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
    public DbSet<PaymentABL> PaymentABL { get; set; }
    public DbSet<BiltyPaymentInvoice> BiltyPaymentInvoice { get; set; }
    public DbSet<EntryVoucher> EntryVoucher { get; set; }


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

        modelBuilder.Entity<Level>().HasOne(l => l.Parent)
                  .WithMany()
                  .HasForeignKey(l => l.ParentId)
                  .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BranchSetting>().HasOne(l => l.Branch)
                  .WithOne()
                  .HasForeignKey<BranchSetting>(bs => bs.BranchId);

        // Configure AblRevenue self-referencing relationship
        modelBuilder.Entity<AblRevenue>()
            .HasOne(x => x.ParentAccount)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure ProjectTarget - Employee relationship properly
        modelBuilder.Entity<ProjectTarget>()
            .HasOne(pt => pt.Employee)
            .WithMany()
            .HasForeignKey(pt => pt.EmployeeId)
            .HasConstraintName("FK_ProjectTarget_Employee");

        // Configure DeliveryBreakup relationships properly
        modelBuilder.Entity<DeliveryBreakup>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Id).ValueGeneratedOnAdd();
            
            // Configure relationship with Contract
            entity.HasOne(d => d.Contract)
                  .WithMany(c => c.BuyerDeliveryBreakups)
                  .HasForeignKey(d => d.ContractId)
                  .OnDelete(DeleteBehavior.Restrict);
                  
            // Configure relationship with ConversionContractRow
            entity.HasOne(d => d.ConversionContractRow)
                  .WithMany(c => c.BuyerDeliveryBreakups)
                  .HasForeignKey(d => d.ConversionContractRowId)
                  .OnDelete(DeleteBehavior.Restrict);
                  
            // Configure relationship with DietContractRow
            entity.HasOne(d => d.DietContractRow)
                  .WithMany(d => d.BuyerDeliveryBreakups)
                  .HasForeignKey(d => d.DietContractRowId)
                  .OnDelete(DeleteBehavior.Restrict);
                  
            // Configure relationship with MultiWidthContractRow
            entity.HasOne(d => d.MultiWidthContractRow)
                  .WithMany(m => m.BuyerDeliveryBreakups)
                  .HasForeignKey(d => d.MultiWidthContractRowId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure Contract relationships
        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasMany(c => c.BuyerDeliveryBreakups)
                  .WithOne(d => d.Contract)
                  .HasForeignKey(d => d.ContractId)
                  .OnDelete(DeleteBehavior.Restrict);
                  
            // Ignore SellerDeliveryBreakups as we'll handle it differently
            entity.Ignore(c => c.SellerDeliveryBreakups);
        });

        // Configure ConversionContractRow relationships
        modelBuilder.Entity<ConversionContractRow>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).ValueGeneratedOnAdd();
            
            entity.HasOne(c => c.Contract)
                  .WithMany(c => c.ConversionContractRow)
                  .HasForeignKey(c => c.ContractId)
                  .OnDelete(DeleteBehavior.Restrict);
                  
            entity.HasMany(c => c.BuyerDeliveryBreakups)
                  .WithOne(d => d.ConversionContractRow)
                  .HasForeignKey(d => d.ConversionContractRowId)
                  .OnDelete(DeleteBehavior.Restrict);
                  
            // Ignore SellerDeliveryBreakups to avoid conflicts
            entity.Ignore(c => c.SellerDeliveryBreakups);
        });

        // Configure DietContractRow relationships
        modelBuilder.Entity<DietContractRow>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Id).ValueGeneratedOnAdd();
            
            entity.HasOne(d => d.Contract)
                  .WithMany(c => c.DietContractRow)
                  .HasForeignKey(d => d.ContractId)
                  .OnDelete(DeleteBehavior.Restrict);
                  
            entity.HasMany(d => d.BuyerDeliveryBreakups)
                  .WithOne(db => db.DietContractRow)
                  .HasForeignKey(db => db.DietContractRowId)
                  .OnDelete(DeleteBehavior.Restrict);
                  
            // Ignore SellerDeliveryBreakups to avoid conflicts
            entity.Ignore(d => d.SellerDeliveryBreakups);
        });

        // Configure MultiWidthContractRow relationships
        modelBuilder.Entity<MultiWidthContractRow>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).ValueGeneratedOnAdd();
            
            entity.HasOne(m => m.Contract)
                  .WithMany(c => c.MultiWidthContractRow)
                  .HasForeignKey(m => m.ContractId)
                  .OnDelete(DeleteBehavior.Restrict);
                  
            entity.HasMany(m => m.BuyerDeliveryBreakups)
                  .WithOne(d => d.MultiWidthContractRow)
                  .HasForeignKey(d => d.MultiWidthContractRowId)
                  .OnDelete(DeleteBehavior.Restrict);
                  
            // Ignore SellerDeliveryBreakups to avoid conflicts
            entity.Ignore(m => m.SellerDeliveryBreakups);
        });

        // Configure decimal properties with proper precision and scale
        modelBuilder.Entity<EmployeeManagement>()
            .Property(e => e.Salary)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Price>(entity =>
        {
            entity.Property(p => p.BasePrice).HasColumnType("decimal(18,2)");
            entity.Property(p => p.DiscountValue).HasColumnType("decimal(18,2)");
            entity.Property(p => p.RetailPrice).HasColumnType("decimal(18,2)");
            entity.Property(p => p.SalePrice).HasColumnType("decimal(18,2)");
            entity.Property(p => p.SpecialOfferPrice).HasColumnType("decimal(18,2)");
            entity.Property(p => p.TaxRate).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<ProjectTarget>()
            .Property(pt => pt.TargetValue)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.Property(s => s.CurrentStockValue).HasColumnType("decimal(18,2)");
            entity.Property(s => s.PurchasePrice).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Receipt>()
            .Property(r => r.ReceiptAmount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<ReceiptItem>(entity =>
        {
            entity.Property(r => r.Balance).HasColumnType("decimal(18,2)");
            entity.Property(r => r.BiltyAmount).HasColumnType("decimal(18,2)");
            entity.Property(r => r.ReceiptAmount).HasColumnType("decimal(18,2)");
            entity.Property(r => r.SrbAmount).HasColumnType("decimal(18,2)");
            entity.Property(r => r.TotalAmount).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<RelatedConsignment>(entity =>
        {
            entity.Property(r => r.RecvAmount).HasColumnType("decimal(18,2)");
            entity.Property(r => r.TotalAmount).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Seed();
    }
}
