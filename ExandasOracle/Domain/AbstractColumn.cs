using System;
using System.Collections.Generic;

using ExandasOracle.Core;
using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public abstract class AbstractColumn
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public string DataTypeMod { get; set; }
        public string DataTypeOwner { get; set; }
        public decimal DataLength { get; set; }
        public decimal? DataPrecision { get; set; }
        public decimal? DataScale { get; set; }
        public string Nullable { get; set; }
        public decimal? ColumnId { get; set; }
        public decimal? DefaultLength { get; set; }
        public string DataDefault { get; set; }
        public decimal? CharLength { get; set; }
        public string CharUsed { get; set; }
        public string HiddenColumn { get; set; }
        public string VirtualColumn { get; set; }
        public string DefaultOnNull { get; set; }
        public string IdentityColumn { get; set; }
        public string Collation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        /// <param name="entity"></param>
        protected void Compare(AbstractColumn target, Guid comparisonSetUid, List<DeltaReport> list, string entity)
        {
            if (this.DataType != target.DataType)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ColumnName, this.TableName, Strings.PropertyDifference, "DATA_TYPE", this.DataType, target.DataType
                    ));
            }
            if (this.DataTypeMod != target.DataTypeMod)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ColumnName, this.TableName, Strings.PropertyDifference, "DATA_TYPE_MOD", this.DataTypeMod, target.DataTypeMod
                    ));
            }
            if (this.DataTypeOwner != target.DataTypeOwner)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ColumnName, this.TableName, Strings.PropertyDifference, "DATA_TYPE_OWNER", this.DataTypeOwner, target.DataTypeOwner
                    ));
            }
            if (this.DataLength != target.DataLength)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ColumnName, this.TableName, Strings.PropertyDifference, "DATA_LENGTH", this.DataLength.ToString(), target.DataLength.ToString()
                    ));
            }
            if (this.DataPrecision != target.DataPrecision)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ColumnName, this.TableName, Strings.PropertyDifference, "DATA_PRECISION", this.DataPrecision.ToString(), target.DataPrecision.ToString()
                    ));
            }
            if (this.DataScale != target.DataScale)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ColumnName, this.TableName, Strings.PropertyDifference, "DATA_SCALE", this.DataScale.ToString(), target.DataScale.ToString()
                    ));
            }
            if (this.Nullable != target.Nullable)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ColumnName, this.TableName, Strings.PropertyDifference, "NULLABLE", this.Nullable, target.Nullable
                    ));
            }
            if (this.ColumnId != target.ColumnId)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ColumnName, this.TableName, Strings.PropertyDifference, "COLUMN_ID", this.ColumnId.ToString(), target.ColumnId.ToString()
                    ));
            }
            if (this.DefaultLength != target.DefaultLength)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ColumnName, this.TableName, Strings.PropertyDifference, "DEFAULT_LENGTH", this.DefaultLength.ToString(), target.DefaultLength.ToString()
                    ));
            }
            if (this.DataDefault != target.DataDefault)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ColumnName, this.TableName, Strings.PropertyDifference, "DATA_DEFAULT", Defs.TruncateTooLong(this.DataDefault), Defs.TruncateTooLong(target.DataDefault)
                    ));
            }
            if (this.CharLength != target.CharLength)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ColumnName, this.TableName, Strings.PropertyDifference, "CHAR_LENGTH", this.CharLength.ToString(), target.CharLength.ToString()
                    ));
            }
            if (this.CharUsed != target.CharUsed)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ColumnName, this.TableName, Strings.PropertyDifference, "CHAR_USED", this.CharUsed, target.CharUsed
                    ));
            }
            if (this.HiddenColumn != target.HiddenColumn)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ColumnName, this.TableName, Strings.PropertyDifference, "HIDDEN_COLUMN", this.HiddenColumn, target.HiddenColumn
                    ));
            }
            if (this.VirtualColumn != target.VirtualColumn)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ColumnName, this.TableName, Strings.PropertyDifference, "VIRTUAL_COLUMN", this.VirtualColumn, target.VirtualColumn
                    ));
            }
            if (this.DefaultOnNull != target.DefaultOnNull)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ColumnName, this.TableName, Strings.PropertyDifference, "DEFAULT_ON_NULL", this.DefaultOnNull, target.DefaultOnNull
                    ));
            }
            if (this.IdentityColumn != target.IdentityColumn)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ColumnName, this.TableName, Strings.PropertyDifference, "IDENTITY_COLUMN", this.IdentityColumn, target.IdentityColumn
                    ));
            }
            if (this.Collation != target.Collation)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, entity, this.ColumnName, this.TableName, Strings.PropertyDifference, "COLLATION", this.Collation, target.Collation
                    ));
            }
        }

    }
}
