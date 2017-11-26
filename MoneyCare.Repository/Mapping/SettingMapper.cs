using System;
using MoneyCare.Model;
using System.Data;


namespace MoneyCare.Repository.Mapping
{
    public class SettingMapper : IDataMapper<Setting>
    {

        public Setting Map(IDataReader rdr)
        {
            Setting setting = new Setting();

            setting.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            setting.Key = rdr["Key"] is DBNull ? string.Empty : (string)rdr["Key"];
            setting.Value = rdr["Value"] is DBNull ? string.Empty : (string)rdr["Value"];


            return setting;
        
        
        }
    }
}
