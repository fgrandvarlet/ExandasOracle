using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

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
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(ForeignKey target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            this.Compare(target, comparisonSetUid, list, ENTITY);

            if (this.ROwner != target.ROwner)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ConstraintName, this.TableName, Strings.PropertyDifference, "R_OWNER", this.ROwner, target.ROwner
                    ));
            }
            if (this.RConstraintName != target.RConstraintName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ConstraintName, this.TableName, Strings.PropertyDifference, "R_CONSTRAINT_NAME", this.RConstraintName, target.RConstraintName
                    ));
            }
            if (this.DeleteRule != target.DeleteRule)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ConstraintName, this.TableName, Strings.PropertyDifference, "DELETE_RULE", this.DeleteRule, target.DeleteRule
                    ));
            }
        }

    }
}
