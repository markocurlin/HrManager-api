using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using HRManager.APIv2.Data;
using HRManager.APIv2.Models;

namespace HRManager.APIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationDegreesController : ControllerBase
    {
        private readonly DataContext _context;

        public EducationDegreesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/EducationDegrees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EducationDegree>>> GetEducationDegree()
        {
            return await _context.EducationDegrees.ToListAsync();
        }

        // GET: api/EducationDegrees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EducationDegree>> GetEducationDegree(string id)
        {
            var educationDegree = await _context.EducationDegrees.FindAsync(id);

            if (educationDegree == null)
            {
                return NotFound();
            }

            return educationDegree;
        }

        [HttpGet("PageNumber")]
        public async Task<int> GetTotalPages()
        {
            var educationDegrees = from e in _context.EducationDegrees
                                   select e;

            return await PaginatedList<EducationDegree>.GetTotalPages(educationDegrees.AsNoTracking(), 5);
        }

        // PUT: api/EducationDegrees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutEducationDegree(string id, EducationDegree educationDegree)
        {
            if (id != educationDegree.Id)
            {
                return BadRequest();
            }

            _context.Entry(educationDegree).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationDegreeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EducationDegrees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EducationDegree>> PostEducationDegree(EducationDegree educationDegree)
        {
            _context.EducationDegrees.Add(educationDegree);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEducationDegree", new { id = educationDegree.Id }, educationDegree);
        }

        [HttpPost("{pageNumber}")]
        public async Task<ActionResult<IEnumerable<EducationDegree>>> SortedPaginatedEducationDegreeList(
            string sortOrder,
            int pageSize,
            int? pageNumber
            )
        {
            var educationDegrees = from e in _context.EducationDegrees
                                   select e;

            switch (sortOrder)
            {
                case "name_asc":
                    educationDegrees = educationDegrees.OrderBy(c => c.Name);
                    break;
                case "name_desc":
                    educationDegrees = educationDegrees.OrderByDescending(c => c.Name);
                    break;
                default:
                    break;
            }

            return await PaginatedList<EducationDegree>.CreateAsync(educationDegrees.AsNoTracking(), pageNumber ?? 1, pageSize);
        }

        // DELETE: api/EducationDegrees/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEducationDegree(string id)
        {
            var educationDegree = await _context.EducationDegrees.FindAsync(id);
            if (educationDegree == null)
            {
                return NotFound();
            }

            _context.EducationDegrees.Remove(educationDegree);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EducationDegreeExists(string id)
        {
            return _context.EducationDegrees.Any(e => e.Id == id);
        }
    }
}
