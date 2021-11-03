using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class ConstraintColumn
    {
        const string ENTITY = "CONSTRAINT COLUMN";
        public string ConstraintName { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public decimal? Position { get; set; }

        public void Compare(ConstraintColumn target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            // TODO concaténer TableName et ConstraintName pour parentObject
            
            if (this.Position != target.Position)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ColumnName, null, Strings.PropertyDifference, "POSITION", this.Position.ToString(), target.Position.ToString()
                    ));
            }
        }

    }
}
