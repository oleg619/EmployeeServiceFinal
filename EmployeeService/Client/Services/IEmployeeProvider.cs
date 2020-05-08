using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeService.Client.Services
{
    public interface IEmployeeProvider
    {
        Task<List<Employee>> GetAll();
        Task<List<Employee>> Filter(string name, Filter filter);
    }
}