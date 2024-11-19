
using CT.VehOne.BL.BusinessEntities.VehicleMastrData;
using CT.VehOne.UI.Services;
using CT.VehOne.UI.UI.Forms.VehicleMasterData;


namespace CT.VehOne.UI.Objects.VehicleMasterData
{
    
    public partial class VehicleMasterDataUI : BaseUIBeanLoader<IVehicleMasterData>
    {
        [GenerateUiUdoDefinition(To = typeof(VehicleMasterDataUI), For = "",
            FormTypeEx = VehicleMasterDataForm.FormTypeEx)]
        public partial class VehicleMasterDataUiHeaderUserDataSources
        {
            /// <summary>
            /// Definition of a User data Sources of the Form  and user defined object
            /// this definitipon will generate some helpers method that then can be used to set up the UDS values to related object 
            /// </summary>
            [UDS(UdsType = typeof(string), UdsUniqueId = "CardName")]
            public const string SellerCardName = "SellerCardName";
            
            [UDS(UdsType = typeof(string), UdsUniqueId = "InvDocNum")]
            public const string SellerInvNumber = "SellerInvNumber";

        }
    
        [GenerateUiUdoDefinition(To = typeof(VehicleMasterDataUI), For = PrevOwnersTableName,
            FormTypeEx = VehicleMasterDataForm.FormTypeEx, Matrix = VehicleMasterDataForm.Controls.OnwersMatrix)]
        public partial   class VehicleMasterDataUiPreviousOwnersUserDataSources
        {
            // [UDS(UdsType = typeof(string), UdsUniqueId = "WrdNameDs")]
            // public const string DS_WordName = "DS_WordName";
        }
        
    
        public const string PrevOwnersTableName = "CT_VO_VMD1";
       
    
        public VehicleMasterDataUI(ICoreConnection connection, ILogger<VehicleMasterDataUI> logger,
            ITranslationService translationService) : base(connection, logger, translationService)
        {
        }
    
        protected override UserInterfaceUdoDefinitions GetDefinitions()
        {
            //in this function we are creating the description of a form it can support multiple form types for the same udo 
            //it handles:
            // - the tabs
            //User data spources
            //Matrixes - settoings add menus and all other logic
            var definitinions = getGeneratedDefinitions();
            definitinions.ForForm( VehicleMasterDataForm.FormTypeEx).SetTabs(
                TabDef(VehicleMasterDataForm.Controls.DetailsTab, 1),
                TabDef(VehicleMasterDataForm.Controls.OwnersTab, 2,VehicleMasterDataForm.Controls.OnwersMatrix)
                );
            
            return  definitinions;
        }
        /// <summary>
        /// This is used to fill user data sources ralated to the displayed Udo
        /// this method is used when data are loaded to form Yellow aroow or navigation on the form
        /// </summary>
        /// <param name="bean"></param>
        /// <param name="frm"></param>
        protected override void FillAllUserDataSource(IVehicleMasterData bean, Form frm)
        {
            var tool = _CoreConnection.GetService<IVehicleMasterDataUiTools>();
            if(!string.IsNullOrEmpty(bean.U_BuyBy))
                bean.SetSellerCardName(tool.GetSellerName(bean.U_BuyBy));
            else
                bean.SetSellerCardName("");
            if (bean.U_InvNr != 0)
                bean.SetSellerInvNumber(tool.GetSellerInvoiceNr(bean.U_InvNr));
            else
                bean.SetSellerInvNumber("");
        }
        
        
    }
}