using Challenge.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Services.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SourceController : ControllerBase
    {
        private readonly ISourceApplication _application;

        public SourceController(ISourceApplication sourceApplication)
        {
            _application = sourceApplication;
        }

        [HttpGet("ListSources")]
        public async Task<IActionResult> ListSources()
        {

            var response = await _application.ListSources();
            if (response.IsSuccess)
                return Ok(response);

            return Ok(response.Message);
        }
    }
}
