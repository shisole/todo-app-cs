using FluentValidation;

namespace MyWebApi {
    public class CreateTodo: AbstractValidator<CreateTodoItemDto> {
        public CreateTodo() {
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.IsComplete).NotNull();
        }
    }
}