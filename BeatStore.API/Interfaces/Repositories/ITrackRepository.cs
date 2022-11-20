using BeatStore.API.DTO;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Interfaces.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BeatStore.API.Interfaces.Repositories
{
    public interface ITrackRepository
    {
        Task<ListResponse<Track>> GetAll();
    }
}
