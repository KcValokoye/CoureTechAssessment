using CountryOperators.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CountryOperators.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PhoneController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _context.Countries
                .Include(c => c.Details)
                .ToListAsync();

            return Ok(countries);
        }

        [HttpGet("{phoneNumber}")]
        public async Task<IActionResult> GetCountryDetails(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length < 3)
            {
                return BadRequest(new { error = "Invalid phone number" });
            }

            string countryCode = phoneNumber.Substring(0, 3);
            var country = await _context.Countries
                .Include(c => c.Details)
                .FirstOrDefaultAsync(c => c.CountryCode == countryCode);

            if (country == null)
            {
                return NotFound(new { error = "Country code not found" });
            }

            var response = new
            {
                number = phoneNumber,
                country = new
                {
                    countryCode = country.CountryCode,
                    name = country.Name,
                    countryIso = country.CountryIso,
                    countryDetails = country.Details.Select(d => new
                    {
                        operatord = d.Operator,
                        operatorCode = d.OperatorCode
                    }).ToList()
                }
            };

            return Ok(response);
        }
    }
}
