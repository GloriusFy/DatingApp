using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Persistence.Photos;
using Application.Members.Common;
using Application.Users;
using Domain.Aggregates.Users;
using Domain.Aggregates.Users.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Members.Commands.Photos
{
    public class AddPhotoCommand : IRequest<PhotoDto>
    {
        public AddPhotoCommand(IAuthenticatedUser authenticatedUser, IFormFile photo)
        {
            AuthenticatedUser = authenticatedUser;
            Photo = photo;
        }

        public IAuthenticatedUser AuthenticatedUser { get; }

        public IFormFile Photo { get; }
    }

    public class AddPhotoCommandHandler : IRequestHandler<AddPhotoCommand, PhotoDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPhotoStorage _photoStorage;
        private readonly IMapper _mapper;

        public AddPhotoCommandHandler(
            IUserRepository userRepository,
            IPhotoStorage photoStorage,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _photoStorage = photoStorage;
            _mapper = mapper;
        }

        public async Task<PhotoDto> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Single(
                u => u.Id == request.AuthenticatedUser.Id,
                cancellationToken
            );

            var newPhotoName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(request.Photo.FileName)}";
            var addPhotoRequest = new AddPhotoRequest(newPhotoName, request.Photo.OpenReadStream(), request.Photo.ContentType);
            var addPhotoResponse = await _photoStorage.Add(addPhotoRequest, cancellationToken);

            var photo = new Photo(newPhotoName, addPhotoResponse.Url);
            user.AddPhoto(photo);

            return _mapper.Map<Photo, PhotoDto>(photo);
        }
    }
}
