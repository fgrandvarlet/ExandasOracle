using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class IndexExpression
    {
        const string ENTITY = "INDEX EXPRESSION";
        public string IndexName { get; set; }
        public string TableOwner { get; set; }
        public string TableName { get; set; }
        public string ColumnExpression { get; set; }
        public decimal ColumnPosition { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSet"></param>
        /// <param name="list"></param>
        public void Compare(IndexExpression target, ComparisonSet comparisonSet, List<DeltaReport> list)
        {
            var objectValue = string.Format("{0}#{1}", this.IndexName, this.ColumnPosition); 
            
            if (this.TableOwner != target.TableOwner && comparisonSet.Schema1 == comparisonSet.Schema2)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, objectValue, this.TableName, Strings.PropertyDifference, "TABLE_OWNER", this.TableOwner, target.TableOwner
                    ));
            }
            if (this.TableName != target.TableName)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, objectValue, this.TableName, Strings.PropertyDifference, "TABLE_NAME", this.TableName, target.TableName
                    ));
            }
            if (this.ColumnExpression != target.ColumnExpression)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, objectValue, this.TableName, Strings.PropertyDifference, "COLUMN_EXPRESSION", this.ColumnExpression, target.ColumnExpression
                    ));
            }
        }

    }
}