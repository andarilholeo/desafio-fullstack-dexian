namespace DesafioDexian.Domain.Common;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? Error { get; }
    public ResultErrorCode? ErrorCode { get; }

    protected Result(bool isSuccess, string? error, ResultErrorCode? errorCode)
    {
        IsSuccess = isSuccess;
        Error = error;
        ErrorCode = errorCode;
    }

    public static Result Success() => new(true, null, null);
    public static Result Failure(string error, ResultErrorCode errorCode) => new(false, error, errorCode);

    public static Result<T> Success<T>(T value) => new(value, true, null, null);
    public static Result<T> Failure<T>(string error, ResultErrorCode errorCode) => new(default, false, error, errorCode);
}

public class Result<T> : Result
{
    public T? Value { get; }

    internal Result(T? value, bool isSuccess, string? error, ResultErrorCode? errorCode)
        : base(isSuccess, error, errorCode)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(value, true, null, null);
    public new static Result<T> Failure(string error, ResultErrorCode errorCode) => new(default, false, error, errorCode);

    public static implicit operator Result<T>(T value) => Success(value);
}

public enum ResultErrorCode
{
    NotFound,
    ValidationError,
    Unauthorized,
    Conflict,
    InternalError
}

