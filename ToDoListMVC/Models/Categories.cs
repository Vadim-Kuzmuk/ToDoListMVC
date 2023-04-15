using System.ComponentModel.DataAnnotations;

namespace ToDoListMVC.Models
{
    public class Categories
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string name { get; set; } = null!;
    }
}