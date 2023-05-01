using Volo.Abp.Settings;

namespace Pusula.InternManagement.Settings;

public class InternManagementSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(InternManagementSettings.MySetting1));
    }
}
