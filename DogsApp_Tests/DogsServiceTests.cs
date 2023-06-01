using AutoFixture;
using DogsApp_BL.Services.DogsService;
using DogsApp_Core.Exceptions;
using DogsApp_DAL.Entities;
using DogsApp_DAL.GenericRepository;
using DogsApp_DAL.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace DogsApp_Tests
{
    public class DogsServiceTests
    {
        private Fixture _fixture;
        private Mock<IGenericRepository<Dog>> _dogsRepository;
        private Mock<ILogger<DogsService>> _logger;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _dogsRepository = new Mock<IGenericRepository<Dog>>();
            _logger = new Mock<ILogger<DogsService>>();
        }

        public DogsService GetDogsService()
        {
            return new DogsService(
                _dogsRepository.Object,
                _logger.Object
                );
        }

        [Test]
        public async Task GetAllDogsAsync_WithValidParameters_ShouldReturnDogs()
        {
            var queryParameters = _fixture.Create<QueryStringParameters>();

            IEnumerable<Dog> dogs = new List<Dog>
            {
                new Dog
                {
                    Id = _fixture.Create<Guid>(),
                    Name = "Neo",
                    Color = "red & amber",
                    TailLength = 22,
                    Weight = 32

                },
               new Dog
               {
                   Id = _fixture.Create<Guid>(),
                   Name = "Jessy",
                   Color = "black & white",
                   TailLength = 7,
                   Weight = 14
               }
            };

            _dogsRepository.Setup(x => x.GetAllAsync(It.IsAny<QueryStringParameters>())).ReturnsAsync(dogs).Verifiable();
            var dogsService = GetDogsService();
            var result = await dogsService.GetAllDogsAsync(queryParameters);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(dogs);
        }

        [Test]
        public async Task CreateDogAsync_WhenNoDogWithSameNameExists_ShouldCreateAndReturnDog()
        {
            var dog = new Dog
            {
                Id = _fixture.Create<Guid>(),
                Name = "Neo",
                Color = "red & amber",
                TailLength = 22,
                Weight = 32
            };
   
            _dogsRepository.Setup(repo => repo.GetByPredicate(It.IsAny<Expression<Func<Dog, bool>>>()))
                .Returns(Enumerable.Empty<Dog>().AsQueryable()).Verifiable();

            var dogsService = GetDogsService();
            var result = await dogsService.CreateDogAsync(dog);
            
            result.Should().BeEquivalentTo(dog);
            _dogsRepository.Verify(x => x.CreateAsync(dog), Times.Once);
        }

        [Test]
        public async Task CreateDogAsync_WhenDogWithSameNameExists_ShouldThrowException()
        {
            Dog expectedDog = new Dog
            {
                Id = _fixture.Create<Guid>(),
                Name = "Neo",
                Color = "red & amber",
                TailLength = 22,
                Weight = 32
            };

            Dog existingDog = new Dog
            {
                Id = _fixture.Create<Guid>(),
                Name = "Neo",
                Color = "red & amber",
                TailLength = 22,
                Weight = 32
            };

            _dogsRepository.Setup(repo => repo.GetByPredicate(It.IsAny<Expression<Func<Dog, bool>>>()))
                .Returns(new[] { existingDog }.AsQueryable()).Verifiable();

            var dogsService = GetDogsService();
            Func<Task> act = async () => await dogsService.CreateDogAsync(expectedDog);

            await act.Should().ThrowAsync<WrongModelException>()
                .WithMessage($"Dog with the name {expectedDog.Name} already exists\nError code: 400");

            _dogsRepository.Verify(repo => repo.CreateAsync(It.IsAny<Dog>()), Times.Never);
        }


        [Test]
        public async Task CreateDogAsync_WhenRepositoryThrowsException_ShouldThrowExceptionAndLogError()
        {
            var dog = new Dog
            {
                Id = _fixture.Create<Guid>(),
                Name = "Neo",
                Color = "red & amber",
                TailLength = 22,
                Weight = 32
            };

            _dogsRepository.Setup(repo => repo.GetByPredicate(It.IsAny<Expression<Func<Dog, bool>>>()))
                .Returns(Enumerable.Empty<Dog>().AsQueryable()).Verifiable();
            _dogsRepository.Setup(repo => repo.CreateAsync(dog)).Throws<Exception>();

            var dogsService = GetDogsService();
            Func<Task> act = async () => await dogsService.CreateDogAsync(dog);
            
            await act.Should().ThrowAsync<WrongModelException>().WithMessage("You entered wrong model\nError code: 400");
        }
    }
}