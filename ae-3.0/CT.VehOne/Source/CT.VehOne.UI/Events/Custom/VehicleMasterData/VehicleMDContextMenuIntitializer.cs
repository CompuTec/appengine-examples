
using CT.VehOne.BL.BusinessEntities.VehicleMastrData;
using CT.VehOne.UI.UI.Forms.VehicleMasterData;

namespace CT.VehOne.UI.Events.Custom.VehicleMasterData;

[EnableEvent(VehicleMasterDataForm.FormTypeEx)]
public class VehicleMDContextMenuIntitializer:ApplicationRightClickEvent<IVehicleMasterData>
{
    public VehicleMDContextMenuIntitializer(AppHolder appHolder, ICoreConnection coreConnection, ILogger<VehicleMDContextMenuIntitializer> logger, ITranslationService translationService, ContextMenuManager menuManager) : base(appHolder, coreConnection, logger, translationService, menuManager)
    {
        _contextMenuManager.AddString(
            VehicleMasterDataForm.MenuIods.ContextMenu1, "CT_VO_ContextMn1Desctiption",
            ModeOk);
        _contextMenuManager.AddString(
            VehicleMasterDataForm.MenuIods.ContextMenu2, "CT_VO_ContextMn2Desctiption",
            WhenCodeStartWithA);
        _contextMenuManager.AddString(
            VehicleMasterDataForm.MenuIods.ContextMenu3, "CT_VO_ContextMn3Desctiption",
            WhenCodeStartEndsWithA); 
        _contextMenuManager.AddSeparator(VehicleMasterDataForm.MenuIods.ContextSeparator, "",AlwaysOn);
        _contextMenuManager.AddSubmenu(VehicleMasterDataForm.MenuIods.SubMenu, "CT_VO_ContextSMn1Desctiption",AlwaysOn);
        _contextMenuManager.AddString(
            VehicleMasterDataForm.MenuIods.ContextMenu4, "CT_VO_ContextMn4Desctiption",
            NotFindMode,VehicleMasterDataForm.MenuIods.SubMenu);
        _contextMenuManager.AddString(
            VehicleMasterDataForm.MenuIods.ContextMenu5, "CT_VO_ContextMn5Desctiption",
            NotFindMode,VehicleMasterDataForm.MenuIods.SubMenu);
    }

    private bool NotFindMode(Form form, ref IUDOBean udo, PContextMenuInfo eventinfo)=>form.Mode != BoFormMode.fm_FIND_MODE;

    private bool AlwaysOn(Form form, ref IUDOBean udo, PContextMenuInfo eventinfo)=>true;
    

    private bool ModeOk(Form form, ref IUDOBean udo, PContextMenuInfo eventInfo)
    {
        return form.Mode == BoFormMode.fm_OK_MODE;
    }
    private bool WhenCodeStartWithA(Form form, ref IUDOBean udo, PContextMenuInfo eventInfo)
    {
        if(udo==null)
            udo=LoadFrom(form);
        return ((IVehicleMasterData)udo).Code.StartsWith("A");
    }
    private bool WhenCodeStartEndsWithA(Form form, ref IUDOBean udo, PContextMenuInfo eventInfo)
    {
        if(udo==null)
            udo=LoadFrom(form);
        return ((IVehicleMasterData)udo).Code.EndsWith("A");
    }
}