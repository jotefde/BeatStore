using BeatStore.API.DTO;
using BeatStore.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NJsonSchema.Infrastructure;
using System.Net;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace BeatStore.API.Interfaces.DTO.Responses
{
    public abstract class AJsonResponse<T> : ABaseResponse<T>
    {
        public AJsonResponse(IEnumerable<string> errors, HttpStatusCode errCode)
        {
            StatusCode = errCode;
            Errors = new List<string>(errors);
            Success = false;
        }
        public AJsonResponse(string errMsg, HttpStatusCode errCode)
        {
            StatusCode = errCode;
            Errors = new List<string>() { errMsg };
            Success = false;
        }

        public AJsonResponse(HttpStatusCode code)
        {
            StatusCode = code;
            Success = true;
        }

        public override ABaseResponse<T> SetData(T data)
        {
            Data = data;
            return this;
        }

        public override ContentResult GetResult()
        {
            var result = new ContentResult();
            result.StatusCode = (int)StatusCode;
            result.ContentType = "application/json";
            var jsonResolver = new PropertyRenameAndIgnoreSerializerContractResolver();
            jsonResolver.IgnoreProperty(typeof(AppUser), "PasswordHash");
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = jsonResolver;

            if(Success)
                result.Content = JsonConvert.SerializeObject(Data, serializerSettings);
            else
                result.Content = JsonConvert.SerializeObject(new { errors = Errors });
            return result;
        }
    }
}
