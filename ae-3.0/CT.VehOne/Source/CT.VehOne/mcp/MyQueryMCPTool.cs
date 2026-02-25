using System.ComponentModel;
using CompuTec.Core2.AE.Utils.Extensions;
using ModelContextProtocol.Server;

namespace CT.VehOne.mcp;

[McpServerToolType]
internal class MyQueryMCPTool
{
    private readonly ICoreConnection _connection;
    private readonly ILogger<MyQueryMCPTool> _logger;

    public MyQueryMCPTool(ICoreConnection connection,ILogger<MyQueryMCPTool> logger)
    {
        _connection = connection;
        _logger = logger;
    }
    
    [McpServerTool(Name = "ExecuteQuery",Destructive = false,ReadOnly = false,Title = "Execute Query In sap Busines Databse")]
    [Description("Execute Query In sap Busines Databse")]
    public async Task<string> ExecuteWuery([Description("Query to execute")]
        string query) 
    {
        using var measure = _logger.CreateMeasure();
        _logger.LogTrace("Mcp tool executing query {query}",query);
        var queryModel = _connection.GetQuery();
        queryModel.CommandText=query;
        using var res=await queryModel.ExecuteAsync();
        return res.GetCachedData().ToSimpleJson();
    }
    
}