using System;

namespace EFCoreModelApp
{
    public class Employee
    {
        public int EmployeeID { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HireDate { get; set; }
        public int DepartmentID { get; set; } // Foreign Key
        public Department Department { get; set; } // Navigation Property
    }
}
