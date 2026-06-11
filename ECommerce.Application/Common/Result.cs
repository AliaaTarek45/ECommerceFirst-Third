
using System.Text.Json.Serialization;

namespace ECommerce.Application.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public IReadOnlyList<Error> Errors { get; }

        protected Result(bool isSuccess, IReadOnlyList<Error> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }
        public static Result Ok() => new(true, Array.Empty<Error>());

        public static Result Fail(Error error) => new(false, new[] { error });

        public static Result Fail(IReadOnlyList<Error> errors) => new(false, errors);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue _value;

        public TValue data => IsSuccess ? _value : throw new InvalidOperationException("Cannot access the value of a failed result.");

        private Result(TValue value) : base(true, Array.Empty<Error>())
        {
            _value = value;
        }

        private Result(Error error) : base(false, new[] { error })
        {
            _value = default!;
        }

        private Result(IReadOnlyList<Error> errors) : base(false, errors)
        {
            _value = default!;
        }

        public static Result<TValue> Ok(TValue value) => new(value);
        public static new Result<TValue> Fail(Error error) => new(error);
        public static new Result<TValue> Fail(IReadOnlyList<Error> errors) => new(errors);

        public static implicit operator Result<TValue>(TValue value) => Ok(value);
        public static implicit operator Result<TValue>(Error error) => Fail(error);
    }

    public sealed record Error(string Code, string Description, ErrorType Type = ErrorType.Failure)
    {
        public static Error Failure(string code = "General.Failure", string description = "A general failure has occurred.")
            => new(code, description, ErrorType.Failure);

        public static Error Validation(string code = "General.Validation", string description = "A validation error has occurred.")
            => new(code, description, ErrorType.Validation);

        public static Error NotFound(string code = "General.NotFound", string description = "The requested resource was not found.")
            => new(code, description, ErrorType.NotFound);

        public static Error Conflict(string code = "General.Conflict", string description = "A conflict occurred with the current state.")
            => new(code, description, ErrorType.Conflict);

        public static Error Unauthorized(string code = "General.Unauthorized", string description = "Access is denied due to lack of authorization.")
            => new(code, description, ErrorType.Unauthorized);

        public static Error Forbidden(string code = "General.Forbidden", string description = "The operation is forbidden.")
            => new(code, description, ErrorType.Forbidden);

        public static Error InvalidCredentials(string code = "General.InvalidCredentials", string description = "The provided credentials are invalid.")
            => new(code, description, ErrorType.InvalidCredentials);
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum ErrorType
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        Conflict = 3,
        Unauthorized = 4,
        Forbidden = 5,
        InvalidCredentials = 6,
    }
}
