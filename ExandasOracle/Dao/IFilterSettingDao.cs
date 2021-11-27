using System;
using System.Data;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao
{
    public interface IFilterSettingDao
    {
        DataTable GetDataTable(Criteria criteria);

        FilterSetting Get(int id);

        void Add(FilterSetting fs);

        void Delete(FilterSetting fs);

        string GetFilteringWhereClause(Guid comparisonSetUid);
        
    }
}
