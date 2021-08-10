using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using nu_authorizations.Models;
using nu_authorizations.Repository;

namespace nu_authorizations.Actions
{
    public class Processes
    {
        public static List<string> violations = new List<string>();
        public static AccountRoot DeserializeAccount(string accountOperation)
        {
            return JsonConvert.DeserializeObject<AccountRoot>(accountOperation);
        }
        public static TransactionRoot DeserializeTransaction(string transactionOperation)
        {
            return JsonConvert.DeserializeObject<TransactionRoot>(transactionOperation);
        }

        public static string SerializeOperations(AccountRoot account)
        {
            return JsonConvert.SerializeObject(account,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public static void AppendViolations(int violation)
        {
            switch (violation)
            {
                case 1:
                    violations.Add("account-already-initialized");
                    break;

                case 2:
                    violations.Add("account-not-initialized");
                    break;

                case 3:
                    violations.Add("card-not-active");
                    break;

                case 4:
                    violations.Add("insufficient-limit");
                    break;

                case 5:
                    violations.Add("high-frequency-small-interval");
                    break;

                case 6:
                    violations.Add("doubled-transaction");
                    break;
            }
        }

        public static AccountRoot CreateAccount(bool? activeCard, int? availableLimit)
        {
            AccountRoot account = new AccountRoot
            {
                account = new Account
                {
                    activeCard = activeCard,
                    availableLimit = availableLimit
                },
                violations = Processes.violations.ToArray()
            };
            return account;
        }

        //public static AccountRoot InitializeAccountBase ()
        //{
        //    AccountRoot account = new AccountRoot
        //    {
        //        account = new Account { activeCard = null, availableLimit = null }
        //    };

        //    if (OperationsRepository.accounts.Any(acc => acc.account.activeCard == true || acc.account.activeCard == false))
        //    {
        //        account = OperationsRepository.accounts.Last();
        //    }

        //    return account;
        //}
    }
}
