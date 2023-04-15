namespace ToDoListMVC.Models.DTO
{
    public class TasksAndCategoriesViewModel
    {
        public IEnumerable<Tasks> Tasks { get; set; } = new List<Tasks>();
        public IEnumerable<Categories> Categories { get; set; } = new List<Categories>();
    }
}