using Minio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BeatStore.API.Interfaces.Services
{
    public interface IObjectStorageService
    {
        public MinioClient GetClient();
        public Task<string> AddCoverImage(string fileName, Stream fileStream);
        public Task<bool> DeleteCoverImage(string objectName);
        public Task<string> GetCoverImage(string objectName);
        public Task<bool> AddTrackObject(string trackId, string fileName, Stream fileStream);
        public Task<Stream> GetTrackObject(string trackId, string fileName);
    }
}
