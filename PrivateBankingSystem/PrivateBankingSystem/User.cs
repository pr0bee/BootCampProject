using System;


namespace PrivateBankingSystem
{
    class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public bool IsAdmin { get; set; }
        public MainMenu.UserChoice MenuChoice { get; set; }
    }
}
