using System;
using System.Collections.Generic;
using nu_authorizations.Models;

namespace nu_authorizations.Repository
{
    public class OutputList
    {
        static List<AccountRoot> _output;

        static OutputList()
        {
            _output = new List<AccountRoot>();
        }

        public static void AddRecord(AccountRoot record)
        {
            _output.Add(record);
        }

        public static List<AccountRoot> RetrieveOutput()
        {
            return _output;
        }
    }
}
