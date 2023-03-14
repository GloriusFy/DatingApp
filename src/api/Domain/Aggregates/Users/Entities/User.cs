using System.Collections.Generic;
using System.Linq;
using Domain.Aggregates.Users.ValueObjects;
using Domain.Common;
using NodaTime;

namespace Domain.Aggregates.Users.Entities
{
    public class User : IAggregateRoot
    {
        private readonly List<Photo> _photos;
        private readonly List<UserLike> _userLikes;
        private readonly List<Message> _sentMessages;
        private readonly List<Message> _receivedMessages;

        private User(int id, string email, LocalDate dateOfBirth, Gender gender, string name,
            string briefDescription, string? lookingFor, string city, string country, Instant created,
            Instant lastActive, string? interests)
        {
            Id = id;
            Email = email;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Name = name;
            BriefDescription = briefDescription;
            LookingFor = lookingFor;
            Interests = interests;
            City = city;
            Country = country;
            Created = created;
            LastActive = lastActive;

            _photos = new List<Photo>();
            _userLikes = new List<UserLike>();
            _sentMessages = new List<Message>();
            _receivedMessages = new List<Message>();
        }

        public User(string email, string name, Password password, LocalDate dateOfBirth, Gender gender, Instant now)
        {
            Email = email;
            Name = name;
            Password = password;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Created = now;
            LastActive = now;

            _photos = new List<Photo>();
            _userLikes = new List<UserLike>();
            _sentMessages = new List<Message>();
            _receivedMessages = new List<Message>();
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

        public IReadOnlyCollection<Photo> Photos => _photos;

        public IReadOnlyCollection<UserLike> UserLikes => _userLikes;

        public IReadOnlyCollection<Message> SentMessages => _sentMessages;

        public IReadOnlyCollection<Message> ReceivedMessages => _receivedMessages;

        public Instant Created { get; private set; }

        public Instant LastActive { get; private set; }

        public Password Password { get; private set; } = null!;

        public void UpdateInfo(string? briefDescription, string? lookingFor, string? interests, string? city, string? country)
        {
            BriefDescription = briefDescription;
            LookingFor = lookingFor;
            Interests = interests;
            City = city;
            Country = country;
        }

        public void AddPhoto(Photo photo)
        {
            if (_photos.Count == 0)
            {
                photo.MakeMain();
            }

            _photos.Add(photo);
        }

        public void SetPhotoAsMain(string photoName)
        {
            var photo = _photos.FirstOrDefault(p => p.Name == photoName);

            if (photo == null)
            {
                throw new DomainException("Unknown photo");
            }

            _photos.FirstOrDefault(p => p.IsMain)?.MakeOrdinary();

            photo.MakeMain();
        }

        public Photo? GetMainPhoto()
        {
            return _photos.SingleOrDefault(p => p.IsMain);
        }

        public void DeletePhoto(string photoName)
        {
            var photo = _photos.SingleOrDefault(p => p.Name == photoName);

            if (photo == null)
            {
                throw new DomainException("Unknown photo");
            }

            if (photo.IsMain)
            {
                throw new DomainException("It's impossible to delete main photo");
            }

            _photos.Remove(photo);
        }

        public void Active(Instant activityDate)
        {
            LastActive = activityDate;
        }

        public void Like(int targetUserId)
        {
            if (_userLikes.Any(l => l.TargetUserId == targetUserId))
            {
                return;
            }

            _userLikes.Add(new UserLike(Id, targetUserId));
        }

        public Message SendMessage(int recipientId, string content, Instant now)
        {
            var message = new Message(Id, recipientId, content, now);

            _sentMessages.Add(message);

            return message;
        }
    }
}
