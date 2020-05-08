using System;

namespace EmployeeService.Client
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string UserPrincipalName { get; set; }

        public string Mail { get; set; }

        public string JobTitle { get; set; }

        public string Department { get; set; }

        public string MobilePhone { get; set; }

        public string OfficePhone { get; set; }
    }
}