using System;
using System.ComponentModel;
using System.Collections.Generic;

using ExandasOracle.Dao;
using ExandasOracle.Domain;
using ExandasOracle.Properties;

namespace ExandasOracle.Core
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Delta
    {
        // TODO EN COURS LE 15/10/2021
        // Delta.Table, Delta.Index, Delta.ForeignKey, etc.

        private readonly ComparisonSet _comparisonSet;
        private readonly Dictionary<string, DeltaDelegate> _deltaDictionary;
        private readonly int _totalOperationCount;
        private int _operationCounter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparisonSet"></param>
        public Delta(ComparisonSet comparisonSet)
        {
            this._comparisonSet = comparisonSet ?? throw new ArgumentNullException(nameof(comparisonSet));
            this._deltaDictionary = BuildDeltaDictionary();
            this._totalOperationCount = this._deltaDictionary.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, DeltaDelegate> BuildDeltaDictionary()
        {
            var dict = new Dictionary<string, DeltaDelegate>
            {
                { Strings.Tables, DeltaTable },
                { Strings.TableColumns, DeltaTableColumn },
                { Strings.PrimaryKeys, DeltaPrimaryKey },

                { Strings.Uniques, DeltaUnique },
                { Strings.ForeignKeys, DeltaForeignKey },
                { Strings.Checks, DeltaCheck },
                //{ Strings.Constraints, LoadConstraints },

                { Strings.Sequences, DeltaSequence },
                { Strings.TableIndexes, DeltaTableIndex },
                { Strings.IndexPartitions, DeltaIndexPartition },
                { Strings.Sources, DeltaSourceSynthesis },
            };

            return dict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="e"></param>
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

                    item.Value(conn, list);
                    IncrementStep(worker, string.Format("Delta des {0}", item.Key));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="step"></param>
        private void IncrementStep(BackgroundWorker worker, string step)
        {
            this._operationCounter++;
            int percentage = (int)((double)this._operationCounter / this._totalOperationCount * 50) + 50;
            worker.ReportProgress(percentage, step);
            // TODO TEMP
            System.Threading.Thread.Sleep(100);
        }

    }
}
