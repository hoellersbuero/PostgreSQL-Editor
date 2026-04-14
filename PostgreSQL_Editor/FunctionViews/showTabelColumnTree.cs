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
    public partial class showTabelColumnTree : Form
    {
        private Dictionary<string, List<string>> TableColumnsByTable;

        public showTabelColumnTree()
        {
            InitializeComponent();
        }

        public static void Execute(Form owner, Dictionary<string, List<string>> tableColumnsByTable)
        {
            using (var form = new showTabelColumnTree())
            {
                form.TableColumnsByTable = tableColumnsByTable;
                form.ShowDialog(owner);
            }
        }

        private void showTabelColumnTree_Load(object sender, EventArgs e)
        {
            // Reihenfolge-Regeln:
            // 1) id, name, columns, rows, h1, b1, h2, b2 (wenn vorhanden)
            // 2) alle übrigen Spalten alphabetisch
            // 3) created_by, created_ts, modified_by, modified_ts (in genau dieser Reihenfolge, falls vorhanden)
            var primaryFirst = new[] { "id", "name", "columns", "rows", "h1", "b1", "h2", "b2" };
            var primaryLast = new[] { "created_by", "created_ts", "modified_by", "modified_ts" };

            foreach (var table in TableColumnsByTable)
            {
                var tableNode = new TreeNode(table.Key);

                var originalCols = table.Value ?? new List<string>();
                var added = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                var ordered = new List<string>();

                // 1) id, name (wenn vorhanden)
                foreach (var key in primaryFirst)
                {
                    var match = originalCols.FirstOrDefault(c => string.Equals(c, key, StringComparison.OrdinalIgnoreCase));
                    if (match != null && added.Add(match))
                        ordered.Add(match);
                }

                // 2) mittlere Spalten (alles außer bereits hinzugefügte und außer die, die am Ende stehen)
                var middleCols = originalCols
                    .Where(c => !added.Contains(c) && !primaryLast.Any(p => string.Equals(p, c, StringComparison.OrdinalIgnoreCase)))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(c => c, StringComparer.OrdinalIgnoreCase);

                foreach (var c in middleCols)
                {
                    if (added.Add(c))
                        ordered.Add(c);
                }

                // 3) created_by, created_ts, modified_by, modified_ts (in dieser Reihenfolge)
                foreach (var key in primaryLast)
                {
                    var match = originalCols.FirstOrDefault(c => string.Equals(c, key, StringComparison.OrdinalIgnoreCase));
                    if (match != null && added.Add(match))
                        ordered.Add(match);
                }

                // Nodes anlegen in geordneter Reihenfolge
                foreach (var column in ordered)
                {
                    tableNode.Nodes.Add(new TreeNode(column));
                }

                tv.Nodes.Add(tableNode);
            }
            // tv.ExpandAll();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
