// Background job attribute that provides metadata such as JobId, JobName, and whether it emits a progress bar

using CompuTec.ProcessForce.API.Documents.BillOfMaterials;

[BackgroundJob(JobId = "TemplateSecureJobID", JobName = "Long Lasting Job", EmitsProgressBar = true)]
//Secure job means that this job is executedon the connection
internal sealed class CTPFExampleJobOneTimeJob : SecureJob
{
// Logger instance to log information during job execution
    private readonly ILogger<CTPFExampleJobOneTimeJob> _logger;

// Constructor that initializes the job with an invoker and a logger
    public CTPFExampleJobOneTimeJob(IAeJobInvoker aeJobInvoker, ILogger<CTPFExampleJobOneTimeJob> logger) : base(
        aeJobInvoker)
    {
        _logger = logger;
    }

// The main method that gets called when the job is executed
    public override async Task Call()
    {
        // Create a measurement scope to log the execution time of this job
        using var measure = _logger.CreateMeasure();

        // Get required services
        var queryManager = GetService<QueryManager>();
        
        ///getRandomBOmCOde wehre item Code start with CT
        queryManager.CommandText="SELECT TOP 1 T0.\"Code\" FROM OITM T0 WHERE T0.\"Code\" LIKE 'CT%' ORDER BY NEWID()";
        using var rs= queryManager.Execute();
        if (rs.RecordCount==0)
        {
            _logger.LogWarning("No item found with code starting with CT");
            return;
        }
        var code=rs.GetValue<string>("Code");
        _logger.LogInformation("Item code found: {code}", code);
        var billOfMaterial= GetService<IBillOfMaterial>();
        billOfMaterial.GetByKey(code);
        billOfMaterial.U_Instructions+=string.Format( "Executed from CTPFExampleJobOneTimeJob at {0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
        billOfMaterial.Update();
        _logger.LogInformation("Bill of material updated with instructions: {instructions}", billOfMaterial.U_Instructions);

    }
}