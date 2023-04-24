using Microsoft.AspNetCore.Mvc;
using ToDoListMVC.Models.DTO;
using ToDoListMVC.Repository.Interfaces;

namespace ToDoListMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITasksMySqlRepository _repo;
        private readonly ITasksXmlRepository _repoXml;

        public HomeController(ITasksMySqlRepository repo, ITasksXmlRepository repoXml)
        {
            _repo = repo;
            _repoXml = repoXml;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? type = "mysql")
        {
            var taskAndCategories = new TasksAndCategoriesViewModel();
            taskAndCategories.Type = type;
            if (type != "mysql")
            {
                taskAndCategories.Tasks = await _repoXml.GetTasksAsync();
                taskAndCategories.Categories = await _repoXml.GetCategoriesAsync();
            }
            else
            {
                taskAndCategories.Tasks = await _repo.GetTasksAsync();
                taskAndCategories.Categories = await _repo.GetCategoriesAsync();
            }
            return View(taskAndCategories);
        }

        [HttpPost]
        public async Task<IActionResult> Index(TaskCreationModel item)
        {
            if (!ModelState.IsValid)
            {
                var taskAndCategories = new TasksAndCategoriesViewModel();
                taskAndCategories.Type = item.type;
                if (item.type != "mysql")
                {
                    taskAndCategories.Tasks = await _repoXml.GetTasksAsync();
                    taskAndCategories.Categories = await _repoXml.GetCategoriesAsync();
                }
                else
                {
                    taskAndCategories.Tasks = await _repo.GetTasksAsync();
                    taskAndCategories.Categories = await _repo.GetCategoriesAsync();
                }
                return View(taskAndCategories);
            }

            if (item.type != "mysql")
            {
                await _repoXml.CreateTasksAsync(item);
            }
            else
            {
                await _repo.CreateTasksAsync(item);
            }

            return RedirectToAction("Index", new { type = item.type });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTask(int id, string? type = "mysql")
        {
            if (type != "mysql")
            {
                await _repoXml.DeleteTasksAsync(id);
            }
            else
            {
                await _repo.DeleteTasksAsync(id);
            }
            return RedirectToAction("Index", new { type = type });
        }

        [HttpGet]
        public async Task<IActionResult> CompleteTask(int id, string? type = "mysql")
        {
            if (type != "mysql")
            {
                var task = await _repoXml.GetTasksAsync(id);
                if (task == null)
                {
                    return NotFound();
                }
                await _repoXml.CompleteTasksAsync(id);
            }
            else
            {
                var task = await _repo.GetTasksAsync(id);
                if (task == null)
                {
                    return NotFound();
                }
                await _repo.CompleteTasksAsync(id);
            }

            return RedirectToAction("Index", new { type = type });
        }
    }
}