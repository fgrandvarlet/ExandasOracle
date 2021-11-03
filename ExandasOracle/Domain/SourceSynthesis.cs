using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    class SourceSynthesis
    {
        const string ENTITY = "SOURCE";
        public string Name { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }

        public void Compare(SourceSynthesis target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            if (this.Text != target.Text)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.Name, this.Type, Strings.PropertyDifference, "TEXT", this.Text, target.Text
                    ));
            }
        }

    }
}
