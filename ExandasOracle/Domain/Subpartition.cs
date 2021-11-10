using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public abstract class Subpartition : AbstractPartition
    {
        public string SubpartitionName { get; set; }
        public decimal? SubpartitionPosition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        /// <param name="entity"></param>
        /// <param name="parentObject"></param>
        protected void Compare(Subpartition target, Guid comparisonSetUid, List<DeltaReport> list, string entity, string parentObject)
        {
            if (this.SubpartitionPosition != target.SubpartitionPosition)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.SubpartitionName, parentObject, Strings.PropertyDifference, "SUBPARTITION_POSITION", this.SubpartitionPosition.ToString(), target.SubpartitionPosition.ToString()
                    ));
            }
        }

    }
}
