using System;
using System.Collections.Generic;
using nu_authorizations.Models;

namespace nu_authorizations.Repository
{
    public class TransactionsList
    {
        static List<TransactionRoot> _transactions;

        static TransactionsList()
        {
            _transactions = new List<TransactionRoot>();
        }

        public static void AddTransaction(TransactionRoot transaction)
        {
            _transactions.Add(transaction);
        }

        public static List<TransactionRoot> RetrieveTransactions()
        {
            return _transactions;
        }
    }
}
