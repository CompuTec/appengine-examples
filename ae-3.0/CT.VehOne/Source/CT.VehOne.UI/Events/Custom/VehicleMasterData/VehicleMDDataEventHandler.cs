using CompuTec.Core2.UI.Attributes;
using CompuTec.Core2.UI.Events;
using CT.VehOne.BL.BusinessEntities.VehicleMastrData;
using CT.VehOne.UI.UI.Forms.VehicleMasterData;

namespace CT.VehOne.UI.Events.Custom.VehicleMasterData;

[EnableEvent(VehicleMasterDataForm.FormTypeEx)]
public class VehicleMdDataEventHandler:ApplicationDataEvent<IVehicleMasterData>
{
    public VehicleMdDataEventHandler(IUserInterfaceBeanService<IVehicleMasterData> udoLoader,
        IUIExceptionService exceptionService, AppHolder appHolder, ICoreConnection coreConnection, ILogger<VehicleMdDataEventHandler> logger, ITranslationService translationService, StatusBarMessageHolder msgHolder) : base(udoLoader, exceptionService, appHolder, coreConnection, logger, translationService, msgHolder)
    {
    }

    public override bool HandleEvent(ref PBusinessObjectInfo businessObjectInfo)
    {
        return businessObjectInfo.FormTypeEx == VehicleMasterDataForm.FormTypeEx;
    }

    protected override bool HandleAddException(Exception exception, IVehicleMasterData udoBean, PBusinessObjectInfo businessObjectInfo)
    {
        return base.HandleAddException(exception, udoBean, businessObjectInfo);
        
    }

    protected override bool HandleUpdateException(Exception exception, IVehicleMasterData bean, PBusinessObjectInfo businessObjectInfo)
    {
        return base.HandleUpdateException(exception, bean, businessObjectInfo);
    }
}