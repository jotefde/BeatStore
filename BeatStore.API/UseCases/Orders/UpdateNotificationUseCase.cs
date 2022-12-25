using BeatStore.API.DTO.Requests.Orders;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Helpers.Enums;
using BeatStore.API.Interfaces.Repositories;
using BeatStore.API.Interfaces.Services;
using System.Text;

namespace BeatStore.API.UseCases.Orders
{
    public class UpdateNotificationUseCase : ABaseUseCase
    {
        private readonly IOrderRepository _orderRepository;
        public UpdateNotificationUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(NotifyOrderRequest request)
        {
            var getOD = await _orderRepository.GetById(request.order.extOrderId);
            if (getOD.Data == null)
            {
                Console.WriteLine($"There is no order with id '{request.order.extOrderId}'");
                return; 
            }
            var oldOD = getOD.Data;
            try
            {
                object? tempObject;
                Enum.TryParse(typeof(OrderStatus), request.order.status, out tempObject);
                oldOD.Status = (OrderStatus?)tempObject;

                tempObject = null;
                Enum.TryParse(typeof(PaymentMethod), request.order.payMethod?.type, out tempObject);
                var paymentMethod = (PaymentMethod?)tempObject;
                if (paymentMethod != null)
                    oldOD.PayMethod = paymentMethod;
                OutputPort = await _orderRepository.Update(oldOD);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                OutputPort = new StandardResponse(ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }

        }
    }
}
