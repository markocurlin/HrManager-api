using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using HRManager.APIv2.Data;
using HRManager.APIv2.Models;

namespace HRManager.APIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly DataContext _context;

        public PositionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
        {
            return await _context.Positions.ToListAsync();
        }

        // GET: api/Positions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetPosition(string id)
        {
            var position = await _context.Positions.FindAsync(id);

            if (position == null)
            {
                return NotFound();
            }

            return position;
        }

        [HttpGet("PageNumber")]
        public async Task<int> GetTotalPages()
        {
            var positions = from p in _context.Positions
                            select p;

            return await PaginatedList<Position>.GetTotalPages(positions.AsNoTracking(), 5);
        }

        // PUT: api/Positions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutPosition(string id, Position position)
        {
            if (id != position.Id)
            {
                return BadRequest();
            }

            _context.Entry(position).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionExists(id))
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

        // POST: api/Positions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Position>> PostPosition(Position position)
        {
            _context.Positions.Add(position);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosition", new { id = position.Id }, position);
        }

        [HttpPost("{pageNumber}")]
        public async Task<ActionResult<IEnumerable<Position>>> SortedPaginatedPositionList(
            string sortOrder,
            int pageSize,
            int? pageNumber
            )
        {
            var positions = from p in _context.Positions
                            select p;

            switch (sortOrder)
            {
                case "name_asc":
                    positions = positions.OrderBy(c => c.Name);
                    break;
                case "name_desc":
                    positions = positions.OrderByDescending(c => c.Name);
                    break;
                default:
                    break;
            }

            return await PaginatedList<Position>.CreateAsync(positions.AsNoTracking(), pageNumber ?? 1, pageSize);
        }

        // DELETE: api/Positions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePosition(string id)
        {
            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PositionExists(string id)
        {
            return _context.Positions.Any(e => e.Id == id);
        }
    }
}
