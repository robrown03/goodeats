using GoodEats.CLI.Core;
using GoodEats.CLI.Domain.Providers;
using System.Threading.Tasks;

namespace GoodEats.CLI.Domain.Managers
{
    public interface IFoodTruckManager : IManager
    {
        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <returns></returns>
        Task LoadData();
        
    }
    public class FoodTruckManager : IFoodTruckManager
    {
        private readonly IFoodTruckProvider _foodTruckProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="FoodTruckManager"/> class.
        /// </summary>
        /// <param name="foodTruckProvider">The food truck provider.</param>
        public FoodTruckManager(IFoodTruckProvider foodTruckProvider)
        {
            _foodTruckProvider = foodTruckProvider;
        }
        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <returns></returns>
        public async Task LoadData()
        {
            await _foodTruckProvider.LoadData();

        }
        
    }
}
