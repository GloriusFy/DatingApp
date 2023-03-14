using System;
using System.Security.Claims;
using Application.Users;
using Application.Users.Registration.Models;

namespace WebApi.CrossCutting.Auth
{
    public class AuthenticatedUser : IAuthenticatedUser
    {
        public AuthenticatedUser(ClaimsPrincipal user)
        {
            Id = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
            Gender = Enum.Parse<GenderDto>(user.FindFirstValue(ClaimTypes.Gender));
        }

        public int Id { get; set; }

        public GenderDto Gender { get; set; }
    }
}
