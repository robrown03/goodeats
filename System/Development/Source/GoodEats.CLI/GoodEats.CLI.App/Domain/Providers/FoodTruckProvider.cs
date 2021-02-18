using GoodEats.CLI.Core;
using GoodEats.CLI.Domain.Repositories;
using System.Threading.Tasks;

namespace GoodEats.CLI.Domain.Providers
{
    public interface IFoodTruckProvider: IProvider 
    {
        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <returns></returns>
        Task LoadData();

    }
    public class FoodTruckProvider: IFoodTruckProvider
    {
        private readonly ICsvRepository _csvRepository;
        private readonly ICosmosDbRepository _cosmosDbRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FoodTruckProvider"/> class.
        /// </summary>
        /// <param name="csvRepository">The CSV repository.</param>
        /// <param name="cosmosDbRepository">The cosmos database repository.</param>
        public FoodTruckProvider(ICsvRepository csvRepository,
            ICosmosDbRepository cosmosDbRepository)
        {
            _csvRepository = csvRepository;
            _cosmosDbRepository = cosmosDbRepository;
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <returns></returns>
        public async Task LoadData()
        {
            var data = _csvRepository.GetCsvData();
            if (data != null)
            {
                foreach (var entity in data)
                {
                    await _cosmosDbRepository.UpsertItem(entity);
                }
            }            
        }
    }
}
