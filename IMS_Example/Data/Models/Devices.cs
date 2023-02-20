using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS_Example.Data.Models
{
    [Table("Devices")]
    public class Devices
    {
        [Key]
        public int IdDevice { get; set; }
        [Required]
        public string? DeviceName { get; set; }
        [Required]
        public string? Info { get; set; }
        [Required]
        public int IsDeleted { get; set; }
        [Required]
        public int UserCreated { get; set; }
        [Required]
        public int UserUpdated { get; set; }
        [Required]
        public DateTime DateUpdated { get; set; }
        public string? Note { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }

        public void InitAdd(int userCreated)
        {
            UserCreated = userCreated;
        }
        public void InitUpdate(int userUpdated)
        {
            UserUpdated = userUpdated;
            DateUpdated = DateTime.Now;
        }
        public void InitDelete(int userUpdated)
        {
            UserUpdated = userUpdated;
            DateUpdated = DateTime.Now;
            IsDeleted = 1;
        }
    }
}
