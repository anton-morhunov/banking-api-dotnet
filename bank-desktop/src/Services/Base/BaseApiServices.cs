using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    }
}
