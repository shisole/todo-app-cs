using Microsoft.EntityFrameworkCore;
using MyWebApi;

namespace MyWebApi {
    public static class Configuration {
        public static void RegisterApplicationServices(this WebApplicationBuilder builder) {
            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"))
                .AddDatabaseDeveloperPageExceptionFilter()
                .AddAutoMapper(typeof(MappingConfig));
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
        }

    }
}