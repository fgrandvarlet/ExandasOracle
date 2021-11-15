using System;
using System.Collections.Generic;

namespace ExandasOracle.Domain
{
    public class TableIndex : Index
    {
        const string ENTITY = "TABLE INDEX";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(TableIndex target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            base.Compare(target, comparisonSetUid, list, ENTITY);
        }

    }
}
