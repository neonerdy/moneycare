using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.IO;
using MoneyCare.Model;
using System.Data;

namespace MoneyCare.Repository
{
    public class Store
    {
        private const string fileName = @".\moneycare.sdf";

        public static List<Setting> settings = new List<Setting>();
        public static string userName;
        public static string password;

        public static string ConnectionString = "Data Source=" + fileName;

        private const string ACCOUNTS_TABLE = "CREATE TABLE Accounts (ID UNIQUEIDENTIFIER PRIMARY KEY,Name NVARCHAR(500),"
                                                 + "Type NVARCHAR(10),Balance DECIMAL)";

        private const string CATEGORIES_TABLE = "CREATE TABLE Categories (ID UNIQUEIDENTIFIER PRIMARY KEY,Name NVARCHAR(500),"
                                               + "Type NVARCHAR(10),[Group] NVARCHAR(50),IsBudgeted BIT,Budget DECIMAL)";


        private const string TRANSACTIONS_TABLE = "CREATE TABLE Transactions (ID UNIQUEIDENTIFIER PRIMARY KEY,Type NVARCHAR(10),Date DATETIME,"
                                                + "Description NVARCHAR(500),Amount DECIMAL,CategoryId UNIQUEIDENTIFIER,"
                                                + "AccountId UNIQUEIDENTIFIER,Notes NVARCHAR(500))";

        private const string REMINDERS_TABLE = "CREATE TABLE Reminders (ID UNIQUEIDENTIFIER PRIMARY KEY,Description NVARCHAR(500),DueDate DATETIME,"
                                                + "Amount DECIMAL,Status NVARCHAR(50))";

        private const string SETTINGS_TABLE = "CREATE TABLE Settings (ID UNIQUEIDENTIFIER PRIMARY KEY,[Key] NVARCHAR(50),Value NVARCHAR(250))";

        private const string SETTINGS_PROFILE_DATA1 = "INSERT INTO Settings (ID,[Key]) VALUES ('84C892B4-5387-4060-812C-55888A66D3E9','NAME')";
        private const string SETTINGS_PROFILE_DATA2 = "INSERT INTO Settings (ID,[Key]) VALUES ('1E37E4FB-357A-4B73-91E9-798838C989B6','EMAIL')";
        private const string SETTINGS_PROFILE_DATA3 = "INSERT INTO Settings (ID,[Key]) VALUES ('A02C05E3-9340-445E-8B82-E3766022BF44','STATUS')";
        private const string SETTINGS_PROFILE_DATA4 = "INSERT INTO Settings (ID,[Key]) VALUES ('081D18D2-4034-4113-8385-F5B222EEB24E','SEX')";

        private const string SETTINGS_ACCOUNT_DATA1 = "INSERT INTO Settings (ID,[Key]) VALUES ('152ADFD4-45BB-4721-BB19-D7DF69DCC423','SAVING_ACCOUNT')";
        private const string SETTINGS_ACCOUNT_DATA2 = "INSERT INTO Settings (ID,[Key]) VALUES ('53358445-E938-4F40-AC31-40117149D835','EMERGENCY_ACCOUNT')";
        private const string SETTINGS_ACCOUNT_DATA3 = "INSERT INTO Settings (ID,[Key]) VALUES ('CED9745A-CFFA-4D8E-9DD8-2AAA7F4ECE7A','INVESTMENT_ACCOUNT')";
        private const string SETTINGS_ACCOUNT_DATA4 = "INSERT INTO Settings (ID,[Key]) VALUES ('CCE7D7F3-19E8-416A-8D55-2AE7154D69CF','AVERAGE_EXPENSE')";

