using System;
using System.Collections.Generic;
using nu_authorizations.Models;
using nu_authorizations.Repository;
using System.Linq;

namespace nu_authorizations.Actions
{
    public class BusinessRules
    {
        public static bool? activeCard = null;
        public static AccountRoot accountToStore;
        public static List<bool> checkList = new List<bool>();
        public static bool doNotAddTransaction = false;
        
        public static AccountRoot ValidateBRA(AccountRoot account)
        {
            Processes.violations.Clear();
            AccountInitialized(account);
            accountToStore = Processes.CreateAccount(activeCard, CreditLimit.GetLimit());
            return accountToStore;
        }

        public static AccountRoot ValidateBRT(TransactionRoot transaction)
        {
            Processes.violations.Clear();
            checkList.Clear();
            doNotAddTransaction = false;
            AccountNotInitialized(transaction);
            accountToStore = Processes.CreateAccount(activeCard, CreditLimit.GetLimit());
            return accountToStore;
        }

        private static void AccountInitialized(AccountRoot account)
        {
             if (AccountsList.RetrieveAccounts().Any(acc => acc.account.activeCard == true || acc.account.activeCard == false))
             {
                 Processes.AppendViolations(1); // Account already initialized
             }
            else
            {
                activeCard = account.account.activeCard;
                CreditLimit.SetLimit(account.account.availableLimit);
            }
        }

        private static void AccountNotInitialized(TransactionRoot transaction)
        {
            if (!AccountsList.RetrieveAccounts().Any(acc => acc.account.activeCard == true || acc.account.activeCard == false))
            {
                  Processes.AppendViolations(2);
            }
            else
            {
                CardNotActive();
                InsufficientLimit(transaction);
                HighFrequencySmallInterval(transaction);
                DoubledTransaction(transaction);

                if (!checkList.Contains(false))
                {
                    CreditLimit.SetLimit(CreditLimit.GetLimit() - transaction.transaction.amount);
                }
            }
        }

        private static void CardNotActive()
        {
            if (!AccountsList.RetrieveAccounts().Any(acc => acc.account.activeCard == true))
            {
                Processes.AppendViolations(3); // Card not active
                checkList.Add(false);
            }
            else
            {
                checkList.Add(true);
            }
        }

        private static void InsufficientLimit(TransactionRoot transaction)
        {
            if (CreditLimit.GetLimit() < transaction.transaction.amount)
            {
                Processes.AppendViolations(4); // Insufficient limit
                doNotAddTransaction = true;
                checkList.Add(false);
            }
            else
            {
                checkList.Add(true);
            }
        }

        private static void HighFrequencySmallInterval(TransactionRoot transaction)
        {
            int minuteWindow = -3;
            int transactionsNumber = 3;

            // Find all transactions inside minute window and add them to previousTransactions
            List<TransactionRoot> previousTransactions = TransactionsList.RetrieveTransactions().FindAll
                (tran => tran.transaction != null &&
                    tran.transaction.time > transaction.transaction.time.Add(new TimeSpan(0, minuteWindow, 0))); 

            if (previousTransactions.Count >= transactionsNumber)
            {
                Processes.AppendViolations(5); // High frequency small interval
                checkList.Add(false);
            }
            else
            {
                checkList.Add(true);
            }
        }

        private static void DoubledTransaction(TransactionRoot transaction)
        {
            int amount = transaction.transaction.amount;
            string merchant = transaction.transaction.merchant;
            
            List<TransactionRoot> previousTransactions = TransactionsList.RetrieveTransactions().FindAll
                (tran => tran.transaction.merchant.Equals(merchant) &&
                    tran.transaction.amount == amount);
            if (previousTransactions.Count >= 1)
            {
                Processes.AppendViolations(6); // Doubled Transaction
                doNotAddTransaction = true;
                checkList.Add(false);
            }
            else
            {
                checkList.Add(true);
            }
        }
    }
}
