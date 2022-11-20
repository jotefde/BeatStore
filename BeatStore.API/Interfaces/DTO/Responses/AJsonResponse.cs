using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NJsonSchema.Infrastructure;

namespace BeatStore.API.Interfaces.DTO.Responses
{
    public abstract class AJsonResponse : ABaseResponse
    {
        public AJsonResponse(string errMsg, int errCode = 404)
        {
            Data = new ContentResult();
            SetContent(errMsg);
            Data.StatusCode = errCode;
        }

        public AJsonResponse(int code = 200)
        {
            Data = new ContentResult();
            Data.StatusCode = code;
        }

        public void SetContent(Object data)
        {
            Data.ContentType = "application/json";
            var jsonResolver = new PropertyRenameAndIgnoreSerializerContractResolver();
            //jsonResolver.IgnoreProperty(typeof(User), "PasswordHash");

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = jsonResolver;
            Data.Content = JsonConvert.SerializeObject(data, serializerSettings);
        }
    }
}
