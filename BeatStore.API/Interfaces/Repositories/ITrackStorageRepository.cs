using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;

namespace BeatStore.API.Interfaces.Repositories
{
    public interface ITrackStorageRepository
    {
        Task<StandardResponse> Create(TrackObjects to);
    }
}
