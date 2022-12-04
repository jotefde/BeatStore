using BeatStore.API.Interfaces.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BeatStore.API.DTO.Responses
{
    public class ListResponse<T> : AJsonResponse<IEnumerable<T>> where T : class
    {
        public ListResponse(IEnumerable<string> errors, HttpStatusCode errCode = HttpStatusCode.NotFound) : base(errors, errCode) { }
        public ListResponse(string errMsg, HttpStatusCode errCode = HttpStatusCode.NotFound) : base(errMsg, errCode) { }
        public ListResponse(IEnumerable<T> data) : base(HttpStatusCode.OK)
        {
            if(data == null)
                data = new List<T>();
            SetData(data);
        }
    }
}
