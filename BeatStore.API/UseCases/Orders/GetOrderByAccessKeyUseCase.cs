using BeatStore.API.DTO.PayU;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Interfaces.Repositories;
using BeatStore.API.Interfaces.Services;
using System.Net;

namespace BeatStore.API.UseCases.Orders
{
    public class GetOrderByAccessKeyUseCase : ABaseUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly ITrackStorageRepository _trackStorageRepository;
        private readonly IObjectStorageService _minioClient;
        public GetOrderByAccessKeyUseCase(IOrderRepository orderRepository, ITrackRepository trackRepository, ITrackStorageRepository trackStorageRepository, IObjectStorageService minioClient)
        {
            _orderRepository = orderRepository;
            _trackRepository = trackRepository;
            _trackStorageRepository = trackStorageRepository;
            _minioClient = minioClient;
        }

        public async Task Handle(string accessKey)
        {
            var getByAccessResult = await _orderRepository.GetByAccess(accessKey);
            if(!getByAccessResult.Success)
            {
                OutputPort = new StandardResponse("Access denied", HttpStatusCode.Forbidden);
                return;
            }
            var orderDetails = getByAccessResult.Data;

            var trackObjects = new Dictionary<string, IEnumerable<TrackObject>>();
            foreach(var orderItem in orderDetails.Items)
            {
                var getByTrackResult = await _trackStorageRepository.GetByTrackId(orderItem.TrackId);
                trackObjects.Add(orderItem.TrackId, getByTrackResult.Data);
            }
            
            OutputPort = new ValueResponse<object>(new {
                Order = orderDetails,
                TrackObjects = trackObjects
            });
        }
    }
}
