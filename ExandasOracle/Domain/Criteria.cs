using System;
using System.Collections.Generic;
using System.Text;

namespace ExandasOracle.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class Criteria
    {
        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object Entity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasText
        {
            get
            {
                return Text != null && !Text.Equals(string.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Pattern
        {
            get
            {
                return string.Format("%{0}%", Text);
            }
        }

    }
}
