namespace FindExpert.UI.Domain.Utils;

public class OperationResult
{
    public bool IsSuccess { get; protected init; }
    public string? ErrorMessage { get; protected set; }
    public static OperationResult Success() => new() { IsSuccess = true };
    public static OperationResult Error(string code) => new() { IsSuccess = false, ErrorMessage = code };
}

public sealed class OperationResult<T>:OperationResult
{
    public T? Value { get; }

    private OperationResult(T value)
    {
        IsSuccess = true;
        Value = value;
    }

    private OperationResult(string message)
    {
        IsSuccess = false;
        ErrorMessage = message;
    }

    public static OperationResult<T> Success(T value) => new(value);
    public new static OperationResult<T> Error(string code) => new(code);
}