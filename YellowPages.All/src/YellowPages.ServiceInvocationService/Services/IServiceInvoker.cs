namespace YellowPages.ServiceInvocationService.Services
{
    public interface IServiceInvoker
    {
        /// <summary>
        /// Invokes the service.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        InvokeServiceResponse InvokeService(InvokeServiceRequest request);
    }
}
