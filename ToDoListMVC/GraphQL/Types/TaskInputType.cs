using GraphQL.Types;
using ToDoListMVC.Models.DTO;

namespace ToDoListMVC.GraphQL.Types
{
    public class TaskInputType : InputObjectGraphType<TaskCreationModel>
    {
        public TaskInputType()
        {
            Name = "TaskInput";
            Field(i => i.category_id, type: typeof(IntGraphType));
            Field(i => i.name);
            Field(i => i.due_date, type: typeof(DateTimeGraphType), nullable: true);
        }
    }
}
