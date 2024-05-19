using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRManager.APIv2.Models
{
    public class Candidate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        //[Required(ErrorMessage = "FirstName is required.")]
        [StringLength(50, ErrorMessage = "Max length is 50")]
        public string FirstName { get; set; } = String.Empty;

        //[Required(ErrorMessage = "LastName is required.")]
        [StringLength(50, ErrorMessage = "Max length is 50")]
        public string LastName { get; set; } = String.Empty;

        //[Required(ErrorMessage = "DateOfApplication is required.")]
        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string DateOfApplication { get; set; } = String.Empty;

        //[Required(ErrorMessage = "WorkingPlace is required.")]
        //[StringLength(30, ErrorMessage = "Max length is 30")]
        //public string WorkingPlace { get; set; } = String.Empty;
        public IList<WorkingPlace> WorkingPlaces { get; set; }

        //[Required(ErrorMessage = "DateOfBirth is required.")]
        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string DateOfBirth { get; set; } = String.Empty;

        //[Required(ErrorMessage = "Profession is required.")]
        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string Profession { get; set; } = String.Empty;

        //[Required(ErrorMessage = "Employment is required.")]
        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string Employment { get; set; } = String.Empty;

        //[Required(ErrorMessage = "EducationDegree is required.")]
        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string EducationDegree { get; set; } = String.Empty;

        //[Required(ErrorMessage = "Education is required.")]
        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string Education { get; set; } = String.Empty;

        //[Required(ErrorMessage = "Document is required.")]
        //[NotMapped]
        public string CandidateFile { get; set; }
        public IList<Interview> Interviews { get; set; }
    }
}
