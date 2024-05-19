using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRManager.APIv2.Data;
using HRManager.APIv2.Models;
using Microsoft.AspNetCore.Authorization;

namespace HRManager.APIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class InterviewsController : ControllerBase
    {
        private readonly DataContext _context;

        public InterviewsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Interviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interview>>> GetInterview()
        {
            return await _context.Interviews.ToListAsync();
        }

        // GET: api/Interviews/5

        [HttpGet("{id}")]
        public async Task<ActionResult<Interview>> GetInterview(string id)
        {
            var interview = await _context.Interviews.FindAsync(id);

            if (interview == null)
            {
                return NotFound();
            }
            return interview;
        }

        // GET: api/Interviews/5
        [HttpGet("List/{id}")]
        public async Task<ActionResult<IList<Interview>>> GetInterviews(string id)
        {
            var interviews = (from i in _context.Interviews
                              where i.CandidateId == id
                              select i).ToListAsync();

            if (interviews == null)
            {
                return NotFound();
            }

            return await interviews;
        }

        [HttpGet("PageNumber")]
        public async Task<int> GetTotalPages(string id)
        {
            var interviews = from i in _context.Interviews
                             where i.CandidateId == id
                             select i;

            return await PaginatedList<Interview>.GetTotalPages(interviews.AsNoTracking(), 5);
        }

        // PUT: api/Interviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInterview(string id, Interview interview)
        {
            if (id != interview.Id)
            {
                return BadRequest();
            }

            _context.Entry(interview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterviewExists(id))
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

        // POST: api/Interviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Interview>> PostInterview(Interview interview)
        {
            _context.Interviews.Add(interview);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInterview", new { id = interview.Id }, interview);
        }

        [HttpPost("{pageNumber}")]
        public async Task<ActionResult<IEnumerable<Interview>>> SortedPaginatedInterviewList(
            string sortOrder,
            int? pageNumber,
            int pageSize,
            string id
            )
        {
            var interviews = from i in _context.Interviews
                             where i.CandidateId == id
                             select i;

            switch (sortOrder)
            {
                case "dateOfInterview_asc":
                    interviews = interviews.OrderBy(i => i.DateOfInterview);
                    break;
                case "dateOfInterview_desc":
                    interviews = interviews.OrderByDescending(i => i.DateOfInterview);
                    break;
                case "comment_asc":
                    interviews = interviews.OrderBy(i => i.Comment);
                    break;
                case "comment_desc":
                    interviews = interviews.OrderByDescending(i => i.Comment);
                    break;
                case "position_asc":
                    interviews = interviews.OrderBy(i => i.Position);
                    break;
                case "position_desc":
                    interviews = interviews.OrderByDescending(i => i.Position);
                    break;
                case "evaluation_asc":
                    interviews = interviews.OrderBy(i => i.Evaluation);
                    break;
                case "evaluation_desc":
                    interviews = interviews.OrderByDescending(i => i.Evaluation);
                    break;
                case "selectEmployment_asc":
                    interviews = interviews.OrderBy(i => i.SelectEmployment);
                    break;
                case "selectEmployment_desc":
                    interviews = interviews.OrderByDescending(i => i.SelectEmployment);
                    break;
                case "dateOfEmployment_asc":
                    interviews = interviews.OrderBy(i => i.DateOfEmployment);
                    break;
                case "dateOfEmployment_desc":
                    interviews = interviews.OrderByDescending(i => i.DateOfEmployment);
                    break;
                default:
                    break;
            }

            return await PaginatedList<Interview>.CreateAsync(interviews.AsNoTracking(), pageNumber ?? 1, pageSize);
        }

        // DELETE: api/Interviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterview(string id)
        {
            var interview = await _context.Interviews.FindAsync(id);
            if (interview == null)
            {
                return NotFound();
            }

            _context.Interviews.Remove(interview);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool InterviewExists(string id)
        {
            return _context.Interviews.Any(e => e.Id == id);
        }
    }
}
