using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Interfaces.Repositories;
using BeatStore.API.Interfaces.Services;

namespace BeatStore.API.UseCases.TrackStorage
{
    public class GetSampleStreamUseCase : ABaseUseCase
    {
        private readonly ITrackStorageRepository _trackStorageRepository;
        private readonly IObjectStorageService _minioOS;
        public GetSampleStreamUseCase(IObjectStorageService minioOS, ITrackStorageRepository trackstorageRepository)
        {
            _minioOS = minioOS;
            _trackStorageRepository = trackstorageRepository;
        }

        public async Task Handle(string trackId)
        {
            try
            {
                var to = await _trackStorageRepository.GetByTrackId(trackId);
                if(!to.Success)
                {
                    OutputPort = new StandardResponse($"Cannot find track objects for Id '{trackId}'", System.Net.HttpStatusCode.NotFound);
                    return;
                }
                if(to.Data.SampleFile == null)
                {
                    OutputPort = new StandardResponse($"There is no sample file for '{trackId}'", System.Net.HttpStatusCode.NotFound);
                    return;
                }
                var sampleStream = await _minioOS.GetTrackObject(trackId, to.Data.SampleFile);
                OutputPort = new StreamResponse(sampleStream, to.Data.SampleFile, "audio/mpeg");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                OutputPort = new StandardResponse(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
