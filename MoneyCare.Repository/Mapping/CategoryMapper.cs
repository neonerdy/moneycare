using System;
using System.Collections.Generic;
using MoneyCare.Model;
using System.Data;

namespace MoneyCare.Repository.Mapping
{
    public class CategoryMapper : IDataMapper<Category>
    {

        public string ConvertToLocal(string type)
        {
            string categoryType = string.Empty;

            if (type == "Income") categoryType = "Pendapatan";
            if (type == "Expense") categoryType = "Pengeluaran";
         
            return categoryType;

        }

        public Category Map(IDataReader rdr)
        {
            Category category = new Category();

            category.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            category.Name = rdr["Name"] is DBNull ? string.Empty : (string)rdr["Name"];
            category.Type = rdr["Type"] is DBNull ? string.Empty : ConvertToLocal((string)rdr["Type"]);
            category.Group = rdr["Group"] is DBNull ? string.Empty : (string)rdr["Group"];
            category.IsBudgeted = rdr["IsBudgeted"] is DBNull ? false : (bool)rdr["IsBudgeted"];
            category.Budget = rdr["Budget"] is DBNull ? 0 : (decimal)rdr["Budget"];


            return category;
        }
    }
}
