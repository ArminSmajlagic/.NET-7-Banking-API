using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using src.Controllers;
using src.database.Contracts;
using src.database.Implementation;
using src.Repositories.Account;
using src.Repositories.transfer;
using src.Repositories.Transfers;
using src.Requests;

namespace banking.test.Controller
{
    public class AccountControllerTest
    {
        AccountController _controllerUnderTest;

        IConnectionFactory _connFactoryMock;
        ILogger<TransferRepository> _loggerMock;

        ITransferRepository _mockTransferRepo;
        IAccountRepository _mockAccountRepo;
        public AccountControllerTest()
        {
            var mockConnectionString = new Dictionary<string, string> {
                {"ConnectionStrings:bankingdb", "Server=localhost, 5433;database=bankdb;User ID=admin;Password=Qwertz123;"},
            };

            IConfiguration mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(mockConnectionString)
            .Build();

            _loggerMock = Mock.Of<ILogger<TransferRepository>>();

            _connFactoryMock = new ConnectionFactory(mockConfiguration);

            _mockTransferRepo = new TransferRepository(_connFactoryMock, _loggerMock);

            _mockAccountRepo = new AccountRepository(_connFactoryMock);

            _controllerUnderTest = new AccountController(_mockAccountRepo, _mockTransferRepo);
        }

        [Fact]
        public async void GetAccountById_ShouldReturnOkWithValueOfAccount_WhenValidIdIsPassed() {
            //Arrange
            int id = 1;
            //Act
            var respons = await _controllerUnderTest.GetAccountById(id);
            //Assert
            Assert.NotNull(respons.Result);
            Assert.IsType<OkObjectResult>(respons.Result);

            if(respons.Result is OkObjectResult result) {
                Assert.NotNull(result.Value);
                Assert.IsType<src.Models.Account.Account>(result.Value);
            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void InsertAccount_ShouldReturnOkWithValueOfNewAccountId_WhenValidRequestIsPassed()
        {
            //Arrange
            UpsertAccountRequest request = new UpsertAccountRequest() { username = "test", password = "test" };
            //Act
            var respons = await _controllerUnderTest.InsertAccount(request);
            //Assert
            Assert.NotNull(respons.Result);
            Assert.IsType<OkObjectResult>(respons.Result);

            if (respons.Result is OkObjectResult result)
            {
                Assert.NotNull(result.Value);
                Assert.Contains("Account was succesfully inserted", result.Value.ToString());
            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void DeleteAccount_ShouldReturnOkWithValueOfAccounts_WhenValidIdIsPassed()
        {
            //Arrange
            var list = await _mockAccountRepo.FindAll();
            int id = list.Last().id;
            //Act
            var respons = await _controllerUnderTest.DeleteAccount(id);
            //Assert
            Assert.NotNull(respons.Result);
            Assert.IsType<OkObjectResult>(respons.Result);

            if (respons.Result is OkObjectResult result)
            {
                Assert.NotNull(result.Value);
                Assert.IsAssignableFrom<bool>(result.Value);
                Assert.True((bool)result.Value);
            }
            else
            {
                Assert.True(false);
            }
        }
    }
}
