using GraphQL;
using GraphQL.Types;
using ToDoListMVC.GraphQL.Types;
using ToDoListMVC.Models.DTO;
using ToDoListMVC.Repository;

namespace ToDoListMVC.GraphQL.Queries
{
    public class Mutation : ObjectGraphType
    {
        private readonly RepositorySwitcher _switcher;

        public Mutation(RepositorySwitcher switcher)
        {
            _switcher = switcher;

            Field<StringGraphType>("createTask")
                .Argument<NonNullGraphType<TaskInputType>>("task")
                .ResolveAsync(async context =>
                {
                    var task = context.GetArgument<TaskCreationModel>("task");
                    await _switcher.GetDataSource().CreateTasksAsync(task);
                    return "Task created!";
                });

            Field<StringGraphType>("deleteTask")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .ResolveAsync(async context =>
                {
                    int id = context.GetArgument<int>("id");
                    await _switcher.GetDataSource().DeleteTasksAsync(id);
                    return "Task deleted!";
                });

            Field<StringGraphType>("completeTask")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .ResolveAsync(async context =>
                {
                    int id = context.GetArgument<int>("id");
                    await _switcher.GetDataSource().CompleteTasksAsync(id);
                    return "Task is set as completed / uncompleted!";
                });
        }
    }
}
