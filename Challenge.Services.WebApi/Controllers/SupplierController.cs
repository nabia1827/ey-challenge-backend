using Challenge.Application.DTO;
using Challenge.Application.Interface;
using Challenge.Application.Main;
using Challenge.Services.WebApi.Helpers;
using Challenge.Services.WebApi.Models.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Challenge.Services.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierApplication _application;

        public SupplierController(ISupplierApplication supplierApplication)
        {
            _application = supplierApplication;
        }

        [HttpGet("ListSuppliers")]
        public async Task<IActionResult> ListSuppliers([FromQuery] SupplierFilter filter)
        {

            if (filter.Pagina <= 0 || filter.CantidadRegistrosPorPagina <= 0)
                return BadRequest();

            var response = await _application.ListSuppliers(
                filter.Pagina,
                filter.CantidadRegistrosPorPagina,
                filter.LegalName,
                filter.tradeName,
                filter.taxIdentNumber,
                filter.countryId,
                filter.initDate,
                filter.endDate);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpGet("GetSupplier")]
        public async Task<IActionResult> GetSupplier([FromQuery] int supplierId)
        {

            if (supplierId <= 0 )
                return BadRequest();

            var response = await _application.GetSupplier(supplierId);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpPost("UpsertSupplier")]
        public async Task<IActionResult> UpsertSupplier([FromBody] SupplierDto supplier)
        {
            if (supplier.SupplierId < 0 || String.IsNullOrEmpty(supplier.LegalName) || String.IsNullOrEmpty(supplier.TradeName))
                return BadRequest();

            var response = await _application.UpsertSupplier(
                supplier.SupplierId, 
                supplier.LegalName??"",
                supplier.TradeName ?? "", 
                supplier.TaxIndentNumber ?? "",
                supplier.PhoneNumber ?? "",
                supplier.MailAddress ?? "",
                supplier.Website ?? "",
                supplier.PhysicalAddress ?? "",
                supplier.CountryId,
                supplier.AnnualRevenue);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpGet("DeleteSupplier")]
        public async Task<IActionResult> DeleteSupplier([FromQuery] int supplierId)
        {

            if (supplierId <= 0)
                return BadRequest();

            var response = await _application.DeleteSupplier(supplierId);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }
    }
}
