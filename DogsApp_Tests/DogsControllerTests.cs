using AutoFixture;
using DogsApp_Api.Controllers;
using DogsApp_BL.Services.DogsService;
using DogsApp_DAL.Entities;
using DogsApp_DAL.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DogsApp_Tests
{
    public class DogsControllerTests
    {
        private Fixture _fixture;
        private Mock<IDogsService> _dogsService;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _dogsService = new Mock<IDogsService>();
        }

        public DogsController GetDogsController()
        {
            return new DogsController(
                _dogsService.Object
                );
        }

        [Test]
        public void GetDogsAppInfo_ReturnsOkWithResult()
        {
            var expectedInfo = _fixture.Create<string>();

            _dogsService.Setup(x => x.GetDogsAppInfo()).Returns(expectedInfo);

            DogsController dogsController = GetDogsController();
            var result = dogsController.GetDogsAppInfo();

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().Be(expectedInfo);
        }

        [Test]
        public async Task GetAllDogsAsync_ReturnsOkWithResult()
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

            _dogsService.Setup(x => x.GetAllDogsAsync(queryParameters)).ReturnsAsync(dogs);

            DogsController dogsController = GetDogsController();
            var result = await dogsController.GetAllDogsAsync(queryParameters);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().Be(dogs);
        }

        [Test]
        public async Task CreateDogAsync_WithValidDog_ReturnsOkWithResult()
        {
            Dog expectedDog = new Dog
            {
                Id = _fixture.Create<Guid>(),
                Name = "Neo",
                Color = "red & amber",
                TailLength = 22,
                Weight = 32
            };

            Dog dog = new Dog
            {
                Id = _fixture.Create<Guid>(),
                Name = "Neo",
                Color = "red & amber",
                TailLength = 22,
                Weight = 32
            };

            _dogsService.Setup(x => x.CreateDogAsync(dog)).ReturnsAsync(expectedDog);

            DogsController dogsController = GetDogsController();
            var result = await dogsController.CreateDogAsync(dog);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().Be(expectedDog);
        }

        [Test]
        public async Task CreateDogAsync_WithException_ReturnsBadRequestWithErrorMessage()
        {
            var dog = new Dog
            {
                Id = _fixture.Create<Guid>(),
                Name = "Neo",
                Color = "red & amber",
                TailLength = 22,
                Weight = 32
            };

            var expectedErrorMessage = "Error message";
            _dogsService.Setup(x => x.CreateDogAsync(dog)).Throws(new Exception(expectedErrorMessage));

            DogsController dogsController = GetDogsController();
            var result = await dogsController.CreateDogAsync(dog);

            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult!.Value.Should().Be(expectedErrorMessage);
        }
    }
}
