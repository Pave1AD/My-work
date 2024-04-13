namespace EnterpriseCheckpoint.Models.Models
{
    public class SalaryRate : BaseEntity
    {
        public float Rate { get; set; } = 0.0f;
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
    }
}
