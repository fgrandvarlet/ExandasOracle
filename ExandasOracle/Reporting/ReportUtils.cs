using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using OfficeOpenXml;
using OfficeOpenXml.Table;

using ExandasOracle.Core;
using ExandasOracle.Dao;
using ExandasOracle.Domain;

namespace ExandasOracle.Reporting
{
    /// <summary>
    /// 
    /// </summary>
    public static class ReportUtils
    {
        /// <summary>
        /// 
        /// </summary>
        public static void ExportToExcel(ComparisonSet comparisonSet)
        {
            //System.Windows.Forms.MessageBox.Show(comparisonSet.ToFileName);

            var connectionString = DaoFactory.Instance.LocalConnectionString;
            try
            {
                using (var conn = new FbConnection(connectionString))
                {
                    conn.Open();
                    const string sql = "SELECT * FROM delta_report WHERE comparison_set_uid = @comparison_set_uid ORDER BY id";
                    var cmd = new FbCommand(sql, conn);
                    cmd.Parameters.AddWithValue("comparison_set_uid", comparisonSet.Uid);

                    using (var dr = cmd.ExecuteReader())
                    {
                        using (var package = new ExcelPackage())
                        {
                            var sheet = package.Workbook.Worksheets.Add("TestSheet");
                            
                            // The second argument specifies if we should print headers on the first row or not
                            sheet.Cells["A1"].LoadFromDataReader(dr, true, "matable", TableStyles.Medium2);

                            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

                            //var fileName = Path.Combine(Defs.REPORTS_DIRECTORY, comparisonSet.ToFileName + "_" + DateTime.Now.ToString("yyyy_MM_ddThhmmss") + ".xlsx");
                            var fileName = Path.Combine(Defs.REPORTS_DIRECTORY, comparisonSet.ToFileName + ".xlsx");
                            package.SaveAs(new FileInfo(fileName));

                            // TODO finaliser quid type exception levée ?
                            // https://github.com/dotnet/runtime/issues/28005
                            try
                            {
                                var startInfo = new ProcessStartInfo();
                                startInfo.FileName = fileName;
                                startInfo.UseShellExecute = true;   // indispensable pour que cela fonctionne
                                Process.Start(startInfo);
                            }
                            catch (Exception)
                            {
                                throw;
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
