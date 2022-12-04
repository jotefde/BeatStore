using Microsoft.Extensions.Options;
using BeatStore.API.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Minio;
using BeatStore.API.DTO;
using System.Diagnostics;

namespace BeatStore.API.Factories
{
    public class MinioObjectStorageFactory : IObjectStorageFactory
    {
        private readonly MinioClient _minioClient;
        private readonly ObjectStorageOptions _minioConnectionOptions;

        [Obsolete]
        public MinioObjectStorageFactory(IOptions<ObjectStorageOptions> options)
        {
            _minioConnectionOptions = options.Value;
            _minioClient = new MinioClient()
                .WithEndpoint(_minioConnectionOptions.ConnectionString)
                .WithCredentials(_minioConnectionOptions.AccessKey, _minioConnectionOptions.SecretKey)
                .Build();

            /*var policyString = @"{
              ""Version"": ""2012-10-17"",
              ""Statement"": [
                {
                  ""Sid"": ""Set entirely public"",
                  ""Effect"": ""Allow"",
                  ""Principal"": ""*"",
                  ""Action"": ""s3:GetObject"",
                  ""Resource"": ""arn:aws:s3:::{covers}*""
                }
              ]
            }";
            _minioClient.SetPolicyAsync("covers", policyString.Trim());*/
        }

        public MinioClient GetClient()
        {
            return _minioClient;
        }

        [Obsolete]
        public async Task<string> AddCoverImage(string fileName, Stream fileStream)
        {
            await _minioClient.PutObjectAsync("covers", fileName, fileStream, fileStream.Length, "application/octet-stream");
            return fileName;
        }

        [Obsolete]
        public async Task<bool> DeleteCoverImage(string objectName)
        {
            await _minioClient.RemoveObjectAsync("covers", objectName);
            return true;
        }

        [Obsolete]
        public async Task<string> GetCoverImage(string objectName)
        {
            var memStream = new MemoryStream();
            await _minioClient.GetObjectAsync("covers", objectName, (stream) => { 
                stream.CopyTo(memStream);
                memStream.Flush();
                memStream.Position = 0;
                stream.Close();
            });
            var readStream = new StreamReader(memStream);
            var content = await readStream.ReadToEndAsync();
            memStream.Close();
            readStream.Close();
            return content;
        }

        [Obsolete]
        public async Task<bool> AddTrackObject(string trackId, string fileName, Stream fileStream)
        {
            try
            {
                fileStream.Position = 0;
                await _minioClient.PutObjectAsync("tracks", $"{trackId}/{fileName}", fileStream, fileStream.Length, "application/octet-stream");
                return true;
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return false; 
            }
        }
    }
}
