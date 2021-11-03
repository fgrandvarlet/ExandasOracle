using System;
using System.Collections.Generic;

namespace ExandasOracle.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class ViewColumn : AbstractColumn
    {
        /// <summary>
        /// 
        /// </summary>
        const string ENTITY = "VIEW COLUMN";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(ViewColumn target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            this.Compare(target, comparisonSetUid, list, ENTITY);
        }

    }
}