using CompuTec.Core2.DI.Setup.Actions;

namespace CT.VehOne.BL.DatabaseSetup.Actions;

[ExecuteInstallAction(true)]
public class MyFirstAction : SetupCustomAction
{
    public MyFirstAction(ICoreConnection coreConnection, ITranslationService translationService) : base(coreConnection,
        translationService)
    {
    }

    public override Result Run()
    {
        return Result.OK;
    }
}