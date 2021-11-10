using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class IndexSubpartition : Subpartition
    {
        const string ENTITY = "INDEX SUBPARTITION";
        public string IndexName { get; set; }
        public string Status { get; set; }
        public string Parameters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(IndexSubpartition target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            var parentObject = string.Format("{0}.{1}", this.IndexName, this.PartitionName);

            base.Compare(target, comparisonSetUid, list, ENTITY, this.SubpartitionName, parentObject);
            base.Compare(target, comparisonSetUid, list, ENTITY, parentObject);

            if (this.Status != target.Status)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.SubpartitionName, parentObject, Strings.PropertyDifference, "STATUS", this.Status, target.Status
                    ));
            }
            if (this.Parameters != target.Parameters)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.SubpartitionName, parentObject, Strings.PropertyDifference, "PARAMETERS", this.Parameters, target.Parameters
                    ));
            }
        }

    }
}
