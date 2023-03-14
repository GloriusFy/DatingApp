namespace Application.Common.Persistence.Photos
{
    public class AddPhotoResponse
    {
        public AddPhotoResponse(string url)
        {
            Url = url;
        }

        public string Url { get; }
    }
}
