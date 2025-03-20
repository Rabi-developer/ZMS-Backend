using IMS.Domain.Base;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IMS.Domain.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, AppRole, Guid>
{
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
