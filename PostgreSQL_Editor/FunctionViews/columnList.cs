using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgreSQL_Editor.FunctionViews
{
    public partial class columnList : Form
    {
        private static columnList form = null;
        private static Form owner = null;

        public columnList()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        public static columnList Execute(Form owner, List<tableItem> columns)
        {
            form = new columnList();
            form.Owner = owner;
            form.dgv.DataSource = columns;
            form.Show(owner);
            form.dgv.Invalidate();
            form.Width = form.dgv.PreferredSize.Width + 30;
            form.Location = new Point(owner.Left + owner.Width + 20, owner.Top);
            return form;
        }

        public void loadList(List<tableItem> columns)
        {
            form.dgv.DataSource = null;
            form.dgv.DataSource = columns;
            form.dgv.Invalidate();
            form.Width = form.dgv.PreferredSize.Width + 30;
            form.Location = new Point(owner.Left + owner.Width + 20, owner.Top);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
