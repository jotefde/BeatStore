using BeatStore.API.DTO.Responses;
using BeatStore.API.Interfaces.Repositories;
using BeatStore.API.Interfaces.Services;
using BeatStore.API.Repositories;

namespace BeatStore.API.UseCases.TrackStorage
{
    public class GetWaveStreamUseCase : ABaseUseCase
    {
        private readonly ITrackStorageRepository _trackStorageRepository;
        private readonly IObjectStorageService _minioOS;
        private readonly IOrderRepository _orderRepository;
        public GetWaveStreamUseCase(IObjectStorageService minioOS, ITrackStorageRepository trackstorageRepository, IOrderRepository orderRepository)
        {
            _minioOS = minioOS;
            _trackStorageRepository = trackstorageRepository;
            _orderRepository = orderRepository;
        }

        public async Task Handle(string trackId, string accessKey)
        {
            var hasAccess = await _orderRepository.HasAccess(trackId, accessKey);
            if (!hasAccess)
            {
                OutputPort = new StandardResponse($"Access denied", System.Net.HttpStatusCode.Forbidden);
                return;
            }

            var to = await _trackStorageRepository.GetByTrackId(trackId);
            if (!to.Success)
            {
                OutputPort = new StandardResponse($"Cannot find track objects for Id '{trackId}'", System.Net.HttpStatusCode.NotFound);
                return;
            }
            if (to.Data.WaveFile == null)
            {
                OutputPort = new StandardResponse($"There is no wave file for '{trackId}'", System.Net.HttpStatusCode.NotFound);
                return;
            }

            var waveStream = await _minioOS.GetTrackObject(trackId, to.Data.WaveFile);
            OutputPort = new StreamResponse(waveStream, to.Data.WaveFile, "audio/wav");
        }
    }
}
