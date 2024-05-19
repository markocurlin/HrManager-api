using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRManager.APIv2.Data;
using HRManager.APIv2.Models;
using Microsoft.AspNetCore.Authorization;
using IdentityModel;
using System.Security.Claims;
using Microsoft.AspNetCore.StaticFiles;
using System.Net.Http.Headers;

namespace HRManager.APIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class CandidatesController : ControllerBase
    {
        private readonly DataContext _context;

        public CandidatesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("AuthContext")]
        public IActionResult GetAuthContext()
        {
            var userId = this.User.FindFirstValue(JwtClaimTypes.Subject);
            var profile = _context.UserProfiles.FirstOrDefault(u => u.Id == userId);
            if (profile == null) return NotFound();
            var context = new AuthContext
            {
                UserProfile = profile,
                Claims = User.Claims.Select(c => new SimpleClaim { Type = c.Type, Value = c.Value }).ToList()
            };
            return Ok(context);
        }

        [HttpGet("GetFile")]
        public async Task<IActionResult> GetFile([FromQuery] string fileName)
        {
            var folderName = Path.Combine("Resources", "Files");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var fullPath = Path.Combine(filePath, fileName);

            /*
            if (!System.IO.File.Exists(filePath))
                return NotFound();*/

            var memory = new MemoryStream();

            await using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;

            return File(memory, GetContentType(fullPath), fullPath);
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            if (!provider.TryGetContentType(path, out contentType))
            {
                //contentType = "application/pdf";
                contentType = "application/octet-stream";
            }

            return contentType;
        }

        private bool IsAPdfFile(string fileName)
        {
            return fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase);
        }

        private bool IsEqual(string fileName, string file)
        {
            return fileName.Equals(file, StringComparison.OrdinalIgnoreCase);
        }

        [HttpGet("PageNumber")]
        public async Task<int> GetTotalPages()
        {
            var candidates = from c in _context.Candidates
                             select c;

            return await PaginatedList<Candidate>.GetTotalPages(candidates.AsNoTracking(), 5);
        }

        // GET: api/Candidates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidate()
        {
            return await _context.Candidates.ToListAsync();
        }

        // GET: api/Candidates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> GetCandidate(string id)
        {
            var candidate = await _context.Candidates.FindAsync(id);

            if (candidate == null)
            {
                return NotFound();
            }

            return candidate;
        }

        // PUT: api/Candidates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //public async Task<IActionResult> PutCandidate(int id, Candidate candidate)
        /*public async Task<IActionResult> PutCandidate(string id, [FromForm] CandidateForm candidateForm)
        {
            if (id != candidateForm.Id)
            {
                return BadRequest();
            }

            var candidate = new Candidate();

            candidate.Id = candidateForm.Id;
            candidate.FirstName = candidateForm.FirstName;
            candidate.LastName = candidateForm.LastName;
            candidate.DateOfApplication = candidateForm.DateOfApplication;
            //candidate.WorkingPlace = candidateForm.WorkingPlace;
            candidate.WorkingPlaces = candidateForm.WorkingPlaces;
            candidate.DateOfBirth = candidateForm.DateOfBirth;
            candidate.Profession = candidateForm.Profession;
            candidate.Employment = candidateForm.Employment;
            candidate.EducationDegree = candidateForm.EducationDegree;
            candidate.Education = candidateForm.Education;

            if (candidateForm.CandidateFile != null )
            {
                if (Path.GetExtension(candidateForm.CandidateFile.FileName) != ".pdf")
                {
                    return BadRequest($"The uploaded file is not a PDF file.");
                }

                var folderName = Path.Combine("Resources", "Files");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                var fileName = ContentDispositionHeaderValue.Parse(candidateForm.CandidateFile.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await candidateForm.CandidateFile.CopyToAsync(stream);
                }

                candidate.CandidateFile = candidateForm.CandidateFile.FileName;
            } else
            {
                candidate.CandidateFile = candidateForm.CandidateFileName;
            }

            _context.Entry(candidate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }  */

        // POST: api/Candidates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Candidate>> PostCandidate([FromForm] CandidateForm candidateForm)
        {
            if (candidateForm.CandidateFile == null || candidateForm.CandidateFile.Length < 1)
            {
                return BadRequest("The uploaded file is empty.");
            }

            if (Path.GetExtension(candidateForm.CandidateFile.FileName) != ".pdf")
            {
                return BadRequest($"The uploaded file is not a PDF file.");
            }
            /*
            var candidate = new Candidate();
            
            candidate.FirstName = candidateForm.FirstName;
            candidate.LastName = candidateForm.LastName;
            candidate.DateOfApplication = candidateForm.DateOfApplication;
            //candidate.WorkingPlace = candidateForm.WorkingPlace;
            candidate.WorkingPlaces = candidateForm.WorkingPlaces;
            candidate.DateOfBirth = candidateForm.DateOfBirth;
            candidate.Profession = candidateForm.Profession;
            candidate.Employment = candidateForm.Employment;
            candidate.EducationDegree = candidateForm.EducationDegree;
            candidate.Education = candidateForm.Education;*/

            var folderName = Path.Combine("Resources", "Files");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var fileName = ContentDispositionHeaderValue.Parse(candidateForm.CandidateFile.ContentDisposition).FileName.Trim('"');
            var fullPath = Path.Combine(pathToSave, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await candidateForm.CandidateFile.CopyToAsync(stream);
            }
            /*
            IReadOnlyList<string> items;
            {
                //String formList = this.Request.Form["Items"];
                string formList = this.Request.Form["firstName"];

                //items = JsonConvert.<string>(formList);

                //items = JsonConvert.DeserializeObject<List<String>>(formList);
            }*/


            //candidateForm.Probas = items;

            /*
            candidate.CandidateFile = candidateForm.CandidateFile.FileName;
            
            candidate.Interviews = new List<Interview>();
            
            if (candidateForm.DateOfInterview != "") 
            { 
                var Interview = new Interview();

                Interview.DateOfInterview = candidateForm.DateOfInterview;
                Interview.Comment = candidateForm.Comment;
                Interview.Position = candidateForm.Position;
                Interview.Evaluation = candidateForm.Evaluation;
                Interview.SelectEmployment = candidateForm.SelectEmployment;
                Interview.DateOfEmployment = candidateForm.DateOfEmployment;

                candidate.Interviews.Add(Interview);
            }*/
            /*
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCandidate", new { id = candidate.Id }, candidate);
        */

            var formList = this.Request.Form["firstName"];

            return Ok(formList);
        }

        [HttpPost("{pageNumber}")]
        public async Task<ActionResult<IEnumerable<Candidate>>> SortedPaginatedCandidateList(
            string sortOrder,
            int pageSize,
            int? pageNumber
            )
        {

            var candidates = from c in _context.Candidates.Include("Interviews")
                             select c;

            switch (sortOrder)
            {
                case "firstName_asc":
                    candidates = candidates.OrderBy(c => c.FirstName);
                    break;
                case "firstName_desc":
                    candidates = candidates.OrderByDescending(c => c.FirstName);
                    break;
                case "lastName_asc":
                    candidates = candidates.OrderBy(c => c.LastName);
                    break;
                case "lastName_desc":
                    candidates = candidates.OrderByDescending(c => c.LastName);
                    break;
                case "dateOfApplication_asc":
                    candidates = candidates.OrderBy(c => c.DateOfApplication);
                    break;
                case "dateOfApplication_desc":
                    candidates = candidates.OrderByDescending(c => c.DateOfApplication);
                    break;/*
                case "workingPlace_asc":
                    candidates = candidates.OrderBy(c => c.WorkingPlace);
                    break;
                case "workingPlace_desc":
                    candidates = candidates.OrderByDescending(c => c.WorkingPlace);
                    break;*/
                case "profession_asc":
                    candidates = candidates.OrderBy(c => c.Profession);
                    break;
                case "profession_desc":
                    candidates = candidates.OrderByDescending(c => c.Profession);
                    break;
                case "education_asc":
                    candidates = candidates.OrderBy(c => c.Education);
                    break;
                case "education_desc":
                    candidates = candidates.OrderByDescending(c => c.Education);
                    break;
                case "employment_asc":
                    candidates = candidates.OrderBy(c => c.Employment);
                    break;
                case "employment_desc":
                    candidates = candidates.OrderByDescending(c => c.Employment);
                    break;
                case "dateOfInterview_asc":
                    candidates = candidates.OrderBy(c => c.Interviews.OrderBy(i => i.DateOfInterview).Select(i => i.DateOfInterview).FirstOrDefault());
                    break;
                case "dateOfInterview_desc":
                    candidates = candidates.OrderByDescending(c => c.Interviews.OrderByDescending(i => i.DateOfInterview).Select(i => i.DateOfInterview).FirstOrDefault());
                    break;
                case "evaluation_asc":
                    candidates = candidates.OrderBy(c => c.Interviews.OrderBy(i => i.Evaluation).Select(i => i.Evaluation).FirstOrDefault());
                    break;
                case "evaluation_desc":
                    candidates = candidates.OrderByDescending(c => c.Interviews.OrderByDescending(i => i.Evaluation).Select(i => i.Evaluation).FirstOrDefault());
                    break;
                case "selectEmployment_asc":
                    candidates = candidates.OrderBy(c => c.Interviews.OrderBy(i => i.SelectEmployment).Select(i => i.SelectEmployment).FirstOrDefault());
                    break;
                case "selectEmployment_desc":
                    candidates = candidates.OrderByDescending(c => c.Interviews.OrderByDescending(i => i.SelectEmployment).Select(i => i.SelectEmployment).FirstOrDefault());
                    break;
                case "dateOfEmployment_asc":
                    candidates = candidates.OrderBy(c => c.Interviews.OrderBy(i => i.DateOfEmployment).Select(i => i.DateOfEmployment).FirstOrDefault());
                    break;
                case "dateOfEmployment_desc":
                    candidates = candidates.OrderByDescending(c => c.Interviews.OrderByDescending(i => i.DateOfEmployment).Select(i => i.DateOfEmployment).FirstOrDefault());
                    break;
                default:
                    break;
            }

            return await PaginatedList<Candidate>.CreateAsync(candidates.AsNoTracking(), pageNumber ?? 1, pageSize);
        }

        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<Candidate>>> Search(Candidate candidate)
        {
            var candidates = from c in _context.Candidates.Include("Interviews")
                             select c;

            var interviews = from i in _context.Interviews
                             select i;

            if (candidate == null)
            {
                return Problem("Candidate is null.");
            }

            List<string> CandidatesId = (List<string>)interviews.Where(i =>
                (candidate.Interviews[0].DateOfInterview != "" && i.DateOfInterview.Contains(candidate.Interviews[0].DateOfInterview)) ||
                (candidate.Interviews[0].Position != "" && i.Position.Contains(candidate.Interviews[0].Position)) ||
                (candidate.Interviews[0].Evaluation != "" && i.Evaluation.Contains(candidate.Interviews[0].Evaluation))
            ).Select(i => i.CandidateId).ToList();

            candidates = candidates.Where(c =>
                (candidate.FirstName != "" && c.FirstName.Contains(candidate.FirstName)) ||
                (candidate.LastName != "" && c.LastName.Contains(candidate.LastName)) ||
                (candidate.DateOfApplication != "" && c.DateOfApplication.Contains(candidate.DateOfApplication)) ||
                //(candidate.WorkingPlace != "" && c.WorkingPlace.Contains(candidate.WorkingPlace)) ||
                (candidate.EducationDegree != "" && c.EducationDegree.Contains(candidate.EducationDegree)) ||
                CandidatesId.Contains(c.Id)
            ).Select(c => c);

            return await candidates.AsNoTracking().ToListAsync();
        }

        // DELETE: api/Candidates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(string id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CandidateExists(string id)
        {
            return _context.Candidates.Any(e => e.Id == id);
        }
    }
}
