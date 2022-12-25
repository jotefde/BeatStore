using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;

namespace BeatStore.API.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<StandardResponse> Create(OrderDetails order, IEnumerable<Track> orderItems);
        Task<ValueResponse<OrderDetails>> Update(OrderDetails order);
        Task<ValueResponse<OrderDetails>> GetById(string id);
        Task<ValueResponse<OrderDetails>> GetByAccess(string accessKey);
        Task<ValueResponse<OrderAccess>> GetOrderAccess(string orderId);
        Task<ListResponse<OrderItem>> GetItems(string orderId);
        Task<StandardResponse> CreateAccess(string orderId, string accessKey);
        Task<bool> HasAccess(string accessKey);
    }
}
