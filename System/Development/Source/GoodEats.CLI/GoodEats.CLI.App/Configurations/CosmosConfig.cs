namespace GoodEats.CLI.Configurations
{
    public class CosmosConfig
    {
        /// <summary>
        /// Gets or sets the account key.
        /// </summary>
        /// <value>
        /// The account key.
        /// </value>
        public string AccountKey { get; set; }
        /// <summary>
        /// Gets or sets the endpoint URI.
        /// </summary>
        /// <value>
        /// The endpoint URI.
        /// </value>
        public string EndpointUri { get; set; }
        /// <summary>
        /// Gets or sets the name of the cosmos database.
        /// </summary>
        /// <value>
        /// The name of the cosmos database.
        /// </value>
        public string DbName { get; set; }
        /// <summary>
        /// Gets or sets the name of the cosmos database container.
        /// </summary>
        /// <value>
        /// The name of the cosmos database container.
        /// </value>
        public string DbContainerName { get; set; }
    }
}
