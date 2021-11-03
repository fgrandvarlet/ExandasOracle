using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao
{
    /// <summary>
    /// 
    /// </summary>
    public interface IComparisonSetDao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        DataTable GetDataTable(Criteria criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        ComparisonSet Get(Guid uid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cs"></param>
        void Add(ComparisonSet cs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cs"></param>
        void Save(ComparisonSet cs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cs"></param>
        void Delete(ComparisonSet cs);

    }
}
