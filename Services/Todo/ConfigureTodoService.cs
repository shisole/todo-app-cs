namespace MyWebApi {
    public static class ConfigureTodoService {
        public static void RegisterTodoService(this WebApplication app) {

            var todoService = new TodoService();
            var todoItems = app.MapGroup("/todoitems");

            todoItems.MapGet("/", todoService.GetAllTodos)
                .WithName("GetAllTodos")
                .Produces<ApiResponse>(200);

            todoItems.MapGet("/{id}", todoService.GetTodo)
                .WithName("GetTodo")
                .Produces<ApiResponse>(200)
                .Produces<ApiResponse>(404);

            todoItems.MapGet("/complete", todoService.GetCompletedTodos)
                .WithName("GetAllCompletedTodos")
                .Produces<ApiResponse>(200);

            todoItems.MapPost("/", todoService.CreateTodo)
                .WithName("CreateTodo")
                .Produces<ApiResponse>(201)
                .Produces<ApiResponse>(400);

            todoItems.MapPut("/{id:int}", todoService.UpdateTodo)
                .WithName("UpdateTodo")
                .Produces<ApiResponse>(201)
                .Produces<ApiResponse>(404);

            todoItems.MapDelete("/{id:int}", todoService.DeleteTodo)
                .WithName("DeleteTodo")
                .Produces<ApiResponse>(200)
                .Produces<ApiResponse>(404);
        }
    }
}
