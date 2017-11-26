using System;
using System.Collections.Generic;
using MoneyCare.Model;

using MoneyCare.Repository.Mapping;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Data.Common;

namespace MoneyCare.Repository
{
    public enum ExportType
    {
        ByMonth,ByYear
    }

    public class TransactionRepository
    {
        private string tableName = "Transactions";
        private AccountRepository accountRepository;
           
        public TransactionRepository()
        {
            accountRepository = new AccountRepository();
        }


        public bool IsTransactionExist(DateTime date,string type,Guid categoryId,Guid accountId)
        {            
            bool isExist=false;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName + " WHERE Date=@Date"
                    + " AND Type=@Type AND CategoryId=@CategoryId AND AccountId=@AccountId";

                var cmd = new SqlCeCommand(sql, conn);

                cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = date;
                cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = type;
                cmd.Parameters.Add("@CategoryId", SqlDbType.UniqueIdentifier).Value = categoryId;
                cmd.Parameters.Add("@AccountId", SqlDbType.UniqueIdentifier).Value = accountId;

                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        isExist = true;
                    }
                }
                                
                return isExist;
            }
        }



       



        public List<Transaction> GetForExport(ExportType exportType,int month,int year)
        {
            List<Transaction> transactions = new List<Transaction>();

            string sql = string.Empty;
         
            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();
           
                if (exportType == ExportType.ByMonth)
                {
                    sql = "SELECT * FROM " + tableName + " t " 
                        + "WHERE DATEPART(month,Date)=" + month + " AND DATEPART(year,Date)=" + year;
                }
                else if (exportType == ExportType.ByYear)
                {
                    sql = "SELECT * FROM " + tableName + " t WHERE DATEPART(year,Date)=" + year;
                }

                var cmd = new SqlCeCommand(sql, conn);

                using (var rdr = cmd.ExecuteReader())
                {
                    transactions = Mapper.MapList<Transaction>(rdr, new TransactionMapper());
                }
                
            }

            return transactions;

        }




        public List<Transaction> GetByFilter(string clause)
        {

            List<Transaction> transactions = new List<Transaction>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName + " t LEFT JOIN Categories c ON t.CategoryId=c.ID "
                           + "LEFT JOIN Accounts a ON t.AccountId=a.ID "
                           + " WHERE " + clause 
                           + " ORDER BY Date DESC";

                var cmd = new SqlCeCommand(sql, conn);


                using (var rdr = cmd.ExecuteReader())
                {
                    transactions = Mapper.MapList<Transaction>(rdr, new TransactionMapper());
                }
            }

            return transactions;

        }

        public decimal GetAverageExpense()
        {
            decimal averageExpense = 0;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT AVG(Amount) AS AvgOfAmount FROM Transactions "
                              + "WHERE Type='Expense' AND Date <= @Date";

                var cmd = new SqlCeCommand(sql, conn);

                cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now;

                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        averageExpense = rdr["AvgOfAmount"] is DBNull ? 0 : (decimal)rdr["AvgOfAmount"];
                    }
                }
            }

            return averageExpense;

        }


        public decimal GetMonthlyDebt(int month, int year)
        {
            decimal montlyDebt = 0;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT SUM(Amount) AS SumOfAmount FROM Transactions t "
                           + "INNER JOIN Categories c ON t.CategoryId=c.ID "
                           + "INNER JOIN Accounts a ON t.AccountId=a.ID "
                           + "WHERE (a.[Type]='CreditCard' OR c.[Group]='Utang & Cicilan') AND DATEPART(month,Date)=@Month "
                           + "AND DATEPART(year,Date)=@Year";


                var cmd = new SqlCeCommand(sql, conn);

                cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;

                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        montlyDebt = rdr["SumOfAmount"] is DBNull ? 0 : (decimal)rdr["SumOfAmount"];
                    }
                }
            }


            return montlyDebt;

        }



        public decimal GetUsedBudget(Guid categoryId,int month,int year)
        {
            decimal usedBudget=0;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                    conn.Open();

                    string sql = "SELECT SUM(Amount) AS SumOfAmount FROM Transactions "
                               + "WHERE CategoryId=@CategoryId AND DATEPART(month,Date)=@Month "
                               + "AND DATEPART(year,Date)=@Year";

                    var cmd = new SqlCeCommand(sql, conn);

                    cmd.Parameters.Add("@CategoryId", SqlDbType.UniqueIdentifier).Value = categoryId;
                    cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;
                    cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;

                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            usedBudget = rdr["SumOfAmount"] is DBNull ? 0 : (decimal)rdr["SumOfAmount"];
                        }
                    }
             }

          
            return usedBudget;

        }



        public List<Budget> GetAllBudget(int month,int year)
        {
            List<Budget> budgets = new List<Budget>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM Categories WHERE IsBudgeted=1";

                var cmd = new SqlCeCommand(sql, conn);

                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Budget budget = new Budget();

                        Guid categoryId=(Guid)rdr["ID"];

                        budget.Budgeted = rdr["Budget"] is DBNull ? 0 : (decimal)rdr["Budget"];
                        budget.Category = rdr["Name"] is DBNull ? string.Empty : (string)rdr["Name"];
                        budget.Used = GetUsedBudget(categoryId, month, year);
                        budget.Bar=Decimal.ToInt32(budget.Used / budget.Budgeted * 100);
                        budget.Percentage = budget.Bar.ToString() + " %";
                        budget.Remain = budget.Budgeted - budget.Used;

                        budgets.Add(budget);
                    }
                }
            }

            return budgets;

        }




        public Model.Transaction GetById(Guid id)
        {
           
            Model.Transaction transaction = null;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName + " WHERE ID='" + id + "'";
                var cmd = new SqlCeCommand(sql, conn);
                
                using (var rdr = cmd.ExecuteReader())
                {
                    transaction = Mapper.MapObject<Transaction>(rdr, new TransactionMapper());
                }
            }

            return transaction;
        }



        public List<Model.Transaction> GetAll(int month, int year)
        {
            List<Model.Transaction> transactions = new List<Model.Transaction>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName + " WHERE DATEPART(month,Date)=@Month "
                           + "AND DATEPART(year,Date)=@Year ORDER BY Date DESC";

                var cmd = new SqlCeCommand(sql, conn);
                cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;

                using (var rdr = cmd.ExecuteReader())
                {
                    transactions = Mapper.MapList<Transaction>(rdr, new TransactionMapper());
                }
            }

            return transactions;
        }



        public List<Model.Transaction> GetByType(TransactionType transactionType,int month, int year)
        {
            List<Model.Transaction> transactions = new List<Model.Transaction>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();
              
                string sql = string.Empty;

                if (transactionType == TransactionType.Expense)
                {
                    sql = "SELECT * FROM " + tableName + " t WHERE t.Type IN ('Expense','Payment') "
                       + "AND DATEPART(month,Date)=@Month AND DATEPART(year,Date)=@Year ORDER BY Date DESC";
                }
                else
                {
                    sql = "SELECT * FROM " + tableName + " t WHERE t.Type=@Type "
                   + "AND DATEPART(month,Date)=@Month AND DATEPART(year,Date)=@Year ORDER BY Date DESC";
                }

                var cmd = new SqlCeCommand(sql, conn);

                cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = transactionType.ToString();
                cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;
       
                using (var rdr = cmd.ExecuteReader())
                {
                    transactions = Mapper.MapList<Transaction>(rdr, new TransactionMapper());
                }
            }


            return transactions;
        }



        public decimal GetTotalTransaction(CategoryType categoryType,int month, int year)
        {
            decimal totalTransaction=0;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = string.Empty;

                if (categoryType == CategoryType.Income)
                {
                    sql = "SELECT SUM(Amount) AS SumOfAmount FROM " + tableName + " t "
                        + "WHERE Type=@Type AND DATEPART(month,Date)=@Month AND DATEPART(year,Date)=@Year";
                }
                else if (categoryType == CategoryType.Expense)
                {
                    sql = "SELECT SUM(Amount) AS SumOfAmount FROM " + tableName + " t "
                        + "WHERE Type IN ('Expense','Payment') AND DATEPART(month,Date)=@Month AND DATEPART(year,Date)=@Year";
                }

                var cmd = new SqlCeCommand(sql, conn);

                cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = categoryType.ToString();
                cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;

                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        totalTransaction = rdr["SumOfAmount"] is DBNull ? 0 : (decimal)rdr["SumOfAmount"];
                    }
                }
            }


            return totalTransaction;
        }
        
        
        public void Save(Model.Transaction transaction)
        {
            SqlCeTransaction tx=null;

            try
            {
                using (var conn = new SqlCeConnection(Store.ConnectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO " + tableName + " (ID,Type,Date,Amount,Description,CategoryId,AccountId, Notes) "
                        + " VALUES (@ID,@Type,@Date,@Amount,@Description,@CategoryId,@AccountId, @Notes)";

                    tx = conn.BeginTransaction();
                   
                    var cmd = new SqlCeCommand(sql, conn);
                    cmd.Transaction=tx; 

                    cmd.Parameters.Add("@ID",SqlDbType.UniqueIdentifier).Value=Guid.NewGuid();
                    cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = transaction.Type;
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = transaction.Date;
                    cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = transaction.Amount;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = transaction.Description;
                    cmd.Parameters.Add("@CategoryId", SqlDbType.UniqueIdentifier).Value = transaction.CategoryId;
                    cmd.Parameters.Add("@AccountId", SqlDbType.UniqueIdentifier).Value = transaction.AccountId;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = transaction.Notes;

                    cmd.ExecuteNonQuery();

                    if (transaction.Type == TransactionType.Transfer.ToString()
                       || transaction.Type == TransactionType.Withdrawl.ToString()
                       || transaction.Type == TransactionType.Deposit.ToString())
                    {
                        decimal fromAccountBalance = accountRepository.GetBalance(transaction.CategoryId);
                        decimal toAccountBalance = accountRepository.GetBalance(transaction.AccountId);

                        accountRepository.UpdataBalance(transaction.CategoryId, fromAccountBalance - transaction.Amount, conn, tx);
                        accountRepository.UpdataBalance(transaction.AccountId, toAccountBalance + transaction.Amount, conn, tx);
                    }
                    else if (transaction.Type == TransactionType.Payment.ToString())
                    {
                        decimal CreditCardBalance = accountRepository.GetBalance(transaction.CategoryId);
                        decimal PaymentAccountBalance = accountRepository.GetBalance(transaction.AccountId);

                        accountRepository.UpdataBalance(transaction.CategoryId, CreditCardBalance + transaction.Amount, conn, tx);
                        accountRepository.UpdataBalance(transaction.AccountId, PaymentAccountBalance - transaction.Amount, conn, tx);
                    }
                    else if (transaction.Type == TransactionType.Income.ToString()
                        || transaction.Type == TransactionType.Expense.ToString())
                    {
                        decimal currentBalance = accountRepository.GetBalanceAfter(transaction.AccountId, transaction.Type, transaction.Amount,
                            transaction.Amount, ActionType.Add).CurrentBalance2;

                        accountRepository.UpdataBalance(transaction.AccountId, currentBalance, conn, tx);
                    }
                                        
          
                    tx.Commit();
                 }
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw ex;
            }
        }



        public void Update(Model.Transaction transaction)
        {
            SqlCeTransaction tx = null;

            try
            {

                using (var conn = new SqlCeConnection(Store.ConnectionString))
                {
                    conn.Open();

                    string sql = "UPDATE " + tableName + " SET Type=@Type,Date=@Date,Amount=@Amount,"
                               + "CategoryId=@CategoryId,AccountId=@AccountId, Notes=@Notes WHERE ID=@ID";

                    tx = conn.BeginTransaction();

                    var cmd = new SqlCeCommand(sql, conn);
                    cmd.Transaction = tx;

                    cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = transaction.ID;
                    cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = transaction.Type;
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = transaction.Date;
                    cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = transaction.Amount;
                    cmd.Parameters.Add("@CategoryId", SqlDbType.UniqueIdentifier).Value = transaction.CategoryId;
                    cmd.Parameters.Add("@AccountId", SqlDbType.UniqueIdentifier).Value = transaction.AccountId;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = transaction.Notes;

                    cmd.ExecuteNonQuery();


                    Model.Transaction savedTransaction = GetById(transaction.ID);


                    if (transaction.Type == TransactionType.Income.ToString() || transaction.Type == TransactionType.Expense.ToString())
                    {
                        decimal currentBalance = accountRepository.GetBalanceAfter(transaction.AccountId, transaction.Type, savedTransaction.Amount,
                            transaction.Amount, ActionType.Edit).CurrentBalance2;

                        accountRepository.UpdataBalance(transaction.AccountId, currentBalance, conn, tx);
                    }
                    else if (transaction.Type == TransactionType.Transfer.ToString() || transaction.Type == TransactionType.Withdrawl.ToString()
                     || transaction.Type == TransactionType.Payment.ToString())
                    {
                        decimal currentBalance1 = accountRepository.GetBalanceAfter(transaction.CategoryId, transaction.Type, savedTransaction.Amount,
                                transaction.Amount, ActionType.Edit).CurrentBalance1;

                        accountRepository.UpdataBalance(transaction.CategoryId, currentBalance1, conn, tx);

                        decimal currentBalance2 = accountRepository.GetBalanceAfter(transaction.AccountId, transaction.Type, savedTransaction.Amount,
                                transaction.Amount, ActionType.Edit).CurrentBalance2;

                        accountRepository.UpdataBalance(transaction.AccountId, currentBalance2, conn, tx);
                    }

                 
                    tx.Commit();

                }
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw ex;
            }
        }



       


        public void Delete()
        {
            SqlCeTransaction tx = null;

            try
            {
                using (var conn = new SqlCeConnection(Store.ConnectionString))
                {
                    conn.Open();

                    string sql = "DELETE FROM " + tableName;

                    tx = conn.BeginTransaction();

                    var cmd = new SqlCeCommand(sql, conn);
             
                    cmd.ExecuteNonQuery();

             
                }
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw ex;
            }

        }

        public void Delete(Model.Transaction transaction)
        {
            SqlCeTransaction tx = null;

            try
            {
                using (var conn = new SqlCeConnection(Store.ConnectionString))
                {
                    conn.Open();

                    string sql = "DELETE FROM " + tableName + " WHERE ID='" + transaction.ID + "'";

                    tx = conn.BeginTransaction();

                    var cmd = new SqlCeCommand(sql, conn);
                    cmd.Transaction = tx;

                    cmd.ExecuteNonQuery();

                    if (transaction.Type == TransactionType.Income.ToString() || transaction.Type == TransactionType.Expense.ToString())
                    {
                        decimal currentBalance = accountRepository.GetBalanceAfter(transaction.AccountId, transaction.Type, transaction.Amount,
                            transaction.Amount, ActionType.Delete).CurrentBalance2;

                        accountRepository.UpdataBalance(transaction.AccountId, currentBalance, conn, tx);
                    }
                    else if (transaction.Type == TransactionType.Transfer.ToString() || transaction.Type == TransactionType.Withdrawl.ToString()
                        || transaction.Type == TransactionType.Payment.ToString())
                    {
                        decimal currentBalance1 = accountRepository.GetBalanceAfter(transaction.CategoryId, transaction.Type, transaction.Amount,
                                transaction.Amount, ActionType.Delete).CurrentBalance1;

                        accountRepository.UpdataBalance(transaction.CategoryId, currentBalance1, conn, tx);

                        decimal currentBalance2 = accountRepository.GetBalanceAfter(transaction.AccountId, transaction.Type, transaction.Amount,
                                transaction.Amount, ActionType.Delete).CurrentBalance2;

                        accountRepository.UpdataBalance(transaction.AccountId, currentBalance2, conn, tx);
                    }

                    tx.Commit();

                }
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw ex;
            }
        }
    }
}
