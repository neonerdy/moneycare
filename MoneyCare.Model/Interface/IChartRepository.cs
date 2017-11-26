using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyCare.Model
{
    public interface IChartRepository
    {
        Dictionary<string, decimal> GetMonthlyIncome(int month, int year);
        Dictionary<string, decimal> GetMonthlyExpense(int month, int year);
        Dictionary<string, decimal> GetCurrentCashAndBank();
        Dictionary<string, decimal> GetMonthlyAsset(int month, int year);
        Dictionary<string, decimal> GetMonthlyLiability(int month, int year);
        Dictionary<int, decimal> GetYearlyCashFlow(string type, string accountName, DateTime from, DateTime to);
    }
}
