using System;
using System.Collections.Generic;
using nu_authorizations.Models;

namespace nu_authorizations.Repository
{
    public class AccountsList
    {
        static List<AccountRoot> _accounts;

        static AccountsList()
        {
            _accounts = new List<AccountRoot>();
        }

        public static void AddAccount(AccountRoot account)
        {
            _accounts.Add(account);
        }

        public static List <AccountRoot> RetrieveAccounts()
        {
            return _accounts;
        }
    }
}
