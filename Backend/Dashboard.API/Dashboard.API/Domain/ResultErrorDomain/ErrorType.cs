namespace Dashboard.API.Domain.ResultErrorDomain
{
    public enum ErrorType
    {
        Failure = 0,/*
        server error 500,
        it might flag that developer did something wrong,or database,connection issues
        => catched by global exception handler and return a generic error message to the client
         */
        NotFound = 1, // 404 Not Found, if the id from the body of update endpoint is not found
        Conflict = 2, // 409 Conflict, if the status is already done we cannot update it to done again
        TooManyRequests = 3, // 429 Too many requests, protect the server from being exploited
        None = 4          // no error here, we add this to satisfy the error object in Result class

        // for this project scope, there is not 401,403 errors
        // for the 400 Bad Request (validation error),it is handled by
        // FluentValidation on the server side
        // Zod on the client side should prevent this from happening in first place
    }
}