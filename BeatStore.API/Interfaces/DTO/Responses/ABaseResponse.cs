using Microsoft.AspNetCore.Mvc;

namespace BeatStore.API.Interfaces.DTO.Responses
{
    public abstract class ABaseResponse
    {
        public ContentResult? Data { get; set; }
    }
}
