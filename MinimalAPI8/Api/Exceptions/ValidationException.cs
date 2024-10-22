using FluentValidation.Results;

namespace Api.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationFailure> failures)
        : base("One or more validation failures have occurred.")
    {
        Errors = failures
            .GroupBy(e => e.PropertyName)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.Select(e => e.ErrorMessage).ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}
