using System;
using System.Collections.Generic;
using System.Windows.Forms;

using ExandasOracle.Dao;
using ExandasOracle.Properties;

namespace ExandasOracle.Core
{
	/// <summary>
	/// 
	/// </summary>
    public static class Defs
    {
		internal const string APPLICATION_TITLE = "Exandas - Oracle";
		internal const string TEXT_VALIDATING_ERROR = "Erreur de validation des données du formulaire.";
		internal const string CAPTION_ATTENTION = "Attention";
		internal const string CAPTION_ERROR = "Erreur Exandas.Oracle";


		internal const string TITLE_FORM_CONFIGURATION = "Configuration des informations de connexion";
		internal const string TITLE_FORM_CONNECTION_PARAMS = "Détail connexion serveur";
		internal const string TITLE_FORM_COMPARISON_SET = "Détail jeu de comparaison";
		internal const string TITLE_FORM_COMPARISON_RESULT = "Rapport de comparaison";

		// TODO ? internal const string TITLE_FORM_APROPOS = "A propos d'Exandas.Oracle";

		//internal const string TITLE_LIST_CONNECTION_PARAMS = "Liste des connexions serveur";
		//internal const string TITLE_LIST_COMPARISON_SET = "Liste des jeux de comparaison";

		internal const string TITLE_FORM_PROGRESS = "Lancement du rapport de comparaison ?";
		internal const string MESSAGE_REPORT_GENERATOR = "Confirmez-vous la génération du rapport de comparaison ?";
		internal const string MESSAGE_REPORT_GENERATOR_RUNNING = "Génération du rapport de comparaison en cours...";

		internal static Guid EMPTY_ITEM_GUID = Guid.Empty;
		internal static string EMPTY_ITEM_LABEL = Strings.NotSpecified;

		internal const string REPORTS_DIRECTORY = "reports";

		static Defs()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public static void ErrorDialog(string message)
		{
			MessageBox.Show(
				message,
				CAPTION_ERROR,
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
			);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public static void ValidatingErrorDialog(string message)
		{
			MessageBox.Show(
				string.Format(
					"{0}" + System.Environment.NewLine + System.Environment.NewLine + "{1}",
					TEXT_VALIDATING_ERROR,
					message
				),
				CAPTION_ATTENTION,
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
