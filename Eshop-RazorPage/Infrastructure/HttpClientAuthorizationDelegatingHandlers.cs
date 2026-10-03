using System.Net.Http.Headers;

namespace Eshop_RazorPage.Infrastructure
{
    public class HttpClientAuthorizationDelegatingHandlers:DelegatingHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public HttpClientAuthorizationDelegatingHandlers(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(_contextAccessor.HttpContext != null)
            {
                var token = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

                if (!string.IsNullOrWhiteSpace(token) && request.Headers.Authorization == null)
                    //request.Headers.Add("Authorization",token);
                    request.Headers.Authorization = AuthenticationHeaderValue.Parse(token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
