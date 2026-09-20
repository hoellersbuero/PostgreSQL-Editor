using Npgsql;
using PostgreSQL_Editor.FunctionViews;
using PostgreSQL_Editor.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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

    public static string getSpecialFrameJson(NpgsqlConnection npgsql, string basematerial_id, string systemtype_id)
    {
        try
        {
            string result = string.Empty;
            List<string> reslist = new List<string>();
            List<SpecialFrameRoot> reslistObjects = new List<SpecialFrameRoot>();
            string sql = "WITH available_variants AS (\n    SELECT\n        sfc.id AS configuration_id,\n        sfc.base_material_id,\n        sfc.system_type_id,\n        sfc.frame_type_id,\n        sfv.id AS variant_id,\n        sfv.frame_material_code,\n        sfv.flange_width_code,\n        sfv.special_coating_editable,\n        sfv.kit_single_selectable\n    FROM special_frame_configuration sfc\n    JOIN special_frame_variant sfv\n        ON sfv.configuration_id = sfc.id\n    WHERE sfc.base_material_id = '@BASEMATERIAL_ID'\n      AND sfc.system_type_id = '@SYSTEMTYPE_ID'\n      AND sfv.is_available = TRUE\n),\n\nflange_options AS (\n    SELECT\n        frame_material_code,\n\n        jsonb_agg(\n            DISTINCT flange_width_code\n            ORDER BY flange_width_code\n        ) FILTER (\n            WHERE flange_width_code IS NOT NULL\n        ) AS flange_options\n\n    FROM available_variants\n    GROUP BY frame_material_code\n),\n\nwedge_materials AS (\n    SELECT\n        av.frame_material_code,\n\n        ARRAY_AGG(\n            DISTINCT sfvwm.material_code\n            ORDER BY sfvwm.material_code\n        ) AS wedge_ap_materials\n\n    FROM available_variants av\n\n    JOIN special_frame_variant_wedge_material sfvwm\n        ON sfvwm.variant_id = av.variant_id\n\n    GROUP BY av.frame_material_code\n),\n\nfw_rules AS (\n    SELECT\n        av.frame_material_code,\n        sfvfs.fw_size_id,\n\n        ARRAY_AGG(\n            DISTINCT sfrr.rows\n            ORDER BY sfrr.rows\n        ) AS rows,\n\n        MAX(sfrr.max_columns) AS max_columns\n\n    FROM available_variants av\n\n    JOIN special_frame_variant_fw_size sfvfs\n        ON sfvfs.variant_id = av.variant_id\n\n    JOIN special_frame_row_rule sfrr\n        ON sfrr.variant_id = sfvfs.variant_id\n       AND sfrr.fw_size_id = sfvfs.fw_size_id\n\n    GROUP BY\n        av.frame_material_code,\n        sfvfs.fw_size_id\n),\n\nfw_options AS (\n    SELECT\n        frame_material_code,\n\n        ARRAY_AGG(\n            DISTINCT fw_size_id\n            ORDER BY fw_size_id\n        ) AS fw_sizes,\n\n        JSONB_AGG(\n            JSONB_BUILD_OBJECT(\n                'fw_size', fw_size_id,\n                'rows', rows,\n                'columns',\n                    (\n                        SELECT ARRAY_AGG(n ORDER BY n)\n                        FROM generate_series(1, max_columns) AS n\n                    )\n            )\n            ORDER BY fw_size_id\n        ) AS fw_size_rules\n\n    FROM fw_rules\n\n    GROUP BY\n        frame_material_code\n),\n\ncoating_options AS (\n    SELECT\n        frame_material_code,\n\n        BOOL_OR(special_coating_editable) AS special_coating\n\n    FROM available_variants\n    GROUP BY frame_material_code\n),\n\nkit_selectable_options AS (\n    SELECT\n        frame_material_code,\n\n        BOOL_OR(kit_single_selectable) AS kit_single_selectable\n\n    FROM available_variants\n    GROUP BY frame_material_code\n),\n\nframe_material_result AS (\n    SELECT\n        av.base_material_id,\n        bm.name_key AS base_material,\n\n        av.system_type_id,\n        st.name AS system_type,\n\n        av.frame_type_id,\n        ft.name AS frame_type,\n\n        av.frame_material_code AS frame_material,\n\n        fo.flange_options,\n\n        fw.fw_sizes,\n        fw.fw_size_rules,\n\n        wm.wedge_ap_materials,\n\n        co.special_coating,\n\n        kso.kit_single_selectable\n\n    FROM available_variants av\n\n    JOIN base_material bm\n        ON bm.id = av.base_material_id\n\n    JOIN system_type st\n        ON st.id = av.system_type_id\n\n    JOIN frame_type ft\n        ON ft.id = av.frame_type_id\n\n    JOIN flange_options fo\n        ON fo.frame_material_code = av.frame_material_code\n\n    JOIN fw_options fw\n        ON fw.frame_material_code = av.frame_material_code\n\n    LEFT JOIN wedge_materials wm\n        ON wm.frame_material_code = av.frame_material_code\n\n    JOIN coating_options co\n        ON co.frame_material_code = av.frame_material_code\n\n    JOIN kit_selectable_options kso\n        ON kso.frame_material_code = av.frame_material_code\n)\n\nSELECT\n    jsonb_build_object(\n\n        'base_material_id',\n        base_material_id,\n\n        'base_material',\n        base_material,\n\n        'system_type_id',\n        system_type_id,\n\n        'system_type',\n        system_type,\n\n        'frame_type_id',\n        frame_type_id,\n\n        'frame_type',\n        frame_type,\n\n        'frame_materials',\n        jsonb_agg(\n            jsonb_build_object(\n\n                'frame_material',\n                frame_material,\n\n                'flange_options',\n                flange_options,\n\n                'fw_sizes',\n                fw_sizes,\n\n                'fw_size_rules',\n                fw_size_rules,\n\n                'wedge_ap_materials',\n                wedge_ap_materials,\n\n                'special_coating',\n                special_coating,\n\n                'kit_single_selectable',\n                kit_single_selectable\n\n            )\n            ORDER BY frame_material\n        )\n    ) AS special_frame_configuration\n\nFROM frame_material_result\n\nGROUP BY\n    base_material_id,\n    base_material,\n    system_type_id,\n    system_type,\n    frame_type_id,\n    frame_type;";
            string sqlmod = sql.Replace("@BASEMATERIAL_ID", basematerial_id).Replace("@SYSTEMTYPE_ID", systemtype_id);
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.schema + "';" + sqlmod, npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string jsonResult = reader.GetString(0);
                        SpecialFrameRoot specialFrame = JsonSerializer.Deserialize<SpecialFrameRoot>(jsonResult);
                        reslistObjects.Add(specialFrame);
                        reslist.Add(jsonResult);
                    }
                }
            }
            return String.Join(Environment.NewLine, reslist);  
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static List<SpecialFrameConfigurationDto> getSpecialFrame(NpgsqlConnection npgsql, string basematerial, string systemtype)
    {
        try
        {
            string sql = "WITH available_variants AS (\n    SELECT\n        sfc.id AS configuration_id,\n        sfc.base_material_id,\n        sfc.system_type_id,\n        sfc.frame_type_id,\n        sfv.id AS variant_id,\n        sfv.frame_material_code,\n        sfv.flange_width_code,\n        sfv.special_coating_editable,\n        sfv.kit_single_selectable\n    FROM special_frame_configuration sfc\n    JOIN special_frame_variant sfv\n        ON sfv.configuration_id = sfc.id\n    WHERE sfc.base_material_id = '@BASEMATERIAL'\n      AND sfc.system_type_id   = '@SYSTEMTYPE'\n      AND sfv.is_available = TRUE\n),\nflange_options AS (\n    SELECT\n        frame_material_code,\n        jsonb_agg(\n            DISTINCT flange_width_code\n            ORDER BY flange_width_code\n        ) FILTER (\n            WHERE flange_width_code IS NOT NULL\n        ) AS flange_options\n    FROM available_variants\n    GROUP BY frame_material_code\n),\nwedge_materials AS (\n    SELECT\n        av.frame_material_code,\n        ARRAY_AGG(\n            DISTINCT sfvwm.material_code\n            ORDER BY sfvwm.material_code\n        ) AS wedge_ap_materials\n    FROM available_variants av\n    JOIN special_frame_variant_wedge_material sfvwm\n        ON sfvwm.variant_id = av.variant_id\n    GROUP BY av.frame_material_code\n),\nfw_rules AS (\n    SELECT\n        av.frame_material_code,\n        sfvfs.fw_size_id,\n\n        ARRAY_AGG(\n            DISTINCT sfrr.rows\n            ORDER BY sfrr.rows\n        ) AS rows,\n\n        MAX(sfrr.max_columns) AS max_columns\n\n    FROM available_variants av\n\n    JOIN special_frame_variant_fw_size sfvfs\n        ON sfvfs.variant_id = av.variant_id\n\n    JOIN special_frame_row_rule sfrr\n        ON sfrr.variant_id = sfvfs.variant_id\n       AND sfrr.fw_size_id = sfvfs.fw_size_id\n\n    GROUP BY\n        av.frame_material_code,\n        sfvfs.fw_size_id\n),\nfw_options AS (\n    SELECT\n        frame_material_code,\n\n        ARRAY_AGG(\n            DISTINCT fw_size_id\n            ORDER BY fw_size_id\n        ) AS fw_sizes,\n\n        JSONB_AGG(\n            JSONB_BUILD_OBJECT(\n                'fw_size', fw_size_id,\n                'rows', rows,\n                'columns', (\n                    SELECT ARRAY_AGG(n ORDER BY n)\n                    FROM generate_series(1, max_columns) AS n\n                )\n            )\n            ORDER BY fw_size_id\n        ) AS fw_size_rules\n\n    FROM fw_rules\n\n    GROUP BY\n        frame_material_code\n),\ncoating_options AS (\n    SELECT\n        frame_material_code,\n        BOOL_OR(special_coating_editable) AS special_coating\n    FROM available_variants\n    GROUP BY frame_material_code\n),\n\nkit_selectable_options AS (\n    SELECT\n        frame_material_code,\n        BOOL_OR(kit_single_selectable) AS kit_single_selectable\n    FROM available_variants\n    GROUP BY frame_material_code\n)\nSELECT\n    av.base_material_id,\n    bm.name_key AS base_material,\n\n    av.system_type_id,\n    st.name AS system_type,\n\n    av.frame_type_id,\n    ft.name AS frame_type,\n\n    av.frame_material_code AS frame_material,\n\n    -- ALL FLANGE OPTIONS IN ONE JSON ARRAY\n    fo.flange_options,\n\n    -- EXISTING FW INFORMATION\n    fw.fw_sizes,\n    fw.fw_size_rules,\n\n    -- EXISTING WEDGE / A.P. MATERIALS\n    wm.wedge_ap_materials,\n\n    -- EXISTING SPECIAL COATING\n    co.special_coating,\n\n    -- KIT / SINGLE SELECTABLE\n    kso.kit_single_selectable\n\nFROM available_variants av\n\nJOIN base_material bm\n    ON bm.id = av.base_material_id\n\nJOIN system_type st\n    ON st.id = av.system_type_id\n\nJOIN frame_type ft\n    ON ft.id = av.frame_type_id\n\nJOIN flange_options fo\n    ON fo.frame_material_code = av.frame_material_code\n\nJOIN fw_options fw\n    ON fw.frame_material_code = av.frame_material_code\n\nLEFT JOIN wedge_materials wm\n    ON wm.frame_material_code = av.frame_material_code\n\nJOIN coating_options co\n    ON co.frame_material_code = av.frame_material_code\n\nJOIN kit_selectable_options kso\n    ON kso.frame_material_code = av.frame_material_code\n\nGROUP BY\n    av.base_material_id,\n    bm.name_key,\n\n    av.system_type_id,\n    st.name,\n\n    av.frame_type_id,\n    ft.name,\n\n    av.frame_material_code,\n\n    fo.flange_options,\n\n    fw.fw_sizes,\n    fw.fw_size_rules,\n\n    wm.wedge_ap_materials,\n\n    co.special_coating,\n    kso.kit_single_selectable\n\nORDER BY\n    ft.name,\n    av.frame_material_code;";
            string sqlmod = sql.Replace("@BASEMATERIAL", basematerial).Replace("@SYSTEMTYPE", systemtype);
            return GetSpecialFrameConfigurations(sqlmod, npgsql);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static List<SpecialFrameConfigurationDto> GetSpecialFrameConfigurations(string sqlmod, NpgsqlConnection npgsql)
    {
        var result = new List<SpecialFrameConfigurationDto>();

        using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.schema + "';" + sqlmod, npgsql))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                int ordBaseMaterialId = reader.GetOrdinal("base_material_id");
                int ordBaseMaterial = reader.GetOrdinal("base_material");
                int ordSystemTypeId = reader.GetOrdinal("system_type_id");
                int ordSystemType = reader.GetOrdinal("system_type");
                int ordFrameTypeId = reader.GetOrdinal("frame_type_id");
                int ordFrameType = reader.GetOrdinal("frame_type");
                int ordFrameMaterial = reader.GetOrdinal("frame_material");
                int ordFlangeOptions = reader.GetOrdinal("flange_options");
                int ordFwSizes = reader.GetOrdinal("fw_sizes");
                int ordFwSizeRules = reader.GetOrdinal("fw_size_rules");
                int ordWedgeApMaterials = reader.GetOrdinal("wedge_ap_materials");
                int ordSpecialCoating = reader.GetOrdinal("special_coating");
                int ordKitSingle = reader.GetOrdinal("kit_single_selectable");

                while (reader.Read())
                {
                    SpecialFrameConfigurationDto item = new SpecialFrameConfigurationDto();
                    {
                        item.BaseMaterialId = reader.GetGuid(ordBaseMaterialId);
                        item.BaseMaterial = reader.GetString(ordBaseMaterial);
                        item.SystemTypeId = reader.GetGuid(ordSystemTypeId);
                        item.SystemType = reader.GetString(ordSystemType);
                        item.FrameTypeId = reader.GetGuid(ordFrameTypeId);
                        item.FrameType = reader.GetString(ordFrameType);
                        item.FrameMaterial = reader.GetString(ordFrameMaterial);
                        item.KitSingleSelectable = reader.GetBoolean(ordKitSingle);

                        // FlangeOptions: SQL liefert jsonb -> string, deshalb deserialisieren
                        item.FlangeOptions = reader.IsDBNull(ordFlangeOptions)
                            ? new List<string>()
                            : JsonSerializer.Deserialize<List<string>>(reader.GetFieldValue<string>(ordFlangeOptions)) ?? new List<string>();

                        // fw_sizes: falls PostgreSQL ARRAY<uuid>
                        item.FwSizes = reader.IsDBNull(ordFwSizes)
                            ? new List<int>()
                            : reader.GetFieldValue<int[]>(ordFwSizes).ToList();

                        string fwSizeRulesJson = reader.IsDBNull(ordFwSizeRules)
                            ? null
                            : reader.GetFieldValue<string>(ordFwSizeRules);
                        item.FwSizeRules = fwSizeRulesJson == null
                            ? new List<FwSizeRule>()
                            : JsonSerializer.Deserialize<List<FwSizeRule>>(fwSizeRulesJson) ?? new List<FwSizeRule>();

                        item.WedgeApMaterials = reader.IsDBNull(ordWedgeApMaterials)
                            ? new List<string>()
                            : reader.GetFieldValue<string[]>(ordWedgeApMaterials).ToList();

                        item.SpecialCoating = reader.GetBoolean(ordSpecialCoating);
                    }
                    result.Add(item);
                }

                return result;
            }
        }
    }

    public static DataTable getCompleteSpecialFrames(NpgsqlConnection npgsql)
    {
        string sql = "SELECT\n    sfc.id AS configuration_id,\n    sfv.id AS variant_id,\n\n    sfv.is_available,\n    sfv.kit_single_selectable,\n\n    bm.name_key AS \"Base Material\",\n    st.name AS \"System Type\",\n    ft.name AS \"Frametype\",\n\n    sfv.flange_width_code AS \"Flange Width\",\n    sfv.frame_material_code AS \"Frame Material\",\n\n    COALESCE(\n        (\n            SELECT string_agg(\n                w.material_code,\n                '/' ORDER BY w.material_code\n            )\n            FROM special_frame_variant_wedge_material w\n            WHERE w.variant_id = sfv.id\n        ),\n        ''\n    ) AS \"Wedge/A.P. Material\",\n\n    (\n        SELECT string_agg(\n            x.fw_size_id::text,\n            '/' ORDER BY x.fw_size_id\n        )\n        FROM special_frame_variant_fw_size x\n        WHERE x.variant_id = sfv.id\n    ) AS \"FW Size\",\n\n    (\n        SELECT\n            CASE\n                WHEN COUNT(*) = 0 THEN NULL\n\n                WHEN\n                    bool_and(\n                        CASE\n                            WHEN x.fw_size_id = 2\n                            THEN x.row_count = 1\n                            ELSE x.row_count = 3\n                        END\n                    )\n                    AND COUNT(*) = 4\n                THEN '1 row for size 2; up to 3 rows for size 4/6/8'\n\n                WHEN\n                    bool_and(\n                        CASE\n                            WHEN x.fw_size_id = 2\n                            THEN x.row_count = 1\n                            ELSE x.row_count = 2\n                        END\n                    )\n                    AND COUNT(*) = 4\n                THEN '1 row for size 2; up to 2 rows for size 4/6/8'\n\n                WHEN\n                    COUNT(*) = 4\n                    AND bool_and(x.row_count = 1)\n                THEN '1 row for size 2/4/6/8'\n\n                WHEN\n                    COUNT(*) = 4\n                    AND bool_and(x.row_count = 6)\n                THEN '1/2/3/4/5/6'\n\n                WHEN\n                    COUNT(*) = 4\n                    AND bool_and(x.row_count = 2)\n                THEN '1/2'\n\n                WHEN\n                    COUNT(*) = 4\n                    AND bool_and(x.row_count = 3)\n                THEN '1/2/3'\n\n                ELSE (\n                    SELECT string_agg(\n                        x2.fw_size_id::text || ':' ||\n                        x2.row_count::text,\n                        '/' ORDER BY x2.fw_size_id\n                    )\n                    FROM (\n                        SELECT\n                            sfrr.fw_size_id,\n                            COUNT(*) AS row_count\n                        FROM special_frame_row_rule sfrr\n                        WHERE sfrr.variant_id = sfv.id\n                        GROUP BY sfrr.fw_size_id\n                    ) x2\n                )\n            END\n        FROM (\n            SELECT\n                sfrr.fw_size_id,\n                COUNT(*) AS row_count\n            FROM special_frame_row_rule sfrr\n            WHERE sfrr.variant_id = sfv.id\n            GROUP BY sfrr.fw_size_id\n        ) x\n    ) AS \"Rows\",\n\n    (\n        SELECT\n            CASE\n                WHEN COUNT(*) = 0 THEN NULL\n\n                WHEN bool_and(x.max_columns = 15)\n                THEN '1 thru 15'\n\n                WHEN bool_and(x.max_columns = 10)\n                THEN '1 thru 10'\n\n                WHEN\n                    bool_and(\n                        CASE\n                            WHEN x.fw_size_id = 2\n                                THEN x.max_columns = 3\n                            WHEN x.fw_size_id IN (4, 6)\n                                THEN x.max_columns = 5\n                            WHEN x.fw_size_id = 8\n                                THEN x.max_columns = 8\n                            ELSE false\n                        END\n                    )\n                THEN\n                    'Up to 3 columns for size 2, ' ||\n                    '5 columns for sizes 4/6, ' ||\n                    '8 columns for size 8'\n\n                ELSE (\n                    SELECT string_agg(\n                        x2.fw_size_id::text || ':' ||\n                        x2.max_columns::text,\n                        '/' ORDER BY x2.fw_size_id\n                    )\n                    FROM (\n                        SELECT\n                            sfrr.fw_size_id,\n                            MAX(sfrr.max_columns) AS max_columns\n                        FROM special_frame_row_rule sfrr\n                        WHERE sfrr.variant_id = sfv.id\n                        GROUP BY sfrr.fw_size_id\n                    ) x2\n                )\n            END\n        FROM (\n            SELECT\n                sfrr.fw_size_id,\n                MAX(sfrr.max_columns) AS max_columns\n            FROM special_frame_row_rule sfrr\n            WHERE sfrr.variant_id = sfv.id\n            GROUP BY sfrr.fw_size_id\n        ) x\n    ) AS \"Columns\",\n\n    sfv.name_editable AS name_editable,\n\n    sfv.special_coating_editable AS special_coating_editable\n\nFROM special_frame_configuration sfc\n\nJOIN special_frame_variant sfv\n    ON sfv.configuration_id = sfc.id\n\nJOIN base_material bm\n    ON bm.id = sfc.base_material_id\n\nJOIN system_type st\n    ON st.id = sfc.system_type_id\n\nJOIN frame_type ft\n    ON ft.id = sfc.frame_type_id\n\nORDER BY\n    sfv.is_available DESC,\n    bm.name_key,\n    st.name,\n    ft.name,\n    sfv.flange_width_code DESC NULLS LAST,\n    sfv.frame_material_code;";
        return GetResultFromTable(npgsql, sql);
    }
}