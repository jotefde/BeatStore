using BeatStore.API.DTO.Responses;
using BeatStore.API.Interfaces.Repositories;
using BeatStore.API.Interfaces.Services;
using System.IO.Compression;

namespace BeatStore.API.UseCases.Stock
{
    public class GetStockBySlugUseCase : ABaseUseCase
    {
        private readonly IStockRepository _stockRepository;
        private readonly ITrackStorageRepository _trackStorageRepository;
        private readonly IObjectStorageService _minioClient;
        public GetStockBySlugUseCase(IStockRepository stockRepository, IObjectStorageService minioClient, ITrackStorageRepository trackStorageRepository)
        {
            _stockRepository = stockRepository;
            _minioClient = minioClient;
            _trackStorageRepository = trackStorageRepository;
        }

        public async Task Handle(string slug)
        {
            try
            {
                var response = await _stockRepository.GetBySlug(slug);
                if (!response.Success)
                {
                    OutputPort = new StandardResponse($"Cannot find track with slug '{slug}'", System.Net.HttpStatusCode.NotFound);
                    return;
                }
                var trackObject = await _trackStorageRepository.GetByTrackId(response?.Data?.Track.Id, Helpers.Enums.TrackObjectType.TRACKOUT_FILE);
                if(!trackObject.Success)
                {
                    OutputPort = new ValueResponse<object>(new { Stock = response?.Data, Trackout = new List<string>() });
                    return;
                }
                var zipFile = await _minioClient.GetTrackObject(trackObject.Data.TrackId, trackObject.Data.Name);
                var entryList = new List<string>();

                using (ZipArchive archive = new ZipArchive(zipFile))
                {
                    entryList = archive.Entries.Select(e => e.Name).ToList();
                }
                zipFile.Close();
                OutputPort = new ValueResponse<object>(new { Stock = response?.Data, Trackout = entryList });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                OutputPort = new StandardResponse(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
