using System;
using System.Collections.Generic;

using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public class ClusterColumnMapping
    {
        const string ENTITY = "CLUSTER COLUMN MAPPING";
        public string ClusterName { get; set; }
        public string CluColumnName { get; set; }
        public string TableName { get; set; }
        public string TabColumnName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(ClusterColumnMapping target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            if (this.TabColumnName != target.TabColumnName)
            {
                var objectValue = string.Format("{0}->{1}", this.CluColumnName, this.TableName);
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, objectValue, this.ClusterName, LabelId.PropertyDifference, "TAB_COLUMN_NAME", this.TabColumnName, target.TabColumnName
                    ));
            }
        }

    }
}
