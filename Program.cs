using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Todos tutorial from MS");

var todoItems = app.MapGroup("/todoitems");

/**
    NEED SEPARATION OF CONCERNS, idk where to place these static funcs
*/
static async Task<IResult> GetAllTodos(TodoDb db) {
    return TypedResults.Ok(await db.Todos.Select(todo => new TodoItemDTO(todo)).ToArrayAsync());
};

static async Task<IResult> GetTodo(int id, TodoDb db) {
    return await db.Todos.FindAsync(id) is Todo todo ? TypedResults.Ok(todo) : TypedResults.NotFound();
};

static async Task<IResult> GetCompletedTodos(TodoDb db) {
    return TypedResults.Ok(await db.Todos.Where(todo => todo.IsComplete)
        .Select((todo) => new TodoItemDTO(todo))
        .ToListAsync());
};

static async Task<IResult> CreateTodo(TodoItemDTO todoItemDto, TodoDb db) {
    var todoItem = new Todo {
        IsComplete = todoItemDto.IsComplete,
        Name = todoItemDto.Name
    };

    db.Todos.Add(todoItem);

    await db.SaveChangesAsync();

    todoItemDto = new TodoItemDTO(todoItem);

    return TypedResults.Created($"/todoitems/{todoItem.Id}", todoItem);
};

static async Task<IResult> UpdateTodo(int id, TodoItemDTO inputTodoItemDto, TodoDb db) {
    var todo = await db.Todos.FindAsync(id);
    if (todo is null) return TypedResults.NotFound();

    todo.Name = inputTodoItemDto.Name;
    todo.IsComplete = inputTodoItemDto.IsComplete;

    await db.SaveChangesAsync();
    
    return TypedResults.NoContent();
};

static async Task<IResult> DeleteTodo(int id, TodoDb db) {
    var todo = await db.Todos.FindAsync(id);
    if (todo is null) return TypedResults.NotFound();

    db.Todos.Remove(todo);

    await db.SaveChangesAsync();

    return TypedResults.Ok(todo);
}


/**
    Gotta have a way to utilize GroupMap
*/
todoItems.MapGet("/", GetAllTodos);

todoItems.MapGet("/{id:int}", GetTodo);

todoItems.MapGet("/complete", GetCompletedTodos);

todoItems.MapPost("/", CreateTodo);

todoItems.MapPut("/{id}", UpdateTodo);

todoItems.MapDelete("/{id}", DeleteTodo);



app.Run($"http://localhost:{port}");
