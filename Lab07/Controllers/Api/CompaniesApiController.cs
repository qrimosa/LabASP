using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab07.Models.Movies;

namespace Lab07.Controllers.Api
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesApiController : ControllerBase
    {
        private readonly MoviesDbContext _context;

        public CompaniesApiController(MoviesDbContext context)
        {
            _context = context;
        }

        // GET /api/companies?filter=war
        [HttpGet]
        public IActionResult GetFiltered(string? filter)
        {
            var query = _context.ProductionCompanies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                var f = filter.ToLower();
                query = query.Where(o => o.CompanyName!.ToLower().Contains(f));
            }

            var result = query
                .OrderBy(o => o.CompanyName)
                .Take(50)
                .AsNoTracking()
                .ToList();

            return Ok(result);
        }
    }
}