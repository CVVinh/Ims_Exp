using System.ComponentModel.DataAnnotations;

namespace IMS_Example.Data.Models
{
    public class Menu
    {
        [Required]
        public int id { get; set; }
        public int idModule { get; set; }
        public string title { get; set; }
        public string icon { get; set; }
        public string view { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public int parent { get; set; }
        public int isDeleted { get; set; }
    }
}
