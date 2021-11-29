using System;
using System.Text.Json.Serialization;

using ExandasOracle.Core;
using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public class FilterSetting
    {
        public FilterSetting(
            Guid comparisonSetUid,
            string entity,
            short? labelId,
            string property
            )
        {
            ComparisonSetUid = comparisonSetUid;
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            LabelId = labelId;
            if (labelId.HasValue)
            {
                Label = Defs.GetLabel((LabelId)labelId.Value);
            }
            Property = property;
        }

        public FilterSetting()
        {
        }

        public int Id { get; set; }
        public Guid ComparisonSetUid { get; set; }
        public string Entity { get; set; }
        public short? LabelId { get; set; }
        public string Label { get; set; }
        public string Property { get; set; }

        [JsonIgnore]
        public string Predicate
        {
            get
            {
                string result;

                if (this.LabelId == Defs.PROPERTY_DIFFERENCE)
                {
                    result = string.Format(
                        "NOT(entity = {0} AND label_id = {1} AND property = {2})",
                        Defs.QuoteValue(this.Entity),
                        this.LabelId,
                        Defs.QuoteValue(this.Property)
                        );
                }
                else if (this.LabelId == Defs.OBJECT_IN_SOURCE_NOT_IN_TARGET || this.LabelId == Defs.OBJECT_IN_TARGET_NOT_IN_SOURCE)
                {
                    result = string.Format(
                        "NOT(entity = {0} AND label_id = {1})",
                        Defs.QuoteValue(this.Entity),
                        this.LabelId
                        );
                }
                else if (this.LabelId == null)
                {
                    result = string.Format(
                        "NOT(entity = {0})",
                        Defs.QuoteValue(this.Entity)
                        );
                }
                else
                {
                    throw new InvalidOperationException();
                }
                return result;
            }
        }

        public override string ToString()
        {
            if (Entity != null && Label != null && Property != null)
            {
                return string.Format("{0} | {1} | {2}", Entity, Label, Property);
            }
            else if (Entity != null && Label != null)
            {
                return string.Format("{0} | {1}", Entity, Label);
            }
            else
            {
                return Entity;
            }
        }

    }
}
