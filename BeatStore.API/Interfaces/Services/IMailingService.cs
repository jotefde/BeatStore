namespace BeatStore.API.Interfaces.Services
{
    public interface IMailingService
    {
        public Task<bool> SendOrderCompletedNotification(string destination, string recipient, string orderId, string accessKey);
    }
}
