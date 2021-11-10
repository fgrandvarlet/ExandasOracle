using System;
using System.Collections.Generic;

using ExandasOracle.Core;
using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public abstract class AbstractPartition
    {
        public string PartitionName { get; set; }
        public string HighValue { get; set; }
        public decimal? HighValueLength { get; set; }
        public decimal? PartitionPosition { get; set; }
        public string TablespaceName { get; set; }
        public string Logging { get; set; }
        public string Compression { get; set; }
        public string Interval { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        /// <param name="entity"></param>
        /// <param name="objectValue"></param>
        /// <param name="parentObject"></param>
        protected void Compare(AbstractPartition target, Guid comparisonSetUid, List<DeltaReport> list, string entity, string objectValue, string parentObject)
        {
            if (this.HighValue != target.HighValue)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, Strings.PropertyDifference, "HIGH_VALUE", Defs.TruncateTooLong(this.HighValue), Defs.TruncateTooLong(target.HighValue)
                    ));
            }
            if (this.HighValueLength != target.HighValueLength)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, Strings.PropertyDifference, "HIGH_VALUE_LENGTH", this.HighValueLength.ToString(), target.HighValueLength.ToString()
                    ));
            }
            if (this.PartitionPosition != target.PartitionPosition)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, Strings.PropertyDifference, "PARTITION_POSITION", this.PartitionPosition.ToString(), target.PartitionPosition.ToString()
                    ));
            }
            if (this.TablespaceName != target.TablespaceName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, Strings.PropertyDifference, "TABLESPACE_NAME", this.TablespaceName, target.TablespaceName
                    ));
            }
            if (this.Logging != target.Logging)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, Strings.PropertyDifference, "LOGGING", this.Logging, target.Logging
                    ));
            }
            if (this.Compression != target.Compression)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, Strings.PropertyDifference, "COMPRESSION", this.Compression, target.Compression
                    ));
            }
            if (this.Interval != target.Interval)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, objectValue, parentObject, Strings.PropertyDifference, "INTERVAL", this.Interval, target.Interval
                    ));
            }
        }

    }
}
