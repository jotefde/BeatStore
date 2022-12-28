using BeatStore.API.Context;
using BeatStore.API.DTO.PayU;
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
        public async Task<StandardResponse> Create(OrderDetails order, IEnumerable<Track> tracks)
        {
            try
            {
                order.Status = Helpers.Enums.OrderStatus.PENDING;
                if(string.IsNullOrEmpty(order.PaymentId))
                    order.PaymentId = "";
                /*var existsTrackCount = await _dbContext.Stock.CountAsync(
                    s => trackIds.Contains(s.Track.Id) && s.IsAvailable());
                if(existsTrackCount != trackIds.Count())
                    return new StandardResponse("Cannot recognize one or more tracks with given ids", HttpStatusCode.NotFound);*/

                await _dbContext.AddAsync<OrderDetails>(order);

                var orderItems = tracks.Select(t =>
                    new OrderItem()
                    {
                        Id = Guid.NewGuid().ToString(),
                        TrackId = t.Id,
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
                Console.WriteLine($"OrderRepository.Create: {e.Message}");
                return new StandardResponse(e.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<StandardResponse> CreateAccess(string orderId, string accessKey)
        {
            try
            {
                var access = new OrderAccess() {
                    Id = Guid.NewGuid().ToString(),
                    OrderId = orderId,
                    Key = accessKey
                };
                await _dbContext.AddAsync(access);

                var results = await _dbContext.SaveChangesAsync();
                if (results < 1)
                    throw new Exception("Something went wrong [SQL Exception]");

                return new StandardResponse(access.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"OrderRepository.CreateAccess: {e.Message}");
                return new StandardResponse(e.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ValueResponse<OrderDetails>> GetByAccess(string accessKey)
        {
            try
            {
                var access = await _dbContext.OrderAccesses
                .Where(oa => oa.Key.Equals(accessKey))
                .SingleAsync();
                if (access == null)
                    throw new Exception("Access Key not found");
                var od = await _dbContext.Orders
                    .Include(o => o.Items)
                    .ThenInclude(oi => oi.Track)
                    .Where(o => o.Id.Equals(access.OrderId))
                    .SingleAsync();
                return new ValueResponse<OrderDetails>(od);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
               return new ValueResponse<OrderDetails>("Access key is invalid", HttpStatusCode.Forbidden);
            }
        }

        public async Task<ValueResponse<OrderDetails>> GetById(string id)
        {
            var od = await _dbContext.Orders.FindAsync(id);
            return new ValueResponse<OrderDetails>(od);
        }

        public async Task<ListResponse<OrderItem>> GetItems(string orderId)
        {
            try
            {
                var items = await _dbContext.OrderItems
                    .Where(o => o.OrderDetailsId == orderId)
                    .ToListAsync();
                return new ListResponse<OrderItem>(items);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return new ListResponse<OrderItem>(e.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ValueResponse<OrderAccess>> GetOrderAccess(string orderId)
        {
            try
            {
                var access = await _dbContext.OrderAccesses
                .Where(oa => oa.OrderId.Equals(orderId))
                .SingleAsync();
                if (access == null)
                    throw new Exception($"Access for order '{orderId}' does not exists");
                return new ValueResponse<OrderAccess>(access);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ValueResponse<OrderAccess>($"Access for order '{orderId}' does not exists", HttpStatusCode.NotFound);
            }
        }

        public async Task<bool> HasAccess(string trackId, string accessKey)
        {
            try
            {
                var foundAccess = await _dbContext.OrderAccesses
                    .Include(oa => oa.OrderDetails)
                    .ThenInclude(od => od.Items)
                    .Where(oa => oa.Key.Equals(accessKey))
                    .SingleAsync();
                if (foundAccess == null)
                    return false;
                var anyTrackFound = foundAccess.OrderDetails.Items.Any(item => item.TrackId.Equals(trackId));
                return anyTrackFound;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ValueResponse<OrderDetails>> Update(OrderDetails orderDetails)
        {
            try
            {
                var orderDetailsBuff = _dbContext.Orders.Update(orderDetails);
                var results = await _dbContext.SaveChangesAsync();
                if (results < 1)
                    throw new Exception("Something went wrong [SQL Exception]");
                return new ValueResponse<OrderDetails>(orderDetailsBuff.Entity);

            }
            catch(Exception e)
            {
                Console.WriteLine($"OrderRepository.Update: {e.Message}");
                return new ValueResponse<OrderDetails>(e.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
