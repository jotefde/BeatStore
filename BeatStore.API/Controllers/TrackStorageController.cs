using BeatStore.API.DTO.Requests.Tracks;
using BeatStore.API.DTO.Requests.TrackStorage;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.UseCases.Tracks;
using BeatStore.API.UseCases.TrackStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatStore.API.Controllers
{
    [Route("track-storage")]
    [ApiController]
    //[Authorize(Roles = Helpers.Constants.UserRole.Admin)]
    public class TrackStorageController : ControllerBase
    {
        private readonly CreateTrackObjectsUseCase _createTrackObjectsUseCase;

        public TrackStorageController(CreateTrackObjectsUseCase createTrackObjectsUseCase)
        {
            _createTrackObjectsUseCase = createTrackObjectsUseCase;
        }

        #region POST /track-storage/upload-package
        [HttpPost("upload-package")]
        [RequestSizeLimit(200_000_000)]
        public async Task<ActionResult> UploadPackage([FromForm] UploadPackageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _createTrackObjectsUseCase.Handle(request.TrackId, request.WaveFile, request.SampleFile, request.TrackoutPack);
            return _createTrackObjectsUseCase.OutputPort.GetResult();
        }

        #endregion
    }
}
