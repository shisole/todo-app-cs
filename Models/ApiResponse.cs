using System.Net;

namespace MyWebApi {
    public class ApiResponse {
        public ApiResponse() {
            ErrorMessages = new List<string>();
            Result = new { };
            IsSuccess = true;
        }

        public bool IsSuccess { get; set; }
        public Object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}