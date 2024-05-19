using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRManager.APIv2.Models
{
    public class Position
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        //[Required(ErrorMessage = "Name is required.")]
        [StringLength(30, ErrorMessage = "Max length is 30")]
        public string Name { get; set; } = String.Empty;
    }
}
