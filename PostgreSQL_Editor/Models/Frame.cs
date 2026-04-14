using System;
using System.Linq;
using PostgreSQL_Editor.Global;
using PostgreSQL_Editor.DBUtils;

namespace PostgreSQL_Editor.Models
{
    public class Frame : product
    {
        public bool is_selectable { get; set; }
        public Guid frame_type_id { get; set; }
        public Guid material_type_id { get; set; }
        public Guid geometry_id { get; set; }
        public int rows { get; set; }
        public int columns { get; set; }
        public Guid wedge_type_id { get; set; }
        public int wedge_quantity { get; set; }
        public int holes_horizontal { get; set; }
        public int holes_vertical { get; set; }
        public decimal drill_diameter { get; set; }
        public decimal SpecialFlangeAddition { get; set; }
        public decimal FlangeAdd_top { get; set; }
        public decimal FlangeAdd_bottom { get; set; }
        public decimal hole_offset_hor { get; set; }
        public decimal hole_offset_ver { get; set; }
        public bool has_drilled_holes { get; set; }
        public int? drilling_schema_id { get; set; }
        public int? hole_schema_id { get; set; }

        public frame_type getFrameType()
        {
            return standardLists.frameTypes.Where(x => x.id == frame_type_id).FirstOrDefault();
        }

        public material_type getMaterialType()
        {
            return standardLists.materialTypes.Where(x => x.id == material_type_id).FirstOrDefault();
        }

        public frame_geometry getFrameGeometry()
        {
            return standardLists.frameGeometries.Where(x => x.id == geometry_id).FirstOrDefault();
        }

        public frame_window getFrameWindow()
        {
            return standardLists.frameWindows.Where(x => x.frame_id == id).FirstOrDefault();
        }

        public wedge_type getWedgeType()
        {
            return standardLists.wedgeTypes.Where(x => x.id == wedge_type_id).FirstOrDefault();
        }

        public drilling_schema getDrillingSchema()
        {
            if (drilling_schema_id == null)
                return null;
            return standardLists.drillingSchemas.Where(x => x.id == drilling_schema_id).FirstOrDefault();
        }

        public object getDrillingSchemaAR()
        {
            if (drilling_schema_id == null)
                return null;
            drilling_schema_angled drsa = standardLists.drillingSchemaAngled.Where(x => x.id == drilling_schema_id).FirstOrDefault();
            drilling_schema_round drsr = standardLists.drillingSchemaRound.Where(x => x.id == drilling_schema_id).FirstOrDefault();
            if (drsa != null)
                return drsa;
            else if (drsr != null)
                return drsr;
            else
                return null;
        }

        public frame_hole_schema getHoleSchema()
        {
            if (hole_schema_id == null)
                return null;
            return standardLists.frameHoleSchemas.Where(x => x.id == hole_schema_id).FirstOrDefault();
        }
    }
}
