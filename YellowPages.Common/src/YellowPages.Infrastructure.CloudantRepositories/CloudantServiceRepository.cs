using System.Text.Encodings.Web;
using YellowPages.Core.Entities;
using YellowPages.Core.Repositories;

namespace YellowPages.Infrastructure.CloudantRepositories
{
    public class CloudantServiceRepository : BaseCloudantRepository<Service>, IServiceRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CloudantServiceRepository"/> class.
        /// </summary>
        /// <param name="creds">The creds.</param>
        /// <param name="urlEncoder">The URL encoder.</param>
        public CloudantServiceRepository(Creds creds, UrlEncoder urlEncoder) : base(creds, urlEncoder)
        {
        }

        /// <summary>
        /// Gets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        protected override string DatabaseName => @"services";

        /// <summary>
        /// Creates the post request.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        protected override PostRequest CreatePostRequest(Service item)
        {
            return new PostServiceRequest(item);
        }
    }
}
