using System.Net;

namespace TraineeDemo.Models
{
    public class ResponseDto
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public string Message { get; set; } = string.Empty;//error

        public object Result { get; set; } = default!;//no error-desired output
    }
}
