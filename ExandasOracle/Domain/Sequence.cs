using System;
using System.Collections.Generic;

using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public class Sequence
    {
        const string ENTITY = "SEQUENCE";
        public string SequenceName { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public decimal IncrementBy { get; set; }
        public string CycleFlag { get; set; }
        public string OrderFlag { get; set; }
        public decimal CacheSize { get; set; }
        public string ScaleFlag { get; set; }
        public string ExtendFlag { get; set; }
        public string ShardedFlag { get; set; }
        public string SessionFlag { get; set; }
        public string KeepValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(Sequence target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            if (this.MinValue != target.MinValue)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "MIN_VALUE", this.MinValue.ToString(), target.MinValue.ToString()
                    ));
            }
            if (this.MaxValue != target.MaxValue)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "MAX_VALUE", this.MaxValue.ToString(), target.MaxValue.ToString()
                    ));
            }
            if (this.IncrementBy != target.IncrementBy)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "INCREMENT_BY", this.IncrementBy.ToString(), target.IncrementBy.ToString()
                    ));
            }
            if (this.CycleFlag != target.CycleFlag)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "CYCLE_FLAG", this.CycleFlag, target.CycleFlag
                    ));
            }
            if (this.CacheSize != target.CacheSize)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "CACHE_SIZE", this.CacheSize.ToString(), target.CacheSize.ToString()
                    ));
            }
            if (this.ScaleFlag != target.ScaleFlag)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "SCALE_FLAG", this.ScaleFlag, target.ScaleFlag
                    ));
            }
            if (this.ExtendFlag != target.ExtendFlag)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "EXTEND_FLAG", this.ExtendFlag, target.ExtendFlag
                    ));
            }
            if (this.ShardedFlag != target.ShardedFlag)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "SHARDED_FLAG", this.ShardedFlag, target.ShardedFlag
                    ));
            }
            if (this.SessionFlag != target.SessionFlag)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "SESSION_FLAG", this.SessionFlag, target.SessionFlag
                    ));
            }
            if (this.KeepValue != target.KeepValue)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.SequenceName, null, LabelId.PropertyDifference, "KEEP_VALUE", this.KeepValue, target.KeepValue
                    ));
            }
        }

    }
}
