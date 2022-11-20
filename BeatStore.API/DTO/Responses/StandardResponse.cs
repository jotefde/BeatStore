using BeatStore.API.Interfaces.DTO.Responses;

namespace BeatStore.API.DTO.Responses
{
    public class StandardResponse : AJsonResponse
    {
        public StandardResponse(string errMsg, int errCode = 404) : base(errMsg, errCode) { }
        public StandardResponse(string data) : base(200)
        {
            SetContent(data);
        }
    }
}
