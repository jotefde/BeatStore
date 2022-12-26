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
    public class TrackStorageController : ControllerBase
    {
        private readonly CreateTrackObjectsUseCase _createTrackObjectsUseCase;
        private readonly GetSampleStreamUseCase _getSampleStreamUseCase;
        private readonly GetWaveStreamUseCase _getWaveStreamUseCase;
        private readonly GetTrackoutStreamUseCase _getTrackoutStreamUseCase;

        public TrackStorageController(CreateTrackObjectsUseCase createTrackObjectsUseCase, GetSampleStreamUseCase getSampleStreamUseCase, GetWaveStreamUseCase getWaveStreamUseCase, GetTrackoutStreamUseCase getTrackoutStreamUseCase)
        {
            _createTrackObjectsUseCase = createTrackObjectsUseCase;
            _getSampleStreamUseCase = getSampleStreamUseCase;
            _getWaveStreamUseCase = getWaveStreamUseCase;
            _getTrackoutStreamUseCase = getTrackoutStreamUseCase;
        }

        #region POST /track-storage/upload-all
        [HttpPost("upload-all")]
        [Authorize(Roles = Helpers.Constants.UserRole.Admin)]
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

        #region GET /track-storage/{trackId}/wave
        [HttpPost("{trackId}/wave")]
        public async Task<ActionResult> GetWave([FromRoute] [GUID] string trackId, [FromBody] string accessKey)
        {
            await _getWaveStreamUseCase.Handle(trackId, accessKey);
            var result = _getWaveStreamUseCase.OutputPort.GetResult();
            return result;
        }
        #endregion

        #region GET /track-storage/{trackId}/trackout
        [HttpPost("{trackId}/trackout")]
        public async Task<ActionResult> GetTrackout([FromRoute] [GUID] string trackId, [FromBody] string accessKey)
        {
            await _getTrackoutStreamUseCase.Handle(trackId, accessKey);
            var result = _getTrackoutStreamUseCase.OutputPort.GetResult();
            return result;
        }
        #endregion

        #region GET /track-storage/{trackId}/sample
        [HttpGet("{trackId}/sample")]
        public async Task<ActionResult> GetSample([FromRoute] [GUID] string trackId)
        {
            await _getSampleStreamUseCase.Handle(trackId);
            var result = _getSampleStreamUseCase.OutputPort.GetResult();
            return result;
        }
        #endregion
    }
}
