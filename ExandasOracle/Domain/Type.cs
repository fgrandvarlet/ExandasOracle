using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class Type
    {
        const string ENTITY = "TYPE";
        public string TypeName { get; set; }
        public string TypeCode { get; set; }
        public decimal? Attributes { get; set; }
        public decimal? Methods { get; set; }
        public string Predefined { get; set; }
        public string Incomplete { get; set; }
        public string Final { get; set; }
        public string Instantiable { get; set; }
        public string Persistable { get; set; }
        public string SupertypeOwner { get; set; }
        public string SupertypeName { get; set; }
        public decimal? LocalAttributes { get; set; }
        public decimal? LocalMethods { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSet"></param>
        /// <param name="list"></param>
        public void Compare(Type target, ComparisonSet comparisonSet, List<DeltaReport> list)
        {
            if (this.TypeCode != target.TypeCode)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, Strings.PropertyDifference, "TYPE_CODE", this.TypeCode, target.TypeCode
                    ));
            }
            if (this.Attributes != target.Attributes)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, Strings.PropertyDifference, "ATTRIBUTES", this.Attributes.ToString(), target.Attributes.ToString()
                    ));
            }
            if (this.Methods != target.Methods)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, Strings.PropertyDifference, "METHODS", this.Methods.ToString(), target.Methods.ToString()
                    ));
            }
            if (this.Predefined != target.Predefined)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, Strings.PropertyDifference, "PREDEFINED", this.Predefined, target.Predefined
                    ));
            }
            if (this.Incomplete != target.Incomplete)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, Strings.PropertyDifference, "INCOMPLETE", this.Incomplete, target.Incomplete
                    ));
            }
            if (this.Final != target.Final)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, Strings.PropertyDifference, "FINAL", this.Final, target.Final
                    ));
            }
            if (this.Instantiable != target.Instantiable)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, Strings.PropertyDifference, "INSTANTIABLE", this.Instantiable, target.Instantiable
                    ));
            }
            if (this.Persistable != target.Persistable)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, Strings.PropertyDifference, "PERSISTABLE", this.Persistable, target.Persistable
                    ));
            }
            if (this.SupertypeOwner != target.SupertypeOwner && comparisonSet.Schema1 = comparisonSet.Schema2)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, Strings.PropertyDifference, "SUPERTYPE_OWNER", this.SupertypeOwner, target.SupertypeOwner
                    ));
            }
            if (this.SupertypeName != target.SupertypeName)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, Strings.PropertyDifference, "SUPERTYPE_NAME", this.SupertypeName, target.SupertypeName
                    ));
            }
             if (this.LocalAttributes != target.LocalAttributes)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, Strings.PropertyDifference, "LOCAL_ATTRIBUTES", this.LocalAttributes.ToString(), target.LocalAttributes.ToString()
                    ));
            }
            if (this.LocalMethods != target.LocalMethods)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, Strings.PropertyDifference, "LOCAL_METHODS", this.LocalMethods.ToString(), target.LocalMethods.ToString()
                    ));
            }
        }

    }
}