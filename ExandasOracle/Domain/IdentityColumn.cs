using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class IdentityColumn
    {
        const string ENTITY = "IDENTITY COLUMN";
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string GenerationType { get; set; }
        public string SequenceName { get; set; }
        public string IdentityOptions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSet"></param>
        /// <param name="list"></param>
        public void Compare(IdentityColumn target, ComparisonSet comparisonSet, List<DeltaReport> list)
        {
            if (this.GenerationType != target.GenerationType)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.ColumnName, this.TableName, Strings.PropertyDifference, "GENERATION_TYPE", this.GenerationType, target.GenerationType
                    ));
            }
            if (this.SequenceName != target.SequenceName)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.ColumnName, this.TableName, Strings.PropertyDifference, "SEQUENCE_NAME", this.SequenceName, target.SequenceName
                    ));
            }
            if (this.IdentityOptions != target.IdentityOptions)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.ColumnName, this.TableName, Strings.PropertyDifference, "IDENTITY_OPTIONS", this.IdentityOptions, target.IdentityOptions
                    ));
            }
        }

    }
}