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
    public interface IConnectionParamsDao
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
        ConnectionParams Get(Guid uid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        void Add(ConnectionParams cp);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        void Save(ConnectionParams cp);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        void Delete(ConnectionParams cp);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<ConnectionParams> GetList();
    }
}
