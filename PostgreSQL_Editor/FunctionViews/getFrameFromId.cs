using PostgreSQL_Editor.Functions;
using PostgreSQL_Editor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace PostgreSQL_Editor.FunctionViews
{
    public partial class getFrameFromId : Form
    {
        private bool expanded0 = false;
        private bool expanded1 = false;
        private bool expanded2 = false;
        private bool expanded3 = false;
        private bool expanded4 = false;
        private Frame f = null;

        public getFrameFromId()
        {
            InitializeComponent();
        }

        public static void Execute(Form owner)
        {
            using (var form = new getFrameFromId())
            {
                form.ShowDialog(owner);
            }
        }

        private void getFrameFromId_Load(object sender, EventArgs e)
        {
            cbFrame.DataSource = standardLists.frames;
            cbFrame.DisplayMember = "name";
            cbFrame.ValueMember = "id";
            cbFrame.SelectedIndex = 0;
            panel1.Height = 0;
            panel2.Height = 0;
            panel3.Height = 0;
            panel4.Height = 0;
        }

        private void cbFrame_SelectedIndexChanged(object sender, EventArgs e)
        {
            f = GetElements.getFrameFromId((Guid)(cbFrame.SelectedItem as frame).id);
            if (f != null)
            {
                lbId.Text = f.id.ToString();
                lbName.Text = f.name;
                lbArticleNumber.Text = f.article_number;
                lbWeight.Text = f.weight_kg.ToString();
                lbIsAvailable.Text = f.is_available.ToString();
                lbIsSelectable.Text = f.is_selectable.ToString();
                frame_type ft = f.getFrameType();
                btnUpDn0.Enabled = ft != null;
                lbFrameType.Text = ft?.name;
                lbShape.Text = ft?.shape.ToString();
                lbSupSeaL.Text = ft?.supports_sealant.ToString();
                lbBarWidthInner.Text = ft?.bar_width_inner.ToString();
                lbBarWidthOuter.Text = ft?.bar_width_outer.ToString();
                lbFlangeExtHor.Text = ft?.flange_extension_horizontal.ToString();
                lbFlangeExtVer.Text = ft?.flange_extension_vertical.ToString();
                lbEdgeRadInner.Text = ft?.edge_radius_inner.ToString();
                lbEdgeRadFitting.Text = ft?.edge_radius_fitting.ToString();
                material_type mat = f.getMaterialType();
                lbMaterialType.Text = mat?.name;
                frame_geometry fg = f.getFrameGeometry();
                btnUpDn1.Enabled = fg != null;
                lbFlangeType.Text = fg?.flange_type;
                lbH1.Text = fg?.h1.ToString();
                lbB1.Text = fg?.b1.ToString();
                lbH2.Text = fg?.h2.ToString();
                lbB2.Text = fg?.b2.ToString();
                lbOpenAddMin.Text = fg?.opening_addition_min.ToString();
                lbOpenAddMax.Text = fg?.opening_addition_max.ToString();
                lbRows.Text = f.rows.ToString();
                lbColumns.Text = f.columns.ToString();
                wedge_type wt = f.getWedgeType();
                lbWedge.Text = wt?.name;
                lbWedgeQuantity.Text = f.wedge_quantity.ToString();
                frame_window fw = f.getFrameWindow();
                btnUpDn2.Enabled = fw != null;
                lbFwHeight.Text = fw?.frame_window_height.ToString();
                lbFwWidth.Text = fw?.frame_window_width.ToString();
                lbFwHeightNat.Text = fw?.frame_window_height_natural.ToString();
                lbFwWidthNat.Text = fw?.frame_window_width_natural.ToString();
                drilling_schema ds = f.getDrillingSchema();
                btnUpDn3.Enabled = ds != null;
                lbParentId.Text = ds?.parent_id.ToString() ?? "null";
                lbHoleQuantity.Text = ds?.hole_quantity.ToString() ?? "null";
                lbHoleDiamter.Text = ds?.hole_diameter.ToString() ?? "null";
                object dsar = f.getDrillingSchemaAR();
                if (dsar != null && dsar is drilling_schema_angled)
                {
                    lbSchema.Text = "Angled";
                    lbDs1.Text = "Offset X [mm]:";
                    lbDs1Txt.Text = (dsar as drilling_schema_angled).offset_x_mm.ToString();
                    lbDs2.Text = "Offset Y [mm]:";
                    lbDs2Txt.Text = (dsar as drilling_schema_angled).offset_y_mm.ToString();
                }
                if (dsar != null && dsar is drilling_schema_round)
                {
                    lbSchema.Text = "Round";
                    lbDs1.Text = "Rotation Angle [°]:";
                    lbDs1Txt.Text = (dsar as drilling_schema_round).rotation_angle.ToString();
                    lbDs2.Text = "Radius [mm]:";
                    lbDs2Txt.Text = (dsar as drilling_schema_round).radius_mm.ToString();
                }
                frame_hole_schema fhs = f.getHoleSchema();
                btnUpDn4.Enabled = fhs != null;
                if (fhs != null)
                {
                    lbHoleComment.Text = fhs.comment;
                    lbHoleCenterRadius.Text = fhs.center_radius.ToString();
                    lbHoleRotationAngle.Text = fhs.rotation_angle.ToString();
                    lbFrameHoleQuantity.Text = fhs.hole_quantity.ToString();
                    lbFrameHoleRadius.Text = fhs.hole_radius.ToString();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void btnUpDn0_Click(object sender, EventArgs e)
        {
            if (expanded0)
            {
                panel0.Height = 0;
                btnUpDn0.Image = Properties.Resources.CharetDown;
            }
            else
            {
                panel1.Height = 0;
                expanded1 = false;
                btnUpDn1.Image = Properties.Resources.CharetDown;
                panel2.Height = 0;
                expanded2 = false;
                btnUpDn2.Image = Properties.Resources.CharetDown;
                panel3.Height = 0;
                expanded3 = false;
                btnUpDn3.Image = Properties.Resources.CharetDown;
                panel0.Height = 140;
                btnUpDn0.Image = Properties.Resources.CharetUp;
            }
            expanded0 = !expanded0;
        }

        private void btnUpDn1_Click(object sender, EventArgs e)
        {
            if (expanded1)
            {
                panel1.Height = 0;
                btnUpDn1.Image = Properties.Resources.CharetDown;
            }
            else
            {
                panel0.Height = 0;
                expanded0 = false;
                btnUpDn0.Image = Properties.Resources.CharetDown;
                panel2.Height = 0;
                expanded2 = false;
                btnUpDn2.Image = Properties.Resources.CharetDown;
                panel3.Height = 0;
                expanded3 = false;
                btnUpDn3.Image = Properties.Resources.CharetDown;
                panel4.Height = 0;
                expanded4 = false;
                btnUpDn4.Image = Properties.Resources.CharetDown;
                panel1.Height = 110;
                btnUpDn1.Image = Properties.Resources.CharetUp;
            }
            expanded1 = !expanded1;
        }

        private void btnUpDown2_Click(object sender, EventArgs e)
        {
            if (expanded2)
            {
                panel2.Height = 0;
                btnUpDn2.Image = Properties.Resources.CharetDown;
            }
            else
            {
                panel0.Height = 0;
                expanded0 = false;
                btnUpDn0.Image = Properties.Resources.CharetDown;
                panel1.Height = 0;
                expanded1 = false;
                btnUpDn1.Image = Properties.Resources.CharetDown;
                panel3.Height = 0;
                expanded3 = false;
                btnUpDn3.Image = Properties.Resources.CharetDown;
                panel4.Height = 0;
                expanded4 = false;
                btnUpDn4.Image = Properties.Resources.CharetDown;
                panel2.Height = 65;
                btnUpDn2.Image = Properties.Resources.CharetUp;
            }
            expanded2 = !expanded2;
        }

        private void btnUpDown3_Click(object sender, EventArgs e)
        {
            if (expanded3)
            {
                panel3.Height = 0;
                btnUpDn3.Image = Properties.Resources.CharetDown;
            }
            else
            {
                panel0.Height = 0;
                expanded0 = false;
                btnUpDn0.Image = Properties.Resources.CharetDown;
                panel1.Height = 0;
                expanded1 = false;
                btnUpDn1.Image = Properties.Resources.CharetDown;
                panel2.Height = 0;
                expanded2 = false;
                btnUpDn2.Image = Properties.Resources.CharetDown;
                panel4.Height = 0;
                expanded4 = false;
                btnUpDn4.Image = Properties.Resources.CharetDown;
                panel3.Height = 90;
                btnUpDn3.Image = Properties.Resources.CharetUp;
            }
            expanded3 = !expanded3;
        }

        private void btnUpDn4_Click(object sender, EventArgs e)
        {
            if (expanded4)
            {
                panel4.Height = 0;
                btnUpDn4.Image = Properties.Resources.CharetDown;
            }
            else
            {
                panel0.Height = 0;
                expanded0 = false;
                btnUpDn0.Image = Properties.Resources.CharetDown;
                panel1.Height = 0;
                expanded1 = false;
                btnUpDn1.Image = Properties.Resources.CharetDown;
                panel2.Height = 0;
                expanded2 = false;
                btnUpDn2.Image = Properties.Resources.CharetDown;
                panel3.Height = 0;
                expanded3 = false;
                btnUpDn3.Image = Properties.Resources.CharetDown;
                panel4.Height = 90;
                btnUpDn4.Image = Properties.Resources.CharetUp;
            }
            expanded4 = !expanded4;
        }

        private void btnJson_Click(object sender, EventArgs e)
        {
            string json = JsonSerializer.Serialize(f, new JsonSerializerOptions { WriteIndented = true });
            Clipboard.SetText(json);
            MessageBox.Show("Frame data has been serialized to JSON and copied to clipboard.", "JSON Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Frame nf = JsonSerializer.Deserialize<Frame>(json);
        }

    }
}
