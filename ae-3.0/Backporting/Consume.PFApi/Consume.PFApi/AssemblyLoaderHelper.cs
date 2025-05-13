using System;
using System.Reflection;

namespace Consume.PFApi
{
    public class AssemblyLoaderHelper
    {
        public static void LoadPfApiFromInstallDirecory()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, eventArgs) =>
            {
                var assemblyName = new AssemblyName(eventArgs.Name);
                if (assemblyName.Name == "CompuTec.ProcessForce.API")
                {
                    return Assembly.LoadFrom(@"c:\Program Files (x86)\CompuTec\ProcessForce API\CompuTec.ProcessForce.API.dll");
                }
                if (assemblyName.Name == "CompuTec.ProcessForce.Scheduling")
                {
                    return Assembly.LoadFrom(@"c:\Program Files (x86)\CompuTec\ProcessForce API\CompuTec.ProcessForce.Scheduling.dll");
                }
                if (assemblyName.Name == "CompuTec.ProcessForce.Database")
                {
                    return Assembly.LoadFrom(@"c:\Program Files (x86)\CompuTec\ProcessForce API\CompuTec.ProcessForce.Database.dll");
                }
                return null;
            };
        }
    }
}