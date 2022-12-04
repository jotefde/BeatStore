using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Interfaces.Factories;
using BeatStore.API.Interfaces.Repositories;
using System.Diagnostics;

namespace BeatStore.API.UseCases.Tracks
{
    public class GetTrackUseCase : ABaseUseCase
    {
        private readonly ITrackRepository _trackRepository;
        public GetTrackUseCase(ITrackRepository trackRepository)
        {
            _trackRepository = trackRepository;
        }

        public async Task Handle(string id)
        {
            try
            {
                var response = await _trackRepository.GetById(id);
                OutputPort = response;
            }
            catch(Exception e)
            {
                OutputPort = new StandardResponse(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
