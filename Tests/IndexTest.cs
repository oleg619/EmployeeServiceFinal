using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeService.Client.Pages;
using EmployeeService.Test.Components;
using Fizzler.Systems.HtmlAgilityPack;
using FluentAssertions;
using HtmlAgilityPack;
using Xunit;

namespace EmployeeService.Test
{
    public class IndexComponentTest : ComponentTestBase
    {
        [Fact]
        public void ShouldShowAllEmployees()
        {
            var htmlNodes = GetEmployees();

            htmlNodes.Should().HaveCount(AllEmployees.Count);
        }

        [Fact]
        public async Task ShouldShowEmployeesWithFilter()
        {
            var input = Component.Find("div.input-group.mb-auto > input");
            var button = Component.Find("div.input-group.mb-auto > button");

            await input.ChangeAsync("name");
            await button.ClickAsync();

            var employees = GetEmployees();
            employees.Should().HaveCount(FilteredEmployees.Count);
        }

        [Fact]
        public async Task ShouldShowModalAfterClick()
        {
            var employee = GetEmployees().First();
            await employee.ClickAsync();
            
            //body > app > div.main > div > div.modal.Show

            var modals = Component.FindAll("div.modal");
            modals.Should().HaveCount(1);
            modals[0].GetAttributeValue("tabindex", "").Should().Be("-1");
            modals[0].GetAttributeValue("class", "").Should().Be("modal Show");
        }
    }
}