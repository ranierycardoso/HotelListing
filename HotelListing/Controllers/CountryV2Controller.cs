using AutoMapper;
using HotelListing.Data;
using HotelListing.IRepository;
using HotelListing.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using X.PagedList;

namespace HotelListing.Controllers
{
    [ApiVersion("2.0", Deprecated = true)]
    //[Route("api/{v:apiversion}/[controller]")]
    [Route("api/country")]
    [ApiController]
    public class CountryV2Controller : ControllerBase
    {
        DatabaseContext _context;
        public CountryV2Controller(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParams)
        {
            requestParams ??= new RequestParams();
            var countries = await _context.Countries.AsNoTracking()
                .ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
            return Ok(countries);
        }
    }
}
