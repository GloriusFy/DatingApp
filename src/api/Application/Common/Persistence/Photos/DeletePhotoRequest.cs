namespace Application.Common.Persistence.Photos
{
    public class DeletePhotoRequest
    {
        public DeletePhotoRequest(string photoName)
        {
            PhotoName = photoName;
        }

        public string PhotoName { get; }
    }
}
