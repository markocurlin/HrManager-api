using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRManager.APIv2.Data;
using HRManager.APIv2.Models;

namespace HRManager.APIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;

        public AccountController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetUsers()
        {
            if (_context.UserProfiles == null)
            {
                return NotFound();
            }

            return await _context.UserProfiles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> GetUser(string id)
        {
            if (_context.UserProfiles == null)
            {
                return NotFound();
            }

            var user = await _context.UserProfiles.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("PageNumber")]
        public async Task<int> GetTotalPages()
        {
            var users = from u in _context.UserProfiles
                        select u;

            return await PaginatedList<UserProfile>.GetTotalPages(users.AsNoTracking(), 5);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserProfile>> PutUser(string id, UserProfile user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        [HttpPost]
        public async Task<ActionResult<UserProfile>> PostUser(UserProfile user)
        {
            if (_context.UserProfiles == null)
            {
                return Problem("Entity set 'DataContext.UserProfiles'  is null.");
            }

            _context.UserProfiles.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpPost("{pageNumber}")]
        public async Task<ActionResult<IEnumerable<UserProfile>>> SortedPaginatedUserList(
            string sortOrder,
            int pageSize,
            int? pageNumber
            )
        {
            var users = from u in _context.UserProfiles
                        select u;

            switch (sortOrder)
            {
                case "userName_asc":
                    users = users.OrderBy(u => u.UserName);
                    break;
                case "userName_desc":
                    users = users.OrderByDescending(u => u.UserName);
                    break;
                case "email_asc":
                    users = users.OrderBy(u => u.Email);
                    break;
                case "email_desc":
                    users = users.OrderByDescending(u => u.Email);
                    break;
                case "firstName_asc":
                    users = users.OrderBy(u => u.FirstName);
                    break;
                case "firstName_desc":
                    users = users.OrderByDescending(u => u.FirstName);
                    break;
                case "lastName_asc":
                    users = users.OrderBy(u => u.LastName);
                    break;
                case "lastName_desc":
                    users = users.OrderByDescending(u => u.LastName);
                    break;
                case "role_asc":
                    users = users.OrderBy(u => u.Role);
                    break;
                case "role_desc":
                    users = users.OrderByDescending(u => u.Role);
                    break;
                default:
                    break;
            }

            return await PaginatedList<UserProfile>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            if (_context.UserProfiles == null)
            {
                return NotFound();
            }

            var user = await _context.UserProfiles.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.UserProfiles.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return (_context.UserProfiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
