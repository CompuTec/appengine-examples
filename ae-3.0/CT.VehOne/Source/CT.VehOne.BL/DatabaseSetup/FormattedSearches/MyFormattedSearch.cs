using CT.VehOne.BL.DatabaseSetup.QueryCategory;
using CT.VehOne.BL.DatabaseSetup.Queries;

namespace CT.VehOne.BL.DatabaseSetup.FormattedSearches;

[Install]
public class MyFormattedSearch : FormattedSearch
{
    public MyFormattedSearch(ICoreConnection connection) : base(connection)
    {
        this.QueryCategoryName = MyQueryCategory.CategoryName;
        this.QueryName = MyUserQuery.CostAcct;
        this.FormID = "810";
        this.ItemID = "U_CT_Acct5";
        this.ColID = "-1";
        this.ActionType = BoFormattedSearchActionEnum.bofsaQuery;
        this.Refresh = BoYesNoEnum.tNO;
        this.FieldID = null;
        this.ForceRefresh = BoYesNoEnum.tNO;
        this.ByField = BoYesNoEnum.tNO;
    }
}