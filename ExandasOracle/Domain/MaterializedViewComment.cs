using System;
using System.Collections.Generic;

using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public class MaterializedViewComment
    {
        const string ENTITY = "MATERIALIZED VIEW COMMENT";
        public string MViewName { get; set; }
        public string Comments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(MaterializedViewComment target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            if (this.Comments != target.Comments)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.MViewName, null, LabelId.PropertyDifference, "COMMENTS", this.Comments, target.Comments
                    ));
            }
        }

    }
}
