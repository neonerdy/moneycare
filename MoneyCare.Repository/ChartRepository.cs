using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MoneyCare.Model;
using System.Data.SqlServerCe;

namespace MoneyCare.Repository
{
    public class ChartRepository
    {
        private string tableName = "Transactions";
 
        private const string CATEGORY_CREDIT_CARD = "CreditCard";
        private const string CATEGORY_ACCOUNT_PAYABLE = "Hutang";
        private const string CATEGORY_ACCOUNT_RECEIVABLE = "Piutang";
       
        public Dictionary<string, decimal> GetYearlyExpense(string categoryName,int year)
        {
            Dictionary<string, decimal> yearlyExpense = new Dictionary<string, decimal>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT DATEPART(month,t.Date) AS Month,SUM(t.Amount) AS Amount FROM " + tableName + " t "
                         + "INNER JOIN Categories c ON t.CategoryId=c.ID "
                         + "WHERE c.Name=@CategoryName AND DATEPART(year,t.Date)=@Year "
                         + "GROUP BY DATEPART(month,t.Date)";

                var cmd = new SqlCeCommand(sql, conn);

                cmd.Parameters.Add("@CategoryName", SqlDbType.NVarChar).Value = categoryName;
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;

                using (var rdr = cmd.ExecuteReader())
                {                   

                    while (rdr.Read())
                    {
                        int month = (int)rdr["Month"];

                        yearlyExpense.Add(month.ToString(), (decimal)rdr["Amount"]);
                    }
                }
            }


            return yearlyExpense;

        }



        public Dictionary<string, decimal> GetAccountPayableBalance()
        {          
            Dictionary<string, decimal> monthlyExpense = new Dictionary<string, decimal>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql1 = "SELECT c.Name,SUM(Amount) AS Amount FROM " + tableName + " t INNER JOIN Categories c ON t.CategoryId=c.ID "
                    + "WHERE c.[Group]='" + CATEGORY_ACCOUNT_PAYABLE + "' AND t.Type='Income' "
                    + "GROUP BY c.Name";

                 var cmd1 = new SqlCeCommand(sql1, conn);
                            
                using (var rdr1 = cmd1.ExecuteReader())
                {

                    while (rdr1.Read())
                    {
                        bool isRdr2Exist = false;

                        string sql2 = "SELECT c.Name,SUM(Amount) AS Amount FROM " + tableName + " t INNER JOIN Categories c ON t.CategoryId=c.ID "
                                + "WHERE c.[Group]='" + CATEGORY_ACCOUNT_PAYABLE + "' AND t.Type='Expense' AND c.Name='" + rdr1["Name"].ToString() + "' "
                                + "GROUP BY c.Name";

                        var cmd2 = new SqlCeCommand(sql2, conn);

                        using (var rdr2 = cmd2.ExecuteReader())
                        {
                            while (rdr2.Read())
                            {
                                isRdr2Exist = true;

                                decimal balance = (decimal)rdr1["Amount"] - (decimal)rdr2["Amount"];
                                monthlyExpense.Add((string)rdr2["Name"], balance);
                            }

                            if (!isRdr2Exist)
                            {
                                isRdr2Exist = false;
                                monthlyExpense.Add((string)rdr1["Name"], (decimal)rdr1["Amount"]);
                            }
                        }

                    }
                }
            }

            return monthlyExpense;

        }



        public Dictionary<string, decimal> GetAccountReceivableBalance()
        {
           

            Dictionary<string, decimal> monthlyExpense = new Dictionary<string, decimal>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql1 = "SELECT c.Name,SUM(Amount) AS Amount FROM " + tableName + " t INNER JOIN Categories c ON t.CategoryId=c.ID "
                    + "WHERE c.[Group]='" + CATEGORY_ACCOUNT_RECEIVABLE + "' AND t.Type='Expense' "
                    + "GROUP BY c.Name";

                var cmd1 = new SqlCeCommand(sql1, conn);

                using (var rdr1 = cmd1.ExecuteReader())
                {
                    while (rdr1.Read())
                    {
                        bool isRdr2Exist = false;

                        string sql2 = "SELECT c.Name,SUM(Amount) AS Amount FROM " + tableName + " t INNER JOIN Categories c ON t.CategoryId=c.ID "
                             + "WHERE c.[Group]='" + CATEGORY_ACCOUNT_RECEIVABLE + "' AND t.Type='Income' AND c.Name='" + rdr1["Name"].ToString() + "' "
                             + "GROUP BY c.Name";

                        var cmd2 = new SqlCeCommand(sql2, conn);

                        using (var rdr2 = cmd2.ExecuteReader())
                        {
                            while (rdr2.Read())
                            {
                                isRdr2Exist = true;

                                decimal balance = (decimal)rdr1["Amount"] - (decimal)rdr2["Amount"];
                                monthlyExpense.Add((string)rdr2["Name"], balance);
                            }

                            if (!isRdr2Exist)
                            {
                                isRdr2Exist = false;
                                monthlyExpense.Add((string)rdr1["Name"], (decimal)rdr1["Amount"]);
                            }
                            
                        }

                    }

                }


            }


            return monthlyExpense;

        }

     


        public Dictionary<string, decimal> GetMonthlyCreditCard(int month, int year)
        {
            
            Dictionary<string, decimal> monthlyExpense = new Dictionary<string, decimal>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql1 = "SELECT a.Name,SUM(t.Amount) AS Amount FROM " + tableName + " t "
                         + "INNER JOIN Accounts a ON t.AccountId=a.ID "
                         + "WHERE a.[Type]='" + CATEGORY_CREDIT_CARD + "' "
                         + "GROUP BY a.Name ORDER BY Name";
                              

                var cmd1 = new SqlCeCommand(sql1, conn);
         
                using (var rdr1 = cmd1.ExecuteReader())
                {
                    while (rdr1.Read())
                    {
                        bool isRdr2Exist = false;

                        string sql2 = "SELECT a.Name,SUM(t.Amount) AS Amount FROM " + tableName + " t "
                            + "INNER JOIN Accounts a ON t.CategoryId=a.ID "
                            + "WHERE t.Type='Payment' AND a.Name='" + rdr1["Name"].ToString() + "' "
                            + "GROUP BY a.Name";


                        var cmd2 = new SqlCeCommand(sql2, conn);

                        using (var rdr2 = cmd2.ExecuteReader())
                        {
                            while (rdr2.Read())
                            {
                                isRdr2Exist = true;

                                decimal balance = (decimal)rdr1["Amount"] - (decimal)rdr2["Amount"];
                                monthlyExpense.Add((string)rdr2["Name"], balance);
                            }

                            if (!isRdr2Exist)
                            {
                                isRdr2Exist = false;
                                monthlyExpense.Add((string)rdr1["Name"], (decimal)rdr1["Amount"]);
                            }

                    }
                   
                    }
                }
            }


            return monthlyExpense;

        }



        public Dictionary<string, decimal> GetMonthlyIncome(int month, int year)
        {
       
            Dictionary<string, decimal> monthlyIncome = new Dictionary<string, decimal>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT c.Name,SUM(t.Amount) AS Amount FROM " + tableName + " t "
                    + "INNER JOIN Categories c ON t.CategoryId=c.ID "
                    + "WHERE t.Type='Income' AND DATEPART(month,t.Date)=@Month AND DATEPART(year,t.Date)=@Year "
                    + "GROUP BY c.Name ORDER BY NAme";

                var cmd=new SqlCeCommand(sql,conn);

                cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;

                using(var rdr=cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        monthlyIncome.Add((string)rdr["Name"], (decimal)rdr["Amount"]);

                    }
                }
            }

            return monthlyIncome;

        }


        public Dictionary<string, decimal> GetMonthlyExpense(int month, int year)
        {
            Dictionary<string, decimal> monthlyExpense = new Dictionary<string, decimal>();


            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql1 = "SELECT c.Name,SUM(t.Amount) AS Amount FROM " + tableName + " t "
                         + "LEFT JOIN Categories c ON t.CategoryId=c.ID "
                         + "WHERE t.Type='Expense' AND DATEPART(month,t.Date)=@Month AND DATEPART(year,t.Date)=@Year "
                         + "GROUP BY c.Name";
                     
                 string sql2="SELECT a.Name,SUM(t.Amount) AS Amount FROM " + tableName + " t "
                         + "INNER JOIN Accounts a ON t.CategoryId=a.ID "
                         + "WHERE t.Type='Payment' AND DATEPART(month,t.Date)=@Month AND DATEPART(year,t.Date)=@Year "
                         + "GROUP BY a.Name";
                
                var cmd1 = new SqlCeCommand(sql1, conn);

                cmd1.Parameters.Add("@Month", SqlDbType.Int).Value = month;
                cmd1.Parameters.Add("@Year", SqlDbType.Int).Value = year;

                using (var rdr1 = cmd1.ExecuteReader())
                {
                    while (rdr1.Read())
                    {
                        monthlyExpense.Add((string)rdr1["Name"], (decimal)rdr1["Amount"]);
                    }
                }

                var cmd2 = new SqlCeCommand(sql2, conn);

                cmd2.Parameters.Add("@Month", SqlDbType.Int).Value = month;
                cmd2.Parameters.Add("@Year", SqlDbType.Int).Value = year;

                using (var rdr2 = cmd2.ExecuteReader())
                {
                    while (rdr2.Read())
                    {
                        monthlyExpense.Add((string)rdr2["Name"], (decimal)rdr2["Amount"]);
                    }
                }
                
            }


            return monthlyExpense;

        }


        public Dictionary<string, decimal> GetCurrentCashAndBank()
        {
            Dictionary<string, decimal> currentCashAndBank = new Dictionary<string, decimal>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT Name, Balance FROM Accounts"
                    + " ORDER BY Type,Name DESC";

                var cmd = new SqlCeCommand(sql, conn);
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        currentCashAndBank.Add((string)rdr["Name"], (decimal)rdr["Balance"]);

                    }
                }
            }

            return currentCashAndBank;

        }


        public Dictionary<string, decimal> GetCurrentCreditCard()
        {
            Dictionary<string, decimal> currentCashAndBank = new Dictionary<string, decimal>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT Name, Balance FROM Accounts"
                    + " WHERE Type='CreditCard' ORDER BY Balance DESC";

                var cmd = new SqlCeCommand(sql, conn);
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        currentCashAndBank.Add((string)rdr["Name"], (decimal)rdr["Balance"]);

                    }
                }
            }

            return currentCashAndBank;

        }



        public Dictionary<string, decimal> GetMonthlyAsset(int month, int year)
        {
            Dictionary<string, decimal> monthlyAsset = new Dictionary<string, decimal>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();


                string date = month + "/" + DateTime.Now.Day + "/" + year;

                string sql = "SELECT a.Name,(SUM(cfi.Debet)- SUM(cfi.Credit)) AS CurrentBalance FROM CashFlowItems AS cfi "
                    + "INNER JOIN CashFlows cf ON cfi.CashFlowId=cf.ID "
                    + "INNER JOIN  Accounts AS a ON cfi.AccountId = a.ID "
                    + "WHERE a.Type='Asset' AND  MONTH(cf.Date)=" + month + " AND Year(cf.Date)=" + year
                    + "GROUP BY a.Name ORDER BY CurrentBalance DESC";


                var cmd = new SqlCeCommand(sql, conn);
                cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;

                
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        monthlyAsset.Add((string)rdr["Name"], (decimal)rdr["Amount"]);

                    }
                }
            }

            return monthlyAsset;

        }


        public Dictionary<string, decimal> GetCurrentCashFlow(CategoryType categoryType,string categoryName,int month, int year)
        {
            Dictionary<string, decimal> currentCashFlow = new Dictionary<string, decimal>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT DATEPART(month,Date) AS Month,SUM(t.Amount) AS Amount FROM " + tableName + " t "
                    + "INNER JOIN Categories c ON t.CategoryId=c.ID "
                    + "WHERE c.Type=@Type AND c.Name=@Name "
                    + "AND DATEPART(year,Date)=@Year "
                    + "GROUP BY DATEPART(month,Date) ORDER BY DATEPART(month,Date)";

                var cmd = new SqlCeCommand(sql, conn);

                cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = categoryType.ToString();
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = categoryName;
                cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;

                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        currentCashFlow.Add(rdr["Month"].ToString(), (decimal)rdr["Amount"]);
                    }
                }
            }

            return currentCashFlow;

        }

    }
}
