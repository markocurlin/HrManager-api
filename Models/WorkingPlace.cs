using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRManager.APIv2.Models
{
    public class WorkingPlace
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        //[Required(ErrorMessage = "Name is required.")]
        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string Name { get; set; } = String.Empty;

        public IList<Candidate> Candidates { get; set; }
    }
}
