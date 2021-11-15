using System;
using System.Collections.Generic;

namespace ExandasOracle.Domain
{
    public class ViewColumn : Column
    {
        const string ENTITY = "VIEW COLUMN";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(ViewColumn target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            base.Compare(target, comparisonSetUid, list, ENTITY);
        }

    }
}