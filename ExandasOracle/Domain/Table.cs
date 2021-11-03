using System;
using System.Collections.Generic;

using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class Table
    {
        const string ENTITY = "TABLE";
        public string TableName { get; set; }
        public string TablespaceName { get; set; }
        
        // TODO COMPLETER

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        public void Compare(Table target, Guid comparisonSetUid, List<DeltaReport> list)
        {
            if (this.TablespaceName != target.TablespaceName)
            {
                list.Add(new DeltaReport(
                    comparisonSetUid, ENTITY, this.TableName, null, Strings.PropertyDifference, "TABLESPACE_NAME", this.TablespaceName, target.TablespaceName
                    ));
            }
        }
    }
}
