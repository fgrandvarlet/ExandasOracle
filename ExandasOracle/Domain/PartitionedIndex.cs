using System;
using System.Collections.Generic;

using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public class PartitionedIndex : Partitioned
    {
        const string ENTITY = "PARTITIONED INDEX";
        public string IndexName { get; set; }
        public string Locality { get; set; }
        public string Alignment { get; set; }
        public string DefParameters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(PartitionedIndex target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            base.Compare(target, comparisonSetUid, list, ENTITY, this.IndexName, this.TableName);

            if (this.Locality != target.Locality)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "LOCALITY", this.Locality, target.Locality
                    ));
            }

            if (this.Alignment != target.Alignment)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "ALIGNMENT", this.Alignment, target.Alignment
                    ));
            }

            if (this.DefParameters != target.DefParameters)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.IndexName, this.TableName, LabelId.PropertyDifference, "DEF_PARAMETERS", this.DefParameters, target.DefParameters
                    ));
            }
        }

    }
}
