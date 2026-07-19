using ResultPatternDemo.Common;

namespace ResultPatternDemo.Employee
{
    public enum EmployeeErrorType
    {
        None,
        NotFound,
        AlreadyExists,
        InvalidSalary
    }

    public sealed class EmployeeError : BaseError<EmployeeErrorType>
    {
        public string? AffectedEmployeeId { get; init; }

        public EmployeeError(string description, EmployeeErrorType errorType, string? affectedEmployeeId = null)
            : base(description, errorType)
        {
            AffectedEmployeeId = affectedEmployeeId;
        }

        // Einzige Stelle, an der EmployeeErrorType -> ErrorCategory übersetzt wird.
        protected override ErrorCategory MapToCategory(EmployeeErrorType errorType) => errorType switch
        {
            EmployeeErrorType.NotFound      => ErrorCategory.NotFound,
            EmployeeErrorType.AlreadyExists => ErrorCategory.Conflict,
            EmployeeErrorType.InvalidSalary => ErrorCategory.UnprocessableEntity,
            _                                => ErrorCategory.Failure
        };
    }

    public static class EmployeeErrors
    {
        public static EmployeeError NotFound(string id) =>
            new(string.Format(ErrorMessages.EmployeeNotFoundWithId, id), EmployeeErrorType.NotFound, id);

        public static EmployeeError AlreadyExists() =>
            new(ErrorMessages.EmployeeAlreadyExists, EmployeeErrorType.AlreadyExists);

        public static EmployeeError InvalidSalary(decimal value) =>
            new(string.Format(ErrorMessages.EmployeeInvalidSalary, value), EmployeeErrorType.InvalidSalary);
    }
}
