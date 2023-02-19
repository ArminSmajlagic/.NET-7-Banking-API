using Microsoft.Extensions.Configuration;
using Npgsql;
using src.database.Contracts;
using src.database.Implementation;
using System.Data;

namespace banking.test.Database
{
    public class DatabaseConnectionTest
    {
        private readonly IConnectionFactory _connectionFactoryUnderTest;
        private IDataAccess _dataAccessUnderTest;
        private readonly IConfiguration _mockConfiguration;
        public DatabaseConnectionTest()
        {
            var mockConnectionString = new Dictionary<string, string> {
                {"ConnectionStrings:bankingdb", "Server=localhost, 5433;database=bankdb;User ID=admin;Password=Qwertz123;"},
            };

            _mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(mockConnectionString)
                .Build();

            _connectionFactoryUnderTest = new ConnectionFactory(_mockConfiguration);
        }

        [Fact]
        public void GetConnectionNamed_ShouldReturnInstanceOfDbConnection_WhenConStringIsValid() {
            //Arange
            string connectionString = "Server = localhost, 5432; database = bankdb; User ID = admin; Password = Qwertz123";
            //Act
            IDbConnection connection = _connectionFactoryUnderTest
                .GetConnectionNamed(connectionName: connectionString);
            //Assert
            Assert.NotNull(connection);

            Assert.IsType<NpgsqlConnection>(connection);
        }

        [Fact]
        public void GetConnection_ShouldReturnInstanceOfDbConnection_WhenConnectionStringIsNotProvided()
        {
            //Arange
            //Act
            IDbConnection connection = _connectionFactoryUnderTest
                .GetConnection();
            //Assert
            Assert.NotNull(connection);

            Assert.IsType<NpgsqlConnection>(connection);
        }

        [Fact]
        public void DataAccess_ShouldHoldInstanceOfTransaction_WhenTransactionIsSetToTrue()
        {
            //Arange
            string connectionString = "Server = localhost, 5432; database = bankdb; User ID = admin; Password = Qwertz123";
            IDbConnection connection = _connectionFactoryUnderTest
                .GetConnectionNamed(connectionName: connectionString,transaction: true);
            bool useTransaction = true;
            //Act
            _dataAccessUnderTest = new DataAccess(connection, useTransaction);
            //Assert
            Assert.NotNull(_dataAccessUnderTest.DbTransaction);
            Assert.IsType<NpgsqlTransaction>(_dataAccessUnderTest.DbTransaction);

        }
    }
}
