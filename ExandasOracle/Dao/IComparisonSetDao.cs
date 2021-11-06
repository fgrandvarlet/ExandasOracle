using System;
using System.Data;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao
{
    public interface IComparisonSetDao
    {
        DataTable GetDataTable(Criteria criteria);

        ComparisonSet Get(Guid uid);

        void Add(ComparisonSet cs);

        void Save(ComparisonSet cs);

        void Delete(ComparisonSet cs);

    }
}
