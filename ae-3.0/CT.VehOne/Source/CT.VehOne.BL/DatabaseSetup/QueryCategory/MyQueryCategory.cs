namespace CT.VehOne.BL.DatabaseSetup.QueryCategory;

[Install]
public class MyQueryCategory : CompuTec.Core2.DI.Setup.UDO.Model.QueryCategory
{
    public const String CategoryName = "Test Query Category";

    public MyQueryCategory(ICoreConnection coreConnection) : base(coreConnection)
    {
        this.Name = CategoryName;
    }
}