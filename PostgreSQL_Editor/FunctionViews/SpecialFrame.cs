using Npgsql; // Falls noch nicht vorhanden
using PostgreSQL_Editor.DBUtils;
using PostgreSQL_Editor.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Deployment.Application;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace PostgreSQL_Editor.FunctionViews
{
    public partial class SpecialFrame : Form
    {
        private readonly NpgsqlConnection _npgsql;

        public SpecialFrame(NpgsqlConnection npgsql)
        {
            _npgsql = npgsql ?? throw new ArgumentNullException(nameof(npgsql));
            InitializeComponent();
        }

        public static void Execute(Form owner, NpgsqlConnection npgsql)
        {
            SpecialFrame specialFrame = new SpecialFrame(npgsql);
            specialFrame.ShowDialog(owner);
        }

        private void SpecialFrame_Load(object sender, EventArgs e)
        {
            dgvComplete.RowHeadersVisible = false;
            getCompleteData();
            cbBasematerial.DataSource = standardLists.baseMaterials;
            cbBasematerial.DisplayMember = "visiblename";
            cbBasematerial.SelectedIndex = 0;
            cbSystemtype.DataSource = standardLists.systemTypes;
            cbSystemtype.DisplayMember = "name";
            cbSystemtype.SelectedIndex = 0;
            dgv.AutoGenerateColumns = true;
            dgv.DataBindingComplete += Dgv_DataBindingComplete;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void getCompleteData()
        {
            DataTable table = DbUtils.getCompleteSpecialFrames(_npgsql);
            dgvComplete.DataSource = table;
            dgvComplete.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void cbBasematerial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBasematerial.SelectedValue == null || cbSystemtype.SelectedValue == null) return;
            var table = DbUtils.getSpecialFrame(_npgsql, (cbBasematerial.SelectedValue as base_material).id.ToString(), (cbSystemtype.SelectedValue as system_type).id.ToString());
            var vm = table.ToViewModel();

            dgv.SuspendLayout();
            dgv.AutoGenerateColumns = true;
            dgv.DataSource = new BindingSource { DataSource = vm };
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.ResumeLayout();

            string s = DbUtils.getSpecialFrameJson(_npgsql, (cbBasematerial.SelectedValue as base_material).id.ToString(), (cbSystemtype.SelectedValue as system_type).id.ToString());
            tbJSON.Text = s;
        }

        private void cbSystemtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBasematerial.SelectedValue == null || cbSystemtype.SelectedValue == null) return;
            var table = DbUtils.getSpecialFrame(_npgsql, (cbBasematerial.SelectedValue as base_material).id.ToString(), (cbSystemtype.SelectedValue as system_type).id.ToString());
            var vm = table.ToViewModel();

            dgv.SuspendLayout();
            dgv.AutoGenerateColumns = true;
            dgv.DataSource = new BindingSource { DataSource = vm };
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.ResumeLayout();

            string s = DbUtils.getSpecialFrameJson(_npgsql, (cbBasematerial.SelectedValue as base_material).id.ToString(), (cbSystemtype.SelectedValue as system_type).id.ToString());
            tbJSON.Text = s;
        }

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var prop = dgv.Columns[e.ColumnIndex].DataPropertyName;
            if (prop == "BaseMaterial" && e.Value is string bm) { e.Value = (e.Value.ToString()).BaseMaterialName(); }
            if (prop == "FlangeOptions" && e.Value is List<string> fl) { e.Value = string.Join(", ", fl); e.FormattingApplied = true; }
            if (prop == "FwSizes" && e.Value is List<int> fs) { e.Value = string.Join(", ", fs); e.FormattingApplied = true; }
            if (prop == "WedgeApMaterials" && e.Value is List<string> wa) { e.Value = string.Join(", ", wa); e.FormattingApplied = true; }
        }

        private void Dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var grid = (DataGridView)sender;

            // Property-/Spaltennamen, die ausgeblendet werden sollen (DataPropertyName)
            var hide = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "BaseMaterialId",
                "SystemTypeId",
                "FrameTypeId"
                // weitere Namen hinzufügen
            };

            // sichere Kopie, da wir Columns verändern
            var cols = grid.Columns.Cast<DataGridViewColumn>().ToList();
            foreach (var col in cols)
            {
                if (hide.Contains(col.DataPropertyName) || hide.Contains(col.Name))
                {
                    col.Visible = false;
                    // alternativ: grid.Columns.Remove(col);
                }
            }

            grid.AutoResizeColumns();
        }
    }

    public class SpecialFrameConfigurationDto
    {
        public Guid BaseMaterialId { get; set; }
        public string BaseMaterial { get; set; } = string.Empty;

        public Guid SystemTypeId { get; set; }
        public string SystemType { get; set; } = string.Empty;

        public Guid FrameTypeId { get; set; }
        public string FrameType { get; set; } = string.Empty;

        public string FrameMaterial { get; set; } = string.Empty;

        public List<string> FlangeOptions { get; set; }

        public List<int> FwSizes { get; set; }

        public List<FwSizeRule> FwSizeRules { get; set; }

        public List<string> WedgeApMaterials { get; set; }

        public bool SpecialCoating { get; set; }

        public bool KitSingleSelectable { get; set; }
    }

    public class FwSizeRule
    {
        public List<int> rows { get; set; }
        public List<int> columns { get; set; }
        public int fw_size { get; set; }

        public override string ToString()
        {
            return $"FwSize: {fw_size}, Rows: [{string.Join(", ", rows)}], Columns: [{string.Join(", ", columns)}]";
        }
    }

    public class SpecialFrameConfigurationViewModel
    {
        public Guid BaseMaterialId { get; set; }
        public string BaseMaterial { get; set; }
        public Guid SystemTypeId { get; set; }
        public string SystemType { get; set; }
        public Guid FrameTypeId { get; set; }
        public string FrameType { get; set; }
        public string FrameMaterial { get; set; }

        // Menschlich lesbare Strings für Listen / komplexe Felder
        public string FlangeOptions { get; set; }
        public string FwSizes { get; set; }
        public string FwSizeRules { get; set; }
        public string WedgeApMaterials { get; set; }

        public bool SpecialCoating { get; set; }

        public bool KitSingleSelectable { get; set; }
    }

    public static class SpecialFrameMappers
    {
        public static List<SpecialFrameConfigurationViewModel> ToViewModel(this List<SpecialFrameConfigurationDto> list)
        {
            var vm = new List<SpecialFrameConfigurationViewModel>();
            if (list == null) return vm;

            foreach (var s in list)
            {
                vm.Add(new SpecialFrameConfigurationViewModel
                {
                    BaseMaterialId = s.BaseMaterialId,
                    BaseMaterial = s.BaseMaterial,
                    SystemTypeId = s.SystemTypeId,
                    SystemType = s.SystemType,
                    FrameTypeId = s.FrameTypeId,
                    FrameType = s.FrameType,
                    FrameMaterial = s.FrameMaterial,
                    FlangeOptions = s.FlangeOptions == null ? string.Empty : string.Join(", ", s.FlangeOptions),
                    FwSizes = s.FwSizes == null ? string.Empty : string.Join(", ", s.FwSizes),
                    FwSizeRules = s.FwSizeRules == null ? string.Empty : getFwSizeRules(s.FwSizeRules),
                    WedgeApMaterials = s.WedgeApMaterials == null ? string.Empty : string.Join(", ", s.WedgeApMaterials),
                    SpecialCoating = s.SpecialCoating,
                    KitSingleSelectable = s.KitSingleSelectable
                });
            }

            return vm;
        }

        public static string getFwSizeRules(List<FwSizeRule> rules)
        {
            if (rules == null || rules.Count == 0) return string.Empty;
            var ruleStrings = new List<string>();
            foreach (var rule in rules)
            {
                string rows = rule.rows != null ? rule.rows.FirstOrDefault().ToString() + "..." + rule.rows.LastOrDefault().ToString() : "???";
                string columns = rule.columns != null ? rule.columns.FirstOrDefault().ToString() + "..." + rule.columns.LastOrDefault().ToString() : "???"; ;
                ruleStrings.Add($"FwSize: {rule.fw_size}, Rows: {rows}, Columns: {columns}");
            }
            return string.Join("; ", ruleStrings);
        }
    }
}
