using System;
using System.Collections.Generic;

using ExandasOracle.Core;
using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    class SourceSynthesis
    {
        const string ENTITY = "SOURCE";
        public string Name { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(SourceSynthesis target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            if (this.Text.TrimEnd() != target.Text.TrimEnd())
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.Name, this.Type, LabelId.PropertyDifference, "TEXT", Defs.TruncateTooLong(this.Text), Defs.TruncateTooLong(target.Text)
                    ));
            }
        }

    }
}
