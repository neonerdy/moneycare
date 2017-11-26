using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlServerCe;

namespace MoneyCare.Repository
{
    public class SqlCeClientFactory : DbProviderFactory
    {

        public static readonly SqlCeClientFactory Instance = new SqlCeClientFactory();

        public override DbCommand CreateCommand()
        {
            return new SqlCeCommand();
        }

        public override DbCommandBuilder CreateCommandBuilder()
        {
            return new SqlCeCommandBuilder();
        }

        public override DbConnection CreateConnection()
        {
            return new SqlCeConnection();
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            return new SqlCeDataAdapter();
        }

        public override DbParameter CreateParameter()
        {
            return new SqlCeParameter();
        }
    }

}
