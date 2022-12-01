using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;

namespace BeatStore.API.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<StandardResponse> Create(OrderDetails order, IEnumerable<string> orderItems);
    }
}
