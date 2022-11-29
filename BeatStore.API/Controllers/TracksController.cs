using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeatStore.API.Context;
using BeatStore.API.Entities;
using BeatStore.API.Helpers.Constants;
using BeatStore.API.Interfaces.Repositories;
using BeatStore.API.UseCases.Tracks;
using BeatStore.API.DTO.Responses;
using BeatStore.API.DTO.Requests.Tracks;
using BeatStore.API.DTO.Requests.Stock;

namespace BeatStore.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        #region UseCases
        private readonly ListAllTrackUseCase _listAllTracksUseCase;
        private readonly GetTrackUseCase _getTrackUseCase;
        private readonly CreateTrackUseCase _createTrackUseCase;
        #endregion

        public TracksController(ListAllTrackUseCase listAllTracksUseCase, GetTrackUseCase getTrackUseCase, CreateTrackUseCase createTrackUseCase)
        {
            _listAllTracksUseCase = listAllTracksUseCase;
            _getTrackUseCase = getTrackUseCase;
            _createTrackUseCase = createTrackUseCase;
        }


        #region GET /tracks
        [HttpGet("tracks")]
        public async Task<ActionResult> GetTracks()
        {
            var result = await _listAllTracksUseCase.Handle();
            if (result)
                return _listAllTracksUseCase.OutputPort?.Data;
            return new StandardResponse("OutputPort is empty", 500).Data;
        }
        #endregion

        #region GET /tracks/:id
        [HttpGet("{trackId}")]
        public async Task<ActionResult> GetTrack([FromRoute] string trackId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isGUIDValid = Guid.TryParse(trackId, out _);
            if (!isGUIDValid)
                return new StandardResponse("Wrong track id format.", 404).Data;

            var result = await _getTrackUseCase.Handle(trackId);
            if (result)
                return _getTrackUseCase.OutputPort?.Data;
            return new StandardResponse("OutputPort is empty", 500).Data;
        }
        #endregion

        #region POST /tracks
        [HttpPost]
        [RequestSizeLimit(5_000_000)]
        public async Task<ActionResult> AddTrack([FromForm] AddTrackRequest trackModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var track = new Track
            {
                Name = trackModel.Name,
                Price = trackModel.Price,
                Description = trackModel.Description
            };
            var imageStream = new MemoryStream();
            await trackModel.CoverImage.CopyToAsync(imageStream);
            var imageExt = Path.GetExtension(trackModel.CoverImage.FileName);
            var result = await _createTrackUseCase.Handle(track, imageStream, imageExt);
            if (result)
                return _createTrackUseCase.OutputPort?.Data;
            return new StandardResponse("OutputPort is empty", 500).Data;
        }
        #endregion
    }
}
