using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace MyWebApi {
    public class TodoService {
        public async Task<IResult> GetAllTodos(TodoDb db)
        {
            return TypedResults.Ok(await db.Todos.Select(todo => new TodoItemDTO(todo)).ToArrayAsync());
        }

        public async Task<IResult> GetTodo(int id, TodoDb db)
        {
            return await db.Todos.FindAsync(id) is Todo todo ? TypedResults.Ok(todo) : TypedResults.NotFound();
        }

        public async Task<IResult> GetCompletedTodos(TodoDb db)
        {
            return TypedResults.Ok(await db.Todos.Where(todo => todo.IsComplete)
                .Select((todo) => new TodoItemDTO(todo))
                .ToListAsync());
        }

        public async Task<IResult> CreateTodo(IMapper _mapper, TodoItemDTO todoItemDto, TodoDb db)
        {

            Todo todo = _mapper.Map<Todo>(todoItemDto);

            db.Todos.Add(todo);

            await db.SaveChangesAsync();

            TodoItemDTO todoItem = _mapper.Map<TodoItemDTO>(todo);

            return TypedResults.Created($"/todoitems/{todoItem.Id}", todoItem);
            // return TypedResults.CreatedAtRoute("GetTodo", new { id = todoItem.Id }, todoItem);
        }

        public async Task<IResult> UpdateTodo(int id, TodoItemDTO inputTodoItemDto, TodoDb db)
        {
            var todo = await db.Todos.FindAsync(id);
            if (todo is null) return TypedResults.NotFound();

            todo.Name = inputTodoItemDto.Name;
            todo.IsComplete = inputTodoItemDto.IsComplete;

            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        public async Task<IResult> DeleteTodo(int id, TodoDb db)
        {
            var todo = await db.Todos.FindAsync(id);
            if (todo is null) return TypedResults.NotFound();

            db.Todos.Remove(todo);

            await db.SaveChangesAsync();

            return TypedResults.Ok(todo);
        }

    }
}