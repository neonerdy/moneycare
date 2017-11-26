using System;
using System.Collections.Generic;
using System.Data;
using MoneyCare.Model;

namespace MoneyCare.Repository.Mapping
{
    public class TransactionMapper : IDataMapper<Transaction>
    {
        public Transaction Map(IDataReader rdr)
        {
            Transaction transaction = new Transaction();

            transaction.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            transaction.Type = rdr["Type"] is DBNull ? string.Empty : (string)rdr["Type"];
            transaction.Date = rdr["Date"] is DBNull ? DateTime.Now : (DateTime)rdr["Date"];
            transaction.Amount = rdr["Amount"] is DBNull ? 0 : (decimal)rdr["Amount"];
            transaction.Description = rdr["Description"] is DBNull ? string.Empty : (string)rdr["Description"];
            transaction.CategoryId = rdr["CategoryId"] is DBNull ? Guid.Empty : (Guid)rdr["CategoryId"];
            transaction.AccountId = rdr["AccountId"] is DBNull ? Guid.Empty : (Guid)rdr["AccountId"];
            transaction.Notes = rdr["Notes"] is DBNull ? string.Empty : (string)rdr["Notes"];



            return transaction;

        }
    }
}
