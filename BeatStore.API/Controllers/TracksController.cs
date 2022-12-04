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
using System.Net;
using BeatStore.API.Extensions.RequestAttributes;

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
            await _listAllTracksUseCase.Handle();
            return _listAllTracksUseCase.OutputPort.GetResult();
        }
        #endregion

        #region GET /tracks/:id
        [HttpGet("{trackId}")]
        public async Task<ActionResult> GetTrack([FromRoute] [GUID] string trackId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _getTrackUseCase.Handle(trackId);
            return _getTrackUseCase.OutputPort.GetResult();
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

            if(trackModel.CoverImage != null)
            {
                var imageStream = new MemoryStream();
                await trackModel.CoverImage.CopyToAsync(imageStream);
                var imageExt = Path.GetExtension(trackModel.CoverImage.FileName);
                await _createTrackUseCase.Handle(track, imageStream, imageExt);
            }
            else
                await _createTrackUseCase.Handle(track);

            return _createTrackUseCase.OutputPort.GetResult();
        }
        #endregion
    }
}
