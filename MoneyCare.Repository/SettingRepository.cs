using System;
using MoneyCare.Model;
using System.Data.SqlServerCe;
using MoneyCare.Repository.Mapping;
using System.Collections.Generic;

namespace MoneyCare.Repository
{

    public class SettingRepository
    {

        private string tableName = "Settings";


        public List<Setting> GetAll()
        {
            List<Setting> settings = new List<Setting>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName;
                
                var cmd = new SqlCeCommand(sql, conn);
                using (var rdr = cmd.ExecuteReader())
                {
                    settings = Mapper.MapList<Setting>(rdr, new SettingMapper());
                }
            }

            return settings;

        }


        public Setting GetByKey(string key)
        {
            Setting setting = null;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName + " WHERE Key='" + key + "'";
                var cmd = new SqlCeCommand(sql, conn);
                using (var rdr = cmd.ExecuteReader())
                {
                    setting = Mapper.MapObject<Setting>(rdr, new SettingMapper());
                }
            }

            return setting;

        }



        public void UpdateLinkedAccount(string saving,string emergencyFund,string investment,string averageExpense)
        {
            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql1 = "UPDATE " + tableName + " SET Value='" + saving + "' WHERE [Key]='SAVING_ACCOUNT'";
                var cmd1 = new SqlCeCommand(sql1, conn);
                cmd1.ExecuteNonQuery();

                string sql2 = "UPDATE " + tableName + " SET Value='" + emergencyFund + "' WHERE [Key]='EMERGENCY_ACCOUNT'";
                var cmd2 = new SqlCeCommand(sql2, conn);
                cmd2.ExecuteNonQuery();

                string sql3 = "UPDATE " + tableName + " SET Value='" + investment + "' WHERE [Key]='INVESTMENT_ACCOUNT'";
                var cmd3 = new SqlCeCommand(sql3, conn);
                cmd3.ExecuteNonQuery();

                string sql4 = "UPDATE " + tableName + " SET Value='" + averageExpense.Replace(".",string.Empty) + "' WHERE [Key]='AVERAGE_EXPENSE'";
                var cmd4 = new SqlCeCommand(sql4, conn);
                cmd4.ExecuteNonQuery();


            }


        }


        public void UpdateProfile(string name, string email, string status, string sex)
        {
            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql1 = "UPDATE " + tableName + " SET Value='" + name + "' WHERE [Key]='NAME'";
                var cmd1 = new SqlCeCommand(sql1, conn);
                cmd1.ExecuteNonQuery();

                string sql2 = "UPDATE " + tableName + " SET Value='" + email + "' WHERE [Key]='EMAIL'";
                var cmd2 = new SqlCeCommand(sql2, conn);
                cmd2.ExecuteNonQuery();

                string sql3 = "UPDATE " + tableName + " SET Value='" + status + "' WHERE [Key]='STATUS'";
                var cmd3 = new SqlCeCommand(sql3, conn);
                cmd3.ExecuteNonQuery();

                string sql4 = "UPDATE " + tableName + " SET Value='" + sex + "' WHERE [Key]='SEX'";
                var cmd4 = new SqlCeCommand(sql4, conn);
                cmd4.ExecuteNonQuery();

            }

        }

        public void UpdateSecurity(string isProtected, string userName, string password)
        {
            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql1 = "UPDATE " + tableName + " SET Value='" + isProtected + "' WHERE [Key]='IS_PROTECTED'";
                var cmd1 = new SqlCeCommand(sql1, conn);
                cmd1.ExecuteNonQuery();

                string sql2 = "UPDATE " + tableName + " SET Value='" + userName + "' WHERE [Key]='USER_NAME'";
                var cmd2 = new SqlCeCommand(sql2, conn);
                cmd2.ExecuteNonQuery();

                string sql3 = "UPDATE " + tableName + " SET Value='" + password + "' WHERE [Key]='PASSWORD'";
                var cmd3 = new SqlCeCommand(sql3, conn);
                cmd3.ExecuteNonQuery();

            }

        }



    }
}
