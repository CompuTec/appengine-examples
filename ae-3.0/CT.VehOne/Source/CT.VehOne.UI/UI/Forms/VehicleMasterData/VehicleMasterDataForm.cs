using CompuTec.Core2.UI.Attributes;
using CompuTec.Core2.UI.Forms;
using CompuTec.Core2.UI.Translation;
using CT.VehOne.BL.BusinessEntities.VehicleMastrData;
using SAPbouiCOM;

namespace CT.VehOne.UI.UI.Forms.VehicleMasterData;


[LoadForm(FormTypeEx = VehicleMasterDataForm.FormTypeEx, XmlPath = VehicleMasterDataForm.FormXmlDefinitionPath,
    MenuIDS = new string[] { MenuUIds.VehicleMasterDataMenuUid })]
internal sealed partial class VehicleMasterDataForm:AbstractFormInitializer<IVehicleMasterData>
{
    
    public const string FormTypeEx = "CT_VO_OVMD";
    public const string FormXmlDefinitionPath = "UI\\Forms\\VehicleMasterData\\VehicleMasterDataForm.xml";
    
    public VehicleMasterDataForm(ICoreConnection connection, AppHolder appHolder, ILogger<VehicleMasterDataForm> logger,
        ITranslationServiceUI translationService, IFormDefinitionsCacheService cacheService) : base(connection,
        appHolder, logger, translationService, cacheService)
    {
    }

    public override void InitializeForm(Form form)
    {
        //Its called when the form is dreawn on the screen ( use this method instead of On Form Load Event for custom objects)
        //Send Expand Type
         form.SetExpandTypeToCombobox(BoExpandType.et_DescriptionOnly,Controls.VehicleTypeCombo);
         //Select Default Folder
         form.GetItem<Folder>(Controls.DetailsTab).Select();
    }
}