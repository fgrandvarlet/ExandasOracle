using System;
using System.Collections.Generic;

using ExandasOracle.Dao;

namespace ExandasOracle.Domain
{
    public class Synonym
    {
        const string ENTITY = "SYNONYM";
        public string Owner { get; set; }
        public string SynonymName { get; set; }
        public string TableOwner { get; set; }
        public string TableName { get; set; }
        public string DbLink { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="comparisonSet"></param>
        /// <param name="list"></param>
        public void Compare(Synonym target, ComparisonSet comparisonSet, List<DeltaReport> list)
        {
            var parentObject = string.Format("owner={0}", this.Owner);
            
            if (this.TableOwner != target.TableOwner && comparisonSet.Schema1 == comparisonSet.Schema2)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.SynonymName, parentObject, LabelId.PropertyDifference, "TABLE_OWNER", this.TableOwner, target.TableOwner
                    ));
            }
            if (this.TableName != target.TableName)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.SynonymName, parentObject, LabelId.PropertyDifference, "TABLE_NAME", this.TableName, target.TableName
                    ));
            }
            if (this.DbLink != target.DbLink)
            {
                list.Add(new DeltaReport(
                    comparisonSet.Uid, ENTITY, this.SynonymName, parentObject, LabelId.PropertyDifference, "DB_LINK", this.DbLink, target.DbLink
                    ));
            }
        }

    }
}