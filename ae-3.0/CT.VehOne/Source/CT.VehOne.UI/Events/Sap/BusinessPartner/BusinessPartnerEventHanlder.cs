
using CT.VehOne.BL.BusinessEntities.VehicleMastrData;
using SAPbouiCOM;

namespace CT.VehOne.UI.Events.Sap.BusinessPartner;

[EnableEvent]
internal class BusinessPartnerEventHanlder : ApplicationItemEvent
{
    public const string BusinessPartnerFormTypeEx = "134";
    public const string VehLabelUid="CT.VO.Lbl";
    public const string VehLinklUid="CT.VO.Lnk";
    public const string VehEditlUid="CT.VO.Edt";
    public const string VehicleCFL="CFL_Vehicle";
    public BusinessPartnerEventHanlder(AppHolder appHolder, ICoreConnection coreConnection,
        ILogger<BusinessPartnerEventHanlder> logger, ITranslationService translationService,
        StatusBarMessageHolder messageHolder) : base(appHolder, coreConnection, logger, translationService,
        messageHolder)
    {
    }

    public override void OnFormLoadAfter(string formUid, ref PItemEvent itemEvent)
    {
        //topPosition
        DrawControls(itemEvent);
    }

  

    public override void OnItemPressedBefore(string formUid, ref PItemEvent itemEvent)
    {
        if (itemEvent.ItemUID == VehLinklUid)
        {
            TryOpenVehicleForm(itemEvent);
        }
    }

    public override void OnChooseFromListAfter(string formUid, ref PItemEvent itemEvent)
    {
        if (itemEvent.CFL_Event?.ChooseFromListUID == VehicleCFL)
        {
            SetVehicleCode(itemEvent);
        }
    }

    private void SetVehicleCode(PItemEvent itemEvent)
    {
        var dt = itemEvent.CFL_Event.SelectedObjects;
        var  vehCode= dt.GetValue("Code", 0) as string;
        var edit = itemEvent.CurrentForm.GetItem<EditText>(VehEditlUid);
        try
        {
            edit.Value = vehCode;
            SetBubbleEvent(false);
        }
        finally
        {
            Memory.ReleaseComObject(dt,edit);
        }
    }

    private void TryOpenVehicleForm(PItemEvent itemEvent)
    {
        var datasources = itemEvent.CurrentForm.DataSources;
        var dbdatasource = datasources.DBDataSources;
        var dbDataSource = dbdatasource.Item("OCRD");
        var vehicleCode = dbDataSource.GetValue("U_VehicleCode", 0);
        try
        {
            _coreConnection.GetService<IFormOpener>().OpenForm<IVehicleMasterData>(vehicleCode);
            SetBubbleEvent(false);
        }
        finally
        {
            Memory.ReleaseComObject(dbDataSource, dbdatasource, datasources);
        }

    }
  private void DrawControls(PItemEvent itemEvent)
    {
        var top= itemEvent.CurrentForm.Items.Item("257000001").Top;
        var lalbel= itemEvent.CurrentForm.Items.Item("343");
        var edit= itemEvent.CurrentForm.Items.Item("345");
        var link=itemEvent.CurrentForm.Items.Item("348");
        var myLabel = itemEvent.CurrentForm.Items.Add(VehLabelUid, BoFormItemTypes.it_STATIC);
        var editBox = itemEvent.CurrentForm.Items.Add(VehEditlUid, BoFormItemTypes.it_EDIT);
        var myLink = itemEvent.CurrentForm.Items.Add(VehLinklUid, BoFormItemTypes.it_LINKED_BUTTON);
        var cflCreaPackage=Application.CreateObject(BoCreatableObjectType.cot_ChooseFromListCreationParams) as ChooseFromListCreationParams;
        cflCreaPackage.MultiSelection = false;
        cflCreaPackage.UniqueID = VehicleCFL;
        cflCreaPackage.ObjectType = "CT_VO_OVMD"; 
        
        
        var cflforVehicles=itemEvent.CurrentForm.ChooseFromLists.Add(cflCreaPackage);
        try
        {
            myLabel.Left = lalbel.Left;
            myLabel.Top = top;
            myLabel.Width = lalbel.Width;
            myLabel.Height = lalbel.Height;
            myLabel.FromPane = lalbel.FromPane;
            myLabel.ToPane = lalbel.ToPane;
            myLabel.LinkTo = VehEditlUid;
            var staticText = (StaticText) myLabel.Specific;
            editBox.Left = edit.Left;
            editBox.Top = top;
            editBox.Width = edit.Width;
            editBox.Height = edit.Height;
            editBox.FromPane = edit.FromPane;
            editBox.ToPane = edit.ToPane;
            editBox.AffectsFormMode = true;
            var editText = (EditText) editBox.Specific;
        
            editText.DataBind.SetBound(true, "OCRD", "U_VehicleCode");
            editText.ChooseFromListUID = VehicleCFL;
            editText.ChooseFromListAlias = "Code";
            staticText.Caption = _translationService.GetTranslatedMessage("CT.VehOne.VEhicleLabel");
            myLink.Left = link.Left;
            myLink.Top = top;
            myLink.Width = link.Width;
            myLink.Height = link.Height;
            myLink.FromPane = link.FromPane;
            myLink.ToPane = link.ToPane;
            myLink.LinkTo = VehEditlUid;
            var linkedButton = (LinkedButton) myLink.Specific;
            linkedButton.LinkedObject = BoLinkedObject.lf_None;
            linkedButton.LinkedObjectType = "-1";
        }
        finally
        {
            Memory.ReleaseComObject(myLabel, lalbel, editBox, edit, myLink, link,cflCreaPackage,cflforVehicles);
        }
      
    }
    public override bool HandleEvent(ref PItemEvent itemEvent)
    {
        return itemEvent.FormTypeEx == BusinessPartnerFormTypeEx;
    }
}