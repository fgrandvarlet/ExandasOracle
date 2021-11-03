using System;
using System.Collections.Generic;

namespace ExandasOracle.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class ClusterColumn : AbstractColumn
    {
        /// <summary>
        /// 
        /// </summary>
        const string ENTITY = "CLUSTER COLUMN";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(ClusterColumn target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            this.Compare(target, comparisonSetUid, list, ENTITY);
        }

    }
}
