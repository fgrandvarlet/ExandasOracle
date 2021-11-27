using System;
using System.Collections.Generic;

using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public abstract class Partition : AbstractPartition
    {
        public string Composite { get; set; }
        public decimal? SubpartitionCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        /// <param name="entity"></param>
        /// <param name="parentObject"></param>
        protected void Compare(Partition target, Guid comparisonSetUid, List<DeltaReport> list, string entity, string parentObject)
        {
            if (this.Composite != target.Composite)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.PartitionName, parentObject, LabelId.PropertyDifference, "COMPOSITE", this.Composite, target.Composite
                    ));
            }
            if (this.SubpartitionCount != target.SubpartitionCount)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.PartitionName, parentObject, LabelId.PropertyDifference, "SUBPARTITION_COUNT", this.SubpartitionCount.ToString(), target.SubpartitionCount.ToString()
                    ));
            }
        }

    }
}
