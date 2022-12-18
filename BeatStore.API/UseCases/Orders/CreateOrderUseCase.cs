using BeatStore.API.DTO.PayU;
using BeatStore.API.DTO.PayU.Requests;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Interfaces.Repositories;
using BeatStore.API.Interfaces.Services;
using System.Net;

namespace BeatStore.API.UseCases.Orders
{
    public class CreateOrderUseCase : ABaseUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly IPaymentService _paymentService;
        public CreateOrderUseCase(IOrderRepository orderRepository, IPaymentService paymentService, ITrackRepository trackRepository)
        {
            _orderRepository = orderRepository;
            _paymentService = paymentService;
            _trackRepository = trackRepository;
        }

        public async Task Handle(OrderDetails orderDetails, IEnumerable<string> orderItems)
        {
            try
            {
                var buyer = new Buyer(orderDetails.CustomerEmail, orderDetails.CustomerPhone, orderDetails.CustomerFirstName, orderDetails.CustomerLastName);
                var tracks = new List<Track>();
                int totalAmount = 0;
                foreach (var id in orderItems)
                {
                    var trackWrap = await _trackRepository.GetById(id);
                    if (!trackWrap.Success)
                    {
                        OutputPort = new StandardResponse($"Cannot recognize a track with Id '{id}'", HttpStatusCode.NotFound);
                        return;
                    }
                    totalAmount += Convert.ToInt32(trackWrap?.Data?.Price * 100);
                    tracks.Add(trackWrap.Data);
                }
                var products = tracks.Select(t =>
                    new Product(t.Name, Convert.ToInt32(t.Price * 100).ToString(), "1"))
                    .ToList();
                orderDetails.Id = Guid.NewGuid().ToString();
                var order = new Order(orderDetails.Id, orderDetails.CustomerIP, orderDetails.Description, orderDetails.CurrencyCode.ToString(), totalAmount.ToString(), buyer, products);
                //var request = new NewPaymentRequest(order.CustomerIP, order.Description, order.CurrencyCode.ToString(), totalAmount.ToString(), buyer, products);
                (string redirectUri, string orderId) = await _paymentService.NewPayment(order);
                orderDetails.PaymentId = orderId;
                var response = await _orderRepository.Create(orderDetails, tracks);
                if (response.Success)
                    response.SetData(redirectUri);
                OutputPort = response;
            }
            catch (Exception e)
            {
                OutputPort = new StandardResponse(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
