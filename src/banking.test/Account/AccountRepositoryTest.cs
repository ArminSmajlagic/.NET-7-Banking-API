using Microsoft.Extensions.Configuration;
using src.database.Contracts;
using src.database.Implementation;
using src.Repositories.Account;

namespace banking.test.Account
{
    public class AccountRepositoryTest
    {
        private readonly IAccountRepository _accRepoUnderTest;
        private readonly IConnectionFactory _connFactoryMock;

        public AccountRepositoryTest()
        {
            var mockConnectionString = new Dictionary<string, string> {
                {"ConnectionStrings:bankingdb", "Server=localhost, 5433;database=bankdb;User ID=admin;Password=Qwertz123;"},
            };

            IConfiguration mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(mockConnectionString)
                .Build();

            _connFactoryMock = new ConnectionFactory(mockConfiguration);

            _accRepoUnderTest = new AccountRepository(_connFactoryMock);
        }

        [Fact]
        public async void A_InsertAccount_ShouldReturnNewId_WhenAccountSuccesfullyInserted()
        {
            //Arrange
            var account = new src.Models.Account.Account() { password = "default", username="default"};
            //Act
            var accountId = await _accRepoUnderTest.Insert(account);
            var insertedAccountIsNotNull = await _accRepoUnderTest.Find(accountId);
            //Assert
            Assert.NotNull(insertedAccountIsNotNull);
        }
        [Fact]
        public async void B_GetAllAccounts_ShouldReturnAccounts_WhenAccountsExist()
        {
            //Arrange
            //No aranging neccessary
            //Act
            var listOfAccountsIsNotNullOrEmpty = await _accRepoUnderTest.FindAll();
            //Assert
            Assert.NotNull(listOfAccountsIsNotNullOrEmpty);
            Assert.NotEmpty(listOfAccountsIsNotNullOrEmpty);
        }
        [Fact]
        public async void C_FindAccountById_ShouldReturnAccount_WhenAccountWithGivenIdExists()
        {
            //Arrange
            var accounts = await _accRepoUnderTest.FindAll();
            var id = accounts.Last().id;
            //Act
            var insertedAccountIsNotNull = await _accRepoUnderTest.Find(id);
            //Assert
            Assert.NotNull(insertedAccountIsNotNull);
        }
        [Fact]
        public async void D_PatchAccount_ShouldReturnAccount_WhenAccountWithGivenIdExists()
        {
            //Arrange
            var accounts = await _accRepoUnderTest.FindAll();
            var id = accounts.Last().id;
            var account = new src.Models.Account.Account() { id= id, password = "newValue", username = "newValue" };
            //Act
            var expectedToBeTrue = await _accRepoUnderTest.Update(account);
            var updatedAccountHasNewValues = await _accRepoUnderTest.Find(id);
            //Assert
            Assert.True(expectedToBeTrue);
            Assert.Equal("newValue", updatedAccountHasNewValues.password);
            Assert.Equal("newValue", updatedAccountHasNewValues.username);
        }
        [Fact]
        public async void E_DeleteAccount_ShouldReturnNewId_WhenAccountSuccesfullyInserted()
        {
            //Arrange
            var accounts = await _accRepoUnderTest.FindAll();
            var id = accounts.Last().id;
            //Act
            var expectedToBeTrue = await _accRepoUnderTest.Delete(id);
            var insertedAccount = await _accRepoUnderTest.Find(id);
            //Assert
            Assert.Null(insertedAccount);
            Assert.True(expectedToBeTrue);
        }
    }
}
