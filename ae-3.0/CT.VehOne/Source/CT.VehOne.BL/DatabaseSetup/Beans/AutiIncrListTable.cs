using System;
using System.Collections.Generic;
using CompuTec.BaseLayer.Connection;
using CompuTec.BaseLayer.DI;
using CompuTec.Core2.DI.Setup;
using CompuTec.Core2.DI.Setup.Attributes;
using CompuTec.Core2.DI.Setup.UDO.Model;

namespace ConsoleApp.DataBase.Beans
{
    [Install]
    public class AutiIncrListTable : UDOManager
    {
        public const String OBJECT_CODE = "CT_CS_Lst";
        public const String HEADER_TABLE_NAME = "CT_CS_OLST";
        public const String HEADER_TABLE_DESCRIPTION = "SimList";
        public const string ARCHIVE_TABLE_NAME = "CT_CS_ALST";

        public AutiIncrListTable(ICoreConnection connection, IDatabaseSetupManager manager) : base(connection, manager)
        {
        }


        protected override IUDOTable CreateUDOTable()
        {
            // Authorizations


            List<DocumentLayoutInfo> reportList = new List<DocumentLayoutInfo>();

            //  reportList.Add(new DocumentLayoutInfo("Dane uzupełniające.(ProcessForce)", @"Install\ReportFiles\OIDT.rpt", false, SAPbouiCOM.BoLanguages.ln_Polish));


            List<IUDOField> fields = CreateFieldsForHeaderTable();
            List<IUDOFindColumn> findColumns = CreateFindColumnsList();

            IUDOTable UdoTable = new UDOTable(fields, findColumns, HEADER_TABLE_NAME, HEADER_TABLE_DESCRIPTION,
                BoUTBTableType.bott_NoObjectAutoIncrement, CreateKeys());

            UdoTable.RegisteredUDOName = HEADER_TABLE_NAME;
            UdoTable.RegisteredUDOCode = OBJECT_CODE;

            //If adding Print option this value must be filed
            //UdoTable.ReportType = new CompuTec.Core.Model.DB.ReportTypeUDO("ItemDetailsReportType", "", OBJECT_CODE, reportList);
            UdoTable.CanArchive = BoYesNoEnum.tYES;
            UdoTable.CanCancel = BoYesNoEnum.tNO;
            UdoTable.CanClose = BoYesNoEnum.tNO;
            UdoTable.CanCreateDefaultForm = BoYesNoEnum.tNO;
            UdoTable.CanDelete = BoYesNoEnum.tYES;
            UdoTable.CanFind = BoYesNoEnum.tYES;
            UdoTable.CanLog = BoYesNoEnum.tYES;
            UdoTable.CanYearTransfer = BoYesNoEnum.tNO;
            UdoTable.ArchiveTableName = ARCHIVE_TABLE_NAME;

            return UdoTable;
        }

        private List<IUDOFindColumn> CreateFindColumnsList()
        {
            List<IUDOFindColumn> findColumns = new List<IUDOFindColumn>();

            //Add FindColums heare
            IUDOFindColumn code = new UDOFindColumn();
            code.SetColumnAlias("Code");
            code.SetColumnDescription("Code");
            findColumns.Add(code);
            IUDOFindColumn PhCode = new UDOFindColumn();
            PhCode.SetColumnAlias("U_TxtCode");
            PhCode.SetColumnDescription("Group Code");
            findColumns.Add(PhCode);


            IUDOFindColumn PhName = new UDOFindColumn();
            PhName.SetColumnAlias("U_TxtName");
            PhName.SetColumnDescription("Group Description");
            findColumns.Add(PhName);

            IUDOFindColumn GrpCode = new UDOFindColumn();
            GrpCode.SetColumnAlias("U_GrpCode");
            GrpCode.SetColumnDescription("Group Code");
            findColumns.Add(GrpCode);


            return findColumns;
        }

        private List<IUDOTableKey> CreateKeys()
        {
            List<IUDOTableKey> list = new List<IUDOTableKey>();

            UDOTableKey key = new UDOTableKey("Index", HEADER_TABLE_NAME, new string[] { "TxtCode" });
            key.Unique = BoYesNoEnum.tYES;

            list.Add(key);

            return list;
        }

        private List<IUDOField> CreateFieldsForHeaderTable()
        {
            List<IUDOField> fields = new List<IUDOField>();


            IUDOField PhCode = new UDOTableField();
            PhCode.SetName("TxtCode");
            PhCode.SetDescription("Group Code");
            PhCode.SetType(BoFieldTypes.db_Alpha);
            PhCode.SetEditSize(20);
            fields.Add(PhCode);


            IUDOField PhName = new UDOTableField();
            PhName.SetName("TxtName");
            PhName.SetDescription("Group Description");
            PhName.SetType(BoFieldTypes.db_Alpha);
            PhName.SetEditSize(254);
            fields.Add(PhName);

            IUDOField U_ProdOrders = new UDOTableField();
            U_ProdOrders.SetName("ProdOrders");
            U_ProdOrders.SetDescription("Production Orders");
            U_ProdOrders.SetType(BoFieldTypes.db_Alpha);
            U_ProdOrders.SetEditSize(1);
            fields.Add(U_ProdOrders);


            IUDOField GrpCode = new UDOTableField();
            GrpCode.SetName("GrpCode");
            GrpCode.SetDescription("Group Group Code");
            GrpCode.SetType(BoFieldTypes.db_Alpha);
            GrpCode.SetEditSize(20);
            fields.Add(GrpCode);


            IUDOField U_ShipDoc = new UDOTableField();
            U_ShipDoc.SetName("ShipDoc");
            U_ShipDoc.SetDescription("Shipment Documentation");
            U_ShipDoc.SetType(BoFieldTypes.db_Alpha);
            U_ShipDoc.SetEditSize(1);
            fields.Add(U_ShipDoc);


            IUDOField U_PickLists = new UDOTableField();
            U_PickLists.SetName("PickLists");
            U_PickLists.SetDescription("Pick Lists");
            U_PickLists.SetType(BoFieldTypes.db_Alpha);
            U_PickLists.SetEditSize(1);
            fields.Add(U_PickLists);


            IUDOField MSDS = new UDOTableField();
            MSDS.SetName("MSDS");
            MSDS.SetDescription("MSDS");
            MSDS.SetType(BoFieldTypes.db_Alpha);
            MSDS.SetEditSize(1);
            fields.Add(MSDS);


            IUDOField PurOrders = new UDOTableField();
            PurOrders.SetName("PurOrders");
            PurOrders.SetDescription("Purchase Orders");
            PurOrders.SetType(BoFieldTypes.db_Alpha);
            PurOrders.SetEditSize(1);
            fields.Add(PurOrders);


            IUDOField Returns = new UDOTableField();
            Returns.SetName("Returns");
            Returns.SetDescription("Returns");
            Returns.SetType(BoFieldTypes.db_Alpha);
            Returns.SetEditSize(1);
            fields.Add(Returns);


            IUDOField Remarks = new UDOTableField();
            Remarks.SetName("Remarks");
            Remarks.SetDescription("Remarks");
            Remarks.SetType(BoFieldTypes.db_Memo);
            //Returns.SetEditSize(0);
            fields.Add(Remarks);

            IUDOField Other = new UDOTableField();
            Other.SetName("Other");
            Other.SetDescription("Other");
            Other.SetType(BoFieldTypes.db_Alpha);
            Other.SetEditSize(1);
            fields.Add(Other);

            //AddYourFields
            return fields;
        }

        protected override void SetChildTables()
        {
        }
    }
}