using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class TableComment
    {
        const string ENTITY = "TABLE COMMENT";
        public string TableName { get; set; }
        public string Comments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(TableComment target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            if (this.Comments != target.Comments)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "COMMENTS", this.Comments, target.Comments
                    ));
            }
        }

    }
}
