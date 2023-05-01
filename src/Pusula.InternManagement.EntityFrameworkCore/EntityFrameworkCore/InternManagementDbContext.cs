using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Departments;
using Pusula.InternManagement.Educations;
using Pusula.InternManagement.Experiences;
using Pusula.InternManagement.Interns;
using Pusula.InternManagement.Projects;
using Pusula.InternManagement.Universities;
using Pusula.InternManagement.UniversityDepartments;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Pusula.InternManagement.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class InternManagementDbContext :
    AbpDbContext<InternManagementDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public DbSet<Intern> Interns { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<UniversityDepartment> UniversityDepartments { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<InternProject> InternProjects { get; set; }


    public InternManagementDbContext(DbContextOptions<InternManagementDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(InternManagementConsts.DbTablePrefix + "YourEntities", InternManagementConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<Intern>(b =>
        {
            b.ToTable(InternManagementConsts.DbTablePrefix + "Interns",
                InternManagementConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(InternConsts.MaxNameLength);

            b.HasOne<Department>().WithMany().HasForeignKey(x => x.DepartmentId).IsRequired();
        });

        builder.Entity<Department>(b =>
        {
            b.ToTable(InternManagementConsts.DbTablePrefix + "Departments",
                InternManagementConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(DepartmentConsts.MaxNameLength);

            b.HasIndex(x => x.Name);
        });

        builder.Entity<Education>(b =>
        {
            b.ToTable(InternManagementConsts.DbTablePrefix + "Educations",
                InternManagementConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(EducationConsts.MaxNameLength);

            b.HasOne<University>().WithMany().HasForeignKey(x => x.UniversityId).IsRequired();
            b.HasOne<UniversityDepartment>().WithMany().HasForeignKey(x => x.UniversityDepartmentId).IsRequired();
            b.HasOne<Intern>().WithMany().HasForeignKey(x => x.InternId).IsRequired();
        });

        builder.Entity<University>(b =>
        {
            b.ToTable(InternManagementConsts.DbTablePrefix + "Universities",
                InternManagementConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(UniversityConsts.MaxNameLength);
        });

        builder.Entity<UniversityDepartment>(b =>
        {
            b.ToTable(InternManagementConsts.DbTablePrefix + "UniversityDepartments",
                InternManagementConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(UniversityDepartmentConsts.MaxNameLength);
        });

        builder.Entity<Experience>(b =>
        {
            b.ToTable(InternManagementConsts.DbTablePrefix + "Experiences",
                InternManagementConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(ExperienceConsts.MaxNameLength);
            b.Property(x => x.Description).IsRequired().HasMaxLength(ExperienceConsts.MaxCompanyNameLength);
            b.Property(x => x.Title).IsRequired().HasMaxLength(ExperienceConsts.MaxTitleLength);
            b.Property(x => x.CompanyName).IsRequired().HasMaxLength(ExperienceConsts.MaxCompanyNameLength);

            b.HasOne<Intern>().WithMany().HasForeignKey(x => x.InternId).IsRequired();
        });

        builder.Entity<Project>(b =>
        {
            b.ToTable(InternManagementConsts.DbTablePrefix + "Projects",
                InternManagementConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(ProjectConsts.MaxNameLength);
            b.Property(x => x.Description).IsRequired().HasMaxLength(ProjectConsts.MaxDescriptionLength);

            b.HasMany(x => x.Interns).WithOne().HasForeignKey(x => x.ProjectId).IsRequired();
        });

        builder.Entity<InternProject>(b =>
        {
            b.ToTable(InternManagementConsts.DbTablePrefix + "InternProjects",
                InternManagementConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.HasKey(x => new { x.ProjectId, x.InternId });
            b.HasOne<Project>().WithMany(x => x.Interns).HasForeignKey(x => x.ProjectId).IsRequired();
            b.HasOne<Intern>().WithMany().HasForeignKey(x => x.InternId).IsRequired();

            b.HasIndex(x => new { x.ProjectId, x.InternId });

        });




    }
}
