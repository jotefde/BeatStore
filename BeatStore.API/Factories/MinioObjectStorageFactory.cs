using Microsoft.Extensions.Options;
using BeatStore.API.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Minio;
using BeatStore.API.DTO;

namespace BeatStore.API.Factories
{
    public class MinioObjectStorageFactory : IObjectStorageFactory
    {
        private readonly MinioClient _minioClient;
        private readonly ObjectStorageOptions _minioConnectionOptions;
        public MinioObjectStorageFactory(IOptions<ObjectStorageOptions> options)
        {
            _minioConnectionOptions = options.Value;
            _minioClient = new MinioClient(
                _minioConnectionOptions.ConnectionString,
                _minioConnectionOptions.AccessKey,
                _minioConnectionOptions.SecretKey);

            var policyString = @"{
              ""Version"": ""2012-10-17"",
              ""Statement"": [
                {
                  ""Sid"": ""Set entirely public"",
                  ""Effect"": ""Allow"",
                  ""Principal"": ""*"",
                  ""Action"": ""s3:GetObject"",
                  ""Resource"": ""arn:aws:s3:::{PUBLIC_BUCKET_NAME}*""
                }
              ]
            }";
            var policy = policyString.Replace("PUBLIC_BUCKET_NAME", _minioConnectionOptions.PublicBucketName);
            _minioClient.SetPolicyAsync(_minioConnectionOptions.BucketName, policy.Trim());
        }

        public async Task<string> AddObject(string id, string fileName, Stream fileStream)
        {
            var objectName = string.Format("{0}_{1}", id, fileName);

            await _minioClient.PutObjectAsync(_minioConnectionOptions.BucketName, objectName, fileStream, fileStream.Length, "application/octet-stream");

            return objectName;
        }

        public async Task<string> AddPublicObject(string id, string fileName, Stream fileStream)
        {
            var objectName = string.Format(_minioConnectionOptions.PublicObjectPrefix + "{0}-{1}", id, fileName);

            await _minioClient.PutObjectAsync(_minioConnectionOptions.BucketName, objectName, fileStream, fileStream.Length, "application/octet-stream");

            return objectName;
        }

        public async Task<bool> DeleteObject(string objectName)
        {
            await _minioClient.RemoveObjectAsync(_minioConnectionOptions.BucketName, objectName);

            return true;
        }

        public async Task<string> GetObject(string objectName)
        {
            var memStream = new MemoryStream();
            await _minioClient.GetObjectAsync(_minioConnectionOptions.BucketName, objectName, (stream) => { 
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
    }
}
