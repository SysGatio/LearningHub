namespace SF.App01.Application;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Success()
    {
        return new Result(true, Error.None);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }

}

public class Result<T> : Result
{
    private readonly T _value;

    private Result(bool isSuccess, T value, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    public T Value
    {
        get
        {
            if (!IsSuccess)
            {
                throw new InvalidOperationException("There is no value to return in a failed operation.");
            }

            return _value;
        }
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, Error.None);
    }

    public new static Result<T> Failure(Error error)
    {
        return new Result<T>(false, default!, error);
    }
}

public static class ResultExtensions
{
    public static T Match<T>(
        this Result result,
        Func<T> onSuccess,
        Func<Error, T> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.Error);
    }
}