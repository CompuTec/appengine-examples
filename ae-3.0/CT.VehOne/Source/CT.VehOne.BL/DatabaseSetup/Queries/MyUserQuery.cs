using CT.VehOne.BL.DatabaseSetup.QueryCategory;

namespace CT.VehOne.BL.DatabaseSetup.Queries;

[Install]
public class MyUserQuery : Query
{
    public const string CostAcct = "TEst:TestQuert";

    public MyUserQuery(ICoreConnection coreConnection) : base(coreConnection)
    {
        this.QueryCategory = MyQueryCategory.CategoryName;
        this.QueryDescription = CostAcct;
        this.QueryString =
            "SELECT T0.\"AcctCode\", T0.\"AcctName\" FROM \"OACT\" T0 WHERE T0.\"Postable\" = N'Y' AND T0.\"ActType\" = 'E'";
    }
}