namespace Dashboard.API.Domain.ResultErrorDomain
{
    public record Error(string Title, string Description, ErrorType Type)
    {
        public static readonly Error None =
            new(string.Empty, string.Empty, ErrorType.None);
        //static object representing no error
        // returned when response is success and we will return the proper data type (not an error)
        // we made this static readonly method to create specific error type(no input here)

        public static Error Failure(string title, string description) =>
            new(title, description, ErrorType.Failure);
        //return an instance of Error with Failure type(general errors like 500 or unknown...)
        //title is extracted from the ProblemDetails types defined,
        // description is input from the developer

        public static Error NotFound(string title, string description) =>
            new(title, description, ErrorType.NotFound);

        public static Error Conflict(string title, string description) =>
            new(title, description, ErrorType.Conflict);

        public static Error TooManyRequests(string title, string description) =>
            new(title, description, ErrorType.TooManyRequests);
    }
}