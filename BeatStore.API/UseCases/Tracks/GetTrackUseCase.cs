using BeatStore.API.DTO.Responses;
using BeatStore.API.Interfaces.Factories;
using BeatStore.API.Interfaces.Repositories;
using System.Diagnostics;

namespace BeatStore.API.UseCases.Tracks
{
    public class GetTrackUseCase : ABaseUseCase
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IObjectStorageFactory _minioOS;
        public GetTrackUseCase(ITrackRepository trackRepository, IObjectStorageFactory minioOS)
        {
            _trackRepository = trackRepository;
            _minioOS = minioOS;
        }

        public async Task<bool> Handle(string id)
        {
            try
            {
                var response = await _trackRepository.GetById(id);
                OutputPort = response;
                return response != null;
            }
            catch(Exception e)
            {
                OutputPort = new StandardResponse(e.Message, 500);
                return true;
            }
        }
    }
}
