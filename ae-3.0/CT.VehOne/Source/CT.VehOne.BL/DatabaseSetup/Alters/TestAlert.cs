using CT.VehOne.BL.DatabaseSetup.QueryCategory;
using CT.VehOne.BL.DatabaseSetup.Queries;

namespace CT.VehOne.BL.DatabaseSetup.Alters;

[Install]
internal class TestAlert : Alert
{
    public TestAlert(ICoreConnection connecion) : base(connecion)
    {
        this.Active = false;
        this.AlertName = "TestAlert";
        this.CategoryName = MyQueryCategory.CategoryName;
        this.QueryName = MyUserQuery.CostAcct;
        this.Priority = AlertManagementPriorityEnum.atp_Normal;
        this.Param = string.Empty;
        this.DayOfExecution = 1;
        this.FrequencyInterval = 1;
        this.FrequencyType = AlertManagementFrequencyType.atfi_Hours;
        this.ExecutionTime = DateTime.Today.AddHours(8);
        this.SaveHistory = BoYesNoEnum.tYES;
    }
}