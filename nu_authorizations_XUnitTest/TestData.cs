using System;
using System.Collections.Generic;
using nu_authorizations.Models;
using nu_authorizations.Repository;

namespace nu_authorizations_XUnitTest
{
    public class TestData
    {
        public void ListExistingAccount()
        {
            AccountsList.AddAccount(new AccountRoot
            {
                account = new Account
                {
                    activeCard = true,
                    availableLimit = 100
                },
                violations = new string[] { }
            });
        }

        public void ListAccountNotInitialized()
        {
            AccountsList.AddAccount(new AccountRoot
            {
                account = new Account
                {
                    activeCard = false,
                    availableLimit = 100
                },
                violations = new string[] { }
            });
        }

        public void SetLimit()
        {
            CreditLimit.SetLimit(50);
        }

        public void ListPreviousTransactions()
        {
            TransactionsList.AddTransaction(new TransactionRoot
            {
                transaction = new Transaction
                {
                    amount = 100,
                    time = System.DateTime.Parse("2019-02-13T11:00:01.000Z"),
                    merchant = "Samsung"
                }
            });
            TransactionsList.AddTransaction(new TransactionRoot
            {
                transaction = new Transaction
                {
                    amount = 100,
                    time = System.DateTime.Parse("2019-02-13T11:00:02.000Z"),
                    merchant = "Samsung"
                }
            });
            TransactionsList.AddTransaction(new TransactionRoot
            {
                transaction = new Transaction
                {
                    amount = 100,
                    time = System.DateTime.Parse("2019-02-13T11:00:03.000Z"),
                    merchant = "XX"
                }
            });
            TransactionsList.AddTransaction(new TransactionRoot
            {
                transaction = new Transaction
                {
                    amount = 100,
                    time = System.DateTime.Parse("2019-02-13T11:00:04.000Z"),
                    merchant = "Samsung"
                }
            });
        }
    }
}
