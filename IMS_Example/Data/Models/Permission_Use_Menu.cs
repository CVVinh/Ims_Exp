using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS_Example.Data.Models
{
    [Table("Permission_Use_Menu")]
    public class Permission_Use_Menu
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int idModule { get; set; }
        [Required]
        public int IdUser { get; set; }
        [Required]
        public int IdMenu { get; set; }
        [Required]
        public int Add { get; set; }
        [Required]
        public int Update { get; set; }
        [Required]
        public int Delete { get; set; }
        [Required]
        public int Export { get; set; }
        public int? userCreated { get; set; }
        public DateTime? dateCreated { get; set; }
        public int? userModified { get; set; }
        public DateTime? dateModified { get; set; }
    }
}
