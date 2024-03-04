using Microsoft.AspNetCore.Mvc;
using OneTwenty.Services.Services;
using OneTwenty.Services.Services.User;
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
        [Route("merge-duplicates")]
        public async Task<IActionResult> MergeDuplicateUsers(string email)
        {
            await _duplicateUsersService.MergeDuplicateByUserEmail(email);
            return Ok();
        }

        [HttpGet]
        [Route("last-month-users")]
        public async Task<List<UserModel>> GetUsersFromLastMonth(string interest)
        {
            return await _userService.GetUsersForLastMonth(interest);
        }
    }
}
