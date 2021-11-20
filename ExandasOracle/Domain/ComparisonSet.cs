using System;

namespace ExandasOracle.Domain
{
    public class ComparisonSet
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public Guid Connection1Uid { get; set; }
        public ConnectionParams Connection1 { get; set; }
        public Guid Connection2Uid { get; set; }
        public ConnectionParams Connection2 { get; set; }
        public string Schema1 { get; set; }
        public string Schema2 { get; set; }
        public DateTime? LastReportTime { get; set; }

        public string ToFileName
        {
            get
            {
                if (LastReportTime.HasValue)
                {
                    return Name.Replace(" ", "_") + "_" + LastReportTime.Value.ToString(@"yyyyMMdd-HH\hmm");
                }
                else
                {
                    return Name.Replace(" ", "_");
                }
            }
        }
        
    }
}
