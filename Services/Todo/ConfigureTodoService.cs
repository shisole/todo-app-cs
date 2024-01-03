using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace MyWebApi {
    public static class ConfigureTodoService {
        public static void RegisterTodoService(this WebApplication app) {

            var todoService = new TodoService();
            var todoItems = app.MapGroup("/todoitems");

            todoItems.MapGet("/", todoService.GetAllTodos).WithName("GetAllTodos");

            todoItems.MapGet("/{id}", todoService.GetTodo).WithName("GetTodo");

            todoItems.MapGet("/complete", todoService.GetCompletedTodos).WithName("GetAllCompletedTodos");

            todoItems.MapPost("/", todoService.CreateTodo).WithName("CreateTodo");

            todoItems.MapPut("/{id:int}", todoService.UpdateTodo).WithName("UpdateTodo");

            todoItems.MapDelete("/{id:int}", todoService.DeleteTodo).WithName("DeleteTodo");
        }
    }
}
