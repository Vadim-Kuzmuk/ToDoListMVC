using GraphQL.Types;
using ToDoListMVC.Models;

namespace ToDoListMVC.GraphQL.Types
{
    public class TaskType : ObjectGraphType<Tasks>
    {
        public TaskType()
        {
            Field(i => i.id, type: typeof(IdGraphType)).Description("id property from Task");
            Field(i => i.category_id, type: typeof(IntGraphType)).Description("category_id property from Task");
            Field(i => i.name).Description("name property from Task");
            Field(i => i.due_date, type: typeof(DateTimeGraphType), nullable: true).Description("deadline property from Task");
            Field(i => i.is_completed, type: typeof(BooleanGraphType)).Description("is_completed property from Task");
        }
    }
}
