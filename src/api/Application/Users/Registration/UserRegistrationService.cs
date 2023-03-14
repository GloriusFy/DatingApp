using System.Threading.Tasks;
using Application.Common.Cryptography;
using Application.Common.Identity;
using Application.Common.Persistence;
using Application.Users.Registration.Models;
using Domain;
using Domain.Aggregates.Users.Entities;
using Domain.Aggregates.Users.ValueObjects;
using MapsterMapper;
using NodaTime;

namespace Application.Users.Registration
{
    internal class UserRegistrationService : IUserRegistrationService
    {
        private readonly IDatingAppDbContext _dbContext;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IClock _clock;
        private readonly IMapper _mapper;

        public UserRegistrationService(IDatingAppDbContext dbContext,
            ITokenService tokenService,
            IPasswordHashService passwordHashService,
            IClock clock,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
            _passwordHashService = passwordHashService;
            _clock = clock;
            _mapper = mapper;
        }

        public async Task<UserRegistrationResponse> Register(UserRegistrationRequest request)
        {
            var (passwordHash, passwordSalt) = _passwordHashService.Generate(request.Password);

            var password = new Password(passwordHash, passwordSalt);

            var user = new User(
                request.Email,
                request.Name,
                password,
                request.DateOfBirth,
                _mapper.Map<GenderDto, Gender>(request.Gender),
                _clock.GetCurrentInstant()
            );

            _dbContext.Users.Add(user);

            await _dbContext.SaveChangesAsync();

            var registeredUserDto = new RegisteredUserDto(user.Id);

            return new UserRegistrationResponse(registeredUserDto, _tokenService.Generate(user));
        }
    }
}
