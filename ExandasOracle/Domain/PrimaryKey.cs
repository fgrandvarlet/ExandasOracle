using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class PrimaryKey : AbstractConstraint
    {
        const string ENTITY = "PRIMARY KEY";
        public string Rely { get; set; }
        public string IndexOwner { get; set; }
        public string IndexName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSet"></param>
        /// <param name="list"></param>
        public void Compare(PrimaryKey target, ComparisonSet comparisonSet, List<DeltaReport> list)
        {
            this.Compare(target, comparisonSet.Uid, list, ENTITY);

            if (this.Rely != target.Rely)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.ConstraintName, this.TableName, Strings.PropertyDifference, "RELY", this.Rely, target.Rely
                    ));
            }
            if (this.IndexOwner != target.IndexOwner && comparisonSet.Schema1 == comparisonSet.Schema2)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.ConstraintName, this.TableName, Strings.PropertyDifference, "INDEX_OWNER", this.IndexOwner, target.IndexOwner
                    ));
            }
            if (this.IndexName != target.IndexName)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.ConstraintName, this.TableName, Strings.PropertyDifference, "INDEX_NAME", this.IndexName, target.IndexName
                    ));
            }
        }

    }
}
