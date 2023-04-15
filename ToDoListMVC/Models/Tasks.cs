using System.ComponentModel.DataAnnotations;

namespace ToDoListMVC.Models
{
    public class Tasks
    {
        [Key]
        public int id { get; set; }

        public int category_id { get; set; }

        [Required]
        public string name { get; set; } = null!;

        [Required]
        public DateTime? due_date { get; set; } = null!;

        [Required]
        public bool is_completed { get; set; }
    }
}