using CompuTec.Core2.Services;

namespace CT.VehOne.BL;

public class MyPluginInfo:PluginInfo
{
    public MyPluginInfo()
    {
        SetupDescription.RequiredSolutionDBVersion = PluginSetupInfo.CurrentDBVersion;
        SetupDescription.SetupAssembly = this.GetType().Assembly;
    }

    public override void OnUninstall(ICoreConnection connection)
    {
        base.OnUninstall(connection);
    }

    public override void BeforeInstall(ICoreConnection connection)
    {
        base.BeforeInstall(connection);
    }

    public override void PluginInstalling(ICoreConnection connection)
    {
        base.PluginInstalling(connection);
    }

    public override void OnInstallError(PluginInstalldVersion? current_DB_Number, PluginInstalldVersion final_DB_Number,
        Result actionResult, Exception ex = null)
    {
        base.OnInstallError(current_DB_Number, final_DB_Number, actionResult, ex);
    }

    public override void AfterStructureWasInstalled(PluginInstalldVersion? current_DB_Number, PluginInstalldVersion final_DB_Number,
        ICoreConnection coreConnection)
    {
        base.AfterStructureWasInstalled(current_DB_Number, final_DB_Number, coreConnection);
    }
}