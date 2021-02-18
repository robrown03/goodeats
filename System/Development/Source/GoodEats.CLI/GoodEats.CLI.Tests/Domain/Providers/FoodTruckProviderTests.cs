using GoodEats.CLI.Domain.Entities;
using GoodEats.CLI.Domain.Managers;
using GoodEats.CLI.Domain.Providers;
using GoodEats.CLI.Domain.Repositories;
using GoodEats.CLI.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodEats.CLI.Tests.Domain.Providers
{
    [TestClass]
    public class FoodTruckProviderTests
    {
       
        [TestMethod(), TestCategory(TestCategories.Unit)]
        public async Task FoodTruckProvider_LoadData_CsvRepository_Delegates()
        {
            // arrange
            var scope = new DefaultScope();

            // act
            await scope.InstanceUnderTest.LoadData();

            // assert
            scope.CsvRepositoryMock.Verify(x => x.GetCsvData(), Times.Exactly(1));
        }

        [TestMethod(), TestCategory(TestCategories.Unit)]
        public async Task FoodTruckProvider_LoadData_CosmosDbRepository_Delegates_Expected()
        {
            // arrange
            var scope = new DefaultScope();

            scope.CsvRepositoryMock.Setup(x => x.GetCsvData())
                .Returns(scope.TestEntities);

            // act
            await scope.InstanceUnderTest.LoadData();

            // assert
            scope.CosmosDbRepositoryMock.Verify(x => x.UpsertItem(It.Is<TruckDetailEntity>(y => y.Id == scope.TestEntities[0].Id)), 
                Times.Exactly(1));
            scope.CosmosDbRepositoryMock.Verify(x => x.UpsertItem(It.Is<TruckDetailEntity>(y => y.Id == scope.TestEntities[1].Id)),
                Times.Exactly(1));
        }


        private class DefaultScope: TestScope<IFoodTruckProvider>
        {
            public DefaultScope()
            {
                TestEntities = new List<TruckDetailEntity>
                {
                      new TruckDetailEntity
                      {
                          LocationId = 1,
                          Applicant = "Test 01",
                          Latitude = 37.72695,
                          Longitude = -122.49977
                      },
                      new TruckDetailEntity
                      {
                          LocationId = 2,
                          Applicant = "Test 02",
                          Latitude = 37.72695,
                          Longitude = -122.49977
                      }
                };

                InstanceUnderTest = new FoodTruckProvider(CsvRepositoryMock.Object,
                   CosmosDbRepositoryMock.Object);
            }

            public List<TruckDetailEntity> TestEntities { get; }
            public Mock<ICsvRepository> CsvRepositoryMock { get; } = new Mock<ICsvRepository>();
            public Mock<ICosmosDbRepository> CosmosDbRepositoryMock { get; } = new Mock<ICosmosDbRepository>();
        }
    }
}
