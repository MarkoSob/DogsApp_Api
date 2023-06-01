using DogsApp_BL.Services.DogsService;
using DogsApp_DAL.Entities;
using DogsApp_DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DogsApp_Api.Controllers
{
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IDogsService _dogsService;

        public DogsController(IDogsService dogsService)
        {
            _dogsService = dogsService;
        }

        [HttpGet("ping")]
        public IActionResult GetDogsAppInfo()
        {
            string result = _dogsService.GetDogsAppInfo();
            return Ok(result);
        }

        [HttpGet("dogs")]
        public async Task<IActionResult> GetAllDogsAsync([FromQuery] QueryStringParameters queryParameters)
        {
            var result = await _dogsService.GetAllDogsAsync(queryParameters);
            return Ok(result);
        }

        [HttpPost("dog")]
        public async Task<IActionResult> CreateDogAsync([FromBody] Dog dog)
        {
            Dog result = null!;

            try
            {
                result = await _dogsService.CreateDogAsync(dog);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok(result);
        }
    }
}
