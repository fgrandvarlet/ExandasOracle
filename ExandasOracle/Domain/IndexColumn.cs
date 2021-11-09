using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class IndexColumn
    {
        const string ENTITY = "INDEX COLUMN";
        public string IndexName { get; set; }
        public string TableOwner { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public decimal ColumnPosition { get; set; }
        public decimal ColumnLength { get; set; }
        public decimal? CharLength { get; set; }
        public string Descend { get; set; }
        public decimal? CollatedColumnId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSet"></param>
        /// <param name="list"></param>
        public void Compare(IndexColumn target, ComparisonSet comparisonSet, List<DeltaReport> list)
        {
            var parentObject = string.Format("{0}.{1}", this.TableName, this.IndexName);

            if (this.TableOwner != target.TableOwner && comparisonSet.Schema1 == comparisonSet.Schema2)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.ColumnName, parentObject, Strings.PropertyDifference, "TABLE_OWNER", this.TableOwner, target.TableOwner
                    ));
            }
            if (this.TableName != target.TableName)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.ColumnName, parentObject, Strings.PropertyDifference, "TABLE_NAME", this.TableName, target.TableName
                    ));
            }
            if (this.ColumnPosition != target.ColumnPosition)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.ColumnName, parentObject, Strings.PropertyDifference, "COLUMN_POSITION", this.ColumnPosition.ToString(), target.ColumnPosition.ToString()
                    ));
            }
            if (this.ColumnLength != target.ColumnLength)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.ColumnName, parentObject, Strings.PropertyDifference, "COLUMN_LENGTH", this.ColumnLength.ToString(), target.ColumnLength.ToString()
                    ));
            }
            if (this.CharLength != target.CharLength)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.ColumnName, parentObject, Strings.PropertyDifference, "CHAR_LENGTH", this.CharLength.ToString(), target.CharLength.ToString()
                    ));
            }
            if (this.Descend != target.Descend)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.ColumnName, parentObject, Strings.PropertyDifference, "DESCEND", this.Descend, target.Descend
                    ));
            }
            if (this.CollatedColumnId != target.CollatedColumnId)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.ColumnName, parentObject, Strings.PropertyDifference, "COLLATED_COLUMN_ID", this.CollatedColumnId.ToString(), target.CollatedColumnId.ToString()
                    ));
            }
        }

    }
}
