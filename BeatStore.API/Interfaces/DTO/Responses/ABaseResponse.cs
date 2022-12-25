using BeatStore.API.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BeatStore.API.Interfaces.DTO.Responses
{
    public abstract class ABaseResponse<T> : IBaseResponse
    {
        public T? Data { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public bool Success { get; set; } = false;

        public abstract ActionResult GetResult();
        public abstract ABaseResponse<T> SetData(T data);
    }
}
