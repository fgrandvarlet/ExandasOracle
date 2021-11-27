using System;
using System.Collections.Generic;

using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public class PartitionedTable : Partitioned
    {
        const string ENTITY = "PARTITIONED TABLE";
        public string Status { get; set; }
        public string DefCompression { get; set; }
        public string DefCompressFor { get; set; }
        public string RefPtnConstraintName { get; set; }
        public string IsNested { get; set; }
        public string DefIndexing { get; set; }
        public string DefReadOnly { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(PartitionedTable target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            base.Compare(target, comparisonSetUid, list, ENTITY, this.TableName, null);

            if (this.Status != target.Status)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "STATUS", this.Status, target.Status
                    ));
            }
            if (this.DefCompression != target.DefCompression)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "DEF_COMPRESSION", this.DefCompression, target.DefCompression
                    ));
            }
            if (this.DefCompressFor != target.DefCompressFor)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "DEF_COMPRESS_FOR", this.DefCompressFor, target.DefCompressFor
                    ));
            }
            if (this.RefPtnConstraintName != target.RefPtnConstraintName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "REF_PTN_CONSTRAINT_NAME", this.RefPtnConstraintName, target.RefPtnConstraintName
                    ));
            }
            if (this.IsNested != target.IsNested)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "IS_NESTED", this.IsNested, target.IsNested
                    ));
            }
            if (this.DefIndexing != target.DefIndexing)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "DEF_INDEXING", this.DefIndexing, target.DefIndexing
                    ));
            }
            if (this.DefReadOnly != target.DefReadOnly)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, LabelId.PropertyDifference, "DEF_READ_ONLY", this.DefReadOnly, target.DefReadOnly
                    ));
            }
        }

    }
}
