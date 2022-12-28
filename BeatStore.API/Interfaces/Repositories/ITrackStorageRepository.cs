using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Helpers.Enums;

namespace BeatStore.API.Interfaces.Repositories
{
    public interface ITrackStorageRepository
    {
        Task<StandardResponse> Create(TrackObject trackObject);
        Task<ListResponse<TrackObject>> GetByTrackId(string id);
        Task<ValueResponse<TrackObject>> GetByTrackId(string id, TrackObjectType type);
    }
}
