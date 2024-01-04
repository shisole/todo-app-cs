using FluentValidation;

namespace MyWebApi {
    public class CreateUser : AbstractValidator <CreateUserDto> {
        public CreateUser() {
            RuleFor(model => model.Username).NotEmpty();
            RuleFor(model => model.Password).NotEmpty().MinimumLength(6);
        }
    }
}