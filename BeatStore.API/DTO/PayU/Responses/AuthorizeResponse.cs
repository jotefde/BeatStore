﻿namespace BeatStore.API.DTO.PayU.Responses
{
    public class AuthorizeResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string grant_type { get; set; }
    }
}
