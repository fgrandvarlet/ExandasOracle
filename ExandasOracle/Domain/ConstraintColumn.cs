using System;
using System.Collections.Generic;

using ExandasOracle.Dao;

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
            string parentObject = string.Format("{0}.{1}", TableName, ConstraintName);
            
            if (this.Position != target.Position)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ColumnName, parentObject, LabelId.PropertyDifference, "POSITION", this.Position.ToString(), target.Position.ToString()
                    ));
            }
        }

    }
}
