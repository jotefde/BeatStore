using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Helpers;
using BeatStore.API.Interfaces.Services;
using BeatStore.API.Interfaces.Repositories;
using BeatStore.API.Repositories;
using BeatStore.API.Helpers.Enums;
using BeatStore.API.Interfaces.DTO.Responses;

namespace BeatStore.API.UseCases.TrackStorage
{
    public class CreateTrackObjectsUseCase : ABaseUseCase
    {
        private readonly ITrackRepository _trackRepository;
        private readonly ITrackStorageRepository _trackStorageRepository;
        private readonly IObjectStorageService _minioOS;
        public CreateTrackObjectsUseCase(ITrackRepository trackRepository, IObjectStorageService minioOS, ITrackStorageRepository trackstorageRepository)
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

                var createdObjects = new List<string>();
                var result = await saveObject(trackId, TrackObjectType.WAVE_FILE, waveFile);
                if (!result.Success)
                {
                    OutputPort = result;
                    return;
                }
                createdObjects.Add(result.Data);

                result = await saveObject(trackId, TrackObjectType.TRACKOUT_FILE, trackoutPack);
                if (!result.Success)
                {
                    OutputPort = result;
                    return;
                }
                createdObjects.Add(result.Data);


                if (sampleFile != null)
                {
                    result = await saveObject(trackId, TrackObjectType.SAMPLE_FILE, sampleFile);
                    if (!result.Success)
                    {
                        OutputPort = result;
                        return;
                    }
                    createdObjects.Add(result.Data);
                }

                OutputPort = new ListResponse<string>(createdObjects);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                OutputPort = new StandardResponse(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        private async Task<StandardResponse> saveObject(string trackId, TrackObjectType type, IFormFile file)
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

            var trackObject = new TrackObject
            {
                Id = Guid.NewGuid().ToString(),
                TrackId = trackId,
                Name = file.FileName,
                ObjectType = type,
                MIME = file.ContentType,
                Size = file.Length
            };

            var response = await _trackStorageRepository.Create(trackObject);
            return response;
        }
    }
}
