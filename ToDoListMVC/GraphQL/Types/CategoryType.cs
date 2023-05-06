using GraphQL.Types;
using ToDoListMVC.Models;

namespace ToDoListMVC.GraphQL.Types
{
    public class CategoryType : ObjectGraphType<Categories>
    {
        public CategoryType()
        {
            Field(i => i.id, type: typeof(IdGraphType)).Description("id property from Category");
            Field(i => i.name).Description("name property from Task");
        }
    }
}
