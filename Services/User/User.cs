using  System.Net;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace MyWebApi {
    public class UserService {
        public async Task<IResult> GetAllUsers(UserDb db) {
            ApiResponse response = new ApiResponse {
                Result = await db.Users.Select(user => new UserInfoDto(user)).ToArrayAsync(),
                StatusCode = HttpStatusCode.OK
            };

            return TypedResults.Ok(response);
        }

        public async Task<IResult> CreateUser(IValidator <CreateUserDto> _validation, IMapper _mapper,  UserDb db, CreateUserDto userDetails) {
            ApiResponse response = new ApiResponse {};

            var validatedResult = await _validation.ValidateAsync(userDetails);
            if (!validatedResult.IsValid) {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Errors = validatedResult.Errors;
                response.ErrorMessages.Add(validatedResult.Errors.FirstOrDefault()!.ToString());
                return TypedResults.BadRequest(response);
            }
            
            User user = _mapper.Map<User>(userDetails);

            db.Users.Add(user);
            
            await db.SaveChangesAsync();

            UserInfoDto createdUser = _mapper.Map<UserInfoDto>(user);

            response.Result = createdUser;
            response.StatusCode = HttpStatusCode.Created;

            return TypedResults.Ok(response);

        }
    }
}