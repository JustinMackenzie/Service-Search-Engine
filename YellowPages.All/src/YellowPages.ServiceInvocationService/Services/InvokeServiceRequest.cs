using System;
using System.Collections.Generic;

namespace YellowPages.ServiceInvocationService.Services
{
    public class InvokeServiceRequest
    {
        /// <summary>
        /// Gets or sets the service identifier.
        /// </summary>
        /// <value>
        /// The service identifier.
        /// </value>
        public Guid ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        /// <value>
        /// The input.
        /// </value>
        public Dictionary<string, object> Input { get; set; }
    }
}