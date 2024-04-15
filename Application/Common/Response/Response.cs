
namespace Application.Common.Response
{
    public class Response<T>
    {
        public List<T>? Data { get; set; }
        public bool Result { get; set; }
        public string? Message { get; set; }
    }
}
