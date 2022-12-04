using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Helpers;
using BeatStore.API.Interfaces.Factories;
using BeatStore.API.Interfaces.Repositories;
using BeatStore.API.Repositories;

namespace BeatStore.API.UseCases.TrackStorage
{
    public class CreateTrackObjectsUseCase : ABaseUseCase
    {
        private readonly ITrackRepository _trackRepository;
        private readonly ITrackStorageRepository _trackStorageRepository;
        private readonly IObjectStorageFactory _minioOS;
        public CreateTrackObjectsUseCase(ITrackRepository trackRepository, IObjectStorageFactory minioOS, ITrackStorageRepository trackstorageRepository)
        {
            _trackRepository = trackRepository;
            _minioOS = minioOS;
            _trackStorageRepository = trackstorageRepository;
        }

        public async Task Handle(string trackId, IFormFile waveFile, IFormFile? sampleFile, IFormFile trackoutPack)
        {
            try
            {
                var checkTrackExists = await _trackRepository.GetById(trackId);
                if (!checkTrackExists.Success)
                {
                    OutputPort = new StandardResponse($"Track with id '{trackId}' does not exists", System.Net.HttpStatusCode.NotFound);
                    return;
                }

                var trackObjects = new TrackObjects
                {
                    Id = Guid.NewGuid().ToString(),
                    TrackId = trackId,
                    WaveFile = waveFile.FileName,
                    SampleFile = sampleFile?.FileName,
                    TrackoutFile = trackoutPack.FileName
                };

                await uploadToMinIO(trackId, waveFile);
                await uploadToMinIO(trackId, trackoutPack);
                if (sampleFile != null)
                    await uploadToMinIO(trackId, sampleFile);

                var response = await _trackStorageRepository.Create(trackObjects);
                OutputPort = response;
            }
            catch (Exception e)
            {
                OutputPort = new StandardResponse(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        private async Task uploadToMinIO(string trackId, IFormFile file)
        {
            var minioResult = false;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                minioResult = await _minioOS.AddTrackObject(trackId, file.FileName, stream);
                stream.Close();
            }
            if (!minioResult)
                throw new Exception($"An error occurred while uploading '{file.FileName}' to ObjectStorage");
        }
    }
}
