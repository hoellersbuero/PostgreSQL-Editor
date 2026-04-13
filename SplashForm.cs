public void StartInitialization(Action<Action<string>> initWithReporter)
{
    var bw = new BackgroundWorker { WorkerReportsProgress = true };
    bw.DoWork += (s, e) =>
    {
        // Reporter an den Initialisierer weitergeben
        initWithReporter(msg => bw.ReportProgress(0, msg));
    };

    bw.ProgressChanged += (s, e) =>
    {
        if (!this.IsHandleCreated) return;
        // RunWorkerCallbacks laufen auf UI-Thread; hier direkt setzen ist okay
        this.lblStatus.Text = e.UserState as string ?? string.Empty;
    };

    bw.RunWorkerCompleted += (s, e) =>
    {
        try
        {
            // Wichtig: erst auf e.Error prüfen (Ausnahme im DoWork)
            if (e.Error != null)
            {
                // Loggen für Diagnose
                System.Diagnostics.Debug.WriteLine("Initialization error: " + e.Error);
                if (this.IsHandleCreated)
                {
                    this.lblStatus.Text = "Initialization failed";
                    MessageBox.Show(this, "Initialization error: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close();
                return;
            }

            // Falls der Worker abgebrochen wurde
            if (e.Cancelled)
            {
                this.lblStatus.Text = "Initialization cancelled";
                this.Close();
                return;
            }

            // Falls DoWork ein Exception-Objekt als Result zurückgegeben hat (falls Du das behalten willst)
            if (e.Result is Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Initialization returned exception in Result: " + ex);
                if (this.IsHandleCreated)
                {
                    this.lblStatus.Text = "Initialization failed";
                    MessageBox.Show(this, "Initialization error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close();
                return;
            }

            // Normal beendet
            this.Close();
        }
        catch (Exception ex)
        {
            // Falls trotzdem eine Exception hier auftaucht, logge sie
            System.Diagnostics.Debug.WriteLine("RunWorkerCompleted handler crashed: " + ex);
            try { this.Close(); } catch { }
        }
    };

    bw.RunWorkerAsync();
}