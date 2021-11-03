using System;
using System.Collections.Generic;
using System.Text;

namespace ExandasOracle.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class ComparisonSet
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Uid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid Connection1Uid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ConnectionParams Connection1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid Connection2Uid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ConnectionParams Connection2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Schema1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Schema2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastReportTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ToFileName
        {
            get
            {
                return Name.Replace(" ", "_");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format(
                "Name = {0} - Schema1 = {1} - Schema2 = {2}",
                Name,
                Schema1,
                Schema2
                );
        }
        
    }
}
