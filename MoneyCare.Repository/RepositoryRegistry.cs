using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using System.Configuration;
using MoneyCare.Model;
using System.IO;
using System.Data.SqlServerCe;

namespace MoneyCare.Repository
{
    public class RepositoryRegistry : IRegistry
    {
        private IEntityManager em;

        private const string ACCOUNTS_TABLE = "CREATE TABLE Accounts (ID UNIQUEIDENTIFIER PRIMARY KEY,Name NVARCHAR(250),"
                                                    + "Type VARCHAR(10),Balance DECIMAL)";

        private const string CATEGORIES_TABLE = "CREATE TABLE Categories (ID GUID PRIMARY KEY,Name NVARCHAR(250),"
                                                    + "Type VARCHAR(10),[Group] VARCHAR(50),IsBudgeted BOOLEAN,Budget DECIMAL)";


        private const string TRANSACTIONS_TABLE = "CREATE TABLE Transactions (ID GUID PRIMARY KEY,Type VARCHAR(10),CreatedDate DATETIME,"
                                                        + "Description VARCHAR(50),Amount DECIMAL,CategoryId GUID,AccountId GUID,Notes NVARCHAR(500))";

        private const string REMINDERS_TABLE = "CREATE TABLE Reminders (ID GUID PRIMARY KEY,Description NVARCHAR(500),DueDate DATETIME,"
                                                    + "Amount DECIMAL,RemindBefore VARCHAR(50))";


        private const string fileName=@".\moneycare.db";


        private DataSource ds;

        public RepositoryRegistry()
        {
            ds = new DataSource();

            ds.Provider = "System.Data.SQLite";
            ds.ConnectionString = "Data Source=" + fileName + ";";

            //ds.Provider = "MoneyCare.Repository.SqlCeClientFactory";
            //ds.ConnectionString = "Data Source=" + fileName; 


        }


        //private void CreateDatabase()
        //{
        //    SqlCeEngine en = new SqlCeEngine(ds.ConnectionString);
        //    en.CreateDatabase();

        //    using (IEntityManager em = EntityManagerFactory.CreateInstance(ds))
        //    {
        //        em.ExecuteNonQuery(ACCOUNTS_TABLE);

        //    }
        //}

        private void CreateDatabase()
        {
            using (IEntityManager em = EntityManagerFactory.CreateInstance(ds))
            {
                em.ExecuteNonQuery(ACCOUNTS_TABLE);
                em.ExecuteNonQuery(CATEGORIES_TABLE);
                em.ExecuteNonQuery(TRANSACTIONS_TABLE);
                em.ExecuteNonQuery(REMINDERS_TABLE);
            }
        }



        public void Configure()
        {
            if (!File.Exists(fileName))
            {
                CreateDatabase();
            }
           
            object[] depedency={ ds };

            ServiceLocator.RegisterObject<ICategoryRepository, CategoryRepository>(depedency);
            ServiceLocator.RegisterObject<IAccountRepository,AccountRepository>(depedency);
            ServiceLocator.RegisterObject<ITransactionRepository, TransactionRepository>(depedency);
            ServiceLocator.RegisterObject<IReminderRepository, ReminderRepository>(depedency);
            ServiceLocator.RegisterObject<IChartRepository, ChartRepository>(depedency);
        
        }

        public void Dispose()
        {           
        }
    }
}
