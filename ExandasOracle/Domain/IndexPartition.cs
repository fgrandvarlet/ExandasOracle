using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class IndexPartition
    {
        const string ENTITY = "INDEX PARTITION";
        public string IndexName { get; set; }
        public string Composite { get; set; }
        public string PartitionName { get; set; }
        public decimal? SubpartitionCount { get; set; }
        public string HighValue { get; set; }
        public decimal? HighValueLength { get; set; }
        public decimal? PartitionPosition { get; set; }
        public string Status { get; set; }
        public string TablespaceName { get; set; }
        public string Logging { get; set; }
        public string Compression { get; set; }

        public void Compare(IndexPartition target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            if (this.Composite != target.Composite)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.PartitionName, this.IndexName, Strings.PropertyDifference, "COMPOSITE", this.Composite, target.Composite
                    ));
            }
            if (this.SubpartitionCount != target.SubpartitionCount)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.PartitionName, this.IndexName, Strings.PropertyDifference, "SUBPARTITION_COUNT", this.SubpartitionCount.ToString(), target.SubpartitionCount.ToString()
                    ));
            }
            if (this.HighValue != target.HighValue)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.PartitionName, this.IndexName, Strings.PropertyDifference, "HIGH_VALUE", this.HighValue, target.HighValue
                    ));
            }
            if (this.HighValueLength != target.HighValueLength)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.PartitionName, this.IndexName, Strings.PropertyDifference, "HIGH_VALUE_LENGTH", this.HighValueLength.ToString(), target.HighValueLength.ToString()
                    ));
            }
            if (this.PartitionPosition != target.PartitionPosition)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.PartitionName, this.IndexName, Strings.PropertyDifference, "PARTITION_POSITION", this.PartitionPosition.ToString(), target.PartitionPosition.ToString()
                    ));
            }
            if (this.Status != target.Status)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.PartitionName, this.IndexName, Strings.PropertyDifference, "STATUS", this.Status, target.Status
                    ));
            }
            if (this.TablespaceName != target.TablespaceName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.PartitionName, this.IndexName, Strings.PropertyDifference, "TABLESPACE_NAME", this.TablespaceName, target.TablespaceName
                    ));
            }
            if (this.Logging != target.Logging)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.PartitionName, this.IndexName, Strings.PropertyDifference, "LOGGING", this.Logging, target.Logging
                    ));
            }
            if (this.Compression != target.Compression)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.PartitionName, this.IndexName, Strings.PropertyDifference, "COMPRESSION", this.Compression, target.Compression
                    ));
            }
        }

    }
}
