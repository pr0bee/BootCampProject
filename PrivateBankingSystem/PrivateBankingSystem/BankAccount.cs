﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateBankingSystem
{
    class BankAccount : User
    {
        public string LoggedUser { get; set; }
        public string TypeOfTransaction { get; set; }
        public DateTime TansactionTime { get; set; }
        public string AccountHolder { get; set; }
        public decimal Balance { get; set; }
                   
    }
}
