using Microsoft.AspNetCore.Mvc;
using OneTwenty.Services.Services;
using OneTwenty.Shared.Models;

namespace OneTwenty.WebApi.Controllers
{
    [Route("api/features")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IDuplicateUsersService _duplicateUsersService;
        private readonly IUserService _userService;

        public FeaturesController(IDuplicateUsersService duplicateUsersService, IUserService userService)
        {
            _duplicateUsersService = duplicateUsersService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> MergeDuplicateUsers(string email)
        {
            await _duplicateUsersService.MergeDuplicateByUserEmail(email);
            return Ok();
        }

        [HttpGet]
        public async Task<List<UserModel>> GetUsersFromLastMonth(string interest)
        {
            return await _userService.GetUsersForLastMonth(interest);
        }
    }
}
