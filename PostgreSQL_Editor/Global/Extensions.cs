using PostgreSQL_Editor.DBUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PostgreSQL_Editor.Global
{
    public static class GlobalExtensions
    {
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0x000B;

        public static void AppendTextColor(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = text.Length;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        public static string BaseMaterialName(this base_material baseMaterial)
        {
            if (baseMaterial.name.EndsWith("concrete"))
                return "Concrete";
            else if (baseMaterial.name.EndsWith("steel"))
                return "Steel";
            else if (baseMaterial.name.EndsWith("cabinet"))
                return "Cabinet seal";
            else
                return baseMaterial.name;
        }

        public static string BaseMaterialName(this string name)
        {
            if (name.EndsWith("concrete"))
                return "Concrete";
            else if (name.EndsWith("steel"))
                return "Steel";
            else if (name.EndsWith("cabinet"))
                return "Cabinet seal";
            else
                return name;
        }

        /// <summary>
        /// Deep clone mittels DataContractSerializer. TKey/TValue und alle verschachtelten Typen müssen serialisierbar sein.
        /// Bei komplexen oder großen Objekten ggf. Performance/Kompatibilität prüfen.
        /// </summary>
        public static T DeepCloneDataContract<T>(this T source)
        {
            if (ReferenceEquals(source, null)) return default(T);

            var serializer = new DataContractSerializer(typeof(T));
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, source);
                ms.Position = 0;
                return (T)serializer.ReadObject(ms);
            }
        }

        public static List<string> RegexSplit(this string input, string[] keylist)
        {
            var values = new List<string>();
            int pos = 0;
            foreach (string key in keylist)
            {
                foreach (Match m in Regex.Matches(input, key))
                {
                    values.Add(input.Substring(pos, m.Index - pos));
                    values.Add(m.Value);
                    pos = m.Index + m.Length;
                }
                values.Add(input.Substring(pos));
            }
            return values.Where(s => !string.IsNullOrEmpty(s)).ToList();
        }

        public static List<string> extractColumns(this string query, bool removeEmpty = true)
        {
            List<string> ls = new List<string>();
            if (string.IsNullOrWhiteSpace(query)) return null;
            var m = Regex.Match(query, @"\bselect\b\s*(.*?)\s*\bfrom\b", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            string res = m.Success ? m.Groups[1].Value.Trim() : string.Empty;
            if (!String.IsNullOrEmpty(res))
            {
                ls = res.Split(',').ToList();
                return ls.Select(s => (s ?? string.Empty).Trim())
                    .Where(s => !removeEmpty || s.Length > 0).ToList();
            }
            return null;
        }


        /// <summary>
        /// Entfernt alles außer Unicode-Buchstaben und Whitespace.
        /// Ergebnis: "CFS T SB EX" aus "CFS-T SB EX 20"
        /// </summary>
        public static string KeepLettersAndSpaces(string input, bool collapseSpaces = false)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            // 1) Entferne alles, was kein Unicode-Buchstabe (\p{L}) und kein Whitespace ist
            string cleaned = Regex.Replace(input, @"[^\p{L}\s-]+", "");

            // 2) Optional: mehrere Whitespace-Zeichen zu einem Leerzeichen reduzieren und trimmen
            if (collapseSpaces)
                cleaned = Regex.Replace(cleaned, @"\s+", " ").Trim();

            return cleaned;
        }

        /// <summary>
        /// Liefert einzelne Wörter (nur Buchstaben) als Liste.
        /// Beispiel: ["CFS","T","SB","EX"]
        /// </summary>
        public static List<string> ExtractWords(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return new List<string>();
            var matches = Regex.Matches(input, @"\p{L}+");
            return matches.Cast<Match>().Select(m => m.Value).ToList();
        }

        public static string getModuleNameFromSystemType(this string sysname)
        {
            if (sysname.Contains("STRF"))
                return "CFS-T S ";
            return sysname;
        }

        public static int CountCharFast(this string s, char ch)
        {
            if (string.IsNullOrEmpty(s)) return 0;
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ch) count++;
            }
            return count;
        }

        public static void HighlightSqlWords(this RichTextBox sqltext)
        {
            var keywords = standardLists.SQLStatements;
            if (keywords == null || !keywords.Any() || string.IsNullOrEmpty(sqltext.Text)) return;

            // Auswahl sichern
            int selStart = sqltext.SelectionStart;
            int selLength = sqltext.SelectionLength;

            // Text komplett auf Default zurücksetzen (zuerst Redraw aus)
            SendMessage(sqltext.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
            try
            {
                sqltext.SelectAll();
                sqltext.SelectionColor = Color.Black;

                // Einfache Normalisierung: eindeutige Keywords, leere Einträge überspringen
                var distinctKeys = keywords
                    .Where(k => !string.IsNullOrWhiteSpace(k))
                    .Select(k => k.Trim())
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                string text = sqltext.Text;

                foreach (string key in distinctKeys)
                {
                    // whole-word Match; Escape für Sonderzeichen
                    string pattern = $@"\b{Regex.Escape(key)}\b";
                    foreach (Match m in Regex.Matches(text, pattern, RegexOptions.IgnoreCase))
                    {
                        sqltext.Select(m.Index, m.Length);
                        sqltext.SelectionColor = Color.Blue;
                    }
                }
            }
            finally
            {
                // Auswahl wiederherstellen und Redraw aktivieren
                sqltext.Select(selStart, selLength);
                SendMessage(sqltext.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
                sqltext.Invalidate();
            }
        }
    }
}
