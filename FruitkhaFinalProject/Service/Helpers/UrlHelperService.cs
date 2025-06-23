using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers
{
    public class UrlHelperService
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlHelperService(IUrlHelperFactory urlHelperFactory, IHttpContextAccessor httpContextAccessor)
        {
            _urlHelperFactory = urlHelperFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public IUrlHelper GetUrlHelper()
        {

            var httpContext = _httpContextAccessor.HttpContext ?? new DefaultHttpContext();

            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };

            return _urlHelperFactory.GetUrlHelper(actionContext);
        }
    }
}
