using BeatStore.API.DTO.Responses;
using BeatStore.API.Interfaces.Repositories;

namespace BeatStore.API.UseCases.Tracks
{
    public class ListAllTrackUseCase : ABaseUseCase
    {
        private readonly ITrackRepository _trackRepository;
        public ListAllTrackUseCase(ITrackRepository trackRepository)
        {
            _trackRepository = trackRepository;
        }

        public async Task Handle()
        {
            try
            {
                var response = await _trackRepository.GetAll();
                OutputPort = response;
            }
            catch(Exception e)
            {
                OutputPort = new StandardResponse(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
