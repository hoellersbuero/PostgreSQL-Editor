using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PostgreSQL_Editor.Global
{
    public class Global
    {
        public static string schema = "pct_technical_data";
        public static string getConstraints = "SELECT " +
                                                "con.conname AS constraint_name, " +
                                                "con.contype AS constraint_type, " +
                                                "pg_get_constraintdef(con.oid) AS definition, " +
                                                "rel.relname AS table_name, " +
                                                "nsp.nspname AS schema_name " +
                                              "FROM pg_constraint con " +
                                              "JOIN pg_class rel ON rel.oid = con.conrelid " +
                                              "JOIN pg_namespace nsp ON nsp.oid = rel.relnamespace " +
                                              "WHERE rel.relname = '{{table}}' " +
                                                "AND nsp.nspname = '" + schema + "' " +
                                              "ORDER BY constraint_type DESC, constraint_name;";
        public static string getIndexes = "SELECT tablename,indexname,indexdef FROM pg_indexes WHERE tablename = '{{table}}' AND schemaname = '" + schema + "';";
        public static string allForeignKeys = "SELECT con.conname AS constraint_name, " +
                                                "con.contype AS constraint_type, " +
                                                "pg_get_constraintdef(con.oid) AS definition, " +
                                                "rel.relname AS table_name, " +
                                                "nsp.nspname AS schema_name " +
                                              "FROM pg_constraint con " +
                                                "JOIN pg_class rel ON rel.oid = con.conrelid " +
                                                "JOIN pg_namespace nsp ON nsp.oid = rel.relnamespace " +
                                              "WHERE {{alltables}} " +
                                                "AND nsp.nspname = 'pct_technical_data' " +
                                                "AND con.contype LIKE = 'f' " +
                                              "ORDER BY constraint_type DESC, constraint_name;";
        public static string allForeignKeys2 = "SELECT con.conname AS constraint_name, " +
                                                 "con.contype AS constraint_type, " +
                                                 "pg_get_constraintdef(con.oid) AS definition, " +
                                                 "rel.relname AS table_name, " +
                                                 "nsp.nspname AS schema_name, " +
                                                 "refrel.relname AS referenced_table, " +
                                                 "array_to_string(ARRAY(SELECT att.attname FROM unnest(con.conkey) WITH ORDINALITY AS ck(attnum, ord) " +
                                                 "JOIN pg_attribute att ON att.attrelid = con.conrelid AND att.attnum = ck.attnum " +
                                                 "ORDER BY ck.ord), ',') AS columns, " +
                                                 "array_to_string(ARRAY(SELECT att2.attname FROM unnest(con.confkey) WITH ORDINALITY AS fk(attnum, ord) " +
                                                 "JOIN pg_attribute att2 ON att2.attrelid = con.confrelid AND att2.attnum = fk.attnum " +
                                                 "ORDER BY fk.ord), ',') AS referenced_columns " +
                                                 "FROM pg_constraint con " +
                                                 "JOIN pg_class rel ON rel.oid = con.conrelid " +
                                                 "JOIN pg_namespace nsp ON nsp.oid = rel.relnamespace " +
                                                 "LEFT JOIN pg_class refrel ON refrel.oid = con.confrelid " +
                                                 "WHERE {{alltables}} AND nsp.nspname = 'pct_technical_data' AND con.contype = 'f' " +
                                                 "ORDER BY constraint_type DESC, constraint_name;";
        public static string alterConstraints = "alter table {{table}} drop constraint {{fkey}}, " +
                                                "add constraint {{fkey}} foreign key ({{column}}) " +
                                                "references {{referenced_table}}({{referenced_column}}) " +
                                                "on update no action on delete cascade not valid;";

        public static List<string> SQLCommands = new List<string>();
        public static Dictionary<string, List<string>> TableColumnsByTable = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

    }

    #region base classes =========================================================================

    public class product
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string article_number { get; set; }
        public Guid product_type_id { get; set; }
        public bool? is_available { get; set; }
        public decimal weight_kg { get; set; }
        public string created_by { get; set; }
        public DateTime created_ts { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_ts { get; set; }
    }

    public class base_material
    {
        public string visiblename { get; set; }
        public Guid id { get; set; }
        public string name { get; set; }
        public string created_by { get; set; }
        public DateTime created_ts { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_ts { get; set; }
    }

    public class system_type
    {
        public string name { get; set; }
        public Guid id { get; set; }
        public string created_by { get; set; }
        public DateTime created_ts { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_ts { get; set; }
        public string system_type_hint { get; set; }
        public int row_filling_pattern { get; set; }

        public override string ToString()
        {
            return name + " (" + system_type_hint + ")";
        }
    }

    public class frame_type
    {
        public Guid id { get; set; }
        public bool supports_sealant { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public int shape { get; set; }
        public decimal bar_width_inner { get; set; }
        public decimal bar_width_outer { get; set; }
        public decimal flange_extension_horizontal { get; set; }
        public decimal flange_extension_vertical { get; set; }
        public decimal edge_radius_inner { get; set; }
        public decimal edge_radius_fitting { get; set; }
        public string name { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class material_type
    {
        public Guid id { get; set; }
        public int engineering_color_code { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string modified_by { get; set; }
        public string name { get; set; }
        public string catalog_name { get; set; }
        public string created_by { get; set; }
    }

    public class module_class
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class module : product
    {
        public decimal height { get; set; }
        public decimal width { get; set; }
        public int max_cable_capacity { get; set; }
        public bool is_filler_module { get; set; }
        public bool is_oversize_module { get; set; }

        public override string ToString()
        {
            return name + " (" + height + " x " + width + ")";
        }
    }

    public class frame : product
    {
        public Guid frame_type_id { get; set; }
        public Guid material_type_id { get; set; }
        public Guid geometry_id { get; set; }
        public int wedge_quantity { get; set; }
        public int holes_horizontal { get; set; }
        public int holes_vertical { get; set; }
        public decimal offset_horizontal { get; set; }
        public decimal offset_vertical { get; set; }
        public decimal drill_diameter { get; set; }
        public bool has_drilled_holes { get; set; }
        public int? drilling_schema_id { get; set; }
        public int? hole_schema_id { get; set; }
        public int rows { get; set; }
        public int columns { get; set; }
    }

    public class frame_alone
    {
        public Guid id { get; set; }
        public Guid frame_type_id { get; set; }
        public Guid material_type_id { get; set; }
        public Guid geometry_id { get; set; }
        public int wedge_quantity { get; set; }
        public int holes_horizontal { get; set; }
        public int holes_vertical { get; set; }
        public decimal offset_horizontal { get; set; }
        public decimal offset_vertical { get; set; }
        public decimal drill_diameter { get; set; }
        public bool has_drilled_holes { get; set; }
        public int? drilling_schema_id { get; set; }
        public int? hole_schema_id { get; set; }
        public int rows { get; set; }
        public int columns { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class frame_geometry
    {
        public Guid id { get; set; }
        public decimal opening_addition_max { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public decimal h1 { get; set; }
        public decimal b1 { get; set; }
        public decimal h2 { get; set; }
        public decimal b2 { get; set; }
        public decimal opening_addition_min { get; set; }
        public string flange_type { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class frame_window
    {
        public Guid id { get; set; }
        public Guid frame_id { get; set; }
        public decimal frame_window_height { get; set; }
        public decimal frame_window_width { get; set; }
        public decimal frame_window_height_natural { get; set; }
        public decimal frame_window_width_natural { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class frame_hole_schema
    {
        public int id { get; set; }
        public decimal rotation_angle { get; set; }
        public int hole_quantity { get; set; }
        public decimal center_radius { get; set; }
        public decimal hole_radius { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string comment { get; set; }
        public string created_by { get; set; }
        public string modified_by { get; set; }
    }

    public class wedge_type
    {
        public Guid id { get; set; }
        public string article_number { get; set; } = string.Empty;
        public string name { get; set; }
        public decimal width { get; set; }
        public decimal height { get; set; }
        public Guid material_type_id { get; set; }
        public bool is_compression_kit { get; set; }
        public int anchor_quantity { get; set; }
        public int fixing_anchor_quantity { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string created_by { get; set; }
        public string wedge_description { get; set; }
        public string modified_by { get; set; }
    }

    public class sleeve_type
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public Guid material_type_id { get; set; }
        public bool has_flange { get; set; }
        public decimal installation_tolerance_min { get; set; }
        public decimal installation_tolerance_max { get; set; }
        public decimal? flange_diameter { get; set; }
        public decimal outer_pipe_diameter { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class sticker_type
    {
        public string name { get; set; }
        public string visual_name { get; set; }
        public bool has_sleeve_flange { get; set; }
        public decimal fw_height { get; set; }
        public bool has_sleeve { get; set; }
        public Guid id { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string description { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class entity_metadata
    {
        public bool check { get; set; }
        public Guid id { get; set; }
        public Guid entity_id { get; set; }
        public string entity_name { get; set; }
        public decimal? number_value { get; set; }
        public bool? bool_value { get; set; }
        public JsonDocument json_value { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string entity_type { get; set; }
        public string metadata_key { get; set; }
        public string string_value { get; set; }
        public string created_by { get; set; }
        public string modified_by { get; set; }
    }

    public class module_variation
    {
        public Guid id { get; set; }
        public Guid module_type_id { get; set; }
        public decimal cable_bundle_min_diameter { get; set; }
        public decimal cable_bundle_max_diameter { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class module_packaging
    {
        public Guid id { get; set; }
        public Guid module_id { get; set; }
        public int package { get; set; }
        public int packing_qty { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string packaging_unit { get; set; }
        public string created_by { get; set; }
        public string modified_by { get; set; }
    }

    public class drilling_schema
    {
        public int id { get; set; }
        public int hole_quantity { get; set; }
        public decimal hole_diameter { get; set; }
        public int parent_id { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class drilling_schema_angled
    {
        public int id { get; set; }
        public decimal offset_x_mm { get; set; }
        public decimal offset_y_mm { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string created_by { get; set; }
        public string comment { get; set; }
        public string modified_by { get; set; }
    }

    public class drilling_schema_round
    {
        public int id { get; set; }
        public decimal rotation_angle { get; set; }
        public decimal radius_mm { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string created_by { get; set; }
        public string comment { get; set; }
        public string modified_by { get; set; }
    }

    public class product_type
    {
        public Guid id { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string name { get; set; }
        public string created_by { get; set; }
        public string modified_by { get; set; }
    }

    #endregion =================================================================================

    #region rules classes ======================================================================
    public class material_type_rule
    {
        public bool check { get; set; }
        public Guid id { get; set; }
        public string base_material { get; set; }
        public Guid base_material_id { get; set; }
        public string system_type { get; set; }
        public Guid system_type_id { get; set; }
        public bool is_active { get; set; }
        public int rule_version { get; set; }
    }

    public class system_type_frame_type_rule
    {
        public bool check { get; set; }
        public Guid id { get; set; }
        public Guid system_type_id { get; set; }
        public string system_type { get; set; }
        public Guid frame_type_id { get; set; }
        public string frame_type { get; set; }
        public int rule_version { get; set; }
        public bool is_active { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class frame_type_rule
    {
        public bool check { get; set; }
        public Guid id { get; set; }
        public Guid base_material_id { get; set; }
        public string base_material { get; set; }
        public Guid system_type_id { get; set; }
        public string system_type { get; set; }
        public Guid frame_type_id { get; set; }
        public string frame_type { get; set; }
        public bool is_special { get; set; }
        public int rule_version { get; set; }
        public bool is_active { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class BMSTFTMT_rule
    {
        public bool check { get; set; }
        public Guid id { get; set; }
        public Guid base_material_id { get; set; }
        public string base_material { get; set; }
        public Guid system_type_id { get; set; }
        public string system_type { get; set; }
        public Guid frame_type_id { get; set; }
        public string frame_type { get; set; }
        public Guid material_type_id { get; set; }
        public string material_type { get; set; }
        public bool is_special { get; set; }
        public int rule_version { get; set; }
        public bool is_active { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class module_rule
    {
        public bool check { get; set; }
        public Guid id { get; set; }
        public Guid system_type_id { get; set; }
        public string system_type { get; set; }
        public Guid module_class_id { get; set; }
        public string module_class { get; set; }
        public Guid frame_type_id { get; set; }
        public string frame_type { get; set; }
        public int priority { get; set; }
        public int rule_version { get; set; }
        public bool is_active { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class sleeve_rule
    {
        public bool check { get; set; }
        public Guid id { get; set; }
        public Guid base_material_id { get; set; }
        public string base_material { get; set; }
        public Guid system_type_id { get; set; }
        public string system_type { get; set; }
        public Guid frame_type_id { get; set; }
        public string frame_type { get; set; }
        public int rule_version { get; set; }
        public bool is_active { get; set; }
        public bool is_mandatory { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class frame_sleeve_rule
    {
        public bool check { get; set; }
        public Guid id { get; set; }
        public Guid frame_id { get; set; }
        public string frame_name { get; set; } = null;
        public Guid sleeve_id { get; set; }
        public string sleeve_name { get; set; } = null;
        public bool is_active { get; set; }
        public int rule_version { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class frame_sticker_rule
    {
        public bool check { get; set; }
        public Guid id { get; set; }
        public Guid system_type_id { get; set; }
        public string system_type { get; set; }
        public Guid frame_type_id { get; set; }
        public string frame_type { get; set; }
        public decimal fw_height { get; set; }
        public Guid sticker_type_id { get; set; }
        public string sticker_type { get; set; }
        public int rule_version { get; set; }
        public bool is_active { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class sleeve_sticker_rule
    {
        public bool check { get; set; }
        public Guid id { get; set; }
        public Guid system_type_id { get; set; }
        public string system_type { get; set; }
        public Guid sleeve_type_id { get; set; }
        public string sleeve_type { get; set; }
        public Guid sticker_type_id { get; set; }
        public string sticker_type { get; set; }
        public int rule_version { get; set; }
        public bool is_active { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class wedge_rule
    {
        public bool check { get; set; }
        public Guid id { get; set; }
        public Guid system_type_id { get; set; }
        public string system_type { get; set; }
        public Guid wedge_type_id { get; set; }
        public string wedge_type { get; set; }
        public string article_number { get; set; }
        public bool is_wedge_kit { get; set; }
        public int rule_version { get; set; }
        public bool is_active { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }

    public class wedge_option_rule
    {
        public bool check { get; set; }
        public Guid id { get; set; }
        public Guid system_type_id { get; set; }
        public string system_type { get; set; }
        public Guid material_type_id { get; set; }
        public string material_type { get; set; }
        public Guid wedge_type_id { get; set; }
        public string wedge_type { get; set; }
        public string article_number { get; set; }
        public bool has_wedge_option { get; set; }
        public bool has_material_option { get; set; }
        public bool is_special { get; set; }
        public int rule_version { get; set; }
        public bool is_active { get; set; }
        public DateTime created_ts { get; set; }
        public DateTime modified_ts { get; set; }
        public string modified_by { get; set; }
        public string created_by { get; set; }
    }
    #endregion =================================================================================

    public class SF_FrameMaterial
    {
        public List<int> fw_sizes { get; set; }
        public List<SF_FwSizeRule> fw_size_rules { get; set; }
        public List<string> flange_options { get; set; }
        public string frame_material { get; set; }
        public bool special_coating { get; set; }
        public List<string> wedge_ap_materials { get; set; }
        public bool kit_single_selectable { get; set; }
    }

    public class SF_FwSizeRule
    {
        public List<int> rows { get; set; }
        public List<int> columns { get; set; }
        public int fw_size { get; set; }
    }

    public class SpecialFrameRoot
    {
        public string frame_type { get; set; }
        public string system_type { get; set; }
        public string base_material { get; set; }
        public string frame_type_id { get; set; }
        public string system_type_id { get; set; }
        public List<SF_FrameMaterial> frame_materials { get; set; }
        public string base_material_id { get; set; }
    }
}