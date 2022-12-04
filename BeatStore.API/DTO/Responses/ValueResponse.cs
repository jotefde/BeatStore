using BeatStore.API.Interfaces.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BeatStore.API.DTO.Responses
{
    public class ValueResponse<T> : AJsonResponse<T> where T : class
    {
        public ValueResponse(IEnumerable<string> errors, HttpStatusCode errCode = HttpStatusCode.NotFound) : base(errors, errCode) { }
        public ValueResponse(string errMsg, HttpStatusCode errCode = HttpStatusCode.NotFound) : base(errMsg, errCode) { }
        public ValueResponse(T data) : base(HttpStatusCode.OK)
        {
            SetData(data);
        }
    }
}
