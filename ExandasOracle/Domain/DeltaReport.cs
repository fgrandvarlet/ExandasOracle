using System;
using System.Collections.Generic;
using System.Text;

namespace ExandasOracle.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class DeltaReport
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparisonSetUid"></param>
        /// <param name="entity"></param>
        /// <param name="objectValue"></param>
        /// <param name="parentObject"></param>
        /// <param name="label"></param>
        /// <param name="property"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public DeltaReport(
            Guid comparisonSetUid,
            string entity,
            string objectValue,
            string parentObject,
            string label,
            string property,
            string source,
            string target
            )
        {
            ComparisonSetUid = comparisonSetUid;
            Entity = entity;
            ObjectValue = objectValue;
            ParentObject = parentObject;
            Label = label;
            Property = property;
            Source = source;
            Target = target;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparisonSetUid"></param>
        /// <param name="entity"></param>
        /// <param name="objectValue"></param>
        /// <param name="parentObject"></param>
        /// <param name="label"></param>
        public DeltaReport(
            Guid comparisonSetUid,
            string entity,
            string objectValue,
            string parentObject,
            string label
            )
        {
            ComparisonSetUid = comparisonSetUid;
            Entity = entity;
            ObjectValue = objectValue;
            ParentObject = parentObject;
            Label = label;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparisonSetUid"></param>
        /// <param name="entity"></param>
        /// <param name="objectValue"></param>
        /// <param name="label"></param>
        public DeltaReport(
            Guid comparisonSetUid,
            string entity,
            string objectValue,
            string label
            )
        {
            ComparisonSetUid = comparisonSetUid;
            Entity = entity;
            ObjectValue = objectValue;
            Label = label;
        }

        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid ComparisonSetUid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Entity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ObjectValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParentObject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Target { get; set; }

    }
}
