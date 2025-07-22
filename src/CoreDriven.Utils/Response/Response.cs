using System.Net;

namespace CoreDriven.Utils.Response;

public class Response<T>
{
    public HttpStatusCode Status { get; set; }
    public T? Data { get; set; }
    public bool Success { get; set; } = false;
    public string? Message { get; set; }
}