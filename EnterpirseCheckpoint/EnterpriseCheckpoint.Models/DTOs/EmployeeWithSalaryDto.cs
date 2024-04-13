namespace EnterpriseCheckpoint.Models.DTOs
{
    public class EmployeeWithSalaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public float Salary { get; set; } = 0.0f;
        public float SalaryRate { get; set; } = 0.0f;
    }
}
