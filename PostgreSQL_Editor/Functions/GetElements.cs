using PostgreSQL_Editor.Models;
using PostgreSQL_Editor.Global;
using PostgreSQL_Editor.DBUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PostgreSQL_Editor.Functions
{
    public class GetElements
    {
        public static Module getModuleFromId(Guid id)
        {
            Module m = new Module();
            List<entity_metadata> data = standardLists.entityMetaData.Where(mv => mv.entity_id == id).ToList();
            module_packaging mp = standardLists.modulePackagings.Where(x => x.module_id == id).FirstOrDefault();
            if (data != null && data.Count() > 0)
            {
                decimal minHoleDiameter = (decimal)data.Where(x => x.metadata_key.Equals("hole_diameter_from")).Min(mv => mv.number_value);
                decimal maxHoleDiameter = (decimal)data.Where(x => x.metadata_key.Equals("hole_diameter_to")).Max(mv => mv.number_value);
                m.holeDiameterFrom = minHoleDiameter;
                m.holeDiameterTo = maxHoleDiameter;
                m.id = id;
                module mm = standardLists.modules.Where(x => x.id == id).FirstOrDefault();
                if (mm != null)
                {
                    m.name = mm.name;
                    m.article_number = mm.article_number;
                    m.is_available = mm.is_available;
                    m.weight_kg = mm.weight_kg;
                    m.is_filler = mm.is_filler_module;
                    m.is_oversize_module = mm.is_oversize_module;
                    m.max_cable_capacity = mm.max_cable_capacity;
                    m.GroupCode = (int)data.Where(x => x.metadata_key.Equals("group_code")).FirstOrDefault()?.number_value;
                    m.PackingQty = mp.packing_qty;
                    m.Package = mp.package;
                    m.PackagingUnit = mp.packaging_unit;
                }
            }
            List<module_variation> variations = standardLists.moduleVariations.Where(x => x.module_type_id == id).OrderBy(x => x.cable_bundle_min_diameter).ToList();
            if (variations != null && variations.Count() > 0)
            {
                m.variations = new List<Variation>();
                int lfnr = 1;
                foreach (module_variation mv in variations)
                {
                    entity_metadata md = standardLists.entityMetaData.Where(x => x.entity_id == mv.id).FirstOrDefault();
                    Variation v = new Variation() { lfnr = lfnr++, id = md.id, color = (decimal)md.number_value, holeDiameterFrom = mv.cable_bundle_min_diameter, holeDiameterTo = mv.cable_bundle_max_diameter };
                    if (v.color == Color.Transparent.ToArgb())
                        v.color = Color.White.ToArgb();
                    m.variations.Add(v);
                }
            }
            return m;
        }

        public static Frame getFrameFromId(Guid id)
        {
            Frame f = new Frame();
            frame frame = standardLists.frames.Where(x => x.id == id).FirstOrDefault();
            if (frame != null)
            {
                f.id = id;
                f.name = frame.name;
                f.article_number = frame.article_number;
                f.weight_kg = frame.weight_kg;
                f.is_available = frame.is_available;
                f.columns = frame.columns;
                f.rows = frame.rows;
                f.wedge_quantity = frame.wedge_quantity;
                f.frame_type_id = frame.frame_type_id;
                f.material_type_id = frame.material_type_id;
                f.geometry_id = f.id;
                f.drill_diameter = frame.drill_diameter;
                f.drilling_schema_id = frame.drilling_schema_id;
                f.hole_schema_id = frame.hole_schema_id;
                f.holes_horizontal = frame.holes_horizontal;
                f.holes_vertical = frame.holes_vertical;
                f.has_drilled_holes = frame.has_drilled_holes;
                f.hole_offset_hor = frame.offset_horizontal;
                f.hole_offset_ver = frame.offset_vertical;
                f.created_by = frame.created_by;
                f.created_ts = frame.created_ts;
                f.modified_by = frame.modified_by;
                f.modified_ts = frame.modified_ts;
            }
            List<entity_metadata> data = standardLists.entityMetaData.Where(mv => mv.entity_id == id).ToList();
            if (data != null && data.Count() > 0)
            {
                f.is_selectable = (bool)data.Where(x => x.metadata_key.Equals("isSelectable")).FirstOrDefault()?.bool_value;
            }
            return f;
        }
    }
}
