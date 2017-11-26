using System;
using System.Collections.Generic;
using MoneyCare.Model;
using System.Data;

namespace MoneyCare.Repository.Mapping
{
    public class ReminderMapper : IDataMapper<Reminder>
    {
        public Reminder Map(IDataReader rdr)
        {
            Reminder reminder = new Reminder();

            reminder.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            reminder.Description = rdr["Description"] is DBNull ? string.Empty : (string)rdr["Description"];
            reminder.DueDate = rdr["DueDate"] is DBNull ? DateTime.Now : (DateTime)rdr["DueDate"];
            reminder.Amount = rdr["Amount"] is DBNull ? 0 : (decimal)rdr["Amount"];
            reminder.Status = rdr["Status"] is DBNull ? string.Empty : (string)rdr["Status"];

            return reminder;
        }
    }
}
