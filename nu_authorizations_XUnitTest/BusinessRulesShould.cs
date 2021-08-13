using Xunit;
using nu_authorizations.Models;
using nu_authorizations.Actions;
using System.Linq;

namespace nu_authorizations_XUnitTest
{
    public class BusinessRulesShould
    {

        public TransactionRoot inboundTransaction = new TransactionRoot
        {
            transaction = new Transaction
                {
                    amount = 100,
                    merchant = "Samsung",
                    time = System.DateTime.Parse("2019-02-13T11:00:01.000Z")
            }
        };

        [Fact]
        public void ValidateBRA()
        {
            var initialize = new TestData();
            initialize.ListExistingAccount();

            AccountRoot inboundAccount = new AccountRoot
            {
                account = new Account
                {
                    activeCard = true,
                    availableLimit = 100
                },
                violations = new string[] { }
            };

            AccountRoot result = BusinessRules.ValidateBRA(inboundAccount);
            Assert.NotNull(result);
            var alreadyInitialized = result.violations.Contains("account-already-initialized");
            Assert.True(alreadyInitialized);
        }

        [Fact]
        public void AccountNotInitialized()
        {
            AccountRoot result = BusinessRules.ValidateBRT(inboundTransaction);
            Assert.NotNull(result);
            var notInitialized = result.violations.Contains("account-not-initialized");
            Assert.True(notInitialized);

        }

        [Fact]
        public void CardNotActive()
        {
            var initialize = new TestData();
            initialize.ListAccountNotInitialized();

            AccountRoot result = BusinessRules.ValidateBRT(inboundTransaction);
            Assert.NotNull(result);
            var cardNotActive = result.violations.Contains("card-not-active");
            Assert.True(cardNotActive);

        }

        [Fact]
        public void InsufficientLimit()
        {
            var initialize = new TestData();
            initialize.ListExistingAccount();
            initialize.SetLimit();

            AccountRoot result = BusinessRules.ValidateBRT(inboundTransaction);
            Assert.NotNull(result);
            var insufficientLimit = result.violations.Contains("insufficient-limit");
            Assert.True(insufficientLimit);

        }

        [Fact]
        public void HighFrequencySmallInterval()
        {
            var initialize = new TestData();
            initialize.ListExistingAccount();
            initialize.ListPreviousTransactions();

            AccountRoot result = BusinessRules.ValidateBRT(inboundTransaction);
            Assert.NotNull(result);
            var HighFrequencyTransaction = result.violations.Contains("high-frequency-small-interval");
            Assert.True(HighFrequencyTransaction);

        }

        [Fact]
        public void DoubledTransaction()
        {
            var initialize = new TestData();
            initialize.ListExistingAccount();
            initialize.ListPreviousTransactions();

            AccountRoot result = BusinessRules.ValidateBRT(inboundTransaction);
            Assert.NotNull(result);
            var doubleTransaction = result.violations.Contains("double-transaction");
            Assert.True(doubleTransaction);

        }
    }
}
