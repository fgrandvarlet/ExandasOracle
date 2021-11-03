using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;
using ExandasOracle.Properties;

namespace ExandasOracle.Core
{
    public partial class Delta
    {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="list"></param>
		private void DeltaTableColumn(FbConnection conn, List<DeltaReport> list)
		{
			const string ENTITY = "TABLE COLUMN";
			string sql;
			FbCommand cmd;

			// phase 1 : source minus target
			sql = "SELECT s.table_name, s.column_name FROM src_tab_cols s" +
				" LEFT JOIN tgt_tab_cols t USING (table_name, column_name)" +
				" JOIN common_tables USING(table_name)" +
				" WHERE t.column_name IS NULL" +
				" ORDER BY table_name, column_name";
			cmd = new FbCommand(sql, conn);

			using (FbDataReader dr = cmd.ExecuteReader())
			{
				while (dr.Read())
				{
					var report = new DeltaReport(this._comparisonSet.Uid, ENTITY, (string)dr["column_name"], (string)dr["table_name"], Strings.ObjectInSource);
					list.Add(report);
				}
			}

			// phase 2 : target minus source
			sql = "SELECT t.table_name, t.column_name FROM tgt_tab_cols t" +
				" LEFT JOIN src_tab_cols s USING (table_name, column_name)" +
				" JOIN common_tables USING(table_name)" +
				" WHERE s.column_name IS NULL" +
				" ORDER BY table_name, column_name";
			cmd = new FbCommand(sql, conn);

			using (FbDataReader dr = cmd.ExecuteReader())
			{
				while (dr.Read())
				{
					var report = new DeltaReport(this._comparisonSet.Uid, ENTITY, (string)dr["column_name"], (string)dr["table_name"], Strings.ObjectInTarget);
					list.Add(report);
				}
			}

			// phase 3 : property differences between source and target
			sql = "SELECT * FROM comp_tab_cols";
			cmd = new FbCommand(sql, conn);

			using (FbDataReader dr = cmd.ExecuteReader())
			{
				while (dr.Read())
				{
					var sourceTableColumn = new TableColumn
					{
						TableName = (string)dr["table_name"],
						ColumnName = (string)dr["column_name"],
						DataType = dr["src_data_type"] is DBNull ? null : (string)dr["src_data_type"],
						DataTypeMod = dr["src_data_type_mod"] is DBNull ? null : (string)dr["src_data_type_mod"],
						DataTypeOwner = dr["src_data_type_owner"] is DBNull ? null : (string)dr["src_data_type_owner"],
						DataLength = (int)dr["src_data_length"],
						DataPrecision = dr["src_data_precision"] is DBNull ? null : (int?)dr["src_data_precision"],
						DataScale = dr["src_data_scale"] is DBNull ? null : (int?)dr["src_data_scale"],
						Nullable = dr["src_nullable"] is DBNull ? null : (string)dr["src_nullable"],
						ColumnId = dr["src_column_id"] is DBNull ? null : (int?)dr["src_column_id"],
						DefaultLength = dr["src_default_length"] is DBNull ? null : (int?)dr["src_default_length"],
						DataDefault = dr["src_data_default"] is DBNull ? null : (string)dr["src_data_default"],
						CharLength = dr["src_col_char_length"] is DBNull ? null : (int?)dr["src_col_char_length"],
						CharUsed = dr["src_char_used"] is DBNull ? null : (string)dr["src_char_used"],
						HiddenColumn = dr["src_hidden_column"] is DBNull ? null : (string)dr["src_hidden_column"],
						VirtualColumn = dr["src_virtual_column"] is DBNull ? null : (string)dr["src_virtual_column"],
						DefaultOnNull = dr["src_default_on_null"] is DBNull ? null : (string)dr["src_default_on_null"],
						IdentityColumn = dr["src_identity_column"] is DBNull ? null : (string)dr["src_identity_column"],
						Collation = dr["src_collation"] is DBNull ? null : (string)dr["src_collation"]
					};
					var targetTableColumn = new TableColumn
					{
						TableName = (string)dr["table_name"],
						ColumnName = (string)dr["column_name"],
						DataType = dr["tgt_data_type"] is DBNull ? null : (string)dr["tgt_data_type"],
						DataTypeMod = dr["tgt_data_type_mod"] is DBNull ? null : (string)dr["tgt_data_type_mod"],
						DataTypeOwner = dr["tgt_data_type_owner"] is DBNull ? null : (string)dr["tgt_data_type_owner"],
						DataLength = (int)dr["tgt_data_length"],
						DataPrecision = dr["tgt_data_precision"] is DBNull ? null : (int?)dr["tgt_data_precision"],
						DataScale = dr["tgt_data_scale"] is DBNull ? null : (int?)dr["tgt_data_scale"],
						Nullable = dr["tgt_nullable"] is DBNull ? null : (string)dr["tgt_nullable"],
						ColumnId = dr["tgt_column_id"] is DBNull ? null : (int?)dr["tgt_column_id"],
						DefaultLength = dr["tgt_default_length"] is DBNull ? null : (int?)dr["tgt_default_length"],
						DataDefault = dr["tgt_data_default"] is DBNull ? null : (string)dr["tgt_data_default"],
						CharLength = dr["tgt_col_char_length"] is DBNull ? null : (int?)dr["tgt_col_char_length"],
						CharUsed = dr["tgt_char_used"] is DBNull ? null : (string)dr["tgt_char_used"],
						HiddenColumn = dr["tgt_hidden_column"] is DBNull ? null : (string)dr["tgt_hidden_column"],
						VirtualColumn = dr["tgt_virtual_column"] is DBNull ? null : (string)dr["tgt_virtual_column"],
						DefaultOnNull = dr["tgt_default_on_null"] is DBNull ? null : (string)dr["tgt_default_on_null"],
						IdentityColumn = dr["tgt_identity_column"] is DBNull ? null : (string)dr["tgt_identity_column"],
						Collation = dr["tgt_collation"] is DBNull ? null : (string)dr["tgt_collation"]
					};
					sourceTableColumn.Compare(targetTableColumn, this._comparisonSet.Uid, list);
				}
			}
		}

	}
}
