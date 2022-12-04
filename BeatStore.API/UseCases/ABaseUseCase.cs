using BeatStore.API.DTO.Responses;
using BeatStore.API.Interfaces.DTO.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BeatStore.API.UseCases
{
    public abstract class ABaseUseCase
    {
        public IBaseResponse OutputPort { get; set; }
            = new StandardResponse("Empty OutputPort", System.Net.HttpStatusCode.InternalServerError);
    }
}
