using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using nu_authorizations.Models;
using nu_authorizations.Repository;

namespace nu_authorizations
{
    public class Program
    {
        static void Main(string[] args)
        {
            string line = string.Empty;
            string outputString = string.Empty;

        Input:
            line = Console.ReadLine();

            if (line != string.Empty)
            {
                // Deserialize
                if (line.Contains("account"))
                {
                    AccountRoot account = Actions.Processes.DeserializeAccount(line);
                    OperationsRepository.AddAccount(account);
                }
                else
                {
                    TransactionRoot transaction = Actions.Processes.DeserializeTransaction(line);
                    OperationsRepository.AddTransactionOutput(transaction);
                }

                goto Input;
            }
            else
            {
                foreach(AccountRoot transactionOutput in OutputList.RetrieveOutput())
                {
                    Console.WriteLine(OperationsRepository.RetrieveFormattedResults(transactionOutput));
                }
                
            }
            
        }
    }
}
