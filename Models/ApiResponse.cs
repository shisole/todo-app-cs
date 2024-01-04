using System.Net;
using FluentValidation.Results;

namespace MyWebApi {
    public class ApiResponse {
        public ApiResponse() {
            ErrorMessages = new List<string> {};
            Result = new { };
            IsSuccess = true;
            Errors = new List<ValidationFailure>();
        }

        public bool IsSuccess { get; set; }
        public Object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> ErrorMessages { get; set; }
        public List<ValidationFailure> Errors { get; internal set; }
    }
}
