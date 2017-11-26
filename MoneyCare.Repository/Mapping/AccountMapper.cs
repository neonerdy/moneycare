using System;
using System.Collections.Generic;
using MoneyCare.Model;
using System.Data;

namespace MoneyCare.Repository.Mapping
{
    public class AccountMapper : IDataMapper<Account>
    {

        public string ConvertToINA(string type)
        {
            string accountType = string.Empty;

            if (type == "Cash") accountType = "Kas";
            if (type == "Bank") accountType = "Bank";
            if (type == "CreditCard") accountType = "Kartu Kredit";

            return accountType;

        }

        public Account Map(IDataReader rdr)
        {
            Account account = new Account();

            account.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            account.Name = rdr["Name"] is DBNull ? string.Empty : (string)rdr["Name"];
            account.Type = rdr["Type"] is DBNull ? string.Empty : ConvertToINA((string)rdr["Type"]);
            account.Balance = rdr["Balance"] is DBNull ? 0 : (decimal)rdr["Balance"];

            return account;
        }
    }
}
