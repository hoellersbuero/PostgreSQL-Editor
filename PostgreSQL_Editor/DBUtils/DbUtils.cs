using System;
using System.Collections.Generic;
using System.Data;

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
}