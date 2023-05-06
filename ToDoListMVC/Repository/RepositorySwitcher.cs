using ToDoListMVC.Context;
using ToDoListMVC.Repository.Interfaces;

namespace ToDoListMVC.Repository
{
    public class RepositorySwitcher
    {
        private readonly DapperContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        public string? SourceType { get; set; } = "mysql";

        public RepositorySwitcher(DapperContext context, IWebHostEnvironment env, IConfiguration configuration)
        {
            _context = context;
            _env = env;
            _configuration = configuration;
        }

        public ITaskRepository Switch(string? dataType)
        {
            SourceType = dataType;
            if (dataType == "xml")
            {
                return new TasksXmlRepository(_env, _configuration);
            }
            return new TasksMySqlRepository(_context);
        }

        public ITaskRepository GetDataSource()
        {
            if (SourceType == "xml")
            {
                return new TasksXmlRepository(_env, _configuration);
            }
            return new TasksMySqlRepository(_context);
        }
    }
}
