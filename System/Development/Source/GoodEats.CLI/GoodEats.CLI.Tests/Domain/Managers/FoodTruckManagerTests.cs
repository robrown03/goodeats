using GoodEats.CLI.Domain.Managers;
using GoodEats.CLI.Domain.Providers;
using GoodEats.CLI.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodEats.CLI.Tests.Domain.Managers
{
    [TestClass]
    public class FoodTruckManagerTests
    {
       
        [TestMethod(), TestCategory(TestCategories.Unit)]
        public async Task FoodTruckManager_LoadData_Delegtes()
        {
            // arrange
            var scope = new DefaultScope();

            // act
            await scope.InstanceUnderTest.LoadData();

            // assert
            scope.FoodTruckProviderMock.Verify(x => x.LoadData(), Times.Exactly(1));
        }
        private class DefaultScope: TestScope<IFoodTruckManager>
        {
            public DefaultScope()
            {

                InstanceUnderTest = new FoodTruckManager(FoodTruckProviderMock.Object);
            }

            public Mock<IFoodTruckProvider> FoodTruckProviderMock { get; } = new Mock<IFoodTruckProvider>();
        }
    }
}
