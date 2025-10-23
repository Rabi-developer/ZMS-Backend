using IMS.Domain.Base;
using IMS.Domain.Entities;
using IMS.Domain.Base;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace HMS.Domain.Context;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        Organization organization = new Organization
        {
            Id = Guid.Parse("3AB833EB-917B-4D11-8D13-08DC96DAE48D"),
            Name = "ZMS",
            Description = "Commision Based Company",
            Email = "zms@gmail.com",
            Website = "http://www.ZMS.com",
            IsActive = true,
            IsDeleted = false,
            AddressLine1 = "",
            AddressLine2 = "",
            City = "LHR",
            State = "Punjab",
            Country = "Pakistan",
            Zip = "5400"
        };

        modelBuilder.Entity<Organization>().HasData(
            organization
        );

        var passHasher = new PasswordHasher<ApplicationUser>();

        ApplicationUser user = new ApplicationUser
        {
            Id = Guid.Parse("FC9544A9-4E5C-4032-A27F-3001B29364C5"),
            UserName = "SuperAdmin",
            FirstName = "Super",
            LastName = "Admin",
            NormalizedUserName = "SuperAdmin".ToUpper(),
            Email = "admin@ZMS.com",
            NormalizedEmail = "admin@ZMS.com".ToUpper(),
            SecurityStamp = "d3290d28-d69c-4f25-bbed-d30a1f7a9d5c",
            IsActive = true,
            IsDeleted = false
        };

        user.PasswordHash = passHasher.HashPassword(user, "Admin@123");

        modelBuilder.Entity<ApplicationUser>().HasData(user);

        OrganizationUser organizationUser = new OrganizationUser
        {
            OrganizationId = Guid.Parse("3AB833EB-917B-4D11-8D13-08DC96DAE48D"),
            UserId = Guid.Parse("FC9544A9-4E5C-4032-A27F-3001B29364C5")
        };

        modelBuilder.Entity<OrganizationUser>().HasData(organizationUser);

        #region SuperAdmin Role and Claims for all modules

        // Super Admin Roles and its Permissions
        modelBuilder.Entity<AppRole>().HasData(
            new AppRole
            {
                Name = Constants.SUPERADMIN,
                NormalizedName = Constants.SUPERADMIN.ToUpper(),
                Id = Guid.Parse("F166C1DE-C4AB-456C-ACFD-1050013F19B0"),
                IsDefault = true,
                IsDeleted = false
            });


        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 1,
            RoleId = Guid.Parse("F166C1DE-C4AB-456C-ACFD-1050013F19B0"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.All),
            ClaimValue = string.Join(",", Claimstore.Actions.All)
        });

        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
        {
            RoleId = Guid.Parse("F166C1DE-C4AB-456C-ACFD-1050013F19B0"),
            UserId = Guid.Parse("fc9544a9-4e5c-4032-a27f-3001b29364c5")
        });

        #endregion

        #region OrganizationAdmin Role and Claims for all modules

        // Super Admin Roles and its Permissions
        modelBuilder.Entity<AppRole>().HasData(
            new AppRole
            {
                Name = Constants.ORGANIZATIONADMIN,
                NormalizedName = Constants.ORGANIZATIONADMIN.ToUpper(),
                Id = Guid.Parse("E900C1DE-A4AB-498C-ACBD-1097713F19A1"),
                IsDefault = true,
                IsDeleted = false
            });


        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 2,
            RoleId = Guid.Parse("E900C1DE-A4AB-498C-ACBD-1097713F19A1"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Organization),
            ClaimValue = string.Join(",", Claimstore.Actions.All)
        });

        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 3,
            RoleId = Guid.Parse("E900C1DE-A4AB-498C-ACBD-1097713F19A1"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.OrganizationUser),
            ClaimValue = string.Join(",", Claimstore.Actions.All)
        });



        #endregion

        #region Student Role and Claims for all modules

        // Super Admin Roles and its Permissions
        modelBuilder.Entity<AppRole>().HasData(
            new AppRole
            {
                Name = Constants.STUDENT,
                NormalizedName = Constants.STUDENT.ToUpper(),
                Id = Guid.Parse("BAC8928D-02BF-419C-86A4-79947A6CB457"),
                IsDefault = true,
                IsDeleted = false
            });


        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 4,
            RoleId = Guid.Parse("BAC8928D-02BF-419C-86A4-79947A6CB457"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Home),
            ClaimValue = string.Join(",", Claimstore.Actions.Read)
        });

        #endregion

        #region BranchAdmin Role and Claims for all modules

        // Super Admin Roles and its Permissions
        modelBuilder.Entity<AppRole>().HasData(
            new AppRole
            {
                Name = Constants.BRANCHADMIN,
                NormalizedName = Constants.BRANCHADMIN.ToUpper(),
                Id = Guid.Parse("0931B2B3-66C1-4A88-A0F4-A1B9FA5DBEC9"),
                IsDefault = true,
                IsDeleted = false
            });

        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 5,
            RoleId = Guid.Parse("0931B2B3-66C1-4A88-A0F4-A1B9FA5DBEC9"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Home),
            ClaimValue = string.Join(",", Claimstore.Actions.All)
        });

        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 6,
            RoleId = Guid.Parse("0931B2B3-66C1-4A88-A0F4-A1B9FA5DBEC9"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Branch),
            ClaimValue = string.Join(",", Claimstore.Actions.All)
        });

        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 7,
            RoleId = Guid.Parse("0931B2B3-66C1-4A88-A0F4-A1B9FA5DBEC9"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Section),
            ClaimValue = string.Join(",", Claimstore.Actions.All)
        });

        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 8,
            RoleId = Guid.Parse("0931B2B3-66C1-4A88-A0F4-A1B9FA5DBEC9"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Department),
            ClaimValue = string.Join(",", Claimstore.Actions.All)
        });


        #endregion

        #region Student Role and Claims for all modules
        modelBuilder.Entity<AppRole>().HasData(
            new AppRole
            {
                Name = Constants.GUARDIAN,
                NormalizedName = Constants.GUARDIAN.ToUpper(),
                Id = Guid.Parse("08F4350A-DE87-4BAE-90BD-F340B4EFE46C"),
                IsDefault = true,
                IsDeleted = false
            });


        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 11,
            RoleId = Guid.Parse("08F4350A-DE87-4BAE-90BD-F340B4EFE46C"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Home),
            ClaimValue = string.Join(",", Claimstore.Actions.Read)
        });


        #region Owner Role and Claims

        modelBuilder.Entity<AppRole>().HasData(
            new AppRole
            {
                Name = "Owner",
                NormalizedName = "OWNER",
                Id = Guid.Parse("A1A1A1A1-0000-1111-2222-333344445555"),
                IsDefault = true,
                IsDeleted = false
            });

        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 121,
            RoleId = Guid.Parse("A1A1A1A1-0000-1111-2222-333344445555"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Hotel),
            ClaimValue = string.Join(",", Claimstore.Actions.Read, Claimstore.Actions.Create, Claimstore.Actions.Update, Claimstore.Actions.Execute, Claimstore.Actions.Delete)
        });
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 131,
            RoleId = Guid.Parse("A1A1A1A1-0000-1111-2222-333344445555"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Organization),
            ClaimValue = string.Join(",", Claimstore.Actions.Read, Claimstore.Actions.Create, Claimstore.Actions.Update, Claimstore.Actions.Execute, Claimstore.Actions.Delete)
        });

        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 141,
            RoleId = Guid.Parse("A1A1A1A1-0000-1111-2222-333344445555"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Branch),
            ClaimValue = string.Join(",", Claimstore.Actions.Read, Claimstore.Actions.Create, Claimstore.Actions.Update, Claimstore.Actions.Execute, Claimstore.Actions.Delete)
        });
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 151,
            RoleId = Guid.Parse("A1A1A1A1-0000-1111-2222-333344445555"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Department),
            ClaimValue = string.Join(",", Claimstore.Actions.Read, Claimstore.Actions.Create, Claimstore.Actions.Update, Claimstore.Actions.Execute, Claimstore.Actions.Delete)
        });

        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 155,
            RoleId = Guid.Parse("A1A1A1A1-0000-1111-2222-333344445555"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Room),
            ClaimValue = string.Join(",", Claimstore.Actions.Read, Claimstore.Actions.Create, Claimstore.Actions.Update, Claimstore.Actions.Execute, Claimstore.Actions.Delete)
        });
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 159,
            RoleId = Guid.Parse("A1A1A1A1-0000-1111-2222-333344445555"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Booking),
            ClaimValue = string.Join(",", Claimstore.Actions.Read, Claimstore.Actions.Create, Claimstore.Actions.Update, Claimstore.Actions.Execute, Claimstore.Actions.Delete)
        });

        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 169,
            RoleId = Guid.Parse("A1A1A1A1-0000-1111-2222-333344445555"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Bank),
            ClaimValue = string.Join(",", Claimstore.Actions.Read, Claimstore.Actions.Create, Claimstore.Actions.Update, Claimstore.Actions.Execute, Claimstore.Actions.Delete)
        });
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 179,
            RoleId = Guid.Parse("A1A1A1A1-0000-1111-2222-333344445555"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Agent),
            ClaimValue = string.Join(",", Claimstore.Actions.Read, Claimstore.Actions.Create, Claimstore.Actions.Update, Claimstore.Actions.Execute, Claimstore.Actions.Delete)
        });
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 189,
            RoleId = Guid.Parse("A1A1A1A1-0000-1111-2222-333344445555"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.AgentWork),
            ClaimValue = string.Join(",", Claimstore.Actions.Read, Claimstore.Actions.Create, Claimstore.Actions.Update, Claimstore.Actions.Execute, Claimstore.Actions.Delete)
        });



        modelBuilder.Entity<AppRole>().HasData(
          new AppRole
          {
              Name = Constants.User,
              NormalizedName = Constants.User.ToUpper(),
              Id = Guid.Parse("4b060397-f167-419e-a368-402bb48877c8"),
              IsDefault = true,
              IsDeleted = false
          });

        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 355,
            RoleId = Guid.Parse("4b060397-f167-419e-a368-402bb48877c8"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Room),
            ClaimValue = string.Join(",", Claimstore.Actions.Read)
        });
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 345,
            RoleId = Guid.Parse("4b060397-f167-419e-a368-402bb48877c8"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Hotel),
            ClaimValue = string.Join(",", Claimstore.Actions.Read)
        });
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 365,
            RoleId = Guid.Parse("4b060397-f167-419e-a368-402bb48877c8"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Booking),
            ClaimValue = string.Join(",", Claimstore.Actions.Read, Claimstore.Actions.Create)
        });
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
        {
            Id = 395,
            RoleId = Guid.Parse("4b060397-f167-419e-a368-402bb48877c8"),
            ClaimType = Claimstore.ResourceClaim(Claimstore.Resources.Bank),
            ClaimValue = string.Join(",", Claimstore.Actions.Create, Claimstore.Actions.Update)
        });


        #endregion


        #endregion

    }
}
