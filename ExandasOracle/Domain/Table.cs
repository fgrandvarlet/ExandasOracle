﻿using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class Table
    {
        const string ENTITY = "TABLE";
        public string TableName { get; set; }
        public string TablespaceName { get; set; }
        public string ClusterName { get; set; }
        public string IOTName { get; set; }
        public string Status { get; set; }
        public string Logging { get; set; }
        public string Degree { get; set; }
        public string Partitioned { get; set; }
        public string IOTType { get; set; }
        public string Temporary { get; set; }
        public string Nested { get; set; }
        public string Duration { get; set; }
        public string ClusterOwner { get; set; }
        public string Compression { get; set; }
        public string CompressFor { get; set; }
        public string Dropped { get; set; }
        public string ReadOnly { get; set; }
        public string Clustering { get; set; }
        public string HasIdentity { get; set; }
        public string ContainerData { get; set; }
        public string DefaultCollation { get; set; }
        public string External { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(Table target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            if (this.TablespaceName != target.TablespaceName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "TABLESPACE_NAME", this.TablespaceName, target.TablespaceName
                    ));
            }
            if (this.ClusterName != target.ClusterName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "CLUSTER_NAME", this.ClusterName, target.ClusterName
                    ));
            }
            if (this.IOTName != target.IOTName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "IOT_NAME", this.IOTName, target.IOTName
                    ));
            }
            if (this.Status != target.Status)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "STATUS", this.Status, target.Status
                    ));
            }
            if (this.Logging != target.Logging)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "LOGGING", this.Logging, target.Logging
                    ));
            }
            if (this.Degree != target.Degree)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "DEGREE", this.Degree, target.Degree
                    ));
            }
            if (this.Partitioned != target.Partitioned)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "PARTITIONED", this.Partitioned, target.Partitioned
                    ));
            }
            if (this.IOTType != target.IOTType)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "IOT_TYPE", this.IOTType, target.IOTType
                    ));
            }
            if (this.Temporary != target.Temporary)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "TEMPORARY", this.Temporary, target.Temporary
                    ));
            }
            if (this.Nested != target.Nested)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "NESTED", this.Nested, target.Nested
                    ));
            }
            if (this.Duration != target.Duration)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "DURATION", this.Duration, target.Duration
                    ));
            }
            if (this.ClusterOwner != target.ClusterOwner)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "CLUSTER_OWNER", this.ClusterOwner, target.ClusterOwner
                    ));
            }
            if (this.Compression != target.Compression)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "COMPRESSION", this.Compression, target.Compression
                    ));
            }
            if (this.CompressFor != target.CompressFor)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "COMPRESS_FOR", this.CompressFor, target.CompressFor
                    ));
            }
            if (this.Dropped != target.Dropped)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "DROPPED", this.Dropped, target.Dropped
                    ));
            }
            if (this.ReadOnly != target.ReadOnly)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "READ_ONLY", this.ReadOnly, target.ReadOnly
                    ));
            }
            if (this.Clustering != target.Clustering)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "CLUSTERING", this.Clustering, target.Clustering
                    ));
            }
            if (this.HasIdentity != target.HasIdentity)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "HAS_IDENTITY", this.HasIdentity, target.HasIdentity
                    ));
            }
            if (this.ContainerData != target.ContainerData)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "CONTAINER_DATA", this.ContainerData, target.ContainerData
                    ));
            }
            if (this.DefaultCollation != target.DefaultCollation)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "DEFAULT_COLLATION", this.DefaultCollation, target.DefaultCollation
                    ));
            }
            if (this.External != target.External)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "EXTERNAL", this.External, target.External
                    ));
            }
        }

    }
}
