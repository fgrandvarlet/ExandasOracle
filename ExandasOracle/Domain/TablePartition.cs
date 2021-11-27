using System;
using System.Collections.Generic;

using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public class TablePartition : Partition
    {
        const string ENTITY = "TABLE PARTITION";
        public string TableName { get; set; }
        public string CompressFor { get; set; }
        public string IsNested { get; set; }
        public string ParentTablePartition { get; set; }
        public string Indexing { get; set; }
        public string ReadOnly { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(TablePartition target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            base.Compare(target, comparisonSetUid, list, ENTITY, this.PartitionName, this.TableName);
            base.Compare(target, comparisonSetUid, list, ENTITY, this.TableName);

            if (this.CompressFor != target.CompressFor)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.PartitionName, this.TableName, LabelId.PropertyDifference, "COMPRESS_FOR", this.CompressFor, target.CompressFor
                    ));
            }
            if (this.IsNested != target.IsNested)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.PartitionName, this.TableName, LabelId.PropertyDifference, "IS_NESTED", this.IsNested, target.IsNested
                    ));
            }
            if (this.ParentTablePartition != target.ParentTablePartition)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.PartitionName, this.TableName, LabelId.PropertyDifference, "PARENT_TABLE_PARTITION", this.ParentTablePartition, target.ParentTablePartition
                    ));
            }
            if (this.Indexing != target.Indexing)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.PartitionName, this.TableName, LabelId.PropertyDifference, "INDEXING", this.Indexing, target.Indexing
                    ));
            }
            if (this.ReadOnly != target.ReadOnly)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.PartitionName, this.TableName, LabelId.PropertyDifference, "READ_ONLY", this.ReadOnly, target.ReadOnly
                    ));
            }
        }

    }
}
