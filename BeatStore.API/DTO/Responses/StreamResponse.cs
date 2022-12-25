using BeatStore.API.Entities;
using BeatStore.API.Interfaces.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using NJsonSchema.Infrastructure;
using System.Net;

namespace BeatStore.API.DTO.Responses
{
    public class StreamResponse : ABaseResponse<Stream>
    {
        private string _contentType;
        private string _fileName;
        public StreamResponse(IEnumerable<string> errors, HttpStatusCode errCode)
        {
            StatusCode = errCode;
            Errors = new List<string>(errors);
            Success = false;
        }
        public StreamResponse(string errMsg, HttpStatusCode errCode)
        {
            StatusCode = errCode;
            Errors = new List<string>() { errMsg };
            Success = false;
        }

        public StreamResponse(Stream data, string fileName, string contentType)
        {
            StatusCode = HttpStatusCode.OK;
            Success = true;
            _contentType = contentType;
            _fileName = fileName;
            SetData(data);
        }

        public override ABaseResponse<Stream> SetData(Stream data)
        {
            Data = data;
            if (Data == null)
                Success = false;
            return this;
        }

        public override ActionResult GetResult()
        {
            var result = new FileStreamResult(Data, _contentType);
            result.FileDownloadName = _fileName;
            result.EnableRangeProcessing = true;
            return result;
        }
    }
}
