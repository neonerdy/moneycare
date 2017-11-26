using System;

namespace MoneyCare.Model
{
    public enum AccountType
    {
        Cash,
        Bank,
        CreditCard,
    }

    public class Account
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public decimal Balance { get; set; }

    }
}
