using System.Net;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace MyWebApi {
    public class TodoService {
        public async Task<IResult> GetAllTodos(TodoDb db)
        {
            ApiResponse response = new ApiResponse {
                Result = await db.Todos.Select(todo => new TodoItemDTO(todo)).ToArrayAsync(),
                StatusCode = HttpStatusCode.OK
            };
            
            return TypedResults.Ok(response);
        }

        public async Task<IResult> GetTodo(IMapper _mapper, int id, TodoDb db)
        {
            ApiResponse response = new ApiResponse {};
            var todoResponse = await db.Todos.FindAsync(id);
            var hasTodo = todoResponse is not null;

            if (!hasTodo) {
                response.StatusCode = HttpStatusCode.NotFound;
                response.IsSuccess = false;
                return TypedResults.NotFound(response);
            }

            Todo todo = _mapper.Map<Todo>(todoResponse);

            response.Result = todo;
            response.StatusCode = HttpStatusCode.OK;
            return TypedResults.Ok(response);
        }

        public async Task<IResult> GetCompletedTodos(TodoDb db)
        {

            ApiResponse response = new ApiResponse {
                Result = await db.Todos.Where(todo => todo.IsComplete)
                            .Select((todo) => new TodoItemDTO(todo))
                            .ToListAsync(),
                StatusCode = HttpStatusCode.OK
            };
            return TypedResults.Ok(response);
        }

        public async Task<IResult> CreateTodo(IValidator <TodoItemDTO> _validation, IMapper _mapper, TodoItemDTO todoItemDto, TodoDb db)
        {
            ApiResponse response = new ApiResponse {};
            var validatedResult = await _validation.ValidateAsync(todoItemDto);
            if (!validatedResult.IsValid) {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add(validatedResult.Errors.FirstOrDefault()!.ToString());
                return Results.BadRequest(response);
            }

            Todo todo = _mapper.Map<Todo>(todoItemDto);

            db.Todos.Add(todo);

            await db.SaveChangesAsync();

            TodoItemDTO todoItem = _mapper.Map<TodoItemDTO>(todo);

            response.Result = todoItem;
            response.StatusCode = HttpStatusCode.Created;

            return TypedResults.Ok(response);
            // return TypedResults.Created($"/todoitems/{todoItem.Id}", response);
            // return TypedResults.CreatedAtRoute("GetTodo", new { id = todoItem.Id }, todoItem);
        }

        public async Task<IResult> UpdateTodo(int id, TodoItemDTO inputTodoItemDto, TodoDb db)
        {
            ApiResponse response = new ApiResponse {};
            var todo = await db.Todos.FindAsync(id);
            if (todo is null) {
                response.StatusCode = HttpStatusCode.NotFound;
                return TypedResults.NotFound(response);
            }

            todo.Name = inputTodoItemDto.Name;
            todo.IsComplete = inputTodoItemDto.IsComplete;

            await db.SaveChangesAsync();

            response.StatusCode = HttpStatusCode.Created;
            response.Result = todo;

            return TypedResults.Ok(response);
        }

        public async Task<IResult> DeleteTodo(int id, TodoDb db)
        {
            ApiResponse response = new ApiResponse {};
            var todo = await db.Todos.FindAsync(id);
            if (todo is null) {
                response.StatusCode = HttpStatusCode.NotFound;
                return TypedResults.NotFound(response);
            }

            db.Todos.Remove(todo);

            await db.SaveChangesAsync();

            response.Result = todo;
            response.StatusCode = HttpStatusCode.OK;

            return TypedResults.Ok(response);
        }

    }
}