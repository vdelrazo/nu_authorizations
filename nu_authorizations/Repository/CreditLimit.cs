using System;
using System.Collections.Generic;
using System.Text;

namespace nu_authorizations.Repository
{
    public class CreditLimit
    {
        static int? _availableLimit;

        static CreditLimit()
        {
            _availableLimit = null;
        }

        public static void SetLimit(int? limit)
        {
            _availableLimit = limit;
        }

        public static int? GetLimit()
        {
            return _availableLimit;
        }
    }
}
