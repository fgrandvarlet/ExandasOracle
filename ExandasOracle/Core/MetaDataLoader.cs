using System;
using System.Collections.Generic;
using System.ComponentModel;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Dao;
using ExandasOracle.Domain;
using ExandasOracle.Properties;

namespace ExandasOracle.Core
{
    public partial class MetaDataLoader
    {
        private readonly ComparisonSet _comparisonSet;
        private readonly ILocalDao _localDao;
        private readonly Dictionary<string, LoaderDelegate> _loaderDictionary;
        private readonly int _totalOperationCount;
        private int _operationCounter;

        public MetaDataLoader(ComparisonSet comparisonSet)
        {
            this._comparisonSet = comparisonSet ?? throw new ArgumentNullException(nameof(comparisonSet));
            this._localDao = DaoFactory.Instance.GetLocalDao();
            this._loaderDictionary = BuildLoaderDictionary();
            this._totalOperationCount = this._loaderDictionary.Count * 2;
        }

        private Dictionary<string, LoaderDelegate> BuildLoaderDictionary()
        {
            var dict = new Dictionary<string, LoaderDelegate>
            {
                { Strings.Tables, LoadTables },
                { Strings.TableComments, LoadTableComments },
                { Strings.TableColumns, LoadTableColumns },
                { Strings.IdentityColumns, LoadIdentityColumns },
                { Strings.ColumnComments, LoadColumnComments },
                { Strings.PrimaryKeys, LoadPrimaryKeys },
                { Strings.Uniques, LoadUniques },
                { Strings.ForeignKeys, LoadForeignKeys },
                { Strings.Checks, LoadChecks },
                { Strings.Constraints, LoadConstraints },
                { Strings.ConstraintColumns, LoadConstraintColumns },
                { Strings.PartitionedTables, LoadPartitionedTables },
                { Strings.TablePartitions, LoadTablePartitions },
                { Strings.TableSubpartitions, LoadTableSubpartitions },
                { Strings.Views, LoadViews },
                { Strings.ViewComments, LoadViewComments },
                { Strings.ViewColumns, LoadViewColumns },
                { Strings.MaterializedViews, LoadMaterializedViews },
                { Strings.MaterializedViewComments, LoadMaterializedViewComments },
                { Strings.Sequences, LoadSequences },
                { Strings.TableIndexes, LoadTableIndexes },
                { Strings.IndexColumns, LoadIndexColumns },
                { Strings.IndexExpressions, LoadIndexExpressions },
                { Strings.PartitionedIndexes, LoadPartitionedIndexes },
                { Strings.IndexPartitions, LoadIndexPartitions },
                { Strings.IndexSubpartitions, LoadIndexSubpartitions },
                { Strings.Sources, LoadSources },
                { Strings.Triggers, LoadTriggers },
                { Strings.Clusters, LoadClusters },
                { Strings.ClusterColumns, LoadClusterColumns },
                { Strings.ClusterColumnMappings, LoadClusterColumnMappings },
                { Strings.ClusterIndexes, LoadClusterIndexes },
                { Strings.Types, LoadOracleTypes },
                { Strings.DatabaseLinks, LoadDatabaseLinks },
                { Strings.ObjectPrivileges, LoadObjectPrivileges },
                { Strings.Synonyms, LoadSynonyms },
            };

            return dict;
        }

        public void Execute(BackgroundWorker worker, DoWorkEventArgs e)
        {
            this._operationCounter = 0;

            // enrichment of the ComparisonSet object with instances of ConnectionParams
            this._comparisonSet.Connection1 = DaoFactory.Instance.GetConnectionParamsDao().Get(this._comparisonSet.Connection1Uid);
            this._comparisonSet.Connection2 = DaoFactory.Instance.GetConnectionParamsDao().Get(this._comparisonSet.Connection2Uid);
            
            using (FbConnection conn = this._localDao.GetFirebirdConnection())
            {
                conn.Open();
                FbTransaction tran = conn.BeginTransaction();
                try
                {
                    LoadMetaData(tran, SchemaType.Source, worker, e);
                    LoadMetaData(tran, SchemaType.Target, worker, e);

                    // local transaction validation
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        private void LoadMetaData(FbTransaction tran, SchemaType schemaType, BackgroundWorker worker, DoWorkEventArgs e)
        {
            IRemoteDao dao = null;
            string schema = null;
            bool DBAViews = false;
            string step = null;

            switch(schemaType)
            {
                case SchemaType.Source:
                    dao = DaoFactory.GetRemoteDao(this._comparisonSet.Connection1.ConnectionString);
                    schema = this._comparisonSet.Schema1;
                    DBAViews = this._comparisonSet.Connection1.DBAViews;
                    break;
                case SchemaType.Target:
                    dao = DaoFactory.GetRemoteDao(this._comparisonSet.Connection2.ConnectionString);
                    schema = this._comparisonSet.Schema2;
                    DBAViews = this._comparisonSet.Connection2.DBAViews;
                    break;
            }

            var conn = dao.GetOracleConnection();

            try
            {
                dao.CheckConnection(DBAViews);
                conn.Open();

                foreach (KeyValuePair<string, LoaderDelegate> item in this._loaderDictionary)
                {
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }

                    switch(schemaType)
                    {
                        case SchemaType.Source:
                            step = string.Format(Strings.LoadingObjects, item.Key, Strings.Source);
                            break;
                        case SchemaType.Target:
                            step = string.Format(Strings.LoadingObjects, item.Key, Strings.Target);
                            break;
                    }
                    IncrementStep(worker, step);
                    item.Value(tran, schemaType, dao, conn, schema, DBAViews);
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
            int percentage = (int)((double)this._operationCounter / this._totalOperationCount * 50);
            worker.ReportProgress(percentage, step);
        }

    }
}
