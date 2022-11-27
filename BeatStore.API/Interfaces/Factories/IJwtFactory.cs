using BeatStore.API.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeatStore.API.Interfaces.Factories
{
    public interface IJwtFactory
    {
        Task<Token> GenerateEncodedToken(string id, string email, IEnumerable<string> roles);
    }
}
