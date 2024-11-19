using CompuTec.BaseLayer.Services;
using CompuTec.Core2.Beans;
using CompuTec.Core2.UI.Attributes;
using CompuTec.Core2.UI.Events;
using CompuTec.Core2.UI.Events.Helpers;
using CT.VehOne.BL.BusinessEntities.VehicleMastrData;
using CT.VehOne.UI.UI.Forms.VehicleMasterData;

namespace CT.VehOne.UI.Events.Custom.VehicleMasterData;

[EnableEvent(VehicleMasterDataForm.FormTypeEx)]
internal  sealed class VehicleMdMenuEvent:ApplicationContextMenuEvent<IVehicleMasterData>
{
    public VehicleMdMenuEvent(AppHolder appHolder, ICoreConnection coreConnection, ILogger<VehicleMdMenuEvent> logger, ITranslationService translationService, MatrixContextMenuManager matrixContextMenuManager) : base(appHolder, coreConnection, logger, translationService, matrixContextMenuManager)
    {
    }

    public override void OnMenuClickBefore(ref PMenuEvent menuEvent, bool BubbleEvent)
    {
        
        switch(menuEvent.MenuUID)
        {
            case VehicleMasterDataForm.MenuIods.ContextMenu1:
                DisplayMessage("Menu 1 Clicked");
                break;
            case VehicleMasterDataForm.MenuIods.ContextMenu2:
                OpenDataSelectorWithResult(menuEvent.CurrentForm);
                break;
            case VehicleMasterDataForm.MenuIods.ContextMenu3:
                AddLineWithSomeDescritopm(menuEvent);
                break;
            case VehicleMasterDataForm.MenuIods.ContextMenu4:
                //Do something
                break;
            case VehicleMasterDataForm.MenuIods.ContextMenu5:
                //Do something
                break;
        }
    }

    private void AddLineWithSomeDescritopm(PMenuEvent menuEvent)
    {
        LoadFromToAction(menuEvent.CurrentForm, (udo) =>
        {
            udo.Owners.SetCurrentLine(udo.Owners.Count - 1);
            if (!udo.Owners.IsRowFilled())
                udo.Owners.Add();
            udo.Owners.U_OwnerName = "Test Owner";
            udo.Owners.U_OwnerAddress = "Test Address";
        });
    }

    private void OpenDataSelectorWithResult(Form menuEventCurrentForm)
    {
        _coreConnection.GetService<IExceptionService>().RegisterMessage("This is a test message");
    }

    private void DisplayMessage(string message)
    {
        //show sap message box 
        _application.MessageBox(message);
        //Display using Exception Service
        var service=_coreConnection.GetService<IUIExceptionService>();
        service.DisplaySucess(message);
        
    }
}