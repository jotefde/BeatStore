using BeatStore.API.DTO.Responses;
using BeatStore.API.Helpers.Enums;
using BeatStore.API.Interfaces.Repositories;
using BeatStore.API.Interfaces.Services;
using BeatStore.API.Repositories;

namespace BeatStore.API.UseCases.TrackStorage
{
    public class GetObjectStreamUseCase : ABaseUseCase
    {
        private readonly ITrackStorageRepository _trackStorageRepository;
        private readonly IObjectStorageService _minioOS;
        private readonly IOrderRepository _orderRepository;
        public GetObjectStreamUseCase(IObjectStorageService minioOS, ITrackStorageRepository trackstorageRepository, IOrderRepository orderRepository)
        {
            _minioOS = minioOS;
            _trackStorageRepository = trackstorageRepository;
            _orderRepository = orderRepository;
        }

        public async Task Handle(string trackId, string accessKey, TrackObjectType objectType)
        {
            var hasAccess = await _orderRepository.HasAccess(trackId, accessKey);
            if (!hasAccess)
            {
                OutputPort = new StandardResponse($"Access denied", System.Net.HttpStatusCode.Forbidden);
                return;
            }

            var trackObjectResult = await _trackStorageRepository.GetByTrackId(trackId, objectType);
            if (!trackObjectResult.Success)
            {
                OutputPort = new StandardResponse($"Cannot find {objectType} object for track '{trackId}'", System.Net.HttpStatusCode.NotFound);
                return;
            }

            var stream = await _minioOS.GetTrackObject(trackId, trackObjectResult.Data.Name);
            OutputPort = new StreamResponse(stream, trackObjectResult.Data.Name, trackObjectResult.Data.MIME);
        }
    }
}
