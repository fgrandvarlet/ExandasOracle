using System;
using System.Collections.Generic;

namespace ExandasOracle.Domain
{
    public class ClusterColumn : Column
    {
        const string ENTITY = "CLUSTER COLUMN";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(ClusterColumn target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            base.Compare(target, comparisonSetUid, list, ENTITY);
        }

    }
}
