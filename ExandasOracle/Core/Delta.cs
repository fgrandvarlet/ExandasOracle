using System;
using System.ComponentModel;
using System.Collections.Generic;

using ExandasOracle.Dao;
using ExandasOracle.Domain;
using ExandasOracle.Properties;

namespace ExandasOracle.Core
{
    public partial class Delta
    {
        private readonly ComparisonSet _comparisonSet;
        private readonly Dictionary<string, DeltaDelegate> _deltaDictionary;
        private readonly int _totalOperationCount;
        private int _operationCounter;

        public Delta(ComparisonSet comparisonSet)
        {
            this._comparisonSet = comparisonSet ?? throw new ArgumentNullException(nameof(comparisonSet));
            this._deltaDictionary = BuildDeltaDictionary();
            this._totalOperationCount = this._deltaDictionary.Count;
        }

        private Dictionary<string, DeltaDelegate> BuildDeltaDictionary()
        {
            var dict = new Dictionary<string, DeltaDelegate>
            {
                { Strings.Tables, DeltaTable },
                { Strings.TableComments, DeltaTableComment },
                { Strings.TableColumns, DeltaTableColumn },
                { Strings.IdentityColumns, DeltaIdentityColumn },

                { Strings.ColumnComments, DeltaColumnComment },

                { Strings.PrimaryKeys, DeltaPrimaryKey },
                { Strings.Uniques, DeltaUnique },
                { Strings.ForeignKeys, DeltaForeignKey },
                { Strings.Checks, DeltaCheck },
                { Strings.Constraints, DeltaConstraint },

                { Strings.ConstraintColumns, DeltaConstraintColumn },

                { Strings.PartitionedTables, DeltaPartitionedTable },
                { Strings.TablePartitions, DeltaTablePartition },
                { Strings.TableSubpartitions, DeltaTableSubpartition },

                { Strings.Views, DeltaView },
                { Strings.ViewComments, DeltaViewComment },
                { Strings.ViewColumns, DeltaViewColumn },
                { Strings.MaterializedViews, DeltaMaterializedView },
                { Strings.MaterializedViewComments, DeltaMaterializedViewComment },

                { Strings.Sequences, DeltaSequence },
                { Strings.TableIndexes, DeltaTableIndex },
                { Strings.IndexColumns, DeltaIndexColumn },
                { Strings.IndexExpressions, DeltaIndexExpression },

                { Strings.PartitionedIndexes, DeltaPartitionedIndex },
                { Strings.IndexPartitions, DeltaIndexPartition },
                { Strings.IndexSubpartitions, DeltaIndexSubpartition },

                { Strings.Sources, DeltaSourceSynthesis },
                { Strings.Triggers, DeltaTrigger },

                { Strings.Clusters, DeltaCluster },
                { Strings.ClusterColumns, DeltaClusterColumn },
                { Strings.ClusterColumnMappings, DeltaClusterColumnMapping },
                { Strings.ClusterIndexes, DeltaClusterIndex },

                { Strings.Types, DeltaOracleType },
                { Strings.DatabaseLinks, DeltaDatabaseLink },

                { Strings.ObjectPrivileges, DeltaObjectPrivilege },
                { Strings.Synonyms, DeltaSynonym },
            };

            return dict;
        }

        public void Execute(BackgroundWorker worker, DoWorkEventArgs e)
        {
            var list = new List<DeltaReport>();
            var dao = DaoFactory.Instance.GetDeltaReportDao();
            var conn = dao.GetFirebirdConnection();
            try
            {
                conn.Open();

                foreach (KeyValuePair<string, DeltaDelegate> item in this._deltaDictionary)
                {
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }

                    IncrementStep(worker, string.Format(Strings.DeltaOf, item.Key));
                    item.Value(conn, list);
                }

                if (worker.CancellationPending == false)
                {
                    dao.LoadDeltaReportList(conn, this._comparisonSet.Uid, list);
                }
            }
            finally
            {
                conn.Close();
            }
        }

        private void IncrementStep(BackgroundWorker worker, string step)
        {
            this._operationCounter++;
            int percentage = (int)((double)this._operationCounter / this._totalOperationCount * 50) + 50;
            worker.ReportProgress(percentage, step);
        }

    }
}
