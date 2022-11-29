using BeatStore.API.DTO;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Interfaces.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BeatStore.API.Interfaces.Repositories
{
    public interface IStockRepository
    {
        Task<ListResponse<Stock>> GetAll(bool publishedOnly = true);
        Task<ValueResponse<Stock>> GetById(string stockId);
        Task<StandardResponse> Create(Stock stock);
        Task<ValueResponse<Stock>> Update(Stock stock);
        Task<StandardResponse> Delete(string stockId);
    }
}
