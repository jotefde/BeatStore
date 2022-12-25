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

            /*var getItemsResult = await _orderRepository.GetItems(orderDetails.Id);
            if (!getItemsResult.Success)
            {
                OutputPort = getItemsResult;
                return;
            }
            var trackIds = orderDetails.Items.Select(x => x.TrackId);
            if(!trackIds.Any())
            {
                OutputPort = new StandardResponse("Server cannot find owned tracks", HttpStatusCode.InternalServerError);
                return;
            }

            var getTracksResult = await _trackRepository.GetAll(trackIds);
            if (!getTracksResult.Success)
            {
                OutputPort = getTracksResult;
                return;
            }
            var tracks = getTracksResult.Data;*/

            var trackObjects = new Dictionary<string, TrackObjects>();
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
