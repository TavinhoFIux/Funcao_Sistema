public class Result
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }

    public Result() { }

    public Result(bool success, string message = "", object data = null)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public static Result SuccessResult(object data = null)
    {
        return new Result(true, data: data);
    }

    public static Result FailureResult(string message)
    {
        return new Result(false, message);
    }
}