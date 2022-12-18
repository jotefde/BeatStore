using BeatStore.API.DTO;
using BeatStore.API.DTO.PayU;
using BeatStore.API.DTO.PayU.Requests;
using BeatStore.API.DTO.PayU.Responses;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Helpers.Constants.PayU;
using BeatStore.API.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace BeatStore.API.Services
{
    public class PayU : IPaymentService
    {
        private readonly PaymentAPIOptions _options;
        private readonly HttpClient _client;
        private string _token;
        private DateTime _tokenExpireTime;

        public PayU(IOptions<PaymentAPIOptions> options)
        {
            _options = options.Value;
            _client = new HttpClient(new HttpClientHandler
            {
                AllowAutoRedirect = false
            });
            _client.BaseAddress = new Uri("https://secure.snd.payu.com/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _token = "";
            _tokenExpireTime = DateTime.Now;
        }

        public async Task<(string, string)> NewPayment(Order request)
        {
            if (!isAuthorized())
                await CollectAuthToken();
            request.merchantPosId = _options.client_id;
            request.notifyUrl = _options.notifyUrl;
            var response = await _client.PostAsJsonAsync<Order>(Endpoints.NewPayment, request);
            var model = await response.Content.ReadFromJsonAsync<NewPaymentResponse>();
            return (model?.redirectUri, model?.orderId);
        }

        public async Task CollectAuthToken()
        {
            var queryString = $"grant_type=client_credentials&client_id={_options.client_id}&client_secret={_options.client_secret}";
            var response = await _client.GetAsync($"{Endpoints.Authorize}?{queryString}");
            if (!response.IsSuccessStatusCode)
                return;
            var model = await response.Content.ReadFromJsonAsync<AuthorizeResponse>();
            _token = model?.access_token ?? "";
            _tokenExpireTime= DateTime.Now.AddSeconds(model?.expires_in ?? 0);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        private bool isAuthorized()
        {
            if(string.IsNullOrEmpty( _token))
                return false;
            if(_tokenExpireTime.CompareTo(DateTime.Now) <= 0)
                return false;
            return true;
        }
    }
}
