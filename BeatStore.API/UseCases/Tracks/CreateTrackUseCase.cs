using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Helpers;
using BeatStore.API.Interfaces.Factories;
using BeatStore.API.Interfaces.Repositories;
using System;
using System.IO;
using System.Xml.Linq;

namespace BeatStore.API.UseCases.Tracks
{
    public class CreateTrackUseCase : ABaseUseCase
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IObjectStorageFactory _minioOS;
        public CreateTrackUseCase(ITrackRepository trackRepository, IObjectStorageFactory minioOS)
        {
            _trackRepository = trackRepository;
            _minioOS = minioOS;
        }

        public async Task Handle(Track track, MemoryStream coverFile = null, string coverExt = "")
        {
            try
            {
                track.Id = Guid.NewGuid().ToString();
                track.Slug = Slugify.Generate(track.Name);
                var response = await _trackRepository.Create(track);
                if (response.Success && coverFile != null)
                {
                    coverFile.Position = 0;
                    await _minioOS.AddCoverImage($"{track.Slug}{coverExt}", coverFile);
                    coverFile.Close();
                }
                OutputPort = response;
            }
            catch(Exception e)
            {
                OutputPort = new StandardResponse(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
