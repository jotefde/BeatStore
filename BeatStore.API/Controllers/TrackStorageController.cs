using BeatStore.API.DTO.Requests.Tracks;
using BeatStore.API.DTO.Requests.TrackStorage;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Extensions.RequestAttributes;
using BeatStore.API.UseCases.Tracks;
using BeatStore.API.UseCases.TrackStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatStore.API.Controllers
{
    [Route("track-storage")]
    [ApiController]
    [Authorize(Roles = Helpers.Constants.UserRole.Admin)]
    public class TrackStorageController : ControllerBase
    {
        private readonly CreateTrackObjectsUseCase _createTrackObjectsUseCase;
        private readonly GetSampleStreamUseCase _getSampleStreamUseCase;

        public TrackStorageController(CreateTrackObjectsUseCase createTrackObjectsUseCase, GetSampleStreamUseCase getSampleStreamUseCase)
        {
            _createTrackObjectsUseCase = createTrackObjectsUseCase;
            _getSampleStreamUseCase = getSampleStreamUseCase;
        }

        #region POST /track-storage/upload-all
        [HttpPost("upload-all")]
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

        #region GET /track-storage/{trackId}/sample
        [HttpGet("{trackId}/sample")]
        [AllowAnonymous]
        public async Task<ActionResult> GetSample([FromRoute] [GUID] string trackId)
        {
            await _getSampleStreamUseCase.Handle(trackId);
            var result = _getSampleStreamUseCase.OutputPort.GetResult();
            return result;
        }
        #endregion
    }
}
