// In CreateDgvChangeDetector: RowChanged-Handler robuster machen
_dgvChangeDetector.RowChanged += (s, changedRow) =>
{
    // Schutz: Nur wenn Row gültig und gebunden
    if (changedRow == null) return;
    if (changedRow.Index < 0) return;
    if (changedRow.DataBoundItem == null) return;

    changedRow.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
    frame_sleeve_rule changedRule = changedRow.DataBoundItem as frame_sleeve_rule;
    if (changedRule == null) return;
    string jsonNew = JsonSerializer.Serialize(changedRule);
    var x = from rule in changedFrameSleeveRules where JsonSerializer.Serialize(rule) == jsonNew select rule;
    if (!x.Any())
    {
        changedFrameSleeveRules.Add(changedRule);
        updateLbInfo();
    }
    Action updateButton = () =>
    {
        btnCreateSQL.Enabled = (newFrameSleeveRules?.Count ?? 0) > 0 || changedFrameSleeveRules.Count > 0;
    };

    if (btnCreateSQL.InvokeRequired)
        btnCreateSQL.BeginInvoke(updateButton);
    else
        updateButton();
};