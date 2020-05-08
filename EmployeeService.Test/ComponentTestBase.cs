using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using EmployeeService.Client;
using EmployeeService.Client.Pages;
using EmployeeService.Client.Services;
using EmployeeService.Test.Components;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using NSubstitute;

namespace EmployeeService.Test
{
    public class ComponentTestBase
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly IEmployeeProvider _employeeProvider = Substitute.For<IEmployeeProvider>();
        private readonly TestHost _host = new TestHost();

        protected readonly List<Employee> AllEmployees;
        protected readonly List<Employee> FilteredEmployees;
        protected readonly RenderedComponent<Index> Component;

        protected ComponentTestBase()
        {
            AllEmployees = _fixture.CreateMany<Employee>(3).ToList();
            FilteredEmployees = _fixture.CreateMany<Employee>(2).ToList();
            _employeeProvider.GetAll().Returns(AllEmployees);
            _employeeProvider.Filter(Arg.Any<string>(), Arg.Any<Filter>()).Returns(FilteredEmployees);

            _host.AddService(_employeeProvider);
            Component = _host.AddComponent<Index>();
        }

        protected IEnumerable<HtmlNode> GetEmployees() =>
            Component
                .FindAll("tbody")
                .ElementAt(1) // skip modal
                .QuerySelectorAll("tr")
                .ToList();
    }
}