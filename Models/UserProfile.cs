using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRManager.APIv2.Models
{
    public class UserProfile
    {
        [Key]
        [Required(ErrorMessage = "Id is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        [StringLength(20, ErrorMessage = "Max length is 20")]
        public string UserName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(20, ErrorMessage = "Max length is 20")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(20, ErrorMessage = "Max length is 20")]
        public string FirstName { get; set; } = String.Empty;

        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(20, ErrorMessage = "Max length is 20")]
        public string LastName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Role is required.")]
        [StringLength(20, ErrorMessage = "Max length is 20")]
        public string Role { get; set; } = String.Empty;
    }
}