        private const string SETTINGS_SECURITY_DATA1 = "INSERT INTO Settings (ID,[Key]) VALUES ('BC93DA0E-7789-43EC-873E-F76B049D8856','IS_PROTECTED')";
        private const string SETTINGS_SECURITY_DATA2 = "INSERT INTO Settings (ID,[Key]) VALUES ('09EE467E-300B-4377-AB84-06F2B8363D94','USER_NAME')";
        private const string SETTINGS_SECURITY_DATA3 = "INSERT INTO Settings (ID,[Key]) VALUES ('AD188AC3-E1A1-449B-9844-0D4958FFFEFF','PASSWORD')";

        public const string SETTING_STATUS = "STATUS";
        public const string SETTING_IS_PROTECTED = "IS_PROTECTED";
        public const string SETTING_USER_NAME = "USER_NAME";
        public const string SETTING_PASSWORD = "PASSWORD";
        public const string SETTING_EMERGENCY_ACCOUNT = "EMERGENCY_ACCOUNT";
        public const string SETTING_SAVING_ACCOUNT = "SAVING_ACCOUNT";
        public const string SETTING_INVESTMENT_ACCOUNT = "INVESTMENT_ACCOUNT";
        public const string SETTING_AVERAGE_EXPENSE = "AVERAGE_EXPENSE";


        public static void CreateDatabase()
        {
            if (!File.Exists(fileName))
            {
                try
                {
                    SqlCeEngine en = new SqlCeEngine(ConnectionString);
                    en.CreateDatabase();

                    using (var conn = new SqlCeConnection(ConnectionString))
                    {
                        conn.Open();

                        var cmd1 = new SqlCeCommand(ACCOUNTS_TABLE, conn);
                        cmd1.ExecuteNonQuery();

                        var cmd2 = new SqlCeCommand(CATEGORIES_TABLE, conn);
                        cmd2.ExecuteNonQuery();

                        var cmd3 = new SqlCeCommand(TRANSACTIONS_TABLE, conn);
                        cmd3.ExecuteNonQuery();

                        var cmd4 = new SqlCeCommand(REMINDERS_TABLE, conn);
                        cmd4.ExecuteNonQuery();

                        var cmd5 = new SqlCeCommand(SETTINGS_TABLE, conn);
                        cmd5.ExecuteNonQuery();

                        var cmd6 = new SqlCeCommand(SETTINGS_PROFILE_DATA1, conn);
                        cmd6.ExecuteNonQuery();

                        var cmd7 = new SqlCeCommand(SETTINGS_PROFILE_DATA2, conn);
                        cmd7.ExecuteNonQuery();

                        var cmd8 = new SqlCeCommand(SETTINGS_PROFILE_DATA3, conn);
                        cmd8.ExecuteNonQuery();

                        var cmd9 = new SqlCeCommand(SETTINGS_PROFILE_DATA4, conn);
                        cmd9.ExecuteNonQuery();

                        var cmd10 = new SqlCeCommand(SETTINGS_ACCOUNT_DATA1, conn);
                        cmd10.ExecuteNonQuery();

                        var cmd11 = new SqlCeCommand(SETTINGS_ACCOUNT_DATA2, conn);
                        cmd11.ExecuteNonQuery();

                        var cmd12 = new SqlCeCommand(SETTINGS_ACCOUNT_DATA3, conn);
                        cmd12.ExecuteNonQuery();

                        var cmd13 = new SqlCeCommand(SETTINGS_ACCOUNT_DATA4, conn);
                        cmd13.ExecuteNonQuery();

                        var cmd14 = new SqlCeCommand(SETTINGS_SECURITY_DATA1, conn);
                        cmd14.ExecuteNonQuery();

                        var cmd15 = new SqlCeCommand(SETTINGS_SECURITY_DATA2, conn);
                        cmd15.ExecuteNonQuery();

                        var cmd16 = new SqlCeCommand(SETTINGS_SECURITY_DATA3, conn);
                        cmd16.ExecuteNonQuery();

                    }
                }
                catch (SqlCeException ex)
                {
                    throw new Exception("Database gagal dibuat!",ex);
                }

            }

        }

        
       



    }
}
