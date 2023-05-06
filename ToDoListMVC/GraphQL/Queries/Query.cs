using GraphQL;
using GraphQL.Types;
using ToDoListMVC.GraphQL.Types;
using ToDoListMVC.Repository;
using ToDoListMVC.Repository.Interfaces;

namespace ToDoListMVC.GraphQL.Queries
{
    public class Query : ObjectGraphType
    {
        private readonly RepositorySwitcher _switcher;
        private ITaskRepository repo;

        public Query(RepositorySwitcher switcher)
        {
            _switcher = switcher;
            repo = _switcher.GetDataSource();

            Field<ListGraphType<TaskType>>("tasks")
                .ResolveAsync(async context => await repo.GetTasksAsync());

            Field<TaskType>("task")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .ResolveAsync(async context =>
                {
                    int id = context.GetArgument<int>("id");
                    return await repo.GetTasksAsync(id);
                });

            Field<ListGraphType<CategoryType>>("categories")
                .ResolveAsync(async context => await repo.GetCategoriesAsync());

            Field<StringGraphType>("changeStorageType")
                .Argument<NonNullGraphType<StringGraphType>>("type")
                .Resolve(context =>
                {
                    string type = context.GetArgument<string>("type");
                    repo = _switcher.Switch(type);
                    return $"Storage type changed to {type}";
                });
        }
    }
}
