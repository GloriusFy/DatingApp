using System;
using Domain;
using Domain.Aggregates.Users.Entities;
using Domain.Aggregates.Users.ValueObjects;
using NodaTime;

namespace UnitTests.UserEntity
{
    public static class UserGenerator
    {
        public static User GenerateUser()
        {
            var password = new Password(Array.Empty<byte>(), Array.Empty<byte>());

            return new User("email", "name", password, new LocalDate(), Gender.Male, SystemClock.Instance.GetCurrentInstant());
        }
    }
}
