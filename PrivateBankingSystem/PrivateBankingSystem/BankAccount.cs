using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateBankingSystem
{
    class BankAccount
    {
        public string LoggedUser { get; set; }
        public string AccountHolder { get; set; }
        public Transaction Transaction { get; set; }
        public int Balance { get; set; }
        public DateTime TransactionTimestamp { get; set; }

        public BankAccount(string loggedUser)
        {
            LoggedUser = loggedUser;
        }
    }
}
