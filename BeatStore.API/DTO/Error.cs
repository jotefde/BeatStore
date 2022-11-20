using System;
using System.Collections.Generic;
using System.Text;

namespace BeatStore.API.DTO
{
    public sealed class Error
    {
        public int Code { get; }
        public string Description { get; }

        public Error(int code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
