using System.Net;

namespace IMS.Business.Utitlity;

public static class ResponseHelper
{
    //public static OkObjectResult Ok<T>(this T obj, string message = "Executed Successfully!")
    //{
    //    return new OkObjectResult(SuccessResponse<T>.Ok(message, obj));
    //}
}

public class Response<T>
{
    public T Data { get; set; }
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public string StatusMessage { get; set; }
    public dynamic Misc { get; set; }

    public bool IsSuccess()
    {
        return StatusCode == HttpStatusCode.OK || StatusCode == HttpStatusCode.Created;
    }
}