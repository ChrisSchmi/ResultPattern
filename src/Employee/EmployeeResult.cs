using ResultPatternDemo.Common;

namespace ResultPatternDemo.Employee
{
    // Result MIT Rückgabewert (Employee) -> in ToActionResult(): 200 + Payload
    public sealed class EmployeeResult : BaseResult<Employee, EmployeeError>
    {
        private EmployeeResult(Employee employee) : base(employee) { }
        private EmployeeResult(EmployeeError error) : base(error) { }

        public static EmployeeResult Success(Employee employee) => new(employee);
        public static EmployeeResult Fail(EmployeeError error) => new(error);
    }

    // Result OHNE Rückgabewert (z.B. Delete) -> in ToActionResult(): 204 NoContent
    public sealed class EmployeeDeleteResult : BaseResult<EmployeeError>
    {
        private EmployeeDeleteResult() : base() { }
        private EmployeeDeleteResult(EmployeeError error) : base(error) { }

        public static EmployeeDeleteResult Success() => new();
        public static EmployeeDeleteResult Fail(EmployeeError error) => new(error);
    }
}
