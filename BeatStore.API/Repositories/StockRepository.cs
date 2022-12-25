using BeatStore.API.Context;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Extensions;
using BeatStore.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;

namespace BeatStore.API.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public StockRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ListResponse<Stock>> GetAll(bool publishedOnly = true)
        {
            try
            {
                var list = await _dbContext.Stock
                    .Include(s => s.Track)
                    .ToListAsync();
                var availableStock = list.Where(s => s.IsAvailable() || !publishedOnly);
                return new ListResponse<Stock>(availableStock);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"StockRepository.GetAll: {e.Message}");
                return new ListResponse<Stock>(e.Message);
            }
        }

        public async Task<ValueResponse<Stock>> GetById(string stockId)
        {
            try
            {
                var results = await _dbContext.Stock
                    .Include(s => s.Track)
                    .FirstOrDefaultAsync(s => s.Id.Equals(stockId));
                return new ValueResponse<Stock>(results);
            }
            catch(Exception e)
            {
                Trace.WriteLine($"StockRepository.GetById: {e.Message}");
                return new ValueResponse<Stock>(e.Message);
            }
        }

        public async Task<StandardResponse> Create(Stock stock)
        {
            try
            {
                var track = await _dbContext.Tracks.FindAsync(stock.Track.Id);
                if (track == null)
                    return new StandardResponse($"Track with Id '{stock.Track.Id}' does not exists.", HttpStatusCode.Forbidden);
                var stockExists = await _dbContext.Stock.AnyAsync(s => s.Track.Id.Equals(stock.Track.Id));
                if(stockExists)
                    return new StandardResponse($"Stock for track '{stock.Track.Id}' already exists.", HttpStatusCode.Forbidden);

                stock.Track = track;

                if (stock.IsPublished.Value)
                    stock.PublishTime = DateTime.Now;
                await _dbContext.Stock.AddAsync(stock);
                var results = await _dbContext.SaveChangesAsync();
                if (results < 1)
                    throw new Exception("Something went wrong [SQL Exception]");
                return new StandardResponse(stock.Id);
            }
            catch(Exception e)
            {
                Trace.WriteLine($"StockRepository.Create: {e.Message}");
                return new StandardResponse(e.Message, HttpStatusCode.InternalServerError);
            }
        }

        public Task<StandardResponse> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ValueResponse<Stock>> Update(Stock stock)
        {
            try
            {
                var stockBuff = await _dbContext.Stock.FindAsync(stock.Id);
                if(stockBuff == null)
                    return new ValueResponse<Stock>($"Stock with Id '{stock.Id}' does not exists.", HttpStatusCode.NotFound);

                if (stock.Amount != null)
                    stockBuff.Amount = stock.Amount;
                if(stock.IsUnlimited != null)
                    stockBuff.IsUnlimited= stock.IsUnlimited;
                if (stock.IsPublished != null)
                {
                    stockBuff.IsPublished = stock.IsPublished;
                    if(stock.IsPublished.Value)
                        stockBuff.PublishTime = DateTime.Now;
                }

                _dbContext.Stock.Update(stockBuff);
                var results = await _dbContext.SaveChangesAsync();
                if (results < 1)
                    throw new Exception("Something went wrong [SQL Exception]");
                return new ValueResponse<Stock>(stockBuff);
            }
            catch(Exception e)
            {
                Trace.WriteLine($"StockRepository.Update: {e.Message}");
                return new ValueResponse<Stock>(e.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ValueResponse<Stock>> GetBySlug(string trackSlug)
        {
            try
            {
                var results = await _dbContext.Stock
                    .Include(s => s.Track)
                    .FirstOrDefaultAsync(s => s.Track.Slug.Equals(trackSlug));
                return new ValueResponse<Stock>(results);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"StockRepository.GetBySlug: {e.Message}");
                return new ValueResponse<Stock>(e.Message);
            }
        }
    }
}
