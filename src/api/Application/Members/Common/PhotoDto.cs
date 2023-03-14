namespace Application.Members.Common
{
    public class PhotoDto
    {
        public PhotoDto(string url, bool isMain, string name)
        {
            Url = url;
            IsMain = isMain;
            Name = name;
        }

        public string Name { get; private set; }

        public string Url { get; private set; }

        public bool IsMain { get; private set; }
    }
}
