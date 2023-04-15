using Microsoft.AspNetCore.Mvc;
using ToDoListMVC.Models.DTO;
using ToDoListMVC.Repository.Interfaces;

namespace ToDoListMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITasksRepository _repo;

        public HomeController(ITasksRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var taskAndCategories = new TasksAndCategoriesViewModel();
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