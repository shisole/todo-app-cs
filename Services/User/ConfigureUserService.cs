namespace MyWebApi {
    public static class ConfigureUserService {
        public static void RegisterUserService(this WebApplication app) {
            var userService = new UserService();
            var users = app.MapGroup("/api/users");

            users.MapGet("/", userService.GetAllUsers)
                .WithName("GetAllUsers")
                .Produces<ApiResponse>(200);

            users.MapPost("/create", userService.CreateUser)
                .WithName("CreateUser")
                .Produces<ApiResponse>(201);
        }
    }
}