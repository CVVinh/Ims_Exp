using System.ComponentModel.DataAnnotations;

namespace IMS_Example.Data.DTOs.ProjectDTO
{
    public class EditProjectDTO
    {
        [Required]
        [MaxLength(10)]
        public string ProjectCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Leader { get; set; }
        [Required]
        public int UserCreated { get; set; }
        [Required]
        public int UserUpdate { get; set; }
        public bool IsOnGitlab { get; set; }
    }
}
