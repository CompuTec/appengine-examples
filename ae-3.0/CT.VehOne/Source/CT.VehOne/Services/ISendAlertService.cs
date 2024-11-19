
using CompuTec.BaseLayer.DI;

namespace CT.VehOne.Services;

public interface ISendAlertService
{
    void SendMessage(string subject, string topc, string[] users);
}

[Ioc(ReferenceType = typeof(ISendAlertService), Scope = IocScope.Connection)]
internal sealed class SendAlertService : ISendAlertService
{
    private readonly ILogger<SendAlertService> _logger;
    private readonly ICoreConnection _connection;


    public SendAlertService(ILogger<SendAlertService> logger, ICoreConnection connection)
    {
        _logger = logger;
        _connection = connection;
    }

    public void SendMessage(string subject, string topc, string[] users)
    {
        using var measure = _logger.CreateMeasure();
        var company = _connection.Connection.GetCompany();
        if (users == null || !users.Any())
        {
            //No Userrs
            _logger.LogWarning("No users to notify");
            return;
        }

        using MessagesService messageService =
            (MessagesService)company.GetCompanyService().GetBusinessService(ServiceTypes.MessagesService);

        // Create a new message
        using Message message = messageService.GetDataInterface(MessagesServiceDataInterfaces.msdiMessage);
        message.Subject = subject;
        message.Text = topc;

        // Add a recipient
        using var recipientCollection = message.RecipientCollection;
        foreach (var user in users)
        {
            var recipient = recipientCollection.Add();
            recipient.UserCode = user;
            recipient.SendInternal = BoYesNoEnum.tYES;
        }

        // Send the message
        using var resoponse = messageService.SendMessage(message);
        _logger.LogInformation("Message sent to {0} users {code}", users.Length, resoponse.Code);
    }
}