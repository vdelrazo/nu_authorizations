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

            // string op1 = "{\"account\": {\"active-card\": true, \"available-limit\": 175}}";
            // string op2 = "{\"transaction\": {\"merchant\": \"Burger King\", \"amount\": 20, \"time\": \"2019-02-13T10:00:00.000Z\"}}";

            // Deserialize
            // Account operation = Actions.Processes.DeserializeAccount(op1);
            //Transaction sspqknxpq = Actions.Processes.DeserializeTransaction(op2);

        // Add
        // OperationsRepository.AddOperation(operation);

        // Check violations

        // Retrieve operations
        // Operation[] nusnjams = OperationsRepository.RetrieveOperations();



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
                    OperationsRepository.AddTransaction(transaction);
                }
                //Operation operation = Actions.Processes.DeserializeTransaction(line);
                // Add
                //OperationsRepository.AddOperation(operation);

                goto Input;
            }
            else
            {
                foreach(AccountRoot transactionOutput in OperationsRepository.transactionOutputs)
                {
                    Console.WriteLine(OperationsRepository.RetrieveFormattedResults(transactionOutput));
                }
                
            }
            
        }
    }
}
