using BeatStore.API.Context;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Helpers;
using BeatStore.API.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;

namespace BeatStore.API.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public TrackRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ListResponse<Track>> GetAll(IEnumerable<string>? ids = null)
        {
            try
            {
                if (ids != null)
                {
                    var results = await _dbContext.Tracks.Where(t => ids.Contains(t.Id)).ToListAsync();
                    return new ListResponse<Track>(results);
                }
                else
                {
                    var results = await _dbContext.Tracks.ToListAsync();
                    return new ListResponse<Track>(results);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine($"TrackRepository.GetAll: {e.Message}");
                return new ListResponse<Track>(e.Message);
            }
        }

        public async Task<ValueResponse<Track>> GetById(string trackId)
        {
            try
            {
                var results = await _dbContext.Tracks.Where(t => t.Id == trackId).FirstOrDefaultAsync();
                return new ValueResponse<Track>(results);
            }
            catch(Exception e)
            {
                Trace.WriteLine($"TrackRepository.GetById: {e.Message}");
                return new ValueResponse<Track>(e.Message);
            }
        }

        public async Task<StandardResponse> Create(Track track)
        {
            try
            {
                var trackExists = await _dbContext.Tracks.AnyAsync(t => t.Slug.Equals(Slugify.Generate(track.Name)));
                if(trackExists)
                    return new StandardResponse($"Track with name '{track.Name}' already exists.", System.Net.HttpStatusCode.Forbidden);

                await _dbContext.Tracks.AddAsync(track);
                var results = await _dbContext.SaveChangesAsync();
                if (results < 1)
                    throw new Exception("Something went wrong [SQL Exception]");
                return new StandardResponse(track.Id);
            }
            catch(Exception e)
            {
                Trace.WriteLine($"TrackRepository.Create: {e.Message}");
                return new StandardResponse(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
