using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;

namespace MoneyCare.Model
{
    public enum ActionType
    {
        Add, Edit, Delete
    }

    public interface IAccountRepository
    {
        Account GetById(Guid id);
        Account GetByName(string name);
        List<Account> GetAll();
        List<Account> GetByType(AccountType accountType);
        void Save(Account account);
        void Update(Account account);
        void Delete(Guid id);
        decimal GetBalance(Guid accountId);
        decimal GetBalanceAfter(Guid accountId, string transactionType, decimal amount, ActionType actionType);
        void UpdataBalance(Guid accountId, decimal currentBalance, IEntityManager em, EntityMap.Transaction tx);
  
    }
}
