using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoListMVC.Models.DTO;
using ToDoListMVC.Repository;
using ToDoListMVC.Repository.Interfaces;

namespace ToDoListMVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly RepositorySwitcher _switcher;
        private ITaskRepository _repo;

        public HomeController(RepositorySwitcher switcher)
        {
            _switcher = switcher;
            _repo = _switcher.GetDataSource();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var taskAndCategories = new TasksAndCategoriesViewModel();
            taskAndCategories.Type = _switcher.SourceType;
            taskAndCategories.Tasks = await _repo.GetTasksAsync();
            taskAndCategories.Categories = await _repo.GetCategoriesAsync();
            return View(taskAndCategories);
        }

        [HttpPost]
        public async Task<IActionResult> Index(TaskCreationModel item)
        {
            if (!ModelState.IsValid)
            {
                var taskAndCategories = new TasksAndCategoriesViewModel();
                taskAndCategories.Tasks = await _repo.GetTasksAsync();
                taskAndCategories.Categories = await _repo.GetCategoriesAsync();
                return View(taskAndCategories);
            }

            await _repo.CreateTasksAsync(item);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SwitchSourceData(string? sourse = "mysql")
        {
            _repo = _switcher.Switch(sourse);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _repo.DeleteTasksAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CompleteTask(int id)
        {
            var task = await _repo.GetTasksAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            await _repo.CompleteTasksAsync(id);
            return RedirectToAction("Index");
        }
    }
}