namespace Infrastructure.ObjectStorage
{
    public class ObjectStorageSettings
    {
        public string ServiceUrl { get; set; } = null!;

        public string PublicUrl { get; set; } = null!;

        public string AccessKey { get; set; } = null!;

        public string SecretKey { get; set; } = null!;

        public bool ForcePathStyle { get; set; }
    }
}
