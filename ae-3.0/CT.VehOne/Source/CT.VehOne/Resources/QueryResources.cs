using CompuTec.BaseLayer;
using CompuTec.BaseLayer.Connection;
using CompuTec.Core2.DI.Database;
using CompuTec.Core2.DI.Database.Bulk;
using CompuTec.Core2.Queries;

namespace CT.VehOne.Resources;

public interface IVehicleQueries
{
    IList<(string ItemCOde, string itemName)> GetItemsToResore();
}
    

[Ioc(ReferenceType = typeof(IVehicleQueries),Scope = IocScope.Connection)]
internal class QueryResources:BaseSapQueries<MssqlResources,HanaQueries>,IVehicleQueries
{
    public QueryResources(ICoreConnection connection) : base(connection)
    {
    }
    
    
    public IList<(string ItemCOde, string itemName)> GetItemsToResore()
    {
        var qm= _CoreConnection.GetQuery();
        qm.CommandText=GetResource(nameof(GetItemsToResore));
        using var res= qm.Execute();
        return res.Select(p=>(p.Get<string>("ItemCode"),p.Get<string>("ItemName")));
    }
}