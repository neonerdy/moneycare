using System;
using System.Collections.Generic;
using System.Data;
using MoneyCare.Model;
using System.Data.SqlServerCe;
using MoneyCare.Repository.Mapping;


namespace MoneyCare.Repository
{
    public class AccountBalance
    {
        public decimal CurrentBalance1 { get; set; }
        public decimal CurrentBalance2 { get; set; }
    }
       
    public class AccountRepository 
    {
        private string tableName="Accounts";


        public decimal GetCurrentCashAndBank()
        {
            decimal totalCashBank = 0;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT SUM(Balance) AS SumOfBalance FROM Accounts WHERE Type IN ('Cash','Bank')";

                var cmd = new SqlCeCommand(sql, conn);
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        totalCashBank = rdr["SumOfBalance"] is DBNull ? 0 : (decimal)rdr["SumOfBalance"];
                    }                    
                }
            }

            return totalCashBank;
        }



        public Account GetById(Guid id)
        {
            Account account = null;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName + " WHERE ID='" + id + "'";
                
                var cmd = new SqlCeCommand(sql, conn);
                using (var rdr = cmd.ExecuteReader())
                {
                    account = Mapper.MapObject<Account>(rdr, new AccountMapper());      
                }
            }

            return account;
        }


        public Account QueryByName(string name)
        {
            Account account = null;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName + " WHERE Name LIKE '%" + name + "%'";

                var cmd = new SqlCeCommand(sql, conn);
              
                using (var rdr = cmd.ExecuteReader())
                {
                    account = Mapper.MapObject<Account>(rdr, new AccountMapper());
                }
            }

            return account;
        }



        public Account GetByName(string name)
        {
            Account account = null;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName + " WHERE Name=@Name";

                var cmd = new SqlCeCommand(sql, conn);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;

                using (var rdr = cmd.ExecuteReader())
                {
                    account = Mapper.MapObject<Account>(rdr, new AccountMapper());
                }
            }

            return account;
        }


        public List<Account> GetAll()
        {
            List<Account> accounts = new List<Account>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName + " ORDER BY Type,Name";

                var cmd = new SqlCeCommand(sql, conn);
                using (var rdr = cmd.ExecuteReader())
                {
                    accounts = Mapper.MapList<Account>(rdr, new AccountMapper());
                }
            }

            return accounts;
        }


        public List<Account> GetByCashAndBank()
        {
            List<Account> accounts = new List<Account>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string type = string.Empty;

                string sql = "SELECT * FROM " + tableName + " WHERE Type IN ('Cash','Bank')";

                var cmd = new SqlCeCommand(sql, conn);
                using (var rdr = cmd.ExecuteReader())
                {
                    accounts = Mapper.MapList<Account>(rdr, new AccountMapper());
                }
            }

            return accounts;
        }


        public List<Account> GetByType(AccountType accountType)
        {
            List<Account> accounts = new List<Account>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string type = string.Empty;
               
                string sql = "SELECT * FROM " + tableName + " WHERE Type='" + accountType.ToString() + "'";

                var cmd = new SqlCeCommand(sql, conn);
                using (var rdr = cmd.ExecuteReader())
                {
                    accounts = Mapper.MapList<Account>(rdr, new AccountMapper());
                }
            }

            return accounts;
        }



        public void Save(Account account)
        {
            try
            {
                using (var conn = new SqlCeConnection(Store.ConnectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO " + tableName + " (ID,Name,Type,Balance) VALUES (@ID,@Name,@Type,@Balance)";
                    
                    var cmd = new SqlCeCommand(sql, conn);
                    
                    cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = account.Name;
                    cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = account.Type;
                    cmd.Parameters.Add("@Balance", SqlDbType.Decimal).Value = account.Balance;

                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Update(Account account)
        {
            try
            {
                using (var conn = new SqlCeConnection(Store.ConnectionString))
                {
                    conn.Open();

                    string sql = "UPDATE " + tableName + " SET Name=@Name,"
                                 + "Type=@Type,Balance=@Balance WHERE ID=@ID";

                    var cmd = new SqlCeCommand(sql, conn);

                    cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = account.ID;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = account.Name;
                    cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = account.Type;
                    cmd.Parameters.Add("@Balance", SqlDbType.Decimal).Value = account.Balance;

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

                    string sql = "DELETE FROM  " + tableName + " WHERE ID='" + id + "'";

                    var cmd = new SqlCeCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


      

        public bool IsAccountUsed(Guid accountId)
        {
            bool isUsed = false;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();
                string sql = "SELECT COUNT(ID) FROM Transactions WHERE AccountId='" + accountId + "'";

                var cmd = new SqlCeCommand(sql, conn);
                int result=(int)cmd.ExecuteScalar();

                if (result > 0)
                {
                    isUsed = true;
                }
                else
                {
                    isUsed = false;
                }
            }

            return isUsed;
        }


        public AccountBalance GetBalanceAfter(Guid accountId, string transactionType, decimal currentAmount, decimal changedAmount, ActionType actionType)
        {
            AccountBalance accountBalance = new AccountBalance();
            decimal balance = GetBalance(accountId);

            if (transactionType == TransactionType.Income.ToString())
            {
                if (actionType == ActionType.Add)
                {
                    accountBalance.CurrentBalance2 = balance + changedAmount;
                }
                else if (actionType == ActionType.Edit)
                {
                    accountBalance.CurrentBalance2 = (balance - currentAmount) + changedAmount;
                }
                else if (actionType == ActionType.Delete)
                {
                    accountBalance.CurrentBalance2 = balance - currentAmount;
                }
            }
            else if (transactionType == TransactionType.Expense.ToString())
            {
                if (actionType == ActionType.Add)
                {
                    accountBalance.CurrentBalance2 = balance - changedAmount;
                }
                else if (actionType == ActionType.Edit)
                {
                    accountBalance.CurrentBalance2 = (currentAmount - changedAmount) + balance;
                }
                else if (actionType == ActionType.Delete)
                {
                    accountBalance.CurrentBalance2 = balance + currentAmount;
                }
            }
            else if (transactionType == TransactionType.Transfer.ToString()
                || transactionType == TransactionType.Withdrawl.ToString())
            {
                if (actionType == ActionType.Edit)
                {
                    //from account
                    accountBalance.CurrentBalance1 = balance - (changedAmount - currentAmount);

                    //to account
                    accountBalance.CurrentBalance2 = balance + (changedAmount - currentAmount);
                }
                else if (actionType == ActionType.Delete)
                {
                    //from account
                    accountBalance.CurrentBalance1 = balance + currentAmount;

                    //to account
                    accountBalance.CurrentBalance2 = balance - currentAmount;

                }
            }
            else if (transactionType == TransactionType.Payment.ToString())
            {
                if (actionType == ActionType.Edit)
                {
                    //credit card account
                    accountBalance.CurrentBalance1 = (balance - currentAmount) + changedAmount;

                    //bank account
                    accountBalance.CurrentBalance2 = (currentAmount - changedAmount) + balance;

                }
                else if (actionType == ActionType.Delete)
                {
                    //credit card account
                    accountBalance.CurrentBalance1 = balance - currentAmount;

                    //bank account
                    accountBalance.CurrentBalance2 = balance + currentAmount;
                }
            }

            return accountBalance;
        }





        public decimal GetBalance(Guid accountId)
        {
            decimal balance = 0;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM  " + tableName + " WHERE ID='" + accountId + "'";

                var cmd = new SqlCeCommand(sql, conn);
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        balance = (decimal)rdr["Balance"];
                    }
                }

                cmd.ExecuteNonQuery();
            }

            return balance;
        }



        public void UpdataBalance(Guid accountId, decimal currentBalance, SqlCeConnection conn,SqlCeTransaction tx)
        {
            string sql = "UPDATE " + tableName + " SET Balance=@Balance WHERE ID=@ID";

            var cmd = new SqlCeCommand(sql, conn);

            cmd.Transaction = tx;
            cmd.Parameters.Add("@Balance", SqlDbType.Decimal).Value = currentBalance;
            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = accountId;

            cmd.ExecuteNonQuery();
        }


       

    }
}
