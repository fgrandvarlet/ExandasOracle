using System.Collections.Generic;

using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public class OracleType
    {
        const string ENTITY = "TYPE";
        public string TypeName { get; set; }
        public string Typecode { get; set; }
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
        public void Compare(OracleType target, ComparisonSet comparisonSet, List<DeltaReport> list)
        {
            if (this.Typecode != target.Typecode)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "TYPECODE", this.Typecode, target.Typecode
                    ));
            }
            if (this.Attributes != target.Attributes)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "ATTRIBUTES", this.Attributes.ToString(), target.Attributes.ToString()
                    ));
            }
            if (this.Methods != target.Methods)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "METHODS", this.Methods.ToString(), target.Methods.ToString()
                    ));
            }
            if (this.Predefined != target.Predefined)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "PREDEFINED", this.Predefined, target.Predefined
                    ));
            }
            if (this.Incomplete != target.Incomplete)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "INCOMPLETE", this.Incomplete, target.Incomplete
                    ));
            }
            if (this.Final != target.Final)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "FINAL", this.Final, target.Final
                    ));
            }
            if (this.Instantiable != target.Instantiable)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "INSTANTIABLE", this.Instantiable, target.Instantiable
                    ));
            }
            if (this.Persistable != target.Persistable)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "PERSISTABLE", this.Persistable, target.Persistable
                    ));
            }
            if (this.SupertypeOwner != target.SupertypeOwner && comparisonSet.Schema1 == comparisonSet.Schema2)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "SUPERTYPE_OWNER", this.SupertypeOwner, target.SupertypeOwner
                    ));
            }
            if (this.SupertypeName != target.SupertypeName)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "SUPERTYPE_NAME", this.SupertypeName, target.SupertypeName
                    ));
            }
             if (this.LocalAttributes != target.LocalAttributes)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "LOCAL_ATTRIBUTES", this.LocalAttributes.ToString(), target.LocalAttributes.ToString()
                    ));
            }
            if (this.LocalMethods != target.LocalMethods)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.TypeName, null, LabelId.PropertyDifference, "LOCAL_METHODS", this.LocalMethods.ToString(), target.LocalMethods.ToString()
                    ));
            }
        }

    }
}