using System;
using System.Collections.Generic;
using System.Data;

namespace MoneyCare.Repository.Mapping
{
    public class Mapper
    {
        public static T MapObject<T>(IDataReader rdr, IDataMapper<T> rowMapper)
        {
            T obj = default(T);

            if (rdr.Read())
            {
                obj = rowMapper.Map(rdr);
            }

            return obj;
        }

        public static List<T> MapList<T>(IDataReader rdr, IDataMapper<T> rowMapper)
        {
            List<T> list = new List<T>();

            while (rdr.Read())
            {
                list.Add(rowMapper.Map(rdr));
            }
            return list;

        }

    }
}
