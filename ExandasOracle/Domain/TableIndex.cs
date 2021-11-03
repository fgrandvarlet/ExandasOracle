using System;
using System.Collections.Generic;
using System.Text;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class TableIndex
    {
        /// <summary>
        /// 
        /// </summary>
        const string ENTITY = "TABLE INDEX";

        /// <summary>
        /// 
        /// </summary>
        public string IndexName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IndexType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Uniqueness { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Compression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PrefixLength { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TablespaceName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? IncludeColumn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Logging { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Degree { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Partitioned { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Temporary { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(TableIndex target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            if (this.IndexType != target.IndexType)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.IndexName, this.TableName, Strings.PropertyDifference, "INDEX_TYPE", this.IndexType, target.IndexType
                    ));
            }
            if (this.TableName != target.TableName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.IndexName, this.TableName, Strings.PropertyDifference, "TABLE_NAME", this.TableName, target.TableName
                    ));
            }
            if (this.Uniqueness != target.Uniqueness)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.IndexName, this.TableName, Strings.PropertyDifference, "UNIQUENESS", this.Uniqueness, target.Uniqueness
                    ));
            }
            if (this.Compression != target.Compression)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.IndexName, this.TableName, Strings.PropertyDifference, "COMPRESSION", this.Compression, target.Compression
                    ));
            }
            if (this.PrefixLength != target.PrefixLength)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.IndexName, this.TableName, Strings.PropertyDifference, "PREFIX_LENGTH", this.PrefixLength.ToString(), target.PrefixLength.ToString()
                    ));
            }
            if (this.TablespaceName != target.TablespaceName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.IndexName, this.TableName, Strings.PropertyDifference, "TABLESPACE_NAME", this.TablespaceName, target.TablespaceName
                    ));
            }
            if (this.IncludeColumn != target.IncludeColumn)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.IndexName, this.TableName, Strings.PropertyDifference, "INCLUDE_COLUMN", this.IncludeColumn.ToString(), target.IncludeColumn.ToString()
                    ));
            }
            if (this.Logging != target.Logging)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.IndexName, this.TableName, Strings.PropertyDifference, "LOGGING", this.Logging, target.Logging
                    ));
            }
            if (this.Status != target.Status)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.IndexName, this.TableName, Strings.PropertyDifference, "STATUS", this.Status, target.Status
                    ));
            }
            if (this.Degree != target.Degree)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.IndexName, this.TableName, Strings.PropertyDifference, "DEGREE", this.Degree, target.Degree
                    ));
            }
            if (this.Partitioned != target.Partitioned)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.IndexName, this.TableName, Strings.PropertyDifference, "PARTITIONED", this.Partitioned, target.Partitioned
                    ));
            }
            if (this.Temporary != target.Temporary)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.IndexName, this.TableName, Strings.PropertyDifference, "TEMPORARY", this.Temporary, target.Temporary
                    ));
            }
            if (this.Duration != target.Duration)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.IndexName, this.TableName, Strings.PropertyDifference, "DURATION", this.Duration, target.Duration
                    ));
            }
        }

    }
}
