using System.Threading.Tasks;
using Pusula.InternManagement.Localization;
using Pusula.InternManagement.MultiTenancy;
using Pusula.InternManagement.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace Pusula.InternManagement.Web.Menus;

public class InternManagementMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<InternManagementResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                InternManagementMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "InternManagement",
                l["Menu:InternManagement"],
                icon: "fa fa-list"
            ).AddItem(
                new ApplicationMenuItem(
                    "InternManagement.Interns",
                    l["Menu:Interns"],
                    url: "/Interns",
                    icon: "fa fa-user"
                ).RequirePermissions(InternManagementPermissions.Interns.Default)
            ).AddItem(
                new ApplicationMenuItem(
                    "InternManagement.Courses",
                    l["Menu:Courses"],
                    url: "/Courses",
                    icon: "fa fa-pen"
                ).RequirePermissions(InternManagementPermissions.Courses.Default)
            ).AddItem(
                new ApplicationMenuItem(
                    "InternManagement.Educations",
                    l["Menu:Educations"],
                    url: "/Educations",
                    icon: "fa fa-school"
                ).RequirePermissions(InternManagementPermissions.Educations.Default)
            ).AddItem(
                new ApplicationMenuItem(
                    "InternManagement.Experiences",
                    l["Menu:Experiences"],
                    url: "/Experiences",
                    icon: "fa fa-briefcase"
                ).RequirePermissions(InternManagementPermissions.Experiences.Default)
            ).AddItem(
                new ApplicationMenuItem(
                    "InternManagement.Files",
                    l["Menu:Files"],
                    url: "/Files",
                    icon: "fa fa-download"
                ).RequirePermissions(InternManagementPermissions.Files.Default)
            ).AddItem(
                new ApplicationMenuItem(
                    "InternManagement.Projects",
                    l["Menu:Projects"],
                    url: "/Projects",
                    icon: "fa fa-clipboard-check"
                ).RequirePermissions(InternManagementPermissions.Projects.Default)
            ).AddItem(
                new ApplicationMenuItem(
                    "InternManagement.Works",
                    l["Menu:Works"],
                    url: "/Works",
                    icon: "fa fa-check"
                ).RequirePermissions(InternManagementPermissions.Works.Default)
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Contents",
                l["Contents"],
                icon: "fa fa-list"
            )
            .AddItem(
                new ApplicationMenuItem(
                    "InternManagement.Interns",
                    l["Menu:Interns"],
                    url: "/Interns",
                    icon: "fa fa-user"
                ).RequirePermissions(InternManagementPermissions.Interns.Admin)
                    .AddItem(
                    new ApplicationMenuItem(
                        "InternManagement.Departments",
                        l["Menu:Departments"],
                        url: "/Departments"
                    )
                )
            )
            .AddItem(
                new ApplicationMenuItem(
                    "InternManagement.Educations",
                    l["Menu:Educations"],
                    url: "/Educations",
                    icon: "fa fa-school"
                ).RequirePermissions(InternManagementPermissions.Educations.Admin)
                    .AddItem(
                        new ApplicationMenuItem(
                            "InternManagement.Universities",
                            l["Menu:Universities"],
                            url: "/Universities"
                        )
                    )
                    .AddItem(
                        new ApplicationMenuItem(
                            "InternManagement.UniversityDepartments",
                            l["Menu:UniversityDepartments"],
                            url: "/UniversityDepartments"
                        )
                    )
            )
            .AddItem(
                new ApplicationMenuItem(
                    "InternManagement.Courses",
                    l["Menu:Courses"],
                    url: "/Courses",
                    icon: "fa fa-pen"
                ).RequirePermissions(InternManagementPermissions.Courses.Admin)
                    .AddItem(
                        new ApplicationMenuItem(
                            "InternManagement.Instructors",
                            l["Menu:Instructors"],
                            url: "/Instructors"
                        )
                    )
            )
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        return Task.CompletedTask;
    }
}
