using BeatStore.API.Context;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Helpers.Enums;
using BeatStore.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace BeatStore.API.Repositories
{
    public class TrackStorageRepository : ITrackStorageRepository
    {

        private readonly ApplicationDBContext _dbContext;

        public TrackStorageRepository(ApplicationDBContext dbContext) { _dbContext= dbContext; }

        public async Task<StandardResponse> Create(TrackObject trackObject)
        {
            try
            {
                Expression<Func<TrackObject, bool>> anyStmt = to => to.Id.Equals(trackObject.Id) && to.ObjectType == trackObject.ObjectType;
                var toExists = await _dbContext.TrackObjects.AnyAsync(anyStmt);
                if (toExists)
                    return new StandardResponse($"Track with id '{trackObject.TrackId}' already contains {trackObject.ObjectType} object");
                await _dbContext.TrackObjects.AddAsync(trackObject);
                var results = await _dbContext.SaveChangesAsync();
                if (results < 1)
                    throw new Exception("Something went wrong [SQL Exception]");
                return new StandardResponse(trackObject.Id);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"TrackStorageRepository.Create: {e.Message}");
                return new StandardResponse(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ListResponse<TrackObject>> GetByTrackId(string id)
        {
            try
            {
                var trackObjects = await _dbContext.TrackObjects
                    .Where(to => to.TrackId.Equals(id))
                    .ToListAsync();
                return new ListResponse<TrackObject>(trackObjects);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"TrackStorageRepository.GetByTrackId: {e.Message}");
                return new ListResponse<TrackObject>(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ValueResponse<TrackObject>> GetByTrackId(string id, TrackObjectType type)
        {
            Expression<Func<TrackObject, bool>> whereStmt = to => to.TrackId.Equals(id) && to.ObjectType == type;
            try
            {
                var trackObject = await _dbContext.TrackObjects
                    .Where(whereStmt)
                    .SingleOrDefaultAsync();
                return new ValueResponse<TrackObject>(trackObject);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"TrackStorageRepository.GetByTrackId: {e.Message}");
                return new ValueResponse<TrackObject>(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
