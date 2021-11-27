using System;
using System.Collections.Generic;

using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public class DatabaseLink
    {
        const string ENTITY = "DATABASE LINK";
        public string DbLink { get; set; }
        public string Username { get; set; }
        public string Host { get; set; }
        public string ShardInternal { get; set; }
        public string Valid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(DatabaseLink target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            if (this.Username != target.Username)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.DbLink, null, LabelId.PropertyDifference, "USERNAME", this.Username, target.Username
                    ));
            }
            if (this.Host != target.Host)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.DbLink, null, LabelId.PropertyDifference, "HOST", this.Host, target.Host
                    ));
            }
            if (this.ShardInternal != target.ShardInternal)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.DbLink, null, LabelId.PropertyDifference, "SHARD_INTERNAL", this.ShardInternal, target.ShardInternal
                    ));
            }
            if (this.Valid != target.Valid)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.DbLink, null, LabelId.PropertyDifference, "VALID", this.Valid, target.Valid
                    ));
            }
        }

    }
}
