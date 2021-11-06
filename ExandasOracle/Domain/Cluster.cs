using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class Cluster
    {
        const string ENTITY = "CLUSTER";
        public string ClusterName { get; set; }
        public string TablespaceName { get; set; }
        public string ClusterType { get; set; }
        public string Function { get; set; }
        public decimal? Hashkeys { get; set; }
        public string Degree { get; set; }
        public string Cache { get; set; }
        public string SingleTable { get; set; }
        public string Dependencies { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(Cluster target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            if (this.TablespaceName != target.TablespaceName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ClusterName, null, Strings.PropertyDifference, "TABLESPACE_NAME", this.TablespaceName, target.TablespaceName
                    ));
            }
            if (this.ClusterType != target.ClusterType)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ClusterName, null, Strings.PropertyDifference, "CLUSTER_TYPE", this.ClusterType, target.ClusterType
                    ));
            }
            if (this.Function != target.Function)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ClusterName, null, Strings.PropertyDifference, "FUNCTION", this.Function, target.Function
                    ));
            }
            if (this.Hashkeys != target.Hashkeys)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ClusterName, null, Strings.PropertyDifference, "HASHKEYS", this.Hashkeys.ToString(), target.Hashkeys.ToString()
                    ));
            }
            if (this.Degree != target.Degree)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ClusterName, null, Strings.PropertyDifference, "DEGREE", this.Degree, target.Degree
                    ));
            }
            if (this.Cache != target.Cache)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ClusterName, null, Strings.PropertyDifference, "CACHE", this.Cache, target.Cache
                    ));
            }
            if (this.SingleTable != target.SingleTable)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ClusterName, null, Strings.PropertyDifference, "SINGLE_TABLE", this.SingleTable, target.SingleTable
                    ));
            }
            if (this.Dependencies != target.Dependencies)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ClusterName, null, Strings.PropertyDifference, "DEPENDENCIES", this.Dependencies, target.Dependencies
                    ));
            }
        }

    }
}
