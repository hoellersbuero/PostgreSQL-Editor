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

namespace PostgreSQL_Editor.FunctionViews
{
    public partial class getModuleFromId : Form
    {
        public getModuleFromId()
        {
            InitializeComponent();
        }

        public static void Execute(Form owner)
        {
            using (var form = new getModuleFromId())
            {
                form.ShowDialog(owner);
            }
        }

        private void getModuleFromId_Load(object sender, EventArgs e)
        {
            dgv.AutoGenerateColumns = false;
            cbModule.DataSource = standardLists.modules;
            cbModule.DisplayMember = "name";
            cbModule.ValueMember = "id";
            cbModule.SelectedIndex = 0;
            // dgv.DataSource = null;
        }

        // id        db35c0be-5722-4200-8904-12cdeeb4fbfa
        private void cbModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            module m = cbModule.SelectedItem as module;
            Module mm = GetElements.getModuleFromId(m.id);
            lbId.Text = m.id.ToString();
            lbName.Text = m.name;
            lbArticleNumber.Text = m.article_number;
            lbHeight.Text = m.height.ToString();
            lbWidth.Text = m.width.ToString();
            lbWeight.Text = m.weight_kg.ToString();
            lbFiller.Text = m.is_filler_module ? "Yes" : "No";
            lbOversized.Text = m.is_oversize_module ? "Yes" : "No";
            lbCables.Text = m.max_cable_capacity.ToString();
            lbPackaging.Text = mm.packagingInfo;
            if (mm != null && !mm.id.Equals(Guid.Empty))
            {
                lbHoleFrom.Text = mm.holeDiameterFrom.ToString();
                lbHoleTo.Text = mm.holeDiameterTo.ToString();
            }
            else
            {
                lbHoleFrom.Text = "??";
                lbHoleTo.Text = "??";
            }
            if (mm.variations != null && mm.variations.Count > 0)
            {
                lbVar.Text = $"Variations ({mm.variations.Count}):";
                dgv.Visible = true;
                dgv.DataSource = null;
                dgv.DataSource = mm.variations;
            }
            else
            {
                lbVar.Text = "No variations.";
                dgv.DataSource = null;
                dgv.Visible = false;
            }
            Application.DoEvents();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lbId_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(lbId.Text);
        }

        private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                e.Graphics.DrawRectangle(new Pen(dgv.GridColor), new Rectangle(e.CellBounds.X - 1, e.CellBounds.Y - 1, e.CellBounds.Width, e.CellBounds.Height));
                var c = (int)((dgv.Rows[e.RowIndex].DataBoundItem as Variation).color);
                Brush colorBrush = new SolidBrush(Color.FromArgb(c));
                //if ((Brushes.Transparent as SolidBrush).Color.ToArgb() == c)
                //    colorBrush = Brushes.White;
                e.Graphics.FillRectangle(Brushes.White, new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width - 1, e.CellBounds.Height - 1));
                e.Graphics.FillRectangle(Brushes.Black, new Rectangle(e.CellBounds.X + 1, e.CellBounds.Y + 1, e.CellBounds.Width - 3, e.CellBounds.Height - 3));
                e.Graphics.FillRectangle(colorBrush, new Rectangle(e.CellBounds.X + 2, e.CellBounds.Y + 2, e.CellBounds.Width - 5, e.CellBounds.Height - 5));
                e.Handled = true;
            }
            else
                e.Handled = false;
        }
    }
}
