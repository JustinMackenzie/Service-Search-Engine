using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        [JsonProperty(PropertyName = "serviceId")]
        public Guid ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        /// <value>
        /// The input.
        /// </value>
        [JsonProperty(PropertyName = "input")]
        public List<InputValue> Input { get; set; }
    }

    public class InputValue
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}