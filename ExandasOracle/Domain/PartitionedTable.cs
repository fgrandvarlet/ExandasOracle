using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class PartitionedTable
    {
        const string ENTITY = "PARTITIONED TABLE";
        public string TableName { get; set; }
        public string PartitioningType { get; set; }
        public string SubpartitioningType { get; set; }
        public decimal PartitionCount { get; set; }
        public decimal? DefSubpartitionCount { get; set; }
        public decimal PartitioningKeyCount { get; set; }
        public decimal? SubpartitioningKeyCount { get; set; }
        public string Status { get; set; }
        public string DefTablespaceName { get; set; }
        public string DefLogging { get; set; }
        public string DefCompression { get; set; }
        public string DefCompressFor { get; set; }
        public string RefPtnConstraintName { get; set; }
        public string Interval { get; set; }
        public string Autolist { get; set; }
        public string IntervalSubpartition { get; set; }
        public string AutolistSubpartition { get; set; }
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
            if (this.PartitioningType != target.PartitioningType)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "PARTITIONING_TYPE", this.PartitioningType, target.PartitioningType
                    ));
            }
            if (this.SubpartitioningType != target.SubpartitioningType)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "SUBPARTITIONING_TYPE", this.SubpartitioningType, target.SubpartitioningType
                    ));
            }
            if (this.PartitionCount != target.PartitionCount)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "PARTITION_COUNT", this.PartitionCount.ToString(), target.PartitionCount.ToString()
                    ));
            }
            if (this.DefSubpartitionCount != target.DefSubpartitionCount)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "DEF_SUBPARTITION_COUNT", this.DefSubpartitionCount.ToString(), target.DefSubpartitionCount.ToString()
                    ));
            }
            if (this.PartitioningKeyCount != target.PartitioningKeyCount)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "PARTITIONING_KEY_COUNT", this.PartitioningKeyCount.ToString(), target.PartitioningKeyCount.ToString()
                    ));
            }
            if (this.SubpartitioningKeyCount != target.SubpartitioningKeyCount)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "SUBPARTITIONING_KEY_COUNT", this.SubpartitioningKeyCount.ToString(), target.SubpartitioningKeyCount.ToString()
                    ));
            }
            if (this.Status != target.Status)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "STATUS", this.Status, target.Status
                    ));
            }
            if (this.DefTablespaceName != target.DefTablespaceName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "DEF_TABLESPACE_NAME", this.DefTablespaceName, target.DefTablespaceName
                    ));
            }
            if (this.DefLogging != target.DefLogging)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "DEF_LOGGING", this.DefLogging, target.DefLogging
                    ));
            }
            if (this.DefCompression != target.DefCompression)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "DEF_COMPRESSION", this.DefCompression, target.DefCompression
                    ));
            }
            if (this.DefCompressFor != target.DefCompressFor)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "DEF_COMPRESS_FOR", this.DefCompressFor, target.DefCompressFor
                    ));
            }
            if (this.RefPtnConstraintName != target.RefPtnConstraintName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "REF_PTN_CONSTRAINT_NAME", this.RefPtnConstraintName, target.RefPtnConstraintName
                    ));
            }
            if (this.Interval != target.Interval)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "INTERVAL", this.Interval, target.Interval
                    ));
            }
            if (this.Autolist != target.Autolist)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "AUTOLIST", this.Autolist, target.Autolist
                    ));
            }
            if (this.IntervalSubpartition != target.IntervalSubpartition)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "INTERVAL_SUBPARTITION", this.IntervalSubpartition, target.IntervalSubpartition
                    ));
            }
            if (this.AutolistSubpartition != target.AutolistSubpartition)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "AUTOLIST_SUBPARTITION", this.AutolistSubpartition, target.AutolistSubpartition
                    ));
            }
            if (this.IsNested != target.IsNested)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "IS_NESTED", this.IsNested, target.IsNested
                    ));
            }
            if (this.DefIndexing != target.DefIndexing)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "DEF_INDEXING", this.DefIndexing, target.DefIndexing
                    ));
            }
            if (this.DefReadOnly != target.DefReadOnly)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "DEF_READ_ONLY", this.DefReadOnly, target.DefReadOnly
                    ));
            }
        }

    }
}
