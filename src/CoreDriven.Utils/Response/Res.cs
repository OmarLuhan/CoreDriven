using System.Net;

namespace CoreDriven.Utils.Response;

public class Res<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public string? Code { get; set; } 

    public static Res<T> Ok(T data, string message = "successful operation") => new()
    {
        Success = true,
        Message = message,
        Data = data,
        Code = null
    };

    public static Res<T?> Fail(string message = "operation error",string code="500") => new()
    {
        Success = false,
        Message = message,
        Data = default,
        Code = code
    };
}