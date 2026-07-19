namespace ResultPatternDemo.Employee
{
    public class EmployeeService
    {
        public EmployeeResult GetById(string id)
        {
            if (id == "404")
                return EmployeeResult.Fail(EmployeeErrors.NotFound(id));

            var emp = new Employee { Id = int.Parse(id), Name = "Max Mustermann" };
            return EmployeeResult.Success(emp);
        }

        public EmployeeDeleteResult Delete(string id)
        {
            if (id == "404")
                return EmployeeDeleteResult.Fail(EmployeeErrors.NotFound(id));

            return EmployeeDeleteResult.Success();
        }
    }
}
