using Minio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BeatStore.API.Interfaces.Factories
{
    public interface IObjectStorageFactory
    {
        public MinioClient GetClient();
        public Task<string> AddCoverImage(string fileName, Stream fileStream);
        public Task<bool> DeleteCoverImage(string objectName);
        public Task<string> GetCoverImage(string objectName);
    }
}
