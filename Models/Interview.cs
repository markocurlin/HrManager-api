using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRManager.APIv2.Models
{
    public class Interview
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        //[Required(ErrorMessage = "DateOfInterview is required.")]
        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string DateOfInterview { get; set; } = String.Empty;

        //[Required(ErrorMessage = "Comment is required.")]
        [StringLength(150, ErrorMessage = "Max length is 150")]
        public string Comment { get; set; } = String.Empty;

        //[Required(ErrorMessage = "Position is required.")]
        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string Position { get; set; } = String.Empty;

        //[Required(ErrorMessage = "Evaluation is required.")]
        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string Evaluation { get; set; } = String.Empty;

        //[Required(ErrorMessage = "SelectEmployment is required.")]
        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string SelectEmployment { get; set; } = String.Empty;

        //[Required(ErrorMessage = "DateOfEmployment is required.")]
        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string DateOfEmployment { get; set; } = String.Empty;
        public string CandidateId { get; set; }
    }
}
