using System;
using System.Collections.Generic;

using ExandasOracle.Core;
using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class MaterializedView
    {
        const string ENTITY = "MATERIALIZED VIEW";
        public string MViewName { get; set; }
        public string ContainerName { get; set; }
        public string Query { get; set; }
        public decimal? QueryLen { get; set; }
        public string Updatable { get; set; }
        public string UpdateLog { get; set; }
        public string MasterRollbackSeg { get; set; }
        public string MasterLink { get; set; }
        public string RewriteEnabled { get; set; }
        public string RewriteCapability { get; set; }
        public string RefreshMode { get; set; }
        public string RefreshMethod { get; set; }
        public string BuildMode { get; set; }
        public string FastRefreshable { get; set; }
        public string UseNoIndex { get; set; }
        public string DefaultCollation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(MaterializedView target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            if (this.ContainerName != target.ContainerName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.MViewName, null, Strings.PropertyDifference, "CONTAINER_NAME", this.ContainerName, target.ContainerName
                    ));
            }
            if (this.Query != target.Query)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.MViewName, null, Strings.PropertyDifference, "QUERY", Defs.TruncateTooLong(this.Query), Defs.TruncateTooLong(target.Query)
                    ));
            }
            if (this.QueryLen != target.QueryLen)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.MViewName, null, Strings.PropertyDifference, "QUERY_LEN", this.QueryLen.ToString(), target.QueryLen.ToString()
                    ));
            }
            if (this.Updatable != target.Updatable)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.MViewName, null, Strings.PropertyDifference, "UPDATABLE", this.Updatable, target.Updatable
                    ));
            }
            if (this.UpdateLog != target.UpdateLog)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.MViewName, null, Strings.PropertyDifference, "UPDATE_LOG", this.UpdateLog, target.UpdateLog
                    ));
            }
            if (this.MasterRollbackSeg != target.MasterRollbackSeg)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.MViewName, null, Strings.PropertyDifference, "MASTER_ROLLBACK_SEG", this.MasterRollbackSeg, target.MasterRollbackSeg
                    ));
            }
            if (this.MasterLink != target.MasterLink)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.MViewName, null, Strings.PropertyDifference, "MASTER_LINK", this.MasterLink, target.MasterLink
                    ));
            }
            if (this.RewriteEnabled != target.RewriteEnabled)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.MViewName, null, Strings.PropertyDifference, "REWRITE_ENABLED", this.RewriteEnabled, target.RewriteEnabled
                    ));
            }
            if (this.RewriteCapability != target.RewriteCapability)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.MViewName, null, Strings.PropertyDifference, "REWRITE_CAPABILITY", this.RewriteCapability, target.RewriteCapability
                    ));
            }
            if (this.RefreshMode != target.RefreshMode)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.MViewName, null, Strings.PropertyDifference, "REFRESH_MODE", this.RefreshMode, target.RefreshMode
                    ));
            }
            if (this.RefreshMethod != target.RefreshMethod)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.MViewName, null, Strings.PropertyDifference, "REFRESH_METHOD", this.RefreshMethod, target.RefreshMethod
                    ));
            }
            if (this.BuildMode != target.BuildMode)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.MViewName, null, Strings.PropertyDifference, "BUILD_MODE", this.BuildMode, target.BuildMode
                    ));
            }
            if (this.FastRefreshable != target.FastRefreshable)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.MViewName, null, Strings.PropertyDifference, "FAST_REFRESHABLE", this.FastRefreshable, target.FastRefreshable
                    ));
            }
            if (this.UseNoIndex != target.UseNoIndex)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.MViewName, null, Strings.PropertyDifference, "USE_NO_INDEX", this.UseNoIndex, target.UseNoIndex
                    ));
            }
            if (this.DefaultCollation != target.DefaultCollation)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.MViewName, null, Strings.PropertyDifference, "DEFAULT_COLLATION", this.DefaultCollation, target.DefaultCollation
                    ));
            }
        }

    }
}