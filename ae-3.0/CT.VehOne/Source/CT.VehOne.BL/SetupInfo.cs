namespace CT.VehOne.BL;

 
    public class PluginSetupInfo : SetupInfo
    {
        
        internal const double CurrentDBVersion = 1.0; 
        public PluginSetupInfo()
        {
            SolutionDBVersion = CurrentDBVersion;
            SqlScriptsEmbededResources = true;
            SqlConfigurationXmlFilePath = @"CT.VehOne.BL.DatabaseSetup.DB.LoadOrder.xml";
        }

        public override List<ICustomField> GetUserDefinedFields(ICoreConnection connection)
        {
            var userFiels= new List<ICustomField>();
            var VehOneCode = new TableCustomField();
            VehOneCode.SetTableName("OCRD");
            VehOneCode.SetName("VehicleCode");
            VehOneCode.SetDescription("Vehicle Code");
            VehOneCode.SetType(BoFieldTypes.db_Alpha);
            VehOneCode.SetEditSize(50);
            userFiels.Add(VehOneCode);
            return userFiels;
        }
    }
 