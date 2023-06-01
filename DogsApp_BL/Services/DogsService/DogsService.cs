using DogsApp_DAL.Entities;
using DogsApp_DAL.GenericRepository;
using DogsApp_Core.Extensions;
using DogsApp_Core.Exceptions;
using Microsoft.Extensions.Logging;
using DogsApp_DAL.Models;
using DogsApp_Core;
using System.Reflection;

namespace DogsApp_BL.Services.DogsService
{
    public class DogsService : IDogsService
    {
        private readonly IGenericRepository<Dog> _dogsRepository;
        private readonly ILogger<DogsService> _logger;

        public DogsService(IGenericRepository<Dog> dogsRepository, ILogger<DogsService> logger)
        {
            _dogsRepository = dogsRepository;
            _logger = logger;
        }

        public string GetDogsAppInfo()
        {
            string version = Assembly.GetEntryAssembly()!.GetName().Version!.ToString();
            var result = $"{AppConstants.AppInfo} {version}";
            return result;
        }

        public async Task<IEnumerable<Dog>> GetAllDogsAsync(QueryStringParameters queryParameters) =>
            await _dogsRepository.GetAllAsync(queryParameters);

        public async Task<Dog> CreateDogAsync(Dog dog)
        {
            var dogWithSameName = _dogsRepository.GetByPredicate(d => d.Name == dog.Name).FirstOrDefault();

            if (dogWithSameName is not null)
            {
                _logger.LogMessageAndThrowException(AppConstants.ErrorMessages.LoggerWrongModelErrorMessage, new WrongModelException($"Dog with the name {dog.Name} already exists"));
                return null!;
            }

            dog.Id = Guid.NewGuid();

            try
            {
                await _dogsRepository.CreateAsync(dog);
            }
            catch
            {
                _logger.LogMessageAndThrowException(AppConstants.ErrorMessages.LoggerWrongModelErrorMessage, new WrongModelException(AppConstants.ErrorMessages.WrongModelErrorMessage));
            }
            
            return dog;
        }
    }
}
