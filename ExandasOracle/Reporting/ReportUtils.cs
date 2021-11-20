using System;
using System.Diagnostics;
using System.IO;
using FirebirdSql.Data.FirebirdClient;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Windows.Forms;

using ExandasOracle.Core;
using ExandasOracle.Dao;
using ExandasOracle.Domain;
using ExandasOracle.Properties;

namespace ExandasOracle.Reporting
{
    public static class ReportUtils
    {
        public static void ExportToExcel(ComparisonSet comparisonSet)
        {
            var connectionString = DaoFactory.Instance.LocalConnectionString;
            try
            {
                using (var conn = new FbConnection(connectionString))
                {
                    conn.Open();
                    const string sql = "SELECT id, entity, object, parent_object, label, property, source, target" +
                        " FROM delta_report WHERE comparison_set_uid = @comparison_set_uid ORDER BY id";
                    var cmd = new FbCommand(sql, conn);
                    cmd.Parameters.AddWithValue("comparison_set_uid", comparisonSet.Uid);

                    using (var dr = cmd.ExecuteReader())
                    {
                        using (var package = new ExcelPackage())
                        {
                            string sheetName = comparisonSet.Name;
                            if (comparisonSet.Name.Length > 30)
                            {
                                sheetName = comparisonSet.Name.Substring(0, 30);
                            }
                            var sheet = package.Workbook.Worksheets.Add(sheetName);
                            
                            // The second argument specifies if we should print headers on the first row or not
                            sheet.Cells["A1"].LoadFromDataReader(dr, true, "DeltaReport", TableStyles.Medium2);

                            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

                            var fileName = Path.Combine(Defs.REPORTS_DIRECTORY, comparisonSet.ToFileName + ".xlsx");
                            package.SaveAs(new FileInfo(fileName));

                            var startInfo = new ProcessStartInfo();
                            startInfo.FileName = fileName;

                            // indispensable pour que cela fonctionne
                            // cf. https://github.com/dotnet/runtime/issues/28005
                            startInfo.UseShellExecute = true;   

                            Process.Start(startInfo);
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                var message = ex.Message + Environment.NewLine + Environment.NewLine + Strings.PleaseCheckExcel;
                MessageBox.Show(message, Strings.ExandasOracleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Strings.ExandasOracleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
