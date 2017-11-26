using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyCare.Model
{
    public enum TransactionType
    {
        Income,
        Expense,
        Transfer,
        Withdrawl,
        Deposit,
        Payment
    }


    public class Transaction
    {
        private Category category = null;
        private Account account = null;

        public Guid ID { get; set; }

        public string Type { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public Guid CategoryId { get; set; }

       
        public Category Category
        {
            get 
            {
                if (category == null) category = new Category();
                return category; 
            }
            set { category=value; }
        }


        public Guid AccountId { get; set; }
       
        public Account Account 
        {
            get 
            {
                if (account == null) account = new Account();
                return account; 
            }
            set { account = value; }
        }

        public string Notes { get; set; }
    }
}
