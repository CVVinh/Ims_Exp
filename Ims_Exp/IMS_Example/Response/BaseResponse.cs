namespace IMS_Example.Response
{
    public class BaseResponse<T>
    {
        public bool _sucess { get; set; }
        public string _message { get; set; }
        public T _data { get; set; }

        public BaseResponse (bool sucess, string message, T data)
        {
            _sucess= sucess;
            _message= message;
            _data= data;
        }
    }
}
