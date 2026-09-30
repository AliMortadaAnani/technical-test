using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Dashboard.API.Domain.ResultErrorDomain
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProblemDetails404ErrorTypes // Not Found
    {
        JobApplication_NotFound, // JobApplication with the given Id was not found
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProblemDetails409ErrorTypes // Conflict
    {
        JobApplication_AlreadyDone // JobApplication status is already done, cannot update to done again
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProblemDetails429ErrorTypes // Too Many Requests
    {
        RateLimit_Exceeded
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProblemDetails500ErrorTypes // Server Failure
    {
        Server_Error           // Generic Global Handler
    }

    // below classes are used to make Swagger Documentation more descriptive and specific for each error type
    //so we will notice in Swagger UI (in schema section) the specific error type for each error code (404,429,500)

    public class NotFound404ProblemDetails : ProblemDetails
    {
        [JsonPropertyName("type")] // this will appear in the Swagger UI
        public new ProblemDetails404ErrorTypes Type { get; set; }

        [JsonIgnore] // this will be ignored in the Swagger UI
        public new IDictionary<string, object>? Extensions { get; }
    }

    public class Conflict409ProblemDetails : ProblemDetails
    {
        [JsonPropertyName("type")]
        public new ProblemDetails409ErrorTypes Type { get; set; }

        [JsonIgnore]
        public new IDictionary<string, object>? Extensions { get; }
    }

    public class TooManyRequests429ProblemDetails : ProblemDetails
    {
        [JsonPropertyName("type")]
        public new ProblemDetails429ErrorTypes Type { get; set; }

        [JsonIgnore]
        public new IDictionary<string, object>? Extensions { get; }
    }

    public class ServerError500ProblemDetails : ProblemDetails
    {
        [JsonPropertyName("type")]
        public new ProblemDetails500ErrorTypes Type { get; set; }

        [JsonIgnore]
        public new IDictionary<string, object>? Extensions { get; }
    }
}