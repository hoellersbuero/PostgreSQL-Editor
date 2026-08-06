using Npgsql;
using PostgreSQL_Editor.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace PostgreSQL_Editor.DBUtils
{
    public class standardLists
    {
        public static List<product> products = new List<product>();
        public static List<base_material> baseMaterials = new List<base_material>();
        public static List<system_type> systemTypes = new List<system_type>();
        public static List<system_type> systemTypesAllowed = new List<system_type>();
        public static List<frame_type> frameTypes = new List<frame_type>();
        public static List<material_type> materialTypes = new List<material_type>();
        public static List<module_class> moduleClasses = new List<module_class>();
        public static List<module> modules = new List<module>();
        public static List<frame_geometry> frameGeometries = new List<frame_geometry>();
        public static List<frame_window> frameWindows = new List<frame_window>();
        public static List<frame_hole_schema> frameHoleSchemas = new List<frame_hole_schema>();
        public static List<frame> frames = new List<frame>();
        public static List<wedge_type> wedgeTypes = new List<wedge_type>();
        public static List<sleeve_type> sleeveTypes = new List<sleeve_type>();
        public static List<sticker_type> stickerTypes = new List<sticker_type>();
        public static List<decimal> fwHeights = new List<decimal>();
        public static List<entity_metadata> entityMetaData = new List<entity_metadata>();
        public static List<product_type> productTypes = new List<product_type>();
        public static List<module_variation> moduleVariations = new List<module_variation>();
        public static List<module_packaging> modulePackagings = new List<module_packaging>();
        public static List<drilling_schema> drillingSchemas = new List<drilling_schema>();
        public static List<drilling_schema_angled> drillingSchemaAngled = new List<drilling_schema_angled>();
        public static List<drilling_schema_round> drillingSchemaRound = new List<drilling_schema_round>();
        public static List<material_type_rule> baseMaterialSystemTypeRules = new List<material_type_rule>();
        public static List<system_type_frame_type_rule> systemTypeFrameTypeRules = new List<system_type_frame_type_rule>();
        public static List<material_type_rule> materialTypeRules = new List<material_type_rule>();
        public static List<BMSTFTMT_rule> BMSTFTMT_rules = new List<BMSTFTMT_rule>();
        public static List<frame_type_rule> frameTypeRules = new List<frame_type_rule>();
        public static List<module_rule> moduleRules = new List<module_rule>();
        public static List<sleeve_rule> sleeveRules = new List<sleeve_rule>();
        public static List<frame_sleeve_rule> frameSleeveRules = new List<frame_sleeve_rule>();
        public static List<frame_sticker_rule> frameStickerRules = new List<frame_sticker_rule>();
        public static List<sleeve_sticker_rule> sleeveStickerRules = new List<sleeve_sticker_rule>();
        public static List<wedge_rule> wedgeRules = new List<wedge_rule>();
        public static List<string> SQLStatements = new List<string>();

        #region loading

        public static void loadAll(NpgsqlConnection npgsql)
        {
            getAllSqlStatements();
            getProducts(npgsql);
            getBaseMaterials(npgsql);
            getSystemTypes(npgsql);
            getAllSystemTypesAllowed(npgsql);
            getFrameTypes(npgsql);
            getMaterialTypes(npgsql);
            getModuleClasses(npgsql);
            getModules(npgsql);
            getFrameGeometries(npgsql);
            getFrameWindows(npgsql);
            getFrameHoleSchemas(npgsql);
            getFrames(npgsql);
            getWedgeTypes(npgsql);
            getSleeveTypes(npgsql);
            getStickerTypes(npgsql);
            getFwHeights(npgsql);
            getProductType(npgsql);
            getDrillingSchemas(npgsql);
            getDrillingSchemaAngled(npgsql);
            getDrillingSchemaRound(npgsql);
            getModuleVariations(npgsql);
            getModulePackagings(npgsql);
            getEntityMetaData(npgsql);
            getSystemTypeRules(npgsql);
            getSystemTypeFrameTypeRules(npgsql);
            getFrameTypeRules(npgsql);
            getBMSTFTMTRules(npgsql);
            getModuleRules(npgsql);
            getSleeveRules(npgsql);
            getFrameSleeveRules(npgsql);
            getFrameStickerRules(npgsql);
            getSleeveStickerRules(npgsql);
            getWedgeRules(npgsql);
        }
        #endregion

        #region helper =========================================================================
        public static void getAllSqlStatements()
        {
            var content = Properties.Resources.SQL_Statements ?? string.Empty;
            var sqls = content
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();
            foreach (string s in sqls)
            {
                List<string> ls = s.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (string ss in ls)
                {
                    if (!SQLStatements.Contains(ss))
                        SQLStatements.Add(ss);
                }
            }
            SQLStatements.Sort();
        }

        public static void getAllSystemTypesAllowed(NpgsqlConnection npgsql)
        {
            systemTypesAllowed.Clear();
            string query = "select distinct system_type.* from system_type, material_type_rule where material_type_rule.system_type_id=system_type.id and material_type_rule.is_active";
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; " + query, npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        system_type systemType = new system_type();
                        systemType.name = (string)reader["name"];
                        systemType.id = (Guid)reader["id"];
                        systemType.created_by = (string)reader["created_by"];
                        systemType.created_ts = (DateTime)reader["created_ts"];
                        systemType.modified_by = (string)reader["modified_by"];
                        systemType.modified_ts = (DateTime)reader["modified_ts"];
                        systemType.system_type_hint = (string)reader["system_type_hint"];
                        systemType.row_filling_pattern = (int)reader["row_filling_pattern"];
                        systemTypesAllowed.Add(systemType);
                    }
                }
            }
            systemTypesAllowed.SortByName<system_type>();
        }

        public static List<frame> getAllRoundFrames()
        {
            List<frame> result = new List<frame>();
            foreach (frame f in frames)
            {
                frame_type ft = frameTypes.Where(x => x.id == f.frame_type_id).FirstOrDefault();
                if (ft != null && ft.shape == 1)
                    result.Add(f);
            }
            result.SortByName<frame>();
            return result;
        }

        private static string getBaseMaterialName(Guid basematerial_id)
        {
            base_material bm = baseMaterials.Where(x => x.id == basematerial_id).FirstOrDefault();
            return (bm == null) ? null : bm.visiblename;
        }

        private static string getSystemTypeName(Guid system_type_id)
        {
            system_type st = systemTypes.Where(x => x.id == system_type_id).FirstOrDefault();
            return (st == null) ? null : st.name;
        }

        private static string getFrameTypeName(Guid frame_type_id)
        {
            frame_type ft = frameTypes.Where(x => x.id == frame_type_id).FirstOrDefault();
            return (ft == null) ? null : ft.name;
        }

        private static string getMaterialTypeName(Guid material_type_id)
        {
            material_type mt = materialTypes.Where(x => x.id == material_type_id).FirstOrDefault();
            return (mt == null) ? null : mt.name;
        }

        private static string getModuleClassName(Guid module_class_id)
        {
            module_class mc = moduleClasses.Where(x => x.id == module_class_id).FirstOrDefault();
            return (mc == null) ? null : mc.name;
        }

        public static string getModuleName(Guid module_type_id)
        {
            module m = modules.Where(x => x.id == module_type_id).FirstOrDefault();
            return (m == null) ? null : m.name;
        }
        #endregion =============================================================================

        #region standard tables ================================================================
        public static void getProducts(NpgsqlConnection npgsql)
        {
            products.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM product", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product p = new product();
                        p.id = (Guid)reader["id"];
                        p.name = (string)reader["name"];
                        p.article_number = (string)reader["article_number"];
                        p.weight_kg = (decimal)reader["weight_kg"];
                        p.is_available = (bool)reader["is_available"];
                        p.created_by = (string)reader["created_by"];
                        p.created_ts = (DateTime)reader["created_ts"];
                        p.modified_by = (string)reader["modified_by"];
                        p.modified_ts = (DateTime)reader["modified_ts"];
                        products.Add(p);
                    }
                }
            }
        }

        public static void getBaseMaterials(NpgsqlConnection npgsql)
        {
            baseMaterials.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM base_material", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        base_material baseMaterial = new base_material();
                        baseMaterial.name = (string)reader["name_key"];
                        baseMaterial.visiblename = baseMaterial.BaseMaterialName();
                        baseMaterial.id = (Guid)reader["id"];
                        baseMaterial.created_by = (string)reader["created_by"];
                        baseMaterial.created_ts = (DateTime)reader["created_ts"];
                        baseMaterial.modified_by = (string)reader["modified_by"];
                        baseMaterial.modified_ts = (DateTime)reader["modified_ts"];
                        baseMaterials.Add(baseMaterial);
                    }
                }
                baseMaterials.SortByName<base_material>();
            }
        }

        public static void getSystemTypes(NpgsqlConnection npgsql)
        {
            systemTypes.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM system_type", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        system_type systemType = new system_type();
                        systemType.name = (string)reader["name"];
                        systemType.id = (Guid)reader["id"];
                        systemType.created_by = (string)reader["created_by"];
                        systemType.created_ts = (DateTime)reader["created_ts"];
                        systemType.modified_by = (string)reader["modified_by"];
                        systemType.modified_ts = (DateTime)reader["modified_ts"];
                        systemType.system_type_hint = (string)reader["system_type_hint"];
                        systemType.row_filling_pattern = (int)reader["row_filling_pattern"];
                        systemTypes.Add(systemType);
                    }
                }
                systemTypes.SortByName<system_type>();
            }
        }

        public static void getFrameTypes(NpgsqlConnection npgsql)
        {
            frameTypes.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM frame_type", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        frame_type frameType = new frame_type();
                        frameType.id = (Guid)reader["id"];
                        frameType.name = (string)reader["name"];
                        frameType.created_by = (string)reader["created_by"];
                        frameType.created_ts = (DateTime)reader["created_ts"];
                        frameType.modified_by = (string)reader["modified_by"];
                        frameType.modified_ts = (DateTime)reader["modified_ts"];
                        frameType.shape = (int)reader["shape"];
                        frameType.bar_width_inner = (decimal)reader["bar_width_inner"];
                        frameType.bar_width_outer = (decimal)reader["bar_width_outer"];
                        frameType.edge_radius_inner = (decimal)reader["edge_radius_inner"];
                        frameType.edge_radius_fitting = (decimal)reader["edge_radius_fitting"];
                        frameType.flange_extension_horizontal = (decimal)reader["flange_extension_horizontal"];
                        frameType.flange_extension_vertical = (decimal)reader["flange_extension_vertical"];
                        frameType.supports_sealant = (bool)reader["supports_sealant"];
                        frameTypes.Add(frameType);
                    }
                }
            }
            frameTypes.SortByName<frame_type>();
        }

        public static void getMaterialTypes(NpgsqlConnection npgsql)
        {
            materialTypes.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM material_type", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        material_type materialType = new material_type();
                        materialType.id = (Guid)reader["id"];
                        materialType.name = (string)reader["name"];
                        materialType.created_by = (string)reader["created_by"];
                        materialType.created_ts = (DateTime)reader["created_ts"];
                        materialType.modified_by = (string)reader["modified_by"];
                        materialType.modified_ts = (DateTime)reader["modified_ts"];
                        materialType.engineering_color_code = (int)reader["engineering_color_code"];
                        materialType.catalog_name = (string)reader["catalog_name"];
                        materialTypes.Add(materialType);
                    }
                }
            }
            materialTypes.SortByName<material_type>();
        }

        public static void getModuleClasses(NpgsqlConnection npgsql)
        {
            moduleClasses.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM module_class", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        module_class moduleClass = new module_class();
                        moduleClass.id = (Guid)reader["id"];
                        moduleClass.name = (string)reader["name"];
                        moduleClass.created_by = (string)reader["created_by"];
                        moduleClass.created_ts = (DateTime)reader["created_ts"];
                        moduleClass.modified_by = (string)reader["modified_by"];
                        moduleClass.modified_ts = (DateTime)reader["modified_ts"];
                        moduleClasses.Add(moduleClass);
                    }
                }
            }
            moduleClasses.SortByName<module_class>();
        }

        //public static void getModules(NpgsqlConnection npgsql)
        //{
        //    modules.Clear();
        //    string query = "select product.id, product.name, module_type.height, module_type.width from product, module_type" +
        //                   " where module_type.id = product.id and is_available = true" +
        //                   " order by product.name, module_type.height, module_type.width";
        //    using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.schema + "'; " + query, npgsql))
        //    {
        //        using (NpgsqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                module module = new module();
        //                module.id = (Guid)reader["id"];
        //                module.name = (string)reader["name"];
        //                module.height = (decimal)reader["height"];
        //                module.width = (decimal)reader["width"];
        //                if (modules.Where(x => x.name.Equals(module.name)).Count() == 0)
        //                modules.Add(module);
        //            }
        //        }
        //    }
        //    List<module> filteredModules = standardLists.modules.Where(m => standardLists.systemTypesAllowed
        //    .Any(s => !string.IsNullOrEmpty(m.name) && !string.IsNullOrEmpty(s.name) && m.name.Trim().StartsWith(s.name.Trim(), StringComparison.OrdinalIgnoreCase)))
        //    .GroupBy(m => m.id).Select(g => g.First()).Distinct().ToList();
        //    List<system_type> reorderedSystemTypes = systemTypesAllowed.OrderByDescending(x => x.name.Length).ToList();
        //    modules.Clear();
        //    foreach (system_type st in reorderedSystemTypes)
        //    {
        //        List<module> partlist = filteredModules.Where(x => x.name.StartsWith(st.name.getModuleNameFromSystemType())).OrderBy(x => x.height).ToList();
        //        foreach (module m in partlist)
        //        {
        //            if (modules.Where(x => x.name.Equals(m.name)).Count() == 0)
        //                modules.Add(m);
        //        }
        //    }
        //}

        public static void getModules(NpgsqlConnection npgsql)
        {
            // Lese alle Module aus der DB
            var allModules = new List<module>();
            string query = "select product.id, product.name, product.article_number, product.weight_kg, module_type.height, module_type.width, is_filler_module, is_oversize_module, max_cable_capacity from product, module_type" +
                           " where module_type.id = product.id and is_available = true" +
                           " order by product.name, module_type.height, module_type.width";
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; " + query, npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        module module = new module();
                        module.id = (Guid)reader["id"];
                        module.name = (string)reader["name"];
                        module.height = (decimal)reader["height"];
                        module.width = (decimal)reader["width"];
                        module.article_number = (string)reader["article_number"];
                        module.weight_kg = (decimal)reader["weight_kg"];
                        module.max_cable_capacity = (int)reader["max_cable_capacity"];
                        module.is_filler_module = (bool)reader["is_filler_module"];
                        module.is_oversize_module = (bool)reader["is_oversize_module"];
                        allModules.Add(module);
                    }
                }
            }

            // Helfer: extrahiere Sortierschlüssel — erste Tokens bestehend aus Großbuchstaben, '-' bzw. Leerzeichen;
            // schließe pure Zahlen aus, aber erlaube Token wie "RR3".
            Func<module, string> extractKey = (m) =>
            {
                var name = (m?.name ?? string.Empty).Trim();
                if (string.IsNullOrEmpty(name)) return string.Empty;

                var tokens = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var parts = new List<string>();
                foreach (var t in tokens)
                {
                    // Token "RR" gefolgt von Ziffern (z.B. "RR3") -> behalten
                    if (System.Text.RegularExpressions.Regex.IsMatch(t, @"^RR\d+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                    {
                        parts.Add(t.ToUpperInvariant());
                        continue;
                    }

                    // Nur Großbuchstaben und '-' zulassen (keine Ziffern)
                    if (System.Text.RegularExpressions.Regex.IsMatch(t, @"^CFS-T(?:\s+(?:RR\d*|[A-Z]+))*\s*$"))
                    {
                        parts.Add(t.ToUpperInvariant());
                        continue;
                    }

                    // Sonst Stopp (nachfolgende Token gehören nicht mehr zum Präfix)
                    break;
                }
                return string.Join(" ", parts);
            };
            modules.Clear();
            modules.AddRange(allModules);
        }

        public static void getFrames(NpgsqlConnection npgsql)
        {
            frames.Clear();
            string query = "select * from frame";
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; " + query, npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        frame f = new frame();
                        f.id = (Guid)reader["id"];
                        f.frame_type_id = (Guid)reader["frame_type_id"];
                        f.material_type_id = (Guid)reader["material_type_id"];
                        f.name = products.Where(x => x.id.Equals(f.id)).FirstOrDefault()?.name + " " + materialTypes.Where(x => x.id == f.material_type_id).FirstOrDefault()?.name;
                        f.article_number = products.Where(x => x.id.Equals(f.id)).FirstOrDefault()?.article_number;
                        f.is_available = products.Where(x => x.id.Equals(f.id)).FirstOrDefault()?.is_available;
                        f.weight_kg = products.Where(x => x.id.Equals(f.id)).FirstOrDefault()?.weight_kg ?? 0;
                        f.rows = (int)reader["rows"];
                        f.columns = (int)reader["columns"];
                        f.geometry_id = (Guid)reader["geometry_id"];
                        f.wedge_quantity = (int)reader["wedge_quantity"];
                        f.holes_horizontal = (int)reader["holes_horizontal"];
                        f.holes_vertical = (int)reader["holes_vertical"];
                        f.offset_horizontal = (decimal)reader["offset_horizontal"];
                        f.offset_vertical = (decimal)reader["offset_vertical"];
                        f.drill_diameter = (decimal)reader["drill_diameter"];
                        f.has_drilled_holes = (bool)reader["has_drilled_holes"];
                        int ord = reader.GetOrdinal("drilling_schema_id");
                        f.drilling_schema_id = reader.IsDBNull(ord) ? (int?)null : reader.GetInt32(ord);
                        ord = reader.GetOrdinal("hole_schema_id");
                        f.hole_schema_id = reader.IsDBNull(ord) ? (int?)null : reader.GetInt32(ord);
                        f.created_by = (string)reader["created_by"];
                        f.created_ts = (DateTime)reader["created_ts"];
                        f.modified_by = (string)reader["modified_by"];
                        f.modified_ts = (DateTime)reader["modified_ts"];
                        frames.Add(f);
                    }
                }
            }
            frames = frames.OrderBy(x => x.name.Substring(6, x.name.IndexOf('-'))).
                            ThenBy(x => standardLists.frameGeometries.Where(y => y.id == x.id).First().h1).
                            ThenBy(x => standardLists.frameWindows.Where(y => y.frame_id == x.id).First().frame_window_height).
                            ThenBy(x => x.rows).ThenBy(x => x.columns).
                            ThenBy(x => standardLists.materialTypes.Where(y => y.id == x.material_type_id).First().name).ToList();
        }

        public static void getFrameGeometries(NpgsqlConnection npgsql)
        {
            frameGeometries.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM frame_geometry", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        frame_geometry fg = new frame_geometry();
                        fg.id = (Guid)reader["id"];
                        fg.h1 = (decimal)reader["h1"];
                        fg.b1 = (decimal)reader["b1"];
                        fg.h2 = (decimal)reader["h2"];
                        fg.b2 = (decimal)reader["b2"];
                        fg.opening_addition_min = (decimal)reader["opening_addition_min"];
                        fg.opening_addition_max = (decimal)reader["opening_addition_max"];
                        fg.flange_type = (string)reader["flange_type"];
                        fg.created_by = (string)reader["created_by"];
                        fg.created_ts = (DateTime)reader["created_ts"];
                        fg.modified_by = (string)reader["modified_by"];
                        fg.modified_ts = (DateTime)reader["modified_ts"];
                        frameGeometries.Add(fg);
                    }
                }
            }
        }

        public static void getFrameWindows(NpgsqlConnection npgsql)
        {
            frameWindows.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM frame_window", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        frame_window fw = new frame_window();
                        fw.id = (Guid)reader["id"];
                        fw.frame_id = (Guid)reader["frame_id"];
                        fw.frame_window_height = (decimal)reader["frame_window_height"];
                        fw.frame_window_width = (decimal)reader["frame_window_width"];
                        fw.frame_window_height_natural = (decimal)reader["frame_window_height_natural"];
                        fw.frame_window_width_natural = (decimal)reader["frame_window_width_natural"];
                        fw.created_by = (string)reader["created_by"];
                        fw.created_ts = (DateTime)reader["created_ts"];
                        fw.modified_by = (string)reader["modified_by"];
                        fw.modified_ts = (DateTime)reader["modified_ts"];
                        frameWindows.Add(fw);
                    }
                }
            }
        }

        public static void getFrameHoleSchemas(NpgsqlConnection npgsql)
        {
            frameHoleSchemas.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM frame_hole_schema", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        frame_hole_schema fhs = new frame_hole_schema();
                        fhs.id = (int)reader["id"];
                        fhs.comment = (string)reader["comment"];
                        fhs.hole_quantity = (int)reader["hole_quantity"];
                        fhs.rotation_angle = (decimal)reader["rotation_angle"];
                        fhs.center_radius = (decimal)reader["center_radius"];
                        fhs.hole_radius = (decimal)reader["hole_radius"];
                        fhs.created_by = (string)reader["created_by"];
                        fhs.created_ts = (DateTime)reader["created_ts"];
                        fhs.modified_by = (string)reader["modified_by"];
                        fhs.modified_ts = (DateTime)reader["modified_ts"];
                        frameHoleSchemas.Add(fhs);
                    }
                }
            }
        }

        public static void getWedgeTypes(NpgsqlConnection npgsql)
        {
            wedgeTypes.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM wedge_type", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        wedge_type wedgeType = new wedge_type();
                        wedgeType.id = (Guid)reader["id"];
                        wedgeType.material_type_id = (Guid)reader["material_type_id"];
                        wedgeType.name = products.Where(x => x.id.Equals(wedgeType.id)).FirstOrDefault()?.name + " " + materialTypes.Where(x => x.id == wedgeType.material_type_id).FirstOrDefault()?.name;
                        wedgeType.width = (decimal)reader["width"];
                        wedgeType.height = (decimal)reader["height"];
                        wedgeType.is_compression_kit = (bool)reader["is_compression_kit"];
                        wedgeType.anchor_quantity = (int)reader["anchor_quantity"];
                        wedgeType.fixing_anchor_quantity = (int)reader["fixing_anchor_quantity"];
                        wedgeType.wedge_description = (string)reader["wedge_description"];
                        wedgeType.created_by = (string)reader["created_by"];
                        wedgeType.created_ts = (DateTime)reader["created_ts"];
                        wedgeType.modified_by = (string)reader["modified_by"];
                        wedgeType.modified_ts = (DateTime)reader["modified_ts"];
                        wedgeTypes.Add(wedgeType);
                    }
                }
            }
        }

        public static void getSleeveTypes(NpgsqlConnection npgsql)
        {
            sleeveTypes.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM sleeve_type", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sleeve_type sleeveType = new sleeve_type();
                        sleeveType.id = (Guid)reader["id"];
                        sleeveType.material_type_id = (Guid)reader["material_type_id"];
                        sleeveType.name = products.Where(x => x.id.Equals(sleeveType.id)).FirstOrDefault()?.name + " " + materialTypes.Where(x => x.id == sleeveType.material_type_id).FirstOrDefault()?.name;
                        sleeveType.has_flange = (bool)reader["has_flange"];
                        sleeveType.installation_tolerance_min = (decimal)reader["installation_tolerance_min"];
                        sleeveType.installation_tolerance_max = (decimal)reader["installation_tolerance_max"];
                        int ord = reader.GetOrdinal("flange_diameter");
                        sleeveType.flange_diameter = reader.IsDBNull(ord)
                            ? (decimal?)null
                            : reader.GetDecimal(ord);
                        sleeveType.outer_pipe_diameter = (decimal)reader["outer_pipe_diameter"];
                        sleeveType.created_by = (string)reader["created_by"];
                        sleeveType.created_ts = (DateTime)reader["created_ts"];
                        sleeveType.modified_by = (string)reader["modified_by"];
                        sleeveType.modified_ts = (DateTime)reader["modified_ts"];
                        sleeveTypes.Add(sleeveType);
                    }
                }
            }
        }

        public static void getStickerTypes(NpgsqlConnection npgsql)
        {
            stickerTypes.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM sticker_type", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sticker_type stickerType = new sticker_type();
                        stickerType.id = (Guid)reader["id"];
                        stickerType.name = products.Where(x => x.id.Equals(stickerType.id)).FirstOrDefault()?.name;
                        stickerType.has_sleeve = (bool)reader["has_sleeve"];
                        stickerType.has_sleeve_flange = (bool)reader["has_sleeve_flange"];
                        stickerType.fw_height = (decimal)reader["fw_height"];
                        stickerType.created_by = (string)reader["created_by"];
                        stickerType.created_ts = (DateTime)reader["created_ts"];
                        stickerType.modified_by = (string)reader["modified_by"];
                        stickerType.modified_ts = (DateTime)reader["modified_ts"];
                        stickerTypes.Add(stickerType);
                    }
                }
            }
        }

        public static void getFwHeights(NpgsqlConnection npgsql)
        {             
            fwHeights.Clear();
            string sql = "select distinct frame_window_height " +
                         "from frame_window, frame, frame_type " +
                         "where frame_type.id = frame.frame_type_id " +
                         "and frame_type.shape = 0 " +
                         "and frame_window.frame_id = frame.id " +
                         "and frame_type.name not like 'STRF'";
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "';" + sql, npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fwHeights.Add((decimal)reader["frame_window_height"]);
                    }
                }
            }
            fwHeights.Add(0);
            fwHeights.Sort();
        }

        public static void getEntityMetaData(NpgsqlConnection npgsql)
        {
            entityMetaData.Clear();
            using (var command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM entity_metadata", npgsql))
            using (var reader = command.ExecuteReader())
            {
                int ordStringValue = reader.GetOrdinal("string_value");
                int ordNumberValue = reader.GetOrdinal("number_value");
                int ordBoolValue = reader.GetOrdinal("bool_value");
                int ordJson = reader.GetOrdinal("json_value");

                while (reader.Read())
                {
                    var r = new entity_metadata();
                    r.id = (Guid)reader["id"];
                    r.entity_id = (Guid)reader["entity_id"];
                    r.entity_type = (string)reader["entity_type"];
                    r.metadata_key = (string)reader["metadata_key"];
                    r.string_value = reader.IsDBNull(ordStringValue) ? null : reader.GetString(ordStringValue);
                    r.number_value = reader.IsDBNull(ordNumberValue) ? (decimal?)null : reader.GetDecimal(ordNumberValue);
                    r.bool_value = reader.IsDBNull(ordBoolValue) ? (bool?)null : (bool)reader["bool_value"];
                    r.json_value = reader.IsDBNull(ordJson) ? null : JsonDocument.Parse((string)reader["json_value"]);
                    r.created_by = (string)reader["created_by"];
                    r.created_ts = (DateTime)reader["created_ts"];
                    r.modified_by = (string)reader["modified_by"];
                    r.modified_ts = (DateTime)reader["modified_ts"];
                    if (r.entity_type == "module_variation")
                        r.entity_name = (from product p in standardLists.products from module_variation mv in standardLists.moduleVariations where mv.id == r.entity_id && p.id == mv.module_type_id select p.name).FirstOrDefault();
                    else if (r.entity_type == "sleeve_is_mandatory")
                        r.entity_name = (from product p in standardLists.products from sleeve_rule fsr in standardLists.sleeveRules where fsr.id == r.entity_id && p.id == fsr.frame_type_id select p.name).FirstOrDefault();
                    else if (r.entity_type == "frame_type")
                        r.entity_name = (from frame_type p in standardLists.frameTypes where p.id == r.entity_id select p.name).FirstOrDefault();
                    else
                        r.entity_name = products.Where(x => x.id.Equals(r.entity_id)).FirstOrDefault()?.name;
                    entityMetaData.Add(r);
                }
            }
            entityMetaData = entityMetaData.OrderBy(x => x.entity_type).ThenBy(x => x.entity_name).ThenBy(x => x.metadata_key).ToList();
        }

        public static void getProductType(NpgsqlConnection npgsql)
        {
            productTypes.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM product_type", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product_type pt = new product_type();
                        pt.id = (Guid)reader["id"];
                        pt.name = (string)reader["name"];
                        pt.created_by = (string)reader["created_by"];
                        pt.created_ts = (DateTime)reader["created_ts"];
                        pt.modified_by = (string)reader["modified_by"];
                        pt.modified_ts = (DateTime)reader["modified_ts"];
                        productTypes.Add(pt);
                    }
                }
            }
            productTypes.SortByName<product_type>();
        }

        public static void getDrillingSchemas(NpgsqlConnection npgsql)
        {
            drillingSchemas.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM drilling_schema", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        drilling_schema ds = new drilling_schema();
                        ds.id = (int)reader["id"];
                        ds.parent_id = (int)reader["parent_id"];
                        ds.hole_diameter = (decimal)reader["hole_diameter"];
                        ds.hole_quantity = (int)reader["hole_quantity"];
                        ds.created_by = (string)reader["created_by"];
                        ds.created_ts = (DateTime)reader["created_ts"];
                        ds.modified_by = (string)reader["modified_by"];
                        ds.modified_ts = (DateTime)reader["modified_ts"];
                        drillingSchemas.Add(ds);
                    }
                }
            }
        }

        public static void getDrillingSchemaAngled(NpgsqlConnection npgsql)
        {
            drillingSchemaAngled.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM drilling_schema_angled", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        drilling_schema_angled dsa = new drilling_schema_angled();
                        dsa.id = (int)reader["id"];
                        int ordComment = reader.GetOrdinal("comment");
                        dsa.comment = reader.IsDBNull(ordComment) ? null : (string)reader["comment"];
                        dsa.offset_x_mm = (decimal)reader["offset_x_mm"];
                        dsa.offset_y_mm = (decimal)reader["offset_y_mm"];
                        dsa.created_by = (string)reader["created_by"];
                        dsa.created_ts = (DateTime)reader["created_ts"];
                        dsa.modified_by = (string)reader["modified_by"];
                        dsa.modified_ts = (DateTime)reader["modified_ts"];
                        drillingSchemaAngled.Add(dsa);
                    }
                }
            }
        }

        public static void getDrillingSchemaRound(NpgsqlConnection npgsql)
        {
            drillingSchemaRound.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM drilling_schema_round", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        drilling_schema_round dsr = new drilling_schema_round();
                        dsr.id = (int)reader["id"];
                        int ord = reader.GetOrdinal("comment");
                        dsr.comment = reader.IsDBNull(ord) ? null : (string)reader["comment"];
                        dsr.rotation_angle = (decimal)reader["rotation_angle"];
                        dsr.radius_mm = (decimal)reader["radius_mm"];
                        dsr.created_by = (string)reader["created_by"];
                        dsr.created_ts = (DateTime)reader["created_ts"];
                        dsr.modified_by = (string)reader["modified_by"];
                        dsr.modified_ts = (DateTime)reader["modified_ts"];
                        drillingSchemaRound.Add(dsr);
                    }
                }
            }
        }

        public static void getModuleVariations(NpgsqlConnection npgsql)
        {
            moduleVariations.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM module_variation", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        module_variation mv = new module_variation();
                        mv.id = (Guid)reader["id"];
                        mv.module_type_id = (Guid)reader["module_type_id"];
                        mv.cable_bundle_min_diameter = (decimal)reader["cable_bundle_min_diameter"];
                        mv.cable_bundle_max_diameter = (decimal)reader["cable_bundle_max_diameter"];
                        mv.created_by = (string)reader["created_by"];
                        mv.created_ts = (DateTime)reader["created_ts"];
                        mv.modified_by = (string)reader["modified_by"];
                        mv.modified_ts = (DateTime)reader["modified_ts"];
                        moduleVariations.Add(mv);
                    }
                }
            }
        }

        public static void getModulePackagings(NpgsqlConnection npgsql)
        {
            modulePackagings.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM module_packaging", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        module_packaging mp = new module_packaging();
                        mp.id = (Guid)reader["id"];
                        mp.module_id = (Guid)reader["module_id"];
                        mp.packaging_unit = (string)reader["packaging_unit"];
                        mp.package = (int)reader["package"];
                        mp.packing_qty = (int)reader["packing_qty"];
                        mp.created_by = (string)reader["created_by"];
                        mp.created_ts = (DateTime)reader["created_ts"];
                        mp.modified_by = (string)reader["modified_by"];
                        mp.modified_ts = (DateTime)reader["modified_ts"];
                        modulePackagings.Add(mp);
                    }
                }
            }
        }
        #endregion =============================================================================

        #region Rules ==========================================================================
        public static void getSystemTypeRules(NpgsqlConnection npgsql)
        {
            baseMaterialSystemTypeRules.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM material_type_rule", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        material_type_rule systemTypeRule = new material_type_rule();
                        systemTypeRule.check = false;
                        systemTypeRule.id = (Guid)reader["id"];
                        systemTypeRule.base_material_id = (Guid)reader["base_material_id"];
                        systemTypeRule.base_material = getBaseMaterialName(systemTypeRule.base_material_id);
                        systemTypeRule.system_type_id = (Guid)reader["system_type_id"];
                        systemTypeRule.system_type = getSystemTypeName(systemTypeRule.system_type_id);
                        systemTypeRule.is_active = (bool)reader["is_active"];
                        systemTypeRule.rule_version = (int)reader["rule_version"];
                        baseMaterialSystemTypeRules.Add(systemTypeRule);
                    }
                }
            }
            baseMaterialSystemTypeRules = baseMaterialSystemTypeRules.OrderBy(x => x.base_material).ThenBy(x => x.system_type).ToList();
        }

        public static void getSystemTypeFrameTypeRules(NpgsqlConnection npgsql)
        {
            systemTypeFrameTypeRules.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM system_type_frame_type_rule", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        system_type_frame_type_rule rule = new system_type_frame_type_rule();
                        rule.check = false;
                        rule.id = (Guid)reader["id"];
                        rule.system_type_id = (Guid)reader["system_type_id"];
                        rule.system_type = getSystemTypeName(rule.system_type_id);
                        rule.frame_type_id = (Guid)reader["frame_type_id"];
                        rule.frame_type = getFrameTypeName(rule.frame_type_id);
                        rule.is_active = (bool)reader["is_active"];
                        rule.rule_version = (int)reader["rule_version"];
                        systemTypeFrameTypeRules.Add(rule);
                    }
                }
            }
            systemTypeFrameTypeRules = systemTypeFrameTypeRules.OrderBy(x => x.system_type).ThenBy(x => x.frame_type).ToList();
        }

        public static void getBMSTFTMTRules(NpgsqlConnection npgsql)
        {
            BMSTFTMT_rules.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM material_type_rule", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BMSTFTMT_rule BMRule = new BMSTFTMT_rule();
                        BMRule.check = false;
                        BMRule.id = (Guid)reader["id"];
                        BMRule.base_material_id = (Guid)reader["base_material_id"];
                        BMRule.base_material = getBaseMaterialName(BMRule.base_material_id);
                        BMRule.system_type_id = (Guid)reader["system_type_id"];
                        BMRule.system_type = getSystemTypeName(BMRule.system_type_id);
                        BMRule.frame_type_id = (Guid)reader["frame_type_id"];
                        BMRule.frame_type = getFrameTypeName(BMRule.frame_type_id);
                        BMRule.material_type_id = (Guid)reader["material_type_id"];
                        BMRule.material_type = getMaterialTypeName(BMRule.material_type_id);
                        BMRule.is_active = (bool)reader["is_active"];
                        BMRule.rule_version = (int)reader["rule_version"];
                        BMSTFTMT_rules.Add(BMRule);
                    }
                }
            }
            BMSTFTMT_rules = BMSTFTMT_rules.OrderBy(x => x.base_material).ThenBy(x => x.system_type).ThenBy(x => x.frame_type).ThenBy(x => x.material_type).ToList();
        }

        public static void getFrameTypeRules(NpgsqlConnection npgsql)
        {
            frameTypeRules.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM frame_type_rule", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        frame_type_rule frameTypeRule = new frame_type_rule();
                        frameTypeRule.check = false;
                        frameTypeRule.id = (Guid)reader["id"];
                        frameTypeRule.base_material_id = (Guid)reader["base_material_id"];
                        frameTypeRule.base_material = getBaseMaterialName(frameTypeRule.base_material_id);
                        frameTypeRule.system_type_id = (Guid)reader["system_type_id"];
                        frameTypeRule.system_type = getSystemTypeName(frameTypeRule.system_type_id);
                        frameTypeRule.frame_type_id = (Guid)reader["frame_type_id"];
                        frameTypeRule.frame_type = getFrameTypeName(frameTypeRule.frame_type_id);
                        frameTypeRule.is_active = (bool)reader["is_active"];
                        frameTypeRule.rule_version = (int)reader["rule_version"];
                        frameTypeRules.Add(frameTypeRule);
                    }
                }
            }
            frameTypeRules = frameTypeRules.OrderBy(x => x.base_material).ThenBy(x => x.system_type).ThenBy(x => x.frame_type).ToList();
        }

        public static void getModuleRules(NpgsqlConnection npgsql)
        {
            moduleRules.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM module_rule", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        module_rule moduleRule = new module_rule();
                        moduleRule.check = false;
                        moduleRule.id = (Guid)reader["id"];
                        moduleRule.system_type_id = (Guid)reader["system_type_id"];
                        moduleRule.system_type = getSystemTypeName(moduleRule.system_type_id);
                        moduleRule.frame_type_id = (Guid)reader["frame_type_id"];
                        moduleRule.frame_type = getFrameTypeName(moduleRule.frame_type_id);
                        moduleRule.module_class_id = (Guid)reader["module_class_id"];
                        moduleRule.module_class = getModuleClassName(moduleRule.module_class_id);
                        moduleRule.is_active = (bool)reader["is_active"];
                        moduleRule.rule_version = (int)reader["rule_version"];
                        moduleRules.Add(moduleRule);
                    }
                }
            }
            moduleRules = moduleRules.OrderBy(x => x.system_type).ThenBy(x => x.frame_type).ThenBy(x => x.module_class).ToList();
        }

        public static void getSleeveRules(NpgsqlConnection npgsql)
        {
            sleeveRules.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM sleeve_rule", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sleeve_rule rule = new sleeve_rule();
                        rule.id = (Guid)reader["id"];
                        rule.base_material_id = (Guid)reader["base_material_id"];
                        rule.base_material = getBaseMaterialName(rule.base_material_id);
                        rule.system_type_id = (Guid)reader["system_type_id"];
                        rule.system_type = getSystemTypeName(rule.system_type_id);
                        rule.frame_type_id = (Guid)reader["frame_type_id"];
                        rule.frame_type = getFrameTypeName(rule.frame_type_id);
                        rule.rule_version = (int)reader["rule_version"];
                        rule.is_active = (bool)reader["is_active"];
                        rule.is_mandatory = (bool)reader["is_mandatory"];
                        sleeveRules.Add(rule);
                    }
                }
            }
            List<sleeve_rule> sortedSleeveRules = sleeveRules.Where(x => x.base_material.Equals("Concrete")).OrderBy(x => x.system_type).ThenBy(x => x.frame_type).ToList();
            sortedSleeveRules.AddRange(sleeveRules.Where(x => x.base_material.Equals("Steel")).OrderBy(x => x.system_type).ThenBy(x => x.frame_type).ToList());
            sortedSleeveRules.AddRange(sleeveRules.Where(x => x.base_material.Equals("Cabinet seal")).OrderBy(x => x.system_type).ThenBy(x => x.frame_type).ToList());
            sleeveRules = sortedSleeveRules;
        }

        public static void getFrameSleeveRules(NpgsqlConnection npgsql)
        {
            frameSleeveRules.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM frame_sleeve_rule", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        frame_sleeve_rule rule = new frame_sleeve_rule();
                        rule.id = (Guid)reader["id"];
                        rule.frame_id = (Guid)reader["frame_id"];
                        frame fr = standardLists.frames.FirstOrDefault(x => x.id.Equals(rule.frame_id));
                        material_type mt = standardLists.materialTypes.FirstOrDefault(x => x.id.Equals(fr.material_type_id));
                        rule.frame_name = fr.name;
                        rule.sleeve_id = (Guid)reader["sleeve_type_id"];
                        rule.sleeve_name = products.Where(x => x.id.Equals(rule.sleeve_id)).FirstOrDefault()?.name;
                        rule.rule_version = (int)reader["rule_version"];
                        rule.is_active = (bool)reader["is_active"];
                        frameSleeveRules.Add(rule);
                    }
                }
            }
            frameSleeveRules = frameSleeveRules.OrderBy(x => x.frame_name.Split(new char[] { '-' })[1]).ThenBy(x => int.Parse(Regex.Match(x.frame_name, @"\d+").Value)).ThenBy(x => x.frame_name).ThenBy(x => x.sleeve_name).ToList();
        }

        public static void getFrameStickerRules(NpgsqlConnection npgsql)
        {
            frameStickerRules.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM frame_sticker_rule", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        frame_sticker_rule rule = new frame_sticker_rule();
                        rule.id = (Guid)reader["id"];
                        rule.system_type_id = (Guid)reader["system_type_id"];
                        rule.system_type = getSystemTypeName(rule.system_type_id);
                        rule.frame_type_id = (Guid)reader["frame_type_id"];
                        rule.frame_type = products.Where(x => x.id.Equals(rule.frame_type_id)).FirstOrDefault()?.name;
                        rule.fw_height = (decimal)reader["fw_height"];
                        rule.sticker_type_id = (Guid)reader["sticker_type_id"];
                        rule.sticker_type = products.Where(x => x.id.Equals(rule.sticker_type_id)).FirstOrDefault()?.name;
                        rule.rule_version = (int)reader["rule_version"];
                        rule.is_active = (bool)reader["is_active"];
                    }
                }
            }
            frameStickerRules = frameStickerRules.OrderBy(x => x.system_type).ThenBy(x => x.frame_type).ThenBy(x => x.fw_height).ThenBy(x => x.sticker_type).ToList();
        }

        public static void getSleeveStickerRules(NpgsqlConnection npgsql)
        {
            sleeveStickerRules.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM sleeve_sticker_rule", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sleeve_sticker_rule rule = new sleeve_sticker_rule();
                        rule.id = (Guid)reader["id"];
                        rule.system_type_id = (Guid)reader["system_type_id"];
                        rule.system_type = getSystemTypeName(rule.system_type_id);
                        rule.sleeve_type_id = (Guid)reader["sleeve_type_id"];
                        rule.sleeve_type = products.Where(x => x.id.Equals(rule.sleeve_type_id)).FirstOrDefault()?.name;
                        rule.sticker_type_id = (Guid)reader["frame_id"];
                        rule.sticker_type = products.Where(x => x.id.Equals(rule.sticker_type_id    )).FirstOrDefault()?.name;
                        rule.sleeve_type_id = (Guid)reader["sleeve_type_id"];
                        rule.sleeve_type = products.Where(x => x.id.Equals(rule.sleeve_type_id)).FirstOrDefault()?.name;
                        rule.rule_version = (int)reader["rule_version"];
                        rule.is_active = (bool)reader["is_active"];
                    }
                }
            }
            sleeveStickerRules = sleeveStickerRules.OrderBy(x => x.system_type).ThenBy(x => x.sleeve_type).ThenBy(x => x.sticker_type).ToList();
        }

        public static void getWedgeRules(NpgsqlConnection npgsql)
        {
            // Implementierung ähnlich zu den anderen Regeltypen, z.B.:
            wedgeRules.Clear();
            using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; SELECT * FROM wedge_rule", npgsql))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        wedge_rule rule = new wedge_rule();
                        rule.id = (Guid)reader["id"];
                        rule.system_type_id = (Guid)reader["system_type_id"];
                        rule.system_type = getSystemTypeName(rule.system_type_id);
                        rule.wedge_type_id = (Guid)reader["wedge_type_id"];
                        rule.wedge_type = products.Where(x => x.id.Equals(rule.wedge_type_id)).FirstOrDefault()?.name;
                        rule.rule_version = (int)reader["rule_version"];
                        rule.is_active = (bool)reader["is_active"];
                        wedgeRules.Add(rule);
                    }
                }
            }
            wedgeRules = wedgeRules.OrderBy(x => x.system_type).ThenBy(x => x.wedge_type).ToList();
        }
        #endregion
    }
}
