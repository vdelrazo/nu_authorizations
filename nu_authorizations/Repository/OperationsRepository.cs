using System;
using System.Collections.Generic;
using System.Text;
using nu_authorizations.Models;
using nu_authorizations.Actions;

namespace nu_authorizations.Repository
{
    public static class OperationsRepository
    {
        public static List<AccountRoot> accounts = new List<AccountRoot>();
        public static List<TransactionRoot> transactions = new List<TransactionRoot>();
        public static List<AccountRoot> transactionOutputs = new List<AccountRoot>();

        public static void AddAccount(AccountRoot account)
        {
            transactionOutputs.Add(BusinessRules.ValidateBRA(account));
            accounts.Add(account);
        }
        public static void AddTransactionOutput(TransactionRoot transaction)
        {
            transactionOutputs.Add(BusinessRules.ValidateBRT(transaction));
            AddTransaction(transaction);
        }

        public static void AddTransaction(TransactionRoot transaction)
        {
            if (!BusinessRules.doNotAddTransaction)
            {
                transactions.Add(transaction);
            }
        }

        public static string RetrieveFormattedResults(AccountRoot account)
        {
            string output = Processes.SerializeOperations(account);
            return output + "\n";
        }
    }
}
