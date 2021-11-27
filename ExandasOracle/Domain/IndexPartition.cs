using System;
using System.Collections.Generic;

using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public class IndexPartition : Partition
    {
        const string ENTITY = "INDEX PARTITION";
        public string IndexName { get; set; }
        public string Status { get; set; }
        public string Parameters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(IndexPartition target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            base.Compare(target, comparisonSetUid, list, ENTITY, this.PartitionName, this.IndexName);
            base.Compare(target, comparisonSetUid, list, ENTITY, this.IndexName);

            if (this.Status != target.Status)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.PartitionName, this.IndexName, LabelId.PropertyDifference, "STATUS", this.Status, target.Status
                    ));
            }
            if (this.Parameters != target.Parameters)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.PartitionName, this.IndexName, LabelId.PropertyDifference, "PARAMETERS", this.Parameters, target.Parameters
                    ));
            }
        }

    }
}
