using BeatStore.API.DTO.PayU;
using BeatStore.API.DTO.PayU.Requests;

namespace BeatStore.API.Interfaces.Services
{
    public interface IPaymentService
    {
        public Task<(string, string)> NewPayment(Order request);
        public Task CollectAuthToken();
    }
}
