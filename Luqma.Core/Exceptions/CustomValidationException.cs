using FluentValidation.Results;
using Luqma.Core.ResponseKeys;

namespace Luqma.Core.Exceptions
{
    public class CustomValidationException : Exception
    {
        public Dictionary<string, List<String>> Errors { get; set; }
        public CustomValidationException(IEnumerable<ValidationFailure> failures)
            : base(SharedResponseKeys.ValidationFailed)
        {
            Errors = failures.GroupBy(fail => fail.PropertyName)
            .ToDictionary(map => map.Key, map => map.Select(fail => fail.ErrorMessage).ToList());
        }
    }
}
