using System;
using System.Collections.Generic;

using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public class ColumnComment
    {
        const string ENTITY = "COLUMN COMMENT";
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string Comments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>        
        public void Compare(ColumnComment target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            if (this.Comments != target.Comments)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ColumnName, this.TableName, LabelId.PropertyDifference, "COMMENTS", this.Comments, target.Comments
                    ));
            }
        }

    }
}
