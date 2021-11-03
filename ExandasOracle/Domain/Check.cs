using System;
using System.Collections.Generic;
using System.Text;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class Check : AbstractConstraint
    {
        const string ENTITY = "CHECK";
        public string SearchCondition { get; set; }
        public string SearchConditionVC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(Check target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            this.Compare(target, comparisonSetUid, list, ENTITY);

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
