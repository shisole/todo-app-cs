using FluentValidation;

namespace MyWebApi {
    public class CreateTodo: AbstractValidator<TodoItemDTO> {
        public CreateTodo() {}
    }
}