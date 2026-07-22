using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace bank_desktop.src.Services.Base
{
    public abstract class BaseApiServices
    {
        protected readonly HttpClient _httpClient;
        protected readonly IConfiguration _configuration;

        protected BaseApiServices(
            HttpClient httpClient, 
            IConfiguration configuration
            )
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        protected async Task<TResponse> PostAsync<TRequest, TResponse>(
            string endpoint, 
            TRequest request
            )
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(endpoint, request);
            
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            response.EnsureSuccessStatusCode();

            TResponse? result = await response.Content.ReadFromJsonAsync<TResponse>();

            return result!;
        }

        protected async Task<TResponse> GetAsync<TResponse>(string endpoint)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
            
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            
            response.EnsureSuccessStatusCode();

            TResponse? result = await response.Content.ReadFromJsonAsync<TResponse>();
            
            return result!;
        }
    }
}
