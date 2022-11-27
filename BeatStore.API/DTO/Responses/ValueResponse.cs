using BeatStore.API.Interfaces.DTO.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BeatStore.API.DTO.Responses
{
    public class ValueResponse<T> : AJsonResponse
    {
        public ValueResponse(string errMsg, int errCode = 404) : base(errMsg, errCode) { }
        public ValueResponse(T data) : base(200)
        {
            if (data == null)
                data = (T)new Object();
            SetContent(data);
        }
    }
}
