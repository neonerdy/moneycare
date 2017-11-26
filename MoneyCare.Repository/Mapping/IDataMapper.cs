
using System;
using System.Collections.Generic;
using System.Data;

namespace MoneyCare.Repository.Mapping
{
    public interface IDataMapper<T>
    {
        T Map(IDataReader rdr);
    }


}
