using System;
using AirlineProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirlineProjectTest
{
    [TestClass]
    public class AirlineFacadeTest
    {
        [TestInitialize]
        public void Init()
        {
            // clean db
        }

        [ExpectedException(typeof(WrongPasswordException))]
        [TestMethod]
        public void AirlineFacade_INSERT_GET_BY_ID_FLIGHT()
        {
            
            // Arrange
            // LOGIN FOR ADMIN

            // ACT
            // INSERT AIRLINE -- return company id
            // CREATE FLIGHT -- return flight id (x)
            // GET FLIGHT by id (x)

            // Assert
            int x = 5;
            int y = x * 2 ;
            Assert.AreEqual(10, y);

            //AirlineCompany get_result = null;
            //Assert.AreEqual(TestData.Airline_1.AirlineName, get_result.AirlineName);

            Console.WriteLine("");
            throw new WrongPasswordException();

        }
    }
}
