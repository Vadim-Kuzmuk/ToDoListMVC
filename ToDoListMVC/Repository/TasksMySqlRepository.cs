using Dapper;
using System.Data;
using ToDoListMVC.Context;
using ToDoListMVC.Models;
using ToDoListMVC.Models.DTO;
using ToDoListMVC.Repository.Interfaces;

namespace ToDoListMVC.Repository
{
    public class TasksMySqlRepository : ITasksMySqlRepository
    {
        private readonly DapperContext _context;

        public TasksMySqlRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CompleteTasksAsync(int id)
        {
            string query = "UPDATE Tasks SET is_completed = ~is_completed WHERE id = @id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
                
        }

        public async Task CreateTasksAsync(TaskCreationModel item)
        {
            var query = "INSERT INTO Tasks (category_id, name, due_date, is_completed) " +
                "VALUES (@category_id, @name, @due_date, 0)";

            var param = new DynamicParameters();
            param.Add("name", item.name, DbType.String);
            param.Add("category_id", item.category_id, DbType.Int32);
            param.Add("due_date", item.due_date, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, param);
            }
                
        }

        public async Task DeleteTasksAsync(int id)
        {
            string query = "DELETE FROM Tasks WHERE id = @id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
                
        }

        public async Task<IEnumerable<Categories>> GetCategoriesAsync()
        {
            string query = "SELECT * FROM Categories";

            using (var connection = _context.CreateConnection())
            {
                var categories = await connection.QueryAsync<Categories>(query);
                return categories.ToList();
            }
        }

        public async Task<Tasks> GetTasksAsync(int id)
        {
            string query = "SELECT * FROM Tasks WHERE id = @id";

            using (var connection = _context.CreateConnection())
            {
                var task = await connection.QuerySingleOrDefaultAsync<Tasks>(query, new { id });
                return task;
            }
        }

        public async Task<IEnumerable<Tasks>> GetTasksAsync()
        {
            string query = "SELECT * FROM Tasks";

            using (var connection = _context.CreateConnection())
            {
                var task = await connection.QueryAsync<Tasks>(query);

                task = task.OrderBy(i => i.is_completed).ThenByDescending(i => i.due_date.HasValue).ThenBy(i => i.due_date);
                return task.Distinct().ToList();
            }
        }
    }
}