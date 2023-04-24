using ToDoListMVC.Models;
using ToDoListMVC.Models.DTO;

namespace ToDoListMVC.Repository.Interfaces
{
    public interface ITasksMySqlRepository
    {
        public Task<IEnumerable<Tasks>> GetTasksAsync();
        public Task<Tasks> GetTasksAsync(int id);
        public Task<IEnumerable<Categories>> GetCategoriesAsync();
        public Task CreateTasksAsync(TaskCreationModel item);
        public Task DeleteTasksAsync(int id);
        public Task CompleteTasksAsync(int id);
    }
}