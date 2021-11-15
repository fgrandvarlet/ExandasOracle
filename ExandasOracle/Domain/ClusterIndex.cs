using System;
using System.Collections.Generic;

namespace ExandasOracle.Domain
{
    public class ClusterIndex : Index
    {
        const string ENTITY = "CLUSTER INDEX";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(ClusterIndex target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            base.Compare(target, comparisonSetUid, list, ENTITY);
        }

    }
}
