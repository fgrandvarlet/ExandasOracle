using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class View
    {
        const string ENTITY = "VIEW";
        public string ViewName { get; set; }
        public decimal? TextLength { get; set; }
        public string Text { get; set; }
        public string TextVC { get; set; }
        public string TypeText { get; set; }
        public string OidText { get; set; }
        public string ViewTypeOwner { get; set; }
        public string ViewType { get; set; }
        public string SuperviewName { get; set; }
        public string ReadOnly { get; set; }
        public string Bequeath { get; set; }
        public string DefaultCollation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(View target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            if (this.TextLength != target.TextLength)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ViewName, null, Strings.PropertyDifference, "TEXT_LENGTH", this.TextLength.ToString(), target.TextLength.ToString()
                    ));
            }
            if (this.TextVC != target.TextVC)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ViewName, null, Strings.PropertyDifference, "TEXT_VC", this.TextVC, target.TextVC
                    ));
            }
            else if (this.Text != target.Text)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ViewName, null, Strings.PropertyDifference, "TEXT", this.Text, target.Text
                    ));
            }
            if (this.TypeText != target.TypeText)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ViewName, null, Strings.PropertyDifference, "TYPE_TEXT", this.TypeText, target.TypeText
                    ));
            }
            if (this.OidText != target.OidText)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ViewName, null, Strings.PropertyDifference, "OID_TEXT", this.OidText, target.OidText
                    ));
            }
            if (this.ViewTypeOwner != target.ViewTypeOwner)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ViewName, null, Strings.PropertyDifference, "VIEW_TYPE_OWNER", this.ViewTypeOwner, target.ViewTypeOwner
                    ));
            }
            if (this.ViewType != target.ViewType)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ViewName, null, Strings.PropertyDifference, "VIEW_TYPE", this.ViewType, target.ViewType
                    ));
            }
            if (this.SuperviewName != target.SuperviewName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ViewName, null, Strings.PropertyDifference, "SUPERVIEW_NAME", this.SuperviewName, target.SuperviewName
                    ));
            }
            if (this.ReadOnly != target.ReadOnly)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ViewName, null, Strings.PropertyDifference, "READ_ONLY", this.ReadOnly, target.ReadOnly
                    ));
            }
            if (this.Bequeath != target.Bequeath)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ViewName, null, Strings.PropertyDifference, "BEQUEATH", this.Bequeath, target.Bequeath
                    ));
            }
            if (this.DefaultCollation != target.DefaultCollation)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.ViewName, null, Strings.PropertyDifference, "DEFAULT_COLLATION", this.DefaultCollation, target.DefaultCollation
                    ));
            }
        }

    }
}
