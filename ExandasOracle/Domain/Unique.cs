using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class Unique : AbstractConstraint
    {
        const string ENTITY = "UNIQUE";
        public string Rely { get; set; }
        public string IndexOwner { get; set; }
        public string IndexName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(Unique target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            this.Compare(target, comparisonSetUid, list, ENTITY);

            if (this.Rely != target.Rely)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ConstraintName, this.TableName, Strings.PropertyDifference, "RELY", this.Rely, target.Rely
                    ));
            }
            if (this.IndexOwner != target.IndexOwner)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ConstraintName, this.TableName, Strings.PropertyDifference, "INDEX_OWNER", this.IndexOwner, target.IndexOwner
                    ));
            }
            if (this.IndexName != target.IndexName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ConstraintName, this.TableName, Strings.PropertyDifference, "INDEX_NAME", this.IndexName, target.IndexName
                    ));
            }
        }

    }
}
