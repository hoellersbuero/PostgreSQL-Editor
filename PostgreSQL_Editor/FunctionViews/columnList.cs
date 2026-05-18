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
    public partial class columnList : Form
    {
        public columnList()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        public static void Execute(Form owner, List<tableItem> columns)
        {
            using (var form = new columnList())
            {
                form.dgv.DataSource = columns;
                form.ShowDialog(owner);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
