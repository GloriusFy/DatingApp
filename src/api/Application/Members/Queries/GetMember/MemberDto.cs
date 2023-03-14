using System.Collections.Generic;
using Application.Members.Common;
using Domain;
using NodaTime;

namespace Application.Members.Queries.GetMember
{
    public class MemberDto
    {
        public MemberDto(int id, string email, LocalDate dateOfBirth, Gender gender, string name,
            string? lookingFor, string? briefDescription, string? city,
            string? country, List<PhotoDto> photos, Instant created, Instant lastActive, string? interests)
        {
            Id = id;
            Email = email;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Name = name;
            BriefDescription = briefDescription;
            City = city;
            Country = country;
            Photos = photos;
            Created = created;
            LastActive = lastActive;
            Interests = interests;
            LookingFor = lookingFor;
        }

        public int Id { get; private set; }

        public string Email { get; private set; }

        public LocalDate DateOfBirth { get; private set; }

        public Gender Gender { get; private set; }

        public string Name { get; private set; }

        public string? BriefDescription { get; private set; }

        public string? LookingFor { get; private set; }

        public string? Interests { get; private set; }

        public string? City { get; private set; }

        public string? Country { get; private set; }

        public List<PhotoDto> Photos { get; set; }

        public Instant Created { get; private set; }

        public Instant LastActive { get; private set; }
    }
}
