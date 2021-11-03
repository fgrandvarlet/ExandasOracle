using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public abstract class AbstractConstraint
    {
        public string ConstraintName { get; set; }
        public string TableName { get; set; }
        public string Status { get; set; }
        public string Deferrable { get; set; }
        public string Deferred { get; set; }
        public string Validated { get; set; }
        public string Invalid { get; set; }
        public string ViewRelated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        /// <param name="entity"></param>
        protected void Compare(AbstractConstraint target, Guid comparisonSetUid, List<DeltaReport> list, string entity)
        {
            if (this.Status != target.Status)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ConstraintName, this.TableName, Strings.PropertyDifference, "STATUS", this.Status, target.Status
                    ));
            }
            if (this.Deferrable != target.Deferrable)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ConstraintName, this.TableName, Strings.PropertyDifference, "DEFERRABLE", this.Deferrable, target.Deferrable
                    ));
            }
            if (this.Deferred != target.Deferred)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ConstraintName, this.TableName, Strings.PropertyDifference, "DEFERRED", this.Deferred, target.Deferred
                    ));
            }
            if (this.Validated != target.Validated)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ConstraintName, this.TableName, Strings.PropertyDifference, "VALIDATED", this.Validated, target.Validated
                    ));
            }
            if (this.Invalid != target.Invalid)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ConstraintName, this.TableName, Strings.PropertyDifference, "INVALID", this.Invalid, target.Invalid
                    ));
            }
            if (this.ViewRelated != target.ViewRelated)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ConstraintName, this.TableName, Strings.PropertyDifference, "VIEW_RELATED", this.ViewRelated, target.ViewRelated
                    ));
            }
        }

    }
}
