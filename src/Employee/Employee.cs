namespace ResultPatternDemo.Employee
{
    public class Employee
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }

    // In der Realität automatisch aus ErrorMessages.resx generiert
    public static class ErrorMessages
    {
        public static string EmployeeNotFoundWithId => "Der Mitarbeiter mit der ID '{0}' wurde nicht gefunden.";
        public static string EmployeeAlreadyExists => "Ein Mitarbeiter mit dieser E-Mail-Adresse existiert bereits.";
        public static string EmployeeInvalidSalary => "Das Gehalt '{0}' ist ungültig.";
    }
}
