using System;

namespace MoneyCare.Model
{
    public class Budget
    {
        public string Category { get; set; }

        public decimal Budgeted { get; set; }

        public int Bar { get; set; }

        public string Percentage { get; set; } 

        public decimal Used { get; set; }

        public decimal Remain { get; set; }

    }
}
