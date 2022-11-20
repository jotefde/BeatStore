using BeatStore.API.Context;
using BeatStore.API.DTO;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Interfaces.DTO;
using BeatStore.API.Interfaces.Repositories;
using BeatStore.API.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace BeatStore.API.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public TrackRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ListResponse<Track>> GetAll()
        {
            try
            {
                var results = _dbContext.Tracks?.ToList();
                return new ListResponse<Track>(results);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"TrackRepository.GetAll: {e.Message}");
                return new ListResponse<Track>(e.Message);
            }
        }
    }
}
