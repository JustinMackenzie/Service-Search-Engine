using System.Text.Encodings.Web;
using YellowPages.Core.Entities;

namespace YellowPages.Infrastructure.CloudantRepositories
{
    public class CloudantOrganizationRepository : BaseCloudantRepository<Organization>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CloudantOrganizationRepository"/> class.
        /// </summary>
        /// <param name="creds">The creds.</param>
        /// <param name="urlEncoder">The URL encoder.</param>
        public CloudantOrganizationRepository(Creds creds, UrlEncoder urlEncoder) : base(creds, urlEncoder)
        {
        }

        /// <summary>
        /// Gets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        protected override string DatabaseName => @"organizations";

        /// <summary>
        /// Creates the post request.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        protected override PostRequest CreatePostRequest(Organization item)
        {
            return new OrganizationPostRequest(item);
        }
    }
}
