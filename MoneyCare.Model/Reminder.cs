using System;

namespace MoneyCare.Model
{

    public enum ReminderType
    {
        Unpaid,
        Paid,
    }

    public class Reminder
    {
        public Guid ID { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; }
    }
}
