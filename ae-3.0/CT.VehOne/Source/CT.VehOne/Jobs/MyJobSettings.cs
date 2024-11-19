using CompuTec.BaseLayer.Connection;
using CompuTec.Core2.Configuration;

namespace CT.VehOne.Jobs;

public class MyJobSettings:IAdditionalConfiguration
{
    public bool ValidateConfiguration(ICoreConnection connection, out string message)
    {
        message = string.Empty;
        return true;
    }

    public bool UpgradeConfiguration(ICoreConnection connection)
    {
        //update onfigueration from one version to anouther
        Version = new Version(1, 0, 0, 1);
        return true;
    }

    public IAdditionalConfiguration GetDefaultConfiguration()
    {
        return new MyJobSettings()
        {
            Version = new Version(1, 0, 0, 0),
            MyString = "Hello World",
            Bool = true
        };
    }

    public bool Bool { get; set; }

    public string MyString { get; set; }

    public Version Version { get; set; }
}