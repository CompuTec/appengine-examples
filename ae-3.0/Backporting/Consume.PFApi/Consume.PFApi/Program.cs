using System;
using CompuTec.BaseLayer.DI;
using CompuTec.Core2;

namespace Consume.PFApi
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /// Please red reamde.md
            ///Assembly Resolve - make sure that you use the API from the isntalation directory instead of the one in the project
            ///
          AssemblyLoaderHelper.LoadPfApiFromInstallDirecory();
          var app = PFConnectionHelper.BuildCoreApplication();
          var company = app.GetService<CoreCompany>();
          company.Server = "DEV@hanadev:30013";
          company.SLDServer = "hanadev:40000";
          company.SAP_LicenceServer = "hanadev:40000";
          company.CT_LicenceServer = "localhost:30002";
          company.ServerType = BoDataServerTypes.dst_HANADB;
          company.UserName = "manager";
          company.Password = "1234";
          company.CompanyDB = "PFDEMOGB";
          company.Language = BoSuppLangs.ln_English;
          var connected= company.Connect();
          if (!connected.Success)
          {
              Console.WriteLine(connected.ToString());
              return;
          }

          PFConnectionHelper.QueryExample(company.CoreConnection);
          PFConnectionHelper.PFObjectManipulation(company.CoreConnection);
          PFConnectionHelper.SapOpjectsManupultion(company.CoreConnection);
          company.Disconnect();
          Console.WriteLine("Disconnected");
        }
    }
}