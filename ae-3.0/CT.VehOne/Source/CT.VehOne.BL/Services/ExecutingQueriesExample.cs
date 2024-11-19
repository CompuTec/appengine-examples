using CompuTec.Core2.DI.Database;
using Microsoft.Extensions.Logging;
using NLog;

namespace CT.VehOne.BL.Services;

public class ExecutingQueriesExample
{
    private readonly ICoreConnection _connection;
    private readonly ILogger<ExecutingQueriesExample> _logger;

    public ExecutingQueriesExample(ICoreConnection connection ,ILogger<ExecutingQueriesExample> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public void ExecuteSimpleQueriesFactoryExamples()
    {
        //Slect ItemName from OITM where ItemCode is A00001
        var itemName=QueryManager.ExecuteSimpleQuery<string>(_connection,"OITM","ItemName","ItemCode","A00001");
        
        // check if entry with ItemCode A00001 exists in OITM
        var iytemCodeExists=QueryManager.ValueExists(_connection,"OITM","ItemCode","A00001");
        
    }
    
    public void  SimpleQuery()
    {
        //Slect ItemName from OITM where ItemCode is A00001
        var qm = _connection.GetQuery();
        //specify the result columns 
        qm.SetSimpleResultFields("ItemName","InvntryUom","FrgnName");;
        //specify the table
        qm.SimpleTableName="OITM";
        //specify the where clause
        qm.SetSimpleWhereFields("ItemCode");
        //specify the where clause that contains more than one column
        qm.SetSimpleWhereFields("InvntryUom","FrgnName");
        ///now we can exeute
        using  var result = qm.ExecuteSimpleParameters("A00001");
        //wexecute but with ItemCodes in clause
       // using  var result2 = qm.ExecuteSimpleParameters(qm.GenerateSqlInStatment("A00001","A00002","A00003"));
    }

    public void QueriesWithPatameters()
    {
        var qm = _connection.GetQuery();
        //set Query
        qm.CommandText=_connection.IsHanaConnection()? "select * from OITM where ItemCode = @ItemCode and InvntryUom = @InvntryUom and FrgnName = @FrgnName":
            "select * from OITM where ItemCode = @ItemCode and InvntryUom = @InvntryUom and FrgnName = @FrgnName";
        //Add Parameters
        qm.AddParameter("ItemCode","A00001");   
        qm.AddParameter("InvntryUom","PCE");
        qm.AddParameter("FrgnName","FrgName");
        //Execute
        using var res= qm.Execute();
    }
    public void RetrivingTheResults()
    {
        var qm = _connection.GetQuery();
        //Execute
        using var res= qm.Execute();
        ///standed enumeration
        for(int i=0;i<res.RecordCount;i++)
        {
            var value = res.GetValue("ItemCOde");
            var value2 = res.GetValue<string>("ItemCOde");
            res.MoveNext();
        }
        //while 
        while(res.EoF)
        {
            var value = res.GetValue("ItemCOde");
            res.MoveNext();
        }
        //Ienumerable
           var lst= res.Select(r=>r.Get<string>("ItemCode")).ToList();
           var results = res.Select(r=>new {ItemCode=r.Get<string>("ItemCode"),ItemName=r.Get<string>("ItemName")}).ToList();
           
    }
    
}