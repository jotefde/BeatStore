using BeatStore.API.DTO.Responses;
using BeatStore.API.Interfaces.Repositories;
using BeatStore.API.Interfaces.UseCases.Tracks;

namespace BeatStore.API.UseCases.Tracks
{
    public class ListAllTracksUseCase : ABaseUseCase
    {
        private readonly ITrackRepository _trackRepository;
        public ListAllTracksUseCase(ITrackRepository trackRepository)
        {
            _trackRepository = trackRepository;
        }

        public async Task<bool> Handle()
        {
            try
            {
                var response = await _trackRepository.GetAll();
                OutputPort = response;
                return response != null;
            }
            catch(Exception e)
            {
                OutputPort = new StandardResponse(e.Message, 500);
                return false;
            }
        }
    }
}
