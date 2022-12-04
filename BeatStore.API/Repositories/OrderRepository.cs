using BeatStore.API.Context;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Extensions;
using BeatStore.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Minio.DataModel;
using System;
using System.Diagnostics;
using System.Net;

namespace BeatStore.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public OrderRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<StandardResponse> Create(OrderDetails order, IEnumerable<string> trackIds)
        {
            try
            {
                order.Status = Helpers.Enums.OrderStatus.PENDING;
                var existsTrackCount = await _dbContext.Stock.CountAsync(
                    s => trackIds.Contains(s.Track.Id) && s.IsAvailable());
                if(existsTrackCount != trackIds.Count())
                    return new StandardResponse("Cannot recognize one or more tracks with given ids", HttpStatusCode.NotFound);

                await _dbContext.AddAsync<OrderDetails>(order);

                var orderItems = trackIds.Select(id =>
                    new OrderItem()
                    {
                        Id = Guid.NewGuid().ToString(),
                        TrackId = id,
                        OrderDetailsId = order.Id
                    });
                await _dbContext.AddRangeAsync(orderItems);

                var results = await _dbContext.SaveChangesAsync();
                if (results < orderItems.Count() + 1)
                    throw new Exception("Something went wrong [SQL Exception]");

                return new StandardResponse(order.Id);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"OrderRepository.Create: {e.Message}");
                return new StandardResponse(e.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
