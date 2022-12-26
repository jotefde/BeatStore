using BeatStore.API.DTO.Responses;
using BeatStore.API.Interfaces.Repositories;
using BeatStore.API.Interfaces.Services;

namespace BeatStore.API.UseCases.TrackStorage
{
    public class GetTrackoutStreamUseCase : ABaseUseCase
    {
        private readonly ITrackStorageRepository _trackStorageRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IObjectStorageService _minioOS;
        public GetTrackoutStreamUseCase(IObjectStorageService minioOS, ITrackStorageRepository trackstorageRepository, IOrderRepository orderRepository)
        {
            _minioOS = minioOS;
            _trackStorageRepository = trackstorageRepository;
            _orderRepository = orderRepository;
        }

        public async Task Handle(string trackId, string accessKey)
        {
            var hasAccess = await _orderRepository.HasAccess(trackId, accessKey);
            if(!hasAccess)
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
            if (to.Data.TrackoutFile == null)
            {
                OutputPort = new StandardResponse($"There is no trackout pack for '{trackId}'", System.Net.HttpStatusCode.NotFound);
                return;
            }
            var zipStream = await _minioOS.GetTrackObject(trackId, to.Data.TrackoutFile);
            OutputPort = new StreamResponse(zipStream, to.Data.TrackoutFile, "application/zip");
        }
    }
}