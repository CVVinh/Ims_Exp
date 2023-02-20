using System.Net;

namespace IMS_Example.Response
{
    public class ApiResponse<T>
    {
        public HttpStatusCode Status { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public int ToTalPage { get; set; }
    }
}
