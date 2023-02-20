namespace IMS_Example.Data.DTOs.UserDTO
{
    public class ApiResponseDTO
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
        public object Token { get; set; }
    }
}
