using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeService.Client.Services
{
    public class EmployeeProvider : IEmployeeProvider
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
            {PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

        public EmployeeProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<Employee>> GetAll() => Get();

        public Task<List<Employee>> Filter(string name, Filter filter) =>
            Get(filter == Client.Filter.FirstName ? $"firstName={name}" : $"lastName={name}");

        private async Task<List<Employee>> Get(string action = null)
        {
            var url = "employees";
            if (action != null)
            {
                url += $"?{action}";
            }

            var result = await _httpClient.GetAsync(url);

            var content = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                if (content == "\"\"")
                {
                    return new List<Employee>();
                }

                return JsonSerializer.Deserialize<List<Employee>>(content,
                    _jsonOptions);
            }

            throw new Exception($"Unknown error, status code : {result.StatusCode}, content : {content}");
        }
    }
}