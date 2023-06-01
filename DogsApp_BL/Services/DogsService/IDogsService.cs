using DogsApp_DAL.Entities;
using DogsApp_DAL.Models;

namespace DogsApp_BL.Services.DogsService
{
    public interface IDogsService
    {
        Task<Dog> CreateDogAsync(Dog dog);
        Task<IEnumerable<Dog>> GetAllDogsAsync(QueryStringParameters queryParameters);
        string GetDogsAppInfo();
    }
}