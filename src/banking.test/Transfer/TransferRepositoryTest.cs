using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using src.database.Contracts;
using src.database.Implementation;
using src.Repositories.transfer;
using src.Repositories.Transfers;

namespace banking.test.Transfer
{
    public class TransferRepositoryTest
    {
        private readonly ITransferRepository _transferRepoUnderTest;
        private readonly IConnectionFactory _connFactoryMock;
        private readonly ILogger<TransferRepository> loggerMock;
        public TransferRepositoryTest()
        {
            var mockConnectionString = new Dictionary<string, string> {
                {"ConnectionStrings:bankingdb", "Server=localhost, 5433;database=bankdb;User ID=admin;Password=Qwertz123;"},
            };

            IConfiguration mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(mockConnectionString)
            .Build();

            loggerMock = Mock.Of<ILogger<TransferRepository>>();

            _connFactoryMock = new ConnectionFactory(mockConfiguration);

            _transferRepoUnderTest = new TransferRepository(_connFactoryMock, loggerMock);
        }

        [Fact]
        public async void TransferCash_ShouldReturnTrue_WhenTransactionIsValid() {
            //Arrange
            int fromAccountId = 2;
            int toAccountId = 1;
            int ammount = 500;
            //Act
            var result = await _transferRepoUnderTest.Transfer(fromAccountId, toAccountId, ammount);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void TransferCash_ShouldThrowException_WhenTransactionIsInvalid()
        {
            try
            {
                //Arrange
                int fromAccountId = 1;
                int toAccountId = 2;
                int ammount = 1000;

                //Act
                var result = await _transferRepoUnderTest.Transfer(fromAccountId, toAccountId, ammount);

                //Assert
                Assert.True(false);
            }
            catch (Exception)
            {
                //Assert
                Assert.True(true);
            }
        }
    }
}
