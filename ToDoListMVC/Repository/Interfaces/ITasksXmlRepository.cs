using System.Xml;
using ToDoListMVC.Models;
using ToDoListMVC.Models.DTO;

namespace ToDoListMVC.Repository.Interfaces
{
    public interface ITasksXmlRepository
    {
        public Task<IEnumerable<Tasks>> GetTasksAsync();
        public Task<Tasks?> GetTasksAsync(int id);
        public Task<IEnumerable<Categories>> GetCategoriesAsync();
        public Task CreateTasksAsync(TaskCreationModel item);
        public Task DeleteTasksAsync(int id);
        public Task CompleteTasksAsync(int id);
        public Task<Tasks> ParseXmlTasksAsync(XmlNode taskXml);
        public Task<Categories> ParseXmlCategoriesAsync(XmlNode categoryXml);
        public Task<int> FindNextIdAsync();
    }
}