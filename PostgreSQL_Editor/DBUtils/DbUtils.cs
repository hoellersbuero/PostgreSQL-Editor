using Npgsql;
using PostgreSQL_Editor.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

public static class DbUtils
{
    // Liefert die Indices aller Spalten, deren Name "id" ist oder auf "_id" endet (case‑insensitive).
    public static List<int> GetIdColumnIndices(IDataRecord reader)
    {
        var indices = new List<int>();
        for (int i = 0; i < reader.FieldCount; i++)
        {
            var name = reader.GetName(i);
            if (string.Equals(name, "id", StringComparison.OrdinalIgnoreCase) ||
                name.EndsWith("_id", StringComparison.OrdinalIgnoreCase))
            {
                indices.Add(i);
            }
        }
        return indices;
    }

    public static DataTable GetResultFromTable(NpgsqlConnection npgsql, string sql)
    {
        try
        {
            string result = string.Empty;
            List<string> results = new List<string>();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.schema + "';" + sql, npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    return dataTable;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error executing SQL command: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return null;
    }

    public static void insertIntoMetadata()
    {
        string sql = "INSERT INTO entity_metadata ( uuid_value, entity_id, bool_value, json_value, created_ts, modified_ts, id, number_value, entity_type, metadata_key, string_value, created_by, modified_by) " +
            "VALUES('3f2504e0-4f89-11d3-9a0c-0305e82c3301', '6a0465e1-7329-4570-8e56-0027c7c36781', true, ''::jsonb, now(), now(), '6a0465e1-7329-4570-8e56-0027c7c36781', 42.5, 'my_entity', 'my_key', 'some text value', 'system', 'system');";
    }
}