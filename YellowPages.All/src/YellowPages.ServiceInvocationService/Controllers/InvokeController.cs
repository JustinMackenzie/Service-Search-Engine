using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YellowPages.ServiceInvocationService.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace YellowPages.ServiceInvocationService.Controllers
{
    [Route("api/[controller]")]
    public class InvokeController : Controller
    {
        /// <summary>
        /// The service invoker
        /// </summary>
        private readonly IServiceInvoker serviceInvoker;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvokeController"/> class.
        /// </summary>
        /// <param name="serviceInvoker">The service invoker.</param>
        public InvokeController(IServiceInvoker serviceInvoker)
        {
            this.serviceInvoker = serviceInvoker;
        }

        // POST api/values
        [HttpPost]
        public InvokeServiceResponse Post([FromBody]InvokeServiceRequest value)
        {
            return this.serviceInvoker.InvokeService(value);
        }
    }
}
