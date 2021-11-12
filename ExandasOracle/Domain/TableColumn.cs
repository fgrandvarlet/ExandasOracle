using System;
using System.Collections.Generic;

namespace ExandasOracle.Domain
{
    public class TableColumn : Column
    {
        const string ENTITY = "TABLE COLUMN";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(TableColumn target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            base.Compare(target, comparisonSetUid, list, ENTITY);
        }

    }
}
