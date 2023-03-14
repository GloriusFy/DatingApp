using System.Threading;
using System.Threading.Tasks;
using Application.Users;
using Domain.Aggregates.Users;
using MediatR;

namespace Application.Members.Commands
{
    public class UpdateMemberInfoCommand : IRequest
    {
        public UpdateMemberInfoCommand(IAuthenticatedUser authenticatedUser, string? briefDescription, string? lookingFor,
            string? interests, string? city, string? country)
        {
            AuthenticatedUser = authenticatedUser;
            BriefDescription = briefDescription;
            LookingFor = lookingFor;
            Interests = interests;
            City = city;
            Country = country;
        }

        public IAuthenticatedUser AuthenticatedUser { get; }

        public string? BriefDescription { get; }

        public string? LookingFor { get; }

        public string? Interests { get; }

        public string? City { get; }

        public string? Country { get; }
    }

    internal class UpdateMemberInfoCommandHandler : AsyncRequestHandler<UpdateMemberInfoCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateMemberInfoCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected override async Task Handle(UpdateMemberInfoCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Single(u => u.Id == request.AuthenticatedUser.Id, cancellationToken);

            user.UpdateInfo(request.BriefDescription, request.LookingFor, request.Interests, request.City, request.Country);
        }
    }
}
