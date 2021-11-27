using System;
using System.Collections.Generic;

using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public class TableSubpartition : Subpartition
    {
        const string ENTITY = "TABLE SUBPARTITION";
        public string TableName { get; set; }
        public string CompressFor { get; set; }
        public string Indexing { get; set; }
        public string ReadOnly { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(TableSubpartition target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            var parentObject = string.Format("{0}.{1}", this.TableName, this.PartitionName);

            base.Compare(target, comparisonSetUid, list, ENTITY, this.SubpartitionName, parentObject);
            base.Compare(target, comparisonSetUid, list, ENTITY, parentObject);

            if (this.CompressFor != target.CompressFor)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.SubpartitionName, parentObject, LabelId.PropertyDifference, "COMPRESS_FOR", this.CompressFor, target.CompressFor
                    ));
            }
            if (this.Indexing != target.Indexing)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.SubpartitionName, parentObject, LabelId.PropertyDifference, "INDEXING", this.Indexing, target.Indexing
                    ));
            }
            if (this.ReadOnly != target.ReadOnly)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.SubpartitionName, parentObject, LabelId.PropertyDifference, "READ_ONLY", this.ReadOnly, target.ReadOnly
                    ));
            }
        }

    }
}
