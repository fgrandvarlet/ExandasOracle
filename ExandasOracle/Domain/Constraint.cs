using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class Constraint : AbstractConstraint
    {
        const string ENTITY = "CONSTRAINT";
        public string ConstraintType { get; set; }
        public string SearchCondition { get; set; }
        public string SearchConditionVC { get; set; }

        public void Compare(Constraint target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            this.Compare(target, comparisonSetUid, list, ENTITY);

            if (this.ConstraintType != target.ConstraintType)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ConstraintName, this.TableName, Strings.PropertyDifference, "CONSTRAINT_TYPE", this.ConstraintType, target.ConstraintType
                    ));
            }
            if (this.SearchCondition != target.SearchCondition)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ConstraintName, this.TableName, Strings.PropertyDifference, "SEARCH_CONDITION", this.SearchCondition, target.SearchCondition
                    ));
            }
            else if (this.SearchConditionVC != target.SearchConditionVC)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ConstraintName, this.TableName, Strings.PropertyDifference, "SEARCH_CONDITION_VC", this.SearchConditionVC, target.SearchConditionVC
                    ));
            }
        }

    }
}
