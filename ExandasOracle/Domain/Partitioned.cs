using System;
using System.Collections.Generic;

using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public abstract class Partitioned
    {
        public string TableName { get; set; }
        public string PartitioningType { get; set; }
        public string SubpartitioningType { get; set; }
        public decimal PartitionCount { get; set; }
        public decimal? DefSubpartitionCount { get; set; }
        public decimal PartitioningKeyCount { get; set; }
        public decimal? SubpartitioningKeyCount { get; set; }
        public string DefTablespaceName { get; set; }
        public string DefLogging { get; set; }
        public string Interval { get; set; }
        public string Autolist { get; set; }
        public string IntervalSubpartition { get; set; }
        public string AutolistSubpartition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        /// <param name="entity"></param>
        /// <param name="objectValue"></param>
        /// <param name="parentObject"></param>
        protected void Compare(Partitioned target, Guid comparisonSetUid, List<DeltaReport> list, string entity, string objectValue, string parentObject)
        {
            if (this.PartitioningType != target.PartitioningType)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, LabelId.PropertyDifference, "PARTITIONING_TYPE", this.PartitioningType, target.PartitioningType
                    ));
            }
            if (this.SubpartitioningType != target.SubpartitioningType)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, LabelId.PropertyDifference, "SUBPARTITIONING_TYPE", this.SubpartitioningType, target.SubpartitioningType
                    ));
            }
            if (this.PartitionCount != target.PartitionCount)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, LabelId.PropertyDifference, "PARTITION_COUNT", this.PartitionCount.ToString(), target.PartitionCount.ToString()
                    ));
            }
            if (this.DefSubpartitionCount != target.DefSubpartitionCount)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, LabelId.PropertyDifference, "DEF_SUBPARTITION_COUNT", this.DefSubpartitionCount.ToString(), target.DefSubpartitionCount.ToString()
                    ));
            }
            if (this.PartitioningKeyCount != target.PartitioningKeyCount)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, LabelId.PropertyDifference, "PARTITIONING_KEY_COUNT", this.PartitioningKeyCount.ToString(), target.PartitioningKeyCount.ToString()
                    ));
            }
            if (this.SubpartitioningKeyCount != target.SubpartitioningKeyCount)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, LabelId.PropertyDifference, "SUBPARTITIONING_KEY_COUNT", this.SubpartitioningKeyCount.ToString(), target.SubpartitioningKeyCount.ToString()
                    ));
            }
            if (this.DefTablespaceName != target.DefTablespaceName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, LabelId.PropertyDifference, "DEF_TABLESPACE_NAME", this.DefTablespaceName, target.DefTablespaceName
                    ));
            }
            if (this.DefLogging != target.DefLogging)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, LabelId.PropertyDifference, "DEF_LOGGING", this.DefLogging, target.DefLogging
                    ));
            }
            if (this.Interval != target.Interval)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, LabelId.PropertyDifference, "INTERVAL", this.Interval, target.Interval
                    ));
            }
            if (this.Autolist != target.Autolist)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, LabelId.PropertyDifference, "AUTOLIST", this.Autolist, target.Autolist
                    ));
            }
            if (this.IntervalSubpartition != target.IntervalSubpartition)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, LabelId.PropertyDifference, "INTERVAL_SUBPARTITION", this.IntervalSubpartition, target.IntervalSubpartition
                    ));
            }
            if (this.AutolistSubpartition != target.AutolistSubpartition)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, LabelId.PropertyDifference, "AUTOLIST_SUBPARTITION", this.AutolistSubpartition, target.AutolistSubpartition
                    ));
            }
        }

    }
}
