using Microsoft.AspNetCore.Mvc;
using OneTwenty.Services.Services.Interest;

namespace OneTwenty.WebApi.Controllers
{
    [Route("api/interest")]
    [ApiController]
    public class InterestController : ControllerBase
    {
        private readonly IInterestService _interestService;

        public InterestController(IInterestService interestService)
        {
            _interestService = interestService;
        }

        [HttpPost]
        public async Task<ActionResult<InterestModel>> Create(InterestModel interestModel)
        {
            var model = await _interestService.Create(interestModel);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> Update(InterestModel interestModel)
        {
            var model = await _interestService.Update(interestModel);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _interestService.Delete(id);
            return Ok();
        }
    }
}
