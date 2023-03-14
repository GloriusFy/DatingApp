using System.IO;

namespace Application.Common.Persistence.Photos
{
    public class AddPhotoRequest
    {
        public AddPhotoRequest(string photoName, Stream content, string contentType)
        {
            PhotoName = photoName;
            Content = content;
            ContentType = contentType;
        }

        public string PhotoName { get; }

        public Stream Content { get; }

        public string ContentType { get; }

    }
}
