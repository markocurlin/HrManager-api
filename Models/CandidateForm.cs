using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRManager.APIv2.Models
{
    public class CandidateForm
    {
        //public string Id { get; set; } = String.Empty;

        [StringLength(50, ErrorMessage = "Max length is 50")]
        public string FirstName { get; set; } = String.Empty;

        [StringLength(50, ErrorMessage = "Max length is 50")]
        public string LastName { get; set; } = String.Empty;

        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string DateOfApplication { get; set; } = String.Empty;

        //[StringLength(30, ErrorMessage = "Max length is 30")]
        //public string WorkingPlace { get; set; } = String.Empty;
        //public IList<WorkingPlace> WorkingPlaces { get; set; }
        //public IList<Proba> WorkingPlaces { get; set; }

        //public IReadOnlyList<Proba> Probas { get; set; }

        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string DateOfBirth { get; set; } = String.Empty;

        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string Profession { get; set; } = String.Empty;

        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string Employment { get; set; } = String.Empty;

        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string EducationDegree { get; set; } = String.Empty;

        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string Education { get; set; } = String.Empty;

        [NotMapped]
        public IFormFile CandidateFile { get; set; }

        //public string CandidateFileName { get; set; } = String.Empty;
        /*
        public string DateOfInterview { get; set; } = String.Empty;

        public string Comment { get; set; } = String.Empty;

        public string Position { get; set; } = String.Empty;

        public string Evaluation { get; set; } = String.Empty;

        public string SelectEmployment { get; set; } = String.Empty;

        public string DateOfEmployment { get; set; } = String.Empty;*/
    }
}
