using System;

using ExandasOracle.Core;
using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public class DeltaReport
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparisonSetUid"></param>
        /// <param name="entity"></param>
        /// <param name="objectValue"></param>
        /// <param name="parentObject"></param>
        /// <param name="labelId"></param>
        /// <param name="property"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public DeltaReport(
            Guid comparisonSetUid,
            string entity,
            string objectValue,
            string parentObject,
            LabelId labelId,
            string property,
            string source,
            string target
            )
        {
            ComparisonSetUid = comparisonSetUid;
            Entity = entity;
            ObjectValue = objectValue;
            ParentObject = parentObject;
            LabelId = (short)labelId;
            Label = Defs.GetLabel(labelId);
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
        /// <param name="labelId"></param>
        public DeltaReport(
            Guid comparisonSetUid,
            string entity,
            string objectValue,
            string parentObject,
            LabelId labelId
            )
        {
            ComparisonSetUid = comparisonSetUid;
            Entity = entity;
            ObjectValue = objectValue;
            ParentObject = parentObject;
            LabelId = (short)labelId;
            Label = Defs.GetLabel(labelId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparisonSetUid"></param>
        /// <param name="entity"></param>
        /// <param name="objectValue"></param>
        /// <param name="labelId"></param>
        public DeltaReport(
            Guid comparisonSetUid,
            string entity,
            string objectValue,
            LabelId labelId
            )
        {
            ComparisonSetUid = comparisonSetUid;
            Entity = entity;
            ObjectValue = objectValue;
            LabelId = (short)labelId;
            Label = Defs.GetLabel(labelId);
        }

        public long Id { get; set; }
        public Guid ComparisonSetUid { get; set; }
        public string Entity { get; set; }
        public string ObjectValue { get; set; }
        public string ParentObject { get; set; }
        public short LabelId { get; set; }
        public string Label { get; set; }
        public string Property { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }

    }
}
