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

namespace BeatStore.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        #region UseCases
        private readonly ListAllTracksUseCase _listAllTracksUseCase;
        #endregion

        public TracksController(ListAllTracksUseCase listAllTracksUseCase)
        {
            _listAllTracksUseCase = listAllTracksUseCase;
        }


        #region GET /tracks
        [HttpGet]
        public async Task<ActionResult> GetTracks()
        {
            var result = await _listAllTracksUseCase.Handle();
            if (result)
                return _listAllTracksUseCase.OutputPort?.Data;
            return new StandardResponse("OutputPort is empty", 500).Data;
        }
        #endregion
    }
}
