namespace IMS_Example.Response
{
    public class PaginationResponse<T> : BaseResponse<T>
    {
        public int _totalPages { get; set; }

        public PaginationResponse(bool sucess, string message, T data, int totalPages) : base(sucess, message, data)
        {
            _totalPages = totalPages;
        }
    }
}
