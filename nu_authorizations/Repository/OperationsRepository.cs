using System;
using System.Collections.Generic;
using System.Text;
using nu_authorizations.Models;
using nu_authorizations.Actions;

namespace nu_authorizations.Repository
{
    public static class OperationsRepository
    {
        public static void AddAccount(AccountRoot account)
        {
            OutputList.AddRecord(BusinessRules.ValidateBRA(account));
            AccountsList.AddAccount(account);
        }
        public static void AddTransactionOutput(TransactionRoot transaction)
        {
            OutputList.AddRecord(BusinessRules.ValidateBRT(transaction));
            TransactionsList.AddTransaction(transaction);
        }

        public static void AddTransaction(TransactionRoot transaction)
        {
            if (!BusinessRules.doNotAddTransaction)
            {
                TransactionsList.AddTransaction(transaction);
            }
        }

        public static string RetrieveFormattedResults(AccountRoot account)
        {
            string output = Processes.SerializeOperations(account);
            return output + "\n";
        }
    }
}
