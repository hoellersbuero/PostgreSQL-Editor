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
            string sql = "WITH available_variants AS ( SELECT sfc.id AS configuration_id, sfc.base_material_id, sfc.system_type_id, sfc.frame_type_id, sfv.id AS variant_id, sfv.frame_material_code, sfv.flange_width_code, sfv.special_coating_editable, sfv.kit_single_selectable FROM special_frame_configuration sfc JOIN special_frame_variant sfv ON sfv.configuration_id = sfc.id WHERE sfc.base_material_id = '@BASEMATERIAL_ID' AND sfc.system_type_id = '@SYSTEMTYPE_ID' AND sfv.is_available = TRUE ), flange_options AS ( SELECT frame_material_code, jsonb_agg( DISTINCT flange_width_code ORDER BY flange_width_code ) FILTER ( WHERE flange_width_code IS NOT NULL ) AS flange_options  FROM available_variants GROUP BY frame_material_code ), wedge_materials AS ( SELECT av.frame_material_code, ARRAY_AGG( DISTINCT sfvwm.material_code ORDER BY sfvwm.material_code ) AS wedge_ap_materials FROM available_variants av JOIN special_frame_variant_wedge_material sfvwm ON sfvwm.variant_id = av.variant_id GROUP BY av.frame_material_code ), fw_rules AS ( SELECT av.frame_material_code, sfvfs.fw_size_id, ARRAY_AGG( DISTINCT sfrr.rows ORDER BY sfrr.rows ) AS rows, MAX(sfrr.max_columns) AS max_columns FROM available_variants av JOIN special_frame_variant_fw_size sfvfs ON sfvfs.variant_id = av.variant_id JOIN special_frame_row_rule sfrr ON sfrr.variant_id = sfvfs.variant_id AND sfrr.fw_size_id = sfvfs.fw_size_id GROUP BY av.frame_material_code, sfvfs.fw_size_id ), fw_options AS ( SELECT frame_material_code, ARRAY_AGG( DISTINCT fw_size_id ORDER BY fw_size_id ) AS fw_sizes, JSONB_AGG( JSONB_BUILD_OBJECT( 'fw_size', fw_size_id, 'rows', rows, 'columns', ( SELECT ARRAY_AGG(n ORDER BY n) FROM generate_series(1, max_columns) AS n )) ORDER BY fw_size_id ) AS fw_size_rules FROM fw_rules GROUP BY frame_material_code ), coating_options AS ( SELECT frame_material_code, BOOL_OR(special_coating_editable) AS special_coating FROM available_variants GROUP BY frame_material_code ), kit_selectable_options AS ( SELECT frame_material_code, BOOL_OR(kit_single_selectable) AS kit_single_selectable FROM available_variants GROUP BY frame_material_code ), frame_material_result AS ( SELECT av.base_material_id, bm.name_key AS base_material, av.system_type_id, st.name AS system_type, av.frame_type_id, ft.name AS frame_type, av.frame_material_code AS frame_material, fo.flange_options, fw.fw_sizes, fw.fw_size_rules, wm.wedge_ap_materials, co.special_coating, kso.kit_single_selectable FROM available_variants av JOIN base_material bm ON bm.id = av.base_material_id JOIN system_type st ON st.id = av.system_type_id JOIN frame_type ft ON ft.id = av.frame_type_id JOIN flange_options fo ON fo.frame_material_code = av.frame_material_code JOIN fw_options fw ON fw.frame_material_code = av.frame_material_code LEFT JOIN wedge_materials wm ON wm.frame_material_code = av.frame_material_code JOIN coating_options co ON co.frame_material_code = av.frame_material_code JOIN kit_selectable_options kso ON kso.frame_material_code = av.frame_material_code ) SELECT jsonb_build_object( 'base_material_id', base_material_id, 'base_material', base_material, 'system_type_id', system_type_id, 'system_type', system_type, 'frame_type_id', frame_type_id, 'frame_type', frame_type, 'frame_materials', jsonb_agg( jsonb_build_object( 'frame_material', frame_material, 'flange_options', flange_options, 'fw_sizes', fw_sizes, 'fw_size_rules', fw_size_rules, 'wedge_ap_materials', wedge_ap_materials, 'special_coating', special_coating, 'kit_single_selectable', kit_single_selectable ) ORDER BY frame_material )) AS special_frame_configuration FROM frame_material_result GROUP BY base_material_id, base_material, system_type_id, system_type, frame_type_id, frame_type;";
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
            return String.Join(Environment.NewLine + Environment.NewLine, reslist);  
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
            string sql = "WITH available_variants AS (SELECT sfc.id AS configuration_id, sfc.base_material_id, sfc.system_type_id, sfc.frame_type_id, sfv.id AS variant_id, sfv.frame_material_code, sfv.flange_width_code, sfv.special_coating_editable, sfv.kit_single_selectable FROM special_frame_configuration sfc JOIN special_frame_variant sfv ON sfv.configuration_id = sfc.id WHERE sfc.base_material_id = '@BASEMATERIAL'   AND sfc.system_type_id   = '@SYSTEMTYPE'   AND sfv.is_available = TRUE ), flange_options AS ( SELECT frame_material_code, jsonb_agg( DISTINCT flange_width_code ORDER BY flange_width_code ) FILTER ( WHERE flange_width_code IS NOT NULL ) AS flange_options FROM available_variants GROUP BY frame_material_code ), wedge_materials AS ( SELECT av.frame_material_code, ARRAY_AGG( DISTINCT sfvwm.material_code ORDER BY sfvwm.material_code ) AS wedge_ap_materials FROM available_variants av JOIN special_frame_variant_wedge_material sfvwm ON sfvwm.variant_id = av.variant_id GROUP BY av.frame_material_code ), fw_rules AS ( SELECT av.frame_material_code, sfvfs.fw_size_id,  ARRAY_AGG( DISTINCT sfrr.rows ORDER BY sfrr.rows ) AS rows, MAX(sfrr.max_columns) AS max_columns FROM available_variants av JOIN special_frame_variant_fw_size sfvfs ON sfvfs.variant_id = av.variant_id JOIN special_frame_row_rule sfrr ON sfrr.variant_id = sfvfs.variant_id AND sfrr.fw_size_id = sfvfs.fw_size_id GROUP BY av.frame_material_code, sfvfs.fw_size_id ), fw_options AS ( SELECT frame_material_code, ARRAY_AGG( DISTINCT fw_size_id ORDER BY fw_size_id ) AS fw_sizes, JSONB_AGG( JSONB_BUILD_OBJECT( 'fw_size', fw_size_id, 'rows', rows, 'columns', ( SELECT ARRAY_AGG(n ORDER BY n) FROM generate_series(1, max_columns) AS n )) ORDER BY fw_size_id ) AS fw_size_rules FROM fw_rules GROUP BY frame_material_code ), coating_options AS ( SELECT frame_material_code, BOOL_OR(special_coating_editable) AS special_coating FROM available_variants GROUP BY frame_material_code ), kit_selectable_options AS ( SELECT frame_material_code, BOOL_OR(kit_single_selectable) AS kit_single_selectable FROM available_variants GROUP BY frame_material_code ) SELECT av.base_material_id, bm.name_key AS base_material, av.system_type_id, st.name AS system_type, av.frame_type_id, ft.name AS frame_type, av.frame_material_code AS frame_material, fo.flange_options, fw.fw_sizes, fw.fw_size_rules, wm.wedge_ap_materials, co.special_coating, kso.kit_single_selectable FROM available_variants av JOIN base_material bm ON bm.id = av.base_material_id JOIN system_type st ON st.id = av.system_type_id JOIN frame_type ft ON ft.id = av.frame_type_id JOIN flange_options fo ON fo.frame_material_code = av.frame_material_code JOIN fw_options fw ON fw.frame_material_code = av.frame_material_code LEFT JOIN wedge_materials wm ON wm.frame_material_code = av.frame_material_code JOIN coating_options co ON co.frame_material_code = av.frame_material_code JOIN kit_selectable_options kso ON kso.frame_material_code = av.frame_material_code GROUP BY av.base_material_id, bm.name_key, av.system_type_id, st.name, av.frame_type_id, ft.name, av.frame_material_code, fo.flange_options, fw.fw_sizes, fw.fw_size_rules, wm.wedge_ap_materials, co.special_coating, kso.kit_single_selectable ORDER BY ft.name, av.frame_material_code;";
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
        string sql = "SELECT sfc.id AS configuration_id, sfv.id AS variant_id, sfv.is_available, sfv.kit_single_selectable, bm.name_key AS \"Base Material\", st.name AS \"System Type\", ft.name AS \"Frametype\", sfv.flange_width_code AS \"Flange Width\", sfv.frame_material_code AS \"Frame Material\", COALESCE( (   SELECT string_agg(     w.material_code,     '/' ORDER BY w.material_code   )   FROM special_frame_variant_wedge_material w   WHERE w.variant_id = sfv.id ), '' ) AS \"Wedge/A.P. Material\", ( SELECT string_agg(   x.fw_size_id::text,   '/' ORDER BY x.fw_size_id ) FROM special_frame_variant_fw_size x WHERE x.variant_id = sfv.id ) AS \"FW Size\", ( SELECT   CASE     WHEN COUNT(*) = 0 THEN NULL   WHEN       bool_and(         CASE           WHEN x.fw_size_id = 2           THEN x.row_count = 1           ELSE x.row_count = 3         END       )       AND COUNT(*) = 4     THEN '1 row for size 2; up to 3 rows for size 4/6/8'   WHEN       bool_and(         CASE           WHEN x.fw_size_id = 2           THEN x.row_count = 1           ELSE x.row_count = 2         END       )       AND COUNT(*) = 4     THEN '1 row for size 2; up to 2 rows for size 4/6/8'   WHEN       COUNT(*) = 4       AND bool_and(x.row_count = 1)     THEN '1 row for size 2/4/6/8'   WHEN       COUNT(*) = 4       AND bool_and(x.row_count = 6)     THEN '1/2/3/4/5/6'   WHEN       COUNT(*) = 4       AND bool_and(x.row_count = 2)     THEN '1/2'   WHEN       COUNT(*) = 4       AND bool_and(x.row_count = 3)     THEN '1/2/3'   ELSE (       SELECT string_agg(         x2.fw_size_id::text || ':' ||         x2.row_count::text,         '/' ORDER BY x2.fw_size_id       )       FROM (         SELECT           sfrr.fw_size_id,           COUNT(*) AS row_count         FROM special_frame_row_rule sfrr         WHERE sfrr.variant_id = sfv.id         GROUP BY sfrr.fw_size_id       ) x2     )   END FROM (   SELECT     sfrr.fw_size_id,     COUNT(*) AS row_count   FROM special_frame_row_rule sfrr   WHERE sfrr.variant_id = sfv.id   GROUP BY sfrr.fw_size_id ) x ) AS \"Rows\", ( SELECT   CASE     WHEN COUNT(*) = 0 THEN NULL   WHEN bool_and(x.max_columns = 15)     THEN '1 thru 15'   WHEN bool_and(x.max_columns = 10)     THEN '1 thru 10'   WHEN       bool_and(         CASE           WHEN x.fw_size_id = 2             THEN x.max_columns = 3           WHEN x.fw_size_id IN (4, 6)             THEN x.max_columns = 5           WHEN x.fw_size_id = 8             THEN x.max_columns = 8           ELSE false         END       )     THEN       'Up to 3 columns for size 2, ' ||       '5 columns for sizes 4/6, ' ||       '8 columns for size 8'   ELSE (       SELECT string_agg(         x2.fw_size_id::text || ':' ||         x2.max_columns::text,         '/' ORDER BY x2.fw_size_id       )       FROM (         SELECT           sfrr.fw_size_id,           MAX(sfrr.max_columns) AS max_columns         FROM special_frame_row_rule sfrr         WHERE sfrr.variant_id = sfv.id         GROUP BY sfrr.fw_size_id       ) x2     )   END FROM (   SELECT     sfrr.fw_size_id,     MAX(sfrr.max_columns) AS max_columns   FROM special_frame_row_rule sfrr   WHERE sfrr.variant_id = sfv.id   GROUP BY sfrr.fw_size_id ) x ) AS \"Columns\", sfv.name_editable AS name_editable, sfv.special_coating_editable AS special_coating_editable FROM special_frame_configuration sfc JOIN special_frame_variant sfv ON sfv.configuration_id = sfc.id JOIN base_material bm ON bm.id = sfc.base_material_id JOIN system_type st ON st.id = sfc.system_type_id JOIN frame_type ft ON ft.id = sfc.frame_type_id ORDER BY sfv.is_available DESC, bm.name_key, st.name, ft.name, sfv.flange_width_code DESC NULLS LAST, sfv.frame_material_code;";
        return GetResultFromTable(npgsql, sql);
    }
}