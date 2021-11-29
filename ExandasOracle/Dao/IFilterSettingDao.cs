using System;
using System.Collections.Generic;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao
{
    public interface IFilterSettingDao
    {
        DataTable GetDataTable(Criteria criteria);

        FilterSetting Get(int id);

        void Add(FbTransaction tran, FilterSetting fs);

        void Add(FilterSetting fs);

        void Delete(FilterSetting fs);

        string GetFilteringWhereClause(Guid comparisonSetUid);

        List<FilterSetting> GetListByComparisonSetUid(Guid comparisonSetUid);

    }
}
