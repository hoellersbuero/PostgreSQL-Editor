using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PostgreSQL_Editor.Global;
using PostgreSQL_Editor.DBUtils;

namespace PostgreSQL_Editor.EditRules
{
    public partial class editEntityMetaData : Form
    {
        NpgsqlConnection npgsql;
        private List<entity_metadata> entityData = new List<entity_metadata>();

        public editEntityMetaData()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        public static void Execute(Form owner, NpgsqlConnection npgsql)
        {
            using (var form = new editEntityMetaData())
            {
                form.npgsql = npgsql;
                form.ShowDialog(owner);
            }
        }

        private void editEntityMetaData_Load(object sender, EventArgs e)
        {
            entityData = standardLists.entityMetaData;
            dgv.DataSource = null;
            dgv.DataSource = entityData;
        }
    }
}
