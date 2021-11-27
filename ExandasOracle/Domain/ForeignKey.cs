using System.Collections.Generic;

using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public class ForeignKey : AbstractConstraint
    {
        const string ENTITY = "FOREIGN KEY";
        public string ROwner { get; set; }
        public string RConstraintName { get; set; }
        public string DeleteRule { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSet"></param>
        /// <param name="list"></param>
        public void Compare(ForeignKey target, ComparisonSet comparisonSet, List<DeltaReport> list)
        {
            base.Compare(target, comparisonSet.Uid, list, ENTITY);

            if (this.ROwner != target.ROwner && comparisonSet.Schema1 == comparisonSet.Schema2)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.ConstraintName, this.TableName, LabelId.PropertyDifference, "R_OWNER", this.ROwner, target.ROwner
                    ));
            }
            if (this.RConstraintName != target.RConstraintName)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.ConstraintName, this.TableName, LabelId.PropertyDifference, "R_CONSTRAINT_NAME", this.RConstraintName, target.RConstraintName
                    ));
            }
            if (this.DeleteRule != target.DeleteRule)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.ConstraintName, this.TableName, LabelId.PropertyDifference, "DELETE_RULE", this.DeleteRule, target.DeleteRule
                    ));
            }
        }

    }
}
