using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankOfDotNet.API.Models
{
    public class Customer
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(256)")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "nvarchar(256)")]
        public string LastName { get; set; } = string.Empty;
    }
}
