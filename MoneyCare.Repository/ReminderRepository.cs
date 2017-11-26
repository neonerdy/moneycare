using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoneyCare.Model;
using MoneyCare.Repository.Mapping;
using System.Data.SqlServerCe;
using System.Data;

namespace MoneyCare.Repository
{
    public class ReminderRepository
    {
        private string tableName = "Reminders";


        public Reminder GetById(Guid id)
        {
            Reminder reminder = null;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql="SELECT * FROM " + tableName + " WHERE ID='" + id + "'";
                var cmd = new SqlCeCommand(sql, conn);
                
                using (var rdr = cmd.ExecuteReader())
                {
                    reminder = Mapper.MapObject<Reminder>(rdr, new ReminderMapper());
                }
            }

            return reminder;
        }


        public List<Reminder> GetAll(int month, int year)
        {
            List<Reminder> reminders = new List<Reminder>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName + " WHERE DATEPART(month,DueDate)=@Month "
                           + "AND DATEPART(year,DueDate)=@Year";
                
                var cmd = new SqlCeCommand(sql, conn);

                cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;

                using (var rdr = cmd.ExecuteReader())
                {
                    reminders = Mapper.MapList<Reminder>(rdr, new ReminderMapper());
                }
            }
            
            return reminders;
        }


        public List<Reminder> GetByStatus(ReminderType reminderType,int month, int year)
        {
            List<Reminder> reminders = new List<Reminder>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName + " WHERE DATEPART(month,DueDate)=@Month "
                           + "AND DATEPART(year,DueDate)=@Year AND Status=@Status";

                var cmd = new SqlCeCommand(sql, conn);

                cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;
                cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = reminderType.ToString();

                using (var rdr = cmd.ExecuteReader())
                {
                    reminders = Mapper.MapList<Reminder>(rdr, new ReminderMapper());
                }
            }

            return reminders;
        }


        public void Save(Reminder reminder)
        {
            try
            {
                using (var conn = new SqlCeConnection(Store.ConnectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO " + tableName + " (ID,Description,DueDate,Amount,Status) "
                               + "VALUES (@ID,@Description,@DueDate,@Amount,@Status)";

                    var cmd = new SqlCeCommand(sql, conn);

                    cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = reminder.Description;
                    cmd.Parameters.Add("@DueDate", SqlDbType.DateTime).Value = reminder.DueDate;
                    cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = reminder.Amount;
                    cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = reminder.Status;

                    cmd.ExecuteNonQuery();
              
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Update(Reminder reminder)
        {
            try
            {
                using (var conn = new SqlCeConnection(Store.ConnectionString))
                {
                    conn.Open();

                    string sql = "UPDATE " + tableName + " SET Description=@Description,DueDate=@DueDate,"
                                + "Amount=@Amount,Status=@Status WHERE ID=@ID";

                    var cmd = new SqlCeCommand(sql, conn);

                    cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = reminder.ID;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = reminder.Description;
                    cmd.Parameters.Add("@DueDate", SqlDbType.DateTime).Value = reminder.DueDate;
                    cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = reminder.Amount;
                    cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = reminder.Status;

                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Delete(Guid id)
        {
            try
            {
                using (var conn = new SqlCeConnection(Store.ConnectionString))
                {
                    conn.Open();

                    string sql = "DELETE FROM " + tableName + " WHERE ID='" + id + "'";
                    var cmd = new SqlCeCommand(sql, conn);
                    cmd.ExecuteNonQuery();    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
