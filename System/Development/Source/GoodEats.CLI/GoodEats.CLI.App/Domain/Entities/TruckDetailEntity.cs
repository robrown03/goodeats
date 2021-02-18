using Microsoft.Azure.Cosmos.Spatial;
using Newtonsoft.Json;

namespace GoodEats.CLI.Domain.Entities
{
    public abstract class CosmosEntityBase
    {
        /// <summary>
        /// Gets the type of the document.
        /// </summary>
        /// <value>
        /// The type of the document.
        /// </value>
        public abstract string DocType { get; }
    }

    public abstract class CosmosGeoBase : CosmosEntityBase
    {
        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public abstract Point Location { get; }
    }
    public class TruckDetailEntity : CosmosGeoBase
    {
        /// <summary>
        /// Gets the type of the document.
        /// </summary>
        /// <value>
        /// The type of the document.
        /// </value>
        [JsonProperty("docType")]
        public override string DocType => "TruckDetail";
        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [JsonProperty("location")]
        public override Point Location => new Point(Longitude, Latitude);
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("id")]
        public string Id => LocationId.ToString();
        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int LocationId { get; set; }
        /// <summary>
        /// Gets or sets the applicant.
        /// </summary>
        /// <value>
        /// The applicant.
        /// </value>
        public string Applicant { get; set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }
        /// <summary>
        /// Gets or sets the food items.
        /// </summary>
        /// <value>
        /// The food items.
        /// </value>
        public string FoodItems { get; set; }
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public double Latitude { get; set; }
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public double Longitude { get; set; }

    }

}
