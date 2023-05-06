using GraphQL.Types;
using ToDoListMVC.GraphQL.Queries;

namespace ToDoListMVC.GraphQL.Schemas
{
    public class TaskSchema : Schema
    {
        public TaskSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<Query>();
            Mutation = provider.GetRequiredService<Mutation>();
        }
    }
}
