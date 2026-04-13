using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PostgreSQL_Editor.Utilities
{
    public class DgvChangeDetector : IDisposable
    {
        private readonly DataGridView _dgv;
        private readonly Dictionary<int, object[]> _snapshot = new Dictionary<int, object[]>();
        private readonly HashSet<int> _changedRowIndexes = new HashSet<int>();
        private bool _disposed;

        public event EventHandler<DataGridViewRow> RowChanged;

        public DgvChangeDetector(DataGridView dgv)
        {
            _dgv = dgv ?? throw new ArgumentNullException(nameof(dgv));
            _dgv.CellValueChanged += OnCellValueChanged;
            _dgv.CurrentCellDirtyStateChanged += OnCurrentCellDirtyStateChanged;
            _dgv.RowsRemoved += OnRowsStructureChanged;
            _dgv.RowsAdded += OnRowsStructureChanged;
        }

        /// <summary>
        /// Erzeuge einen Snapshot aller aktuellen Zeilenwerte (außer NewRow).
        /// Muss nach DataSource/Bind/Refresh aufgerufen werden.
        /// </summary>
        public void TakeSnapshot()
        {
            _snapshot.Clear();
            _changedRowIndexes.Clear();
            for (int i = 0; i < _dgv.Rows.Count; i++)
            {
                var row = _dgv.Rows[i];
                if (row.IsNewRow) continue;
                _snapshot[i] = GetRowValues(row);
            }
        }

        private object[] GetRowValues(DataGridViewRow row)
        {
            return row.Cells.Cast<DataGridViewCell>()
                       .Select(c => c.Value ?? DBNull.Value)
                       .ToArray();
        }

        private void OnCurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Für Checkbox/Combo sofort Commit, damit CellValueChanged feuert
            if (_dgv.IsCurrentCellDirty)
                _dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void OnCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _dgv.Rows.Count) return;
            var row = _dgv.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            var newValues = GetRowValues(row);
            if (!_snapshot.TryGetValue(e.RowIndex, out var oldValues) || !RowValuesEqual(oldValues, newValues))
            {
                _changedRowIndexes.Add(e.RowIndex);
                RowChanged?.Invoke(this, row);
            }
            else
            {
                // wenn Werte wieder gleich sind, Änderung entfernen
                if (_changedRowIndexes.Contains(e.RowIndex))
                    _changedRowIndexes.Remove(e.RowIndex);
            }
        }

        private static bool RowValuesEqual(object[] a, object[] b)
        {
            if (a == null || b == null) return false;
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
            {
                var va = a[i] is DBNull ? null : a[i];
                var vb = b[i] is DBNull ? null : b[i];
                if (!object.Equals(va, vb)) return false;
            }
            return true;
        }

        /// <summary>
        /// Gibt alle aktuell geänderten DataGridViewRow zurück.
        /// </summary>
        public IEnumerable<DataGridViewRow> GetChangedRows()
        {
            foreach (var idx in _changedRowIndexes.ToArray())
            {
                if (idx >= 0 && idx < _dgv.Rows.Count)
                    yield return _dgv.Rows[idx];
            }
        }

        /// <summary>
        /// Akzeptiert Änderungen für eine Zeile (aktualisiert Snapshot und entfernt Markierung).
        /// </summary>
        public void AcceptChanges(DataGridViewRow row)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            int idx = row.Index;
            if (!row.IsNewRow)
                _snapshot[idx] = GetRowValues(row);
            _changedRowIndexes.Remove(idx);
        }

        private void OnRowsStructureChanged(object sender, EventArgs e)
        {
            // wenn Zeilenanzahl/Indexierung sich ändert, neu snapshotten
            TakeSnapshot();
        }

        public void Dispose()
        {
            if (_disposed) return;
            _dgv.CellValueChanged -= OnCellValueChanged;
            _dgv.CurrentCellDirtyStateChanged -= OnCurrentCellDirtyStateChanged;
            _dgv.RowsRemoved -= OnRowsStructureChanged;
            _dgv.RowsAdded -= OnRowsStructureChanged;
            _disposed = true;
        }
    }
}