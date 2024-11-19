using CompuTec.Core2.UI.Attributes;
using CompuTec.Core2.UI.Events;
using CT.VehOne.BL.BusinessEntities.VehicleMastrData;
using CT.VehOne.UI.Objects.VehicleMasterData;
using CT.VehOne.UI.Services;
using CT.VehOne.UI.UI.Forms.VehicleMasterData;
using SAPbouiCOM;

namespace CT.VehOne.UI.Events.Custom.VehicleMasterData;

[EnableEvent(VehicleMasterDataForm.FormTypeEx)]
internal sealed class VehicleMDEventHandler:ApplicationItemEvent<IVehicleMasterData>
{
    public VehicleMDEventHandler(AppHolder appHolder, ICoreConnection coreConnection, ILogger<VehicleMDEventHandler> logger, ITranslationService translationService, StatusBarMessageHolder msgHolder) : base(appHolder, coreConnection, logger, translationService, msgHolder)
    {
    }

    public override void OnValidateAfter(string formUid, ref PItemEvent itemEvent)
    {
         
        
        
        if (itemEvent.ItemChanged&&itemEvent.ItemUID==VehicleMasterDataForm.Controls.OnwersMatrix)
        {
            //his trick will force IVehicleMasterData model to loadFromToForm and this will trigger the new Lines to be added to the UI 
            //In Moset of the cases in the lines you have and onbject with CFL that will automatically tyrigger this mechanism
            LoadFromToAction(itemEvent.CurrentForm, udo => { });
        }
    }   

    public override void OnChooseFromListAfter(string formUid, ref PItemEvent itemEvent)
    {
        //handle the CLF 
        if (itemEvent.CurrentForm.Mode == BoFormMode.fm_FIND_MODE)
            return;
        switch (itemEvent.CFL_Event.ChooseFromListUID)
        {
            case VehicleMasterDataForm.Controls.SellerCFLUid:
                HandleSellerCFL(itemEvent);
                break;
            case VehicleMasterDataForm.Controls.InfoiceCFLUid:
                HandleInvoiceCFL(itemEvent);
                break;
        }
    }

    private void HandleSellerCFL(PItemEvent itemEvent)
    {
        DataTable dt = itemEvent.CFL_Event.SelectedObjects;
        LoadFromToAction(itemEvent.CurrentForm, udo =>
        {
            if (dt.Rows.Count == 0)
            {
                //Clear the fields
                udo.U_BuyBy = "";
                udo.SetSellerCardName("");
            }
            else
            {
                var tool = _coreConnection.GetService<IVehicleMasterDataUiTools>();
                udo.U_BuyBy = dt.GetString("CardCode", 0);
                udo.SetSellerCardName(tool.GetSellerName(udo.U_BuyBy));
            }
        });
    }

    private void HandleInvoiceCFL(PItemEvent itemEvent)
    {
        DataTable dt = itemEvent.CFL_Event.SelectedObjects;
        LoadFromToAction(itemEvent.CurrentForm, udo =>
        {
            if (dt.Rows.Count == 0)
            {
                //Clear the fields
                udo.U_InvNr = 0;
                udo.SetSellerInvNumber("");
            }
            else
            {
                var tool = _coreConnection.GetService<IVehicleMasterDataUiTools>();
                udo.U_InvNr = dt.GetInt("DocEntry", 0);
                udo.SetSellerInvNumber(tool.GetSellerInvoiceNr(udo.U_InvNr));
            }
        });
    }

    public override bool HandleEvent(ref PItemEvent itemEvent)=>
        itemEvent.FormTypeEx == VehicleMasterDataForm.FormTypeEx;
}