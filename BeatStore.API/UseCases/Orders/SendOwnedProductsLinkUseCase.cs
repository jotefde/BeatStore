using BeatStore.API.DTO.Requests.Orders;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Helpers;
using BeatStore.API.Helpers.Enums;
using BeatStore.API.Interfaces.Repositories;
using BeatStore.API.Interfaces.Services;

namespace BeatStore.API.UseCases.Orders
{
    public class SendOwnedProductsLinkUseCase : ABaseUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMailingService _mailClient;
        public SendOwnedProductsLinkUseCase(IOrderRepository orderRepository, IMailingService mailClient)
        {
            _orderRepository = orderRepository;
            _mailClient = mailClient;
        }

        public async Task Handle(OrderDetails orderDetails)
        {
            var orderAccessResult = await _orderRepository.GetOrderAccess(orderDetails.Id);
            if (orderAccessResult.Success)
                return;


            if (string.IsNullOrEmpty(orderDetails.PaymentId))
            {
                OutputPort = new StandardResponse($"Order with id {orderDetails.Id} has empty PaymentId", System.Net.HttpStatusCode.BadRequest);
                return;
            }

            var recipient = $"{orderDetails.CustomerFirstName} {orderDetails.CustomerLastName}";
            var accessKey = MD5Hasher.Make(orderDetails.PaymentId);
            var mailSendResult = await _mailClient.SendOrderCompletedNotification(orderDetails.CustomerEmail, recipient, orderDetails.PaymentId, accessKey);
            if(mailSendResult)
            {
                OutputPort = await _orderRepository.CreateAccess(orderDetails.Id, accessKey);
                return;
            }
        }
    }
}