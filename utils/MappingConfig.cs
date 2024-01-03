using AutoMapper;

namespace MyWebApi {
    public class MappingConfig : Profile {
        public MappingConfig() {
             CreateMap<Todo, TodoItemDTO>().ReverseMap();
        }
    }
}