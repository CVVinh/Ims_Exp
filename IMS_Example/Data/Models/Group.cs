using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IMS_Example.Data.Models
{
    [Table("Group")]
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? NameGroup { get; set; }
        public string? Discription { get; set; }
        public int? userCreated { get; set; }
        public DateTime? dateCreated { get; set; }
        public int? userModified { get; set; }
        public DateTime? dateModified { get; set; }
        public int IsDeleted { get; set; }
        public string? Key { get; set; }

    }
}
