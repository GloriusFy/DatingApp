using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Messages;
using Application.Messages.Commands;
using Application.Users;
using Domain.Aggregates.Users;
using Domain.Aggregates.Users.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using NodaTime;
using UnitTests.UserEntity;
using Xunit;

namespace UnitTests.Application
{
    public class AddMessageCommandTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private readonly Mock<IClock> _clockMock = new Mock<IClock>();
        private readonly DbContextOptions<DatingAppDbContext> _options =
            new DbContextOptionsBuilder<DatingAppDbContext>()
                .UseInMemoryDatabase(nameof(AddMessageCommandTests))
                .Options;

        private readonly IRequestHandler<AddMessageCommand, MessageDto> _requestHandler;

        public AddMessageCommandTests()
        {
            _requestHandler = new AddMessageCommandHandler(
                _userRepositoryMock.Object,
                new DatingAppDbContext(_options),
                _clockMock.Object
            );
        }

        [Fact]
        public Task HandleAsync_UnknownRecipient_ThrowsResourceNotFoundException()
        {
            // Arrange
            var userMock = new Mock<IAuthenticatedUser>();

            // Act, Assert
            return Assert.ThrowsAsync<ResourceNotFoundException>(() =>
            {
                return _requestHandler.Handle(new AddMessageCommand(123, "hi", userMock.Object), default);
            });
        }

        [Fact]
        public async Task HandleAsync_HappyPath_MessageIsAdded()
        {
            // Arrange
            var user = UserGenerator.GenerateUser();

            await using (var dbContext = new DatingAppDbContext(_options))
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }

            var userMock = new Mock<IAuthenticatedUser>();
            userMock.SetupGet(u => u.Id).Returns(1);

            _userRepositoryMock
                .Setup(r => r.Single(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            var command = new AddMessageCommand(user.Id, "text", userMock.Object);

            var fakeNow = Instant.FromDateTimeUtc(DateTime.UtcNow.AddDays(-1));
            _clockMock.Setup(c => c.GetCurrentInstant()).Returns(fakeNow);

            // Act
            var messageDto = await _requestHandler.Handle(command, default);

            // Assert
            Assert.NotNull(messageDto);
            Assert.Equal(command.Content, messageDto.Content);
            Assert.Equal(messageDto.SendDate, fakeNow);
        }
    }
}
