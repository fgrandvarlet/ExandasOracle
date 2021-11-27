using System;
using System.Collections.Generic;

using ExandasOracle.Core;
using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public class Constraint : AbstractConstraint
    {
        const string ENTITY = "CONSTRAINT";
        public string ConstraintType { get; set; }
        public string SearchCondition { get; set; }
        public string SearchConditionVC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(Constraint target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            base.Compare(target, comparisonSetUid, list, ENTITY);

            if (this.ConstraintType != target.ConstraintType)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ConstraintName, this.TableName, LabelId.PropertyDifference, "CONSTRAINT_TYPE", this.ConstraintType, target.ConstraintType
                    ));
            }
            if (this.SearchCondition != target.SearchCondition)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ConstraintName, this.TableName, LabelId.PropertyDifference, "SEARCH_CONDITION", Defs.TruncateTooLong(this.SearchCondition), Defs.TruncateTooLong(target.SearchCondition)
                    ));
            }
            else if (this.SearchConditionVC != target.SearchConditionVC)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ConstraintName, this.TableName, LabelId.PropertyDifference, "SEARCH_CONDITION_VC", this.SearchConditionVC, target.SearchConditionVC
                    ));
            }
        }

    }
}
