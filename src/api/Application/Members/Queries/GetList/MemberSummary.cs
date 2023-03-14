using Domain.Aggregates.Users.ValueObjects;

namespace Application.Members.Queries.GetList
{
    public class MemberSummary
    {
        public MemberSummary(int id, string name, string? city, Photo? photo)
        {
            Id = id;
            Name = name;
            City = city;
            MainPhotoUrl = photo?.Url;
        }

        public int Id { get; }

        public string Name { get; }

        public string? City { get; }

        public string? MainPhotoUrl { get; }
    }
}
