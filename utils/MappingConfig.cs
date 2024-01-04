using AutoMapper;

namespace MyWebApi {
    public class MappingConfig : Profile {
        public MappingConfig() {
             CreateMap<Todo, TodoItemDto>().ReverseMap();
             CreateMap<Todo, CreateTodoItemDto>().ReverseMap();
             CreateMap<User, UserInfoDto>().ReverseMap();
             CreateMap<User, CreateUserDto>().ReverseMap();
        }
    }
}