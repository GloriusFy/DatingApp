using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Users;
using Domain.Aggregates.Users;
using MediatR;

namespace Application.Members.Commands.Photos
{
    public class SetPhotoAsMainCommand : IRequest
    {
        public SetPhotoAsMainCommand(IAuthenticatedUser authenticatedUser, string photoName)
        {
            AuthenticatedUser = authenticatedUser;
            PhotoName = photoName;
        }

        public IAuthenticatedUser AuthenticatedUser { get; }

        public string PhotoName { get; }
    }

    public class SetPhotoAsMainCommandHandler : AsyncRequestHandler<SetPhotoAsMainCommand>
    {
        private readonly IUserRepository _userRepository;

        public SetPhotoAsMainCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected override async Task Handle(SetPhotoAsMainCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Single(u => u.Id == request.AuthenticatedUser.Id, cancellationToken);

            user.SetPhotoAsMain(request.PhotoName);
        }
    }
}
