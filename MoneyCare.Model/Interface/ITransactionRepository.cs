using System;
using System.Collections.Generic;


namespace MoneyCare.Model
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAll(int month, int year);
        Transaction GetById(Guid id);
        List<Transaction> GetByType(TransactionType transactionType, int month, int year);
        decimal GetTotalTransaction(CategoryType categoryType, int month, int year);
        void Save(Transaction transaction);
        void Update(Transaction transaction);
        void Delete(Transaction transaction);

    }
}
