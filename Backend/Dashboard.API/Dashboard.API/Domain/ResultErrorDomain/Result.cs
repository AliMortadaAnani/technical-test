namespace Dashboard.API.Domain.ResultErrorDomain
{
    public class Result<T>
    {
        private Result(bool isSuccess, Error error, T value)
        {
            IsSuccess = isSuccess;
            Error = error;
            Value = value;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        public T Value { get; }

        public static Result<T> Success(T value) => new(true, Error.None, value);

        public static Result<T> Failure(Error error) => new(false, error, default!);

        // if we want to return data at 2xx response, we just call this class with the Success static method and passing the response dto (or any response) as parameter, hence the Error value will be empty(none)

        // in case we want to return an error, we just call this class with the Failure static method and passing the Error object as parameter,hence the T value will be empty(default)
    }
}