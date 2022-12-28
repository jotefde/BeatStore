using BeatStore.API.DTO.Requests.Tracks;
using BeatStore.API.DTO.Requests.TrackStorage;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Extensions.RequestAttributes;
using BeatStore.API.Helpers.Enums;
using BeatStore.API.UseCases.Tracks;
using BeatStore.API.UseCases.TrackStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatStore.API.Controllers
{
    [Route("track-storage")]
    [ApiController]
    public class TrackStorageController : ControllerBase
    {
        private readonly CreateTrackObjectsUseCase _createTrackObjectsUseCase;
        private readonly GetObjectStreamUseCase _getObjectStreamUseCase;

        public TrackStorageController(CreateTrackObjectsUseCase createTrackObjectsUseCase, GetObjectStreamUseCase getObjectStreamUseCase)
        {
            _createTrackObjectsUseCase = createTrackObjectsUseCase;
            _getObjectStreamUseCase = getObjectStreamUseCase;
        }

        #region POST /track-storage/upload-all
        [HttpPost("upload-all")]
        //[Authorize(Roles = Helpers.Constants.UserRole.Admin)]
        [RequestSizeLimit(200_000_000)]
        public async Task<ActionResult> UploadAll([FromForm] UploadPackageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _createTrackObjectsUseCase.Handle(request.TrackId, request.WaveFile, request.SampleFile, request.TrackoutPack);
            return _createTrackObjectsUseCase.OutputPort.GetResult();
        }
        #endregion

        #region GET /track-storage/{trackId}/{objectType}
        [HttpGet("{trackId}/{objectType}")]
        public async Task<ActionResult> GetStream([FromRoute] [GUID] string trackId, [FromRoute] TrackObjectType objectType, [FromQuery] string accessKey)
        {
            await _getObjectStreamUseCase.Handle(trackId, accessKey, objectType);
            var result = _getObjectStreamUseCase.OutputPort.GetResult();
            return result;
        }
        #endregion
    }
}
