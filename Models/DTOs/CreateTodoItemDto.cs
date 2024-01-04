public class CreateTodoItemDto {
    public string? Name { get; set; }
    public bool? IsComplete { get; set; }
    public CreateTodoItemDto() {}
    public CreateTodoItemDto(Todo todoItem) => (Name, IsComplete) = (todoItem.Name, todoItem.IsComplete);
}