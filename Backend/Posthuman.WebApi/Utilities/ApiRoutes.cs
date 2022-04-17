namespace Posthuman.WebApi.Utilities
{
    public static class ApiRoutes
    {
        private const string Root = "api/";

        public static class TodoItems
        {
            public const string EntityName = "TodoItems";
            public const string Base = Root + EntityName;

            public const string GetTodoItems                = Base;
            public const string GetTodoItem                 = Base + "/{id}";
            public const string GetTodoItemsHierarchical    = Base + "/Hierarchical";
            public const string GetTodoItemsByDeadline      = Base + "/ByDeadline/{deadline}";

            public const string CreateTodoItem              = Base;
            public const string UpdateTodoItem              = Base + "/{id}";
            public const string DeleteTodoItem              = Base + "/{id}";
                   
            public const string CompleteTodoItem            = Base + "/{id}";
        }
    }
}
