using System;
using System.Collections.Generic;
using System.Text;
using nu_authorizations.Models;
using nu_authorizations.Repository;
using System.Linq;
using Newtonsoft.Json;
using nu_authorizations.Actions;

namespace nu_authorizations.Actions
{
    public class BusinessRules
    {
        public static AccountRoot accountBase = new AccountRoot
        {
            account = new Account { activeCard = null, availableLimit = null} 
        };

        public static AccountRoot ValidateBRA(AccountRoot account)
        {

            Processes.violations.Clear();
            // Processes.ResetAccountBase(accountBase);
            

            // if(operation.account != null)
            // {
            AccountInitialized(account);
            // }
            //if(operation.transaction != null && !Processes.violations.Last().Equals("account-not-initialized"))
            //{
            //    // if (!Processes.violations.Last().Equals("account-not-initialized"))
            //    // {
            //        CardNotActive(operation);
            //        InsufficientLimit(operation);
            //        HighFrequencySmallInterval(operation);
            //        DoubledTransaction(operation);
            //    // }                
            //}

            account.violations = Processes.violations.ToArray();
            // operation.account = accountBackup.account;
            return account;
        }

        public static AccountRoot ValidateBRT(TransactionRoot transaction)
        {
            Processes.violations.Clear();
            List<AccountRoot> kjdbsasdf = OperationsRepository.transactionOutputs;
            // Processes.ResetAccountBase(accountBase);

            // if(operation.account != null)
            // {
            AccountNotInitialized(transaction);
            // }
            //if (operation.transaction != null && !Processes.violations.Last().Equals("account-not-initialized"))
            //{
            //    // if (!Processes.violations.Last().Equals("account-not-initialized"))
            //    // {
            //    CardNotActive(operation);
            //    InsufficientLimit(operation);
            //    HighFrequencySmallInterval(operation);
            //    DoubledTransaction(operation);
            //    // }                
            //}
            List<AccountRoot> kjdbs = OperationsRepository.transactionOutputs;
            accountBase.violations = Processes.violations.ToArray();
            return accountBase;
        }

        private static void AccountInitialized(AccountRoot account)
        {
            // bool existingAccount = false;
            //try
            //{
            //    existingAccount = OperationsRepository.accounts.Any(acc => acc.activeCard == true || acc == null);
            //}
            //catch
            //{
            //    existingOperation = false;
            //}
            if (OperationsRepository.accounts.Any(acc => acc.account.activeCard == true || acc.account.activeCard == false))
            {
                //if (account.account.activeCard == false)
                //{

                //}
                //else
                //{
                    Processes.AppendViolations(1); // Account already initialized
                // }
            }
            else
            {
                bool? activeCard = account.account.activeCard;
                int? availableLimit = account.account.availableLimit;
                accountBase.account.activeCard = activeCard;
                accountBase.account.availableLimit = availableLimit;
                accountBase.violations = new string[] { };
                //account.violations = new string[] { };
                //accountBase = account;
            }
        }

        private static void AccountNotInitialized(TransactionRoot transaction)
        {
            if (!OperationsRepository.accounts.Any(acc => acc.account.activeCard == true || acc.account.activeCard == false))
            {
                // if (operation.account.activeCard == true || operation.account.activeCard == false)
                // {
                // Processes.AppendViolations(1); // Account already initialized
                // }

                Processes.AppendViolations(2);
            }
            //else if (operation.transaction != null)
            //{
            //    Processes.AppendViolations(2); // Account not initialized
            //    operation.account = new Account();
            //}
            else
            {
                //accountBase.violations = new string[] { };
                if (CardNotActive() == true &&
                    InsufficientLimit(transaction) == true)
                {
                    List<AccountRoot> kjdbssoqidnwq = OperationsRepository.transactionOutputs;
                    accountBase.account.availableLimit = accountBase.account.availableLimit - transaction.transaction.amount;
                    List<AccountRoot> kjdbs = OperationsRepository.transactionOutputs;
                }
                //CardNotActive();
            }
        }

        private static bool CardNotActive()
        {
            // Operation cardActive = OperationsRepository.operationsList.FindLast(op => op.account.activeCard == true);
            if (!OperationsRepository.accounts.Any(acc => acc.account.activeCard == true))
            {
                Processes.AppendViolations(3); // Card not active
                return false;
            }
            else
            {
                return true;
                //operation.violations = operation.violations == null ? new string[] { } : operation.violations;
            }
        }

        private static bool InsufficientLimit(TransactionRoot transaction)
        {
            if (accountBase.account.availableLimit < transaction.transaction.amount)
            {
                Processes.AppendViolations(4); // Insufficient limit
                return false;
            }
            else
            {
                return true;
                // operation.violations = operation.violations == null ? new string[] { } : operation.violations;
            }
        }

        //private static void HighFrequencySmallInterval(Operation operation)
        //{
        //    int minuteWindow = -3;
        //    int transactionsNumber = 3;

        //    List<Operation> previousTransactions = OperationsRepository.operationsList.FindAll
        //        (op => op.transaction != null &&
        //            op.transaction.time > operation.transaction.time.Add(new TimeSpan(0, minuteWindow, 0))); // last list index suma sin son tres y rechaza
        //    if (previousTransactions.Count >= transactionsNumber)
        //    {
        //        Processes.AppendViolations(5); // High frequency small interval
        //    }
        //    //else
        //    //{
        //    //    operation.violations = operation.violations == null ? new string[] { } : operation.violations;
        //    //}
        //}

        //private static void DoubledTransaction(Operation operation)
        //{
        //    int amount = operation.transaction.amount;
        //    string merchant = operation.transaction.merchant;
        //    int minuteWindow = 2;

        //    List<Operation> previousTransactions = OperationsRepository.operationsList.FindAll
        //        (op => op.transaction != null &&
        //            op.transaction.time > operation.transaction.time.Add(new TimeSpan(0, minuteWindow, 0)) &&
        //            op.transaction.merchant.Equals(merchant) &&
        //            op.transaction.amount == amount); 
        //    if (previousTransactions.Count > 1)
        //    {
        //        Processes.AppendViolations(6); // Doubled Transaction
        //    }
        //    //else
        //    //{
        //    //    operation.violations = operation.violations == null ? new string[] { } : operation.violations;
        //    //}
        //}
    }
}
