using System;
using System.Collections.Generic;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao
{
    public interface IComparisonSetDao
    {
        DataTable GetDataTable(Criteria criteria);

        ComparisonSet Get(Guid uid);

        void Add(FbTransaction tran, ComparisonSet cs);

        void Add(ComparisonSet cs);

        void Save(ComparisonSet cs);

        void Delete(ComparisonSet cs);

        List<ComparisonSet> GetList();
    }
}
