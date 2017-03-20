using System;
using YellowPages.Core.Entities;

namespace YellowPages.Infrastructure.CloudantRepositories
{
    public class OrganizationPostRequest : PostRequest
    {
        /// <summary>
        /// The organization
        /// </summary>
        private readonly Organization organization;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationPostRequest"/> class.
        /// </summary>
        /// <param name="organization">The organization.</param>
        public OrganizationPostRequest(Organization organization)
        {
            this.organization = organization;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id => organization.Id;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => organization.Name;

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description => organization.Description;
    }
}