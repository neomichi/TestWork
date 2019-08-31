using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Cinema.Data.Services;

namespace Cinema.Web.Filters
{
    public class CustomActionAttribute : Attribute, IActionFilter
    {
        private ISeanseService _seanseService;

        public CustomActionAttribute(ISeanseService seanseService)
        {
            _seanseService = seanseService;
        }
     
        public bool AllowMultiple => throw new NotImplementedException();

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            throw new NotImplementedException();
        }
    }

}

