using GoodEats.CLI.Core;
using GoodEats.CLI.Domain.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodEats.CLI.Operations
{
    public class LoadDataOperation : IOperation
    {
        private readonly IFoodTruckManager _foodTruckManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadDataOperation"/> class.
        /// </summary>
        /// <param name="foodTruckManager">The food truck manager.</param>
        public LoadDataOperation(IFoodTruckManager foodTruckManager)
        {
            _foodTruckManager = foodTruckManager;
        }
        /// <summary>
        /// Gets the type of the operation.
        /// </summary>
        /// <value>
        /// The type of the operation.
        /// </value>
        public OperationType OperationType => OperationType.LoadData;

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            Console.WriteLine("Running Load data operation");
            try
            { 
                await _foodTruckManager.LoadData();
                Console.WriteLine("Operaton complete");
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }
            catch (InvalidOperationException invEx)
            {
                Console.WriteLine(invEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
