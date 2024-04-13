using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseCheckpoint.Models.Models
{
    [NotMapped]
    public class EmployeeHoursWorked
    {
        public double TotalHoursWorked { get; set; }
    }
}
