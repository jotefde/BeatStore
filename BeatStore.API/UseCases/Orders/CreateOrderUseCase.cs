using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Interfaces.Repositories;

namespace BeatStore.API.UseCases.Orders
{
    public class CreateOrderUseCase : ABaseUseCase
    {
        private readonly IOrderRepository _orderRepository;
        public CreateOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(OrderDetails order, IEnumerable<string> orderItems)
        {
            try
            {
                order.Id = Guid.NewGuid().ToString();
                var response = await _orderRepository.Create(order, orderItems);
                OutputPort = response;
            }
            catch (Exception e)
            {
                OutputPort = new StandardResponse(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
