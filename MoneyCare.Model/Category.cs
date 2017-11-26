using System;

namespace MoneyCare.Model
{
    public enum CategoryType
    {
        Income,Expense
    }

    public class Category
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Group { get; set; }

        public bool IsBudgeted { get; set; }

        public decimal Budget { get; set; }

    }
}
