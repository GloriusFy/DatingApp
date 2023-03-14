namespace Domain.Aggregates.Users.ValueObjects
{
    public class Photo
    {
        public Photo(string name, string url, bool isMain = false)
        {
            Name = name;
            Url = url;
            IsMain = isMain;
        }

        public string Name { get; private set; }

        public string Url { get; private set; }

        public bool IsMain { get; private set; }

        public void MakeMain()
        {
            IsMain = true;
        }

        public void MakeOrdinary()
        {
            IsMain = false;
        }
    }
}
