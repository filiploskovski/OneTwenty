using Microsoft.AspNetCore.Mvc;
using OneTwenty.Services.Services.User;
using OneTwenty.Shared.Models;

namespace OneTwenty.WebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Create(UserModel interestModel)
        {
            var model = await _userService.Create(interestModel);
            return Ok(model);
        }

        [HttpPut]
        public async Task<ActionResult<UserModel>> Update(UserModel interestModel)
        {
            var model = await _userService.Update(interestModel);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok();
        }
    }
}
