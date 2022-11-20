using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BeatStore.API.Interfaces.Services
{
    public interface IObjectStorageFactory
    {
        Task<string> AddPublicObject(string id, string fileName, Stream fileStream);
        Task<string> AddObject(string id, string fileName, Stream fileStream);
        Task<bool> DeleteObject(string objectName);
        Task<string> GetObject(string objectName);
    }
}
