using BeatStore.API.Context;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BeatStore.API.Repositories
{
    public class TrackStorageRepository : ITrackStorageRepository
    {

        private readonly ApplicationDBContext _dbContext;

        public TrackStorageRepository(ApplicationDBContext dbContext) { _dbContext= dbContext; }

        public async Task<StandardResponse> Create(TrackObjects to)
        {
            try
            {
                var toExists = await _dbContext.TrackStorage.Where(t => t.Track.Id.Equals(to.TrackId)).ToListAsync();
                if (toExists.Count > 0)
                    return new StandardResponse($"Track with id '{to.TrackId}' already contains objects");
                await _dbContext.TrackStorage.AddAsync(to);
                var results = await _dbContext.SaveChangesAsync();
                if (results < 1)
                    throw new Exception("Something went wrong [SQL Exception]");
                return new StandardResponse(to.Id);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"TrackStorageRepository.Create: {e.Message}");
                return new StandardResponse(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ValueResponse<TrackObjects>> GetByTrackId(string id)
        {
            try
            {
                var to = await _dbContext.TrackStorage.Where(t => t.TrackId.Equals(id)).FirstOrDefaultAsync();
                return new ValueResponse<TrackObjects>(to);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"TrackStorageRepository.GetByTrackId: {e.Message}");
                return new ValueResponse<TrackObjects>(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
