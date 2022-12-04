using BeatStore.API.Interfaces.DTO.Responses;
using System.Net;

namespace BeatStore.API.DTO.Responses
{
    public class StandardResponse : AJsonResponse<string>
    {
        public StandardResponse(IEnumerable<string> errors, HttpStatusCode errCode = HttpStatusCode.NotFound) : base(errors, errCode) { }
        public StandardResponse(string errMsg, HttpStatusCode errCode = HttpStatusCode.NotFound) : base(errMsg, errCode) { }
        public StandardResponse(string data) : base(HttpStatusCode.OK)
        {
            SetData(data);
        }
    }
}
