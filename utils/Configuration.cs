using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MyWebApi;

namespace MyWebApi {
    public static class Configuration {
        public static void RegisterApplicationServices(this WebApplicationBuilder builder) {
            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"))
                .AddDbContext<UserDb>(opt => opt.UseInMemoryDatabase("UserList"))
                .AddDatabaseDeveloperPageExceptionFilter()
                .AddAutoMapper(typeof(MappingConfig))
                .AddValidatorsFromAssemblyContaining<Program>()
                ;
        }

        public static void RegisterMiddlewares (this WebApplication app) {
            if (app.Environment.IsDevelopment()) {
                app
                    .UseSwagger()
                    .UseSwaggerUI();
            }

            app.UseHttpsRedirection();

        }

        public static void ConfigureServices(this WebApplication app) {
            // Todo service
            app.RegisterTodoService();
            app.RegisterUserService();
        }

    }
}
