using BeatStore.API.Interfaces.DTO.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BeatStore.API.DTO.Responses
{
    public class ListResponse<T> : AJsonResponse
    {
        public ListResponse(string errMsg, int errCode = 404) : base(errMsg, errCode) { }
        public ListResponse(IEnumerable<T> data) : base(200)
        {
            if(data == null)
                data = new List<T>();
            SetContent(data);
        }
    }
}
