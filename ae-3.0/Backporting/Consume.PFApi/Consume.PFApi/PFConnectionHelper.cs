using System;
using CompuTec.BaseLayer;
using CompuTec.BaseLayer.Connection;
using CompuTec.BaseLayer.DI;
using CompuTec.Core2;
using CompuTec.Core2.DI.Database;
using CompuTec.ProcessForce.API.Documents.BillOfMaterials;
using CompuTec.SAP;

namespace Consume.PFApi
{
    public class PFConnectionHelper
    {
        public static ICoreApplication BuildCoreApplication()
        {
            ApplicationBuilder builder= new ApplicationBuilder();
            builder.UseCore2().UseSAPDIConnection();
            builder.RegisterPlugin<CompuTec.ProcessForce.API.ProcessForcePluginInformations>();
            return builder.Build();
        }

        public static void QueryExample(ICoreConnection connection)
        {
             var queryManager= connection.GetQuery();
            queryManager.CommandText="select top 10 \"ItemCode\" from \"OITM\"";
            using (var rs = queryManager.Execute())
            {
                while (!rs.EoF)
                {
                    var itemCode = rs.GetValue<string>("ItemCode");
                    Console.WriteLine("Readed from Db ItemCode: " + itemCode);
                    rs.MoveNext();
                }
            }
        }

        public static string GetRandomBOmCode(ICoreConnection connection)
        {
            var queryManager = connection.GetQuery();
            queryManager.CommandText = "select top 1 \"Code\" from \"@CT_PF_OBOM\"";
            using (var rs = queryManager.Execute())
            {
                if (!rs.EoF)
                {
                    var itemCode = rs.GetValue<string>("Code");
                    return itemCode;
                }
            }
            return string.Empty;
        }
        public static void PFObjectManipulation(ICoreConnection companyCoreConnection)
        {
            var billOfMatUdo = companyCoreConnection.GetService<IBillOfMaterial>();
            var randomBOmCode = GetRandomBOmCode(companyCoreConnection);
            billOfMatUdo.GetByKey(randomBOmCode);
            billOfMatUdo.U_Remarks="ChangedFromCode";
            billOfMatUdo.Update();
        }

        public static void SapOpjectsManupultion(ICoreConnection companyCoreConnection)
        {
            var item=companyCoreConnection.GetSapCompany().GetBusinessObject(BoObjectTypes.oItems) as Items;
            item.ItemCode = Guid.NewGuid().ToString();
            item.ItemName = "TestItem";
            item.Add();
        }
    }
}