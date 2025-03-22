using Challenge.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Services.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryApplication _application;

        public CountryController(ICountryApplication countryApplication)
        {
            _application = countryApplication;
        }

        [HttpGet("ListCountries")]
        public async Task<IActionResult> ListCountries()
        {

            var response = await _application.ListCountries();
            if (response.IsSuccess)
                return Ok(response);

            return Ok(response.Message);
        }
    }
}
