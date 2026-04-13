using System;
using System.Threading;
using System.Windows.Forms;

namespace PostgreSQL_Editor
{
    static class Program
    {
        // Sichtbar für Main, damit dieser den Splash schließen kann
        public static SplashForm2 Splash;
        private static readonly ManualResetEventSlim splashReady = new ManualResetEventSlim(false);

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Splash in eigenem STA-Thread mit eigener Message-Loop starten
            var splashThread = new Thread(() =>
            {
                Splash = new SplashForm2();
                // Signal: Splash ist erstellt (aber noch nicht unbedingt sichtbar)
                splashReady.Set();
                Application.Run(Splash); // startet Message-Loop für Splash
            });

            splashThread.IsBackground = true;
            splashThread.SetApartmentState(ApartmentState.STA);
            splashThread.Start();

            // optional: Warte, bis Splash initialisiert ist (Verhindert Race-Conditions)
            splashReady.Wait();

            // Hauptformular starten (Main). Form1_Load schließt später den Splash.
            Application.Run(new Main());
        }
    }
}