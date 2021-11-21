using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class ObjectPrivilege
    {
        const string ENTITY = "OBJECT PRIVILEGE";
        public string Grantee { get; set; }
        public string TableSchema { get; set; }
        public string TableName { get; set; }
        public string Privilege { get; set; }
        public string Grantable { get; set; }
        public string Hierarchy { get; set; }
        public string Common { get; set; }
        public string Type { get; set; }
        public string Inherited { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSet"></param>
        /// <param name="list"></param>
        public void Compare(ObjectPrivilege target, ComparisonSet comparisonSet, List<DeltaReport> list)
        {
            var objectValue = string.Format("{0}/{1}@{2}", this.Privilege, this.TableName, this.Grantee);

            if (this.TableSchema != target.TableSchema && comparisonSet.Schema1 == comparisonSet.Schema2)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, objectValue, null, Strings.PropertyDifference, "TABLE_SCHEMA", this.TableSchema, target.TableSchema
                    ));
            }
            if (this.Grantable != target.Grantable)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, objectValue, null, Strings.PropertyDifference, "GRANTABLE", this.Grantable, target.Grantable
                    ));
            }
            if (this.Hierarchy != target.Hierarchy)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, objectValue, null, Strings.PropertyDifference, "HIERARCHY", this.Hierarchy, target.Hierarchy
                    ));
            }
            if (this.Common != target.Common)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, objectValue, null, Strings.PropertyDifference, "COMMON", this.Common, target.Common
                    ));
            }
            if (this.Type != target.Type)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, objectValue, null, Strings.PropertyDifference, "TYPE", this.Type, target.Type
                    ));
            }
        }

    }
}
