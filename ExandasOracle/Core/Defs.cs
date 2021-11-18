using System;
using System.Collections.Generic;
using System.Windows.Forms;

using ExandasOracle.Dao;
using ExandasOracle.Properties;

namespace ExandasOracle.Core
{
    public static class Defs
    {
		internal const string APPLICATION_TITLE = "Exandas - Oracle";

		internal static readonly Guid EMPTY_ITEM_GUID = Guid.Empty;
		internal static readonly string EMPTY_ITEM_LABEL = Strings.NotSpecified;

		internal const string REPORTS_DIRECTORY = "reports";

		internal static readonly int MAX_PROPERTY_SIZE = (int)Math.Pow(2, 15) - 20;	// 32768 - 20 = 32748

		static Defs()
		{
		}

		public static string TruncateTooLong(string str)
		{
			if (str != null)
			{
				if (str.Length > MAX_PROPERTY_SIZE)
				{
					return string.Format("[{0}] {1}", Strings.Truncated, str.Substring(0, MAX_PROPERTY_SIZE));
				}
				else
				{
					return str;
				}
			}
			else
			{
				return str;
			}
		}

		public static void ErrorDialog(string message)
		{
			MessageBox.Show(
                message,
				Strings.ExandasOracleError,
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
			);
		}

		public static void ValidatingErrorDialog(string message)
		{
			MessageBox.Show(
				string.Format(
					"{0}" + System.Environment.NewLine + System.Environment.NewLine + "{1}",
					Strings.FormError,
					message
				),
				Strings.Warning,
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
			);
		}

		public static bool ConfirmDeleteDialog(string record)
		{
			DialogResult dr = MessageBox.Show(
				string.Format(
					Strings.AreYouSureDelete +
					Environment.NewLine +
					Environment.NewLine +
					"{0} ?",
					record
				),
				Strings.DeleteRecord,
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2
			);
			return (dr == DialogResult.Yes);
		}

		#region dropdown lists sources

		public static List<KeyValuePair<Guid, string>> GetConnectionReferenceList()
		{
			var list = new List<KeyValuePair<Guid, string>>();
			list.Add(new KeyValuePair<Guid, string>(EMPTY_ITEM_GUID, EMPTY_ITEM_LABEL));

			foreach (var cp in DaoFactory.Instance.GetConnectionParamsDao().GetList())
			{
				list.Add(new KeyValuePair<Guid, string>(cp.Uid, cp.FormattedString));
			}
			return list;
		}

		#endregion

	}
}
