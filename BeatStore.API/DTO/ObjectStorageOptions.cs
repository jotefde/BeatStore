namespace BeatStore.API.DTO
{
    public class ObjectStorageOptions
    {
        public string ConnectionString { get; set; }
        /*public string PublicBucketName { get; set; }
        public string BucketName { get; set; }
        public string PublicObjectPrefix { get; set; }*/
        public string ObjectStorageBaseUrl { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}