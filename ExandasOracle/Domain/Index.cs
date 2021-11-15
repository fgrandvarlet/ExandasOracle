using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public abstract class Index
    {
        public string IndexName { get; set; }
        public string IndexType { get; set; }
        public string TableName { get; set; }
        public string Uniqueness { get; set; }
        public string Compression { get; set; }
        public decimal? PrefixLength { get; set; }
        public string TablespaceName { get; set; }
        public decimal? IncludeColumn { get; set; }
        public string Logging { get; set; }
        public string Status { get; set; }
        public string Degree { get; set; }
        public string Partitioned { get; set; }
        public string Temporary { get; set; }
        public string Duration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        /// <param name="entity"></param>
        protected void Compare(Index target, Guid comparisonSetUid, List<DeltaReport> list, string entity)
        {
            if (this.IndexType != target.IndexType)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.IndexName, this.TableName, Strings.PropertyDifference, "INDEX_TYPE", this.IndexType, target.IndexType
                    ));
            }
            if (this.TableName != target.TableName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.IndexName, this.TableName, Strings.PropertyDifference, "TABLE_NAME", this.TableName, target.TableName
                    ));
            }
            if (this.Uniqueness != target.Uniqueness)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.IndexName, this.TableName, Strings.PropertyDifference, "UNIQUENESS", this.Uniqueness, target.Uniqueness
                    ));
            }
            if (this.Compression != target.Compression)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.IndexName, this.TableName, Strings.PropertyDifference, "COMPRESSION", this.Compression, target.Compression
                    ));
            }
            if (this.PrefixLength != target.PrefixLength)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.IndexName, this.TableName, Strings.PropertyDifference, "PREFIX_LENGTH", this.PrefixLength.ToString(), target.PrefixLength.ToString()
                    ));
            }
            if (this.TablespaceName != target.TablespaceName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.IndexName, this.TableName, Strings.PropertyDifference, "TABLESPACE_NAME", this.TablespaceName, target.TablespaceName
                    ));
            }
            if (this.IncludeColumn != target.IncludeColumn)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.IndexName, this.TableName, Strings.PropertyDifference, "INCLUDE_COLUMN", this.IncludeColumn.ToString(), target.IncludeColumn.ToString()
                    ));
            }
            if (this.Logging != target.Logging)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.IndexName, this.TableName, Strings.PropertyDifference, "LOGGING", this.Logging, target.Logging
                    ));
            }
            if (this.Status != target.Status)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.IndexName, this.TableName, Strings.PropertyDifference, "STATUS", this.Status, target.Status
                    ));
            }
            if (this.Degree != target.Degree)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.IndexName, this.TableName, Strings.PropertyDifference, "DEGREE", this.Degree, target.Degree
                    ));
            }
            if (this.Partitioned != target.Partitioned)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.IndexName, this.TableName, Strings.PropertyDifference, "PARTITIONED", this.Partitioned, target.Partitioned
                    ));
            }
            if (this.Temporary != target.Temporary)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.IndexName, this.TableName, Strings.PropertyDifference, "TEMPORARY", this.Temporary, target.Temporary
                    ));
            }
            if (this.Duration != target.Duration)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.IndexName, this.TableName, Strings.PropertyDifference, "DURATION", this.Duration, target.Duration
                    ));
            }
        }

    }
}
