using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using bank_desktop.src.Services.Interfaces;
using System.Net.Http.Headers;


namespace bank_desktop.src.Handlers
{
    public class JwtHandler : DelegatingHandler
    {
        private readonly ITokenStorage _tokenStorage;

        public JwtHandler(ITokenStorage tokenStorage)
        {
            _tokenStorage = tokenStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync
            (HttpRequestMessage request, 
            CancellationToken cancellationToken
            )
        {
            string? token = _tokenStorage.GetToken();
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = 
                    new AuthenticationHeaderValue(
                        "Bearer", 
                        token
                        );
            }
            return await base.SendAsync(
                request, 
                cancellationToken
                );
        }
    }
}
