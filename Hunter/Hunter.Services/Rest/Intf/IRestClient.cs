namespace Hunter.Services.Rest.Intf
{
    /// <summary>
    /// The interface of the client for accessing the REST service.
    /// </summary>
    public interface IRestClient
    {
        /// <summary>
        /// Creates GET request.
        /// </summary>
        /// <param name="url">The URL of the REST service.</param>
        /// <returns>The instance of the <see cref="IRestRequest"/>.</returns>
        IRestRequest Get(string url);

        /// <summary>
        /// Creates POST request.
        /// </summary>
        /// <param name="url">The URL of the REST service.</param>
        /// <returns>The instance of the <see cref="IRestRequest"/>.</returns>
        IRestRequest Post(string url);

        /// <summary>`
        /// Creates PUT request.
        /// </summary>
        /// <param name="url">The URL of the REST service.</param>
        /// <returns>The instance of the <see cref="IRestRequest"/>.</returns>
        IRestRequest Put(string url);

        /// <summary>
        /// Creates DELETE request.
        /// </summary>
        /// <param name="url">The URL of the REST service.</param>
        /// <returns>The instance of the <see cref="IRestRequest"/>.</returns>
        IRestRequest Delete(string url);
    }
}