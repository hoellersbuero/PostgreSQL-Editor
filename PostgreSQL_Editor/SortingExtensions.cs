using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PostgreSQL_Editor
{
    /// <summary>
    /// Universelle Sortier‑Extensions — sortiert IList<T> nach einem "Name"-Key oder nach einem Key‑Selector.
    /// Kompatibel mit .NET Framework 4.8.
    /// </summary>
    public static class SortingExtensions
    {
        // Cache für MemberInfo pro Type (sucht Property oder Field mit Namen "Name", case‑insensitive)
        private static readonly ConcurrentDictionary<Type, Func<object, string>> _nameSelectorCache
            = new ConcurrentDictionary<Type, Func<object, string>>();

        /// <summary>
        /// Sortiert die Liste in-place nach dem <c>Name</c>-Member (Property oder Field, case‑insensitive).
        /// Wirft, wenn kein Name‑Member vorhanden ist.
        /// </summary>
        public static void SortByName<T>(this IList<T> list, bool ascending = true, IComparer<string> comparer = null)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            comparer = comparer ?? StringComparer.OrdinalIgnoreCase;

            Func<T, string> selector = CreateOrGetNameSelector<T>();
            SortByName(list, selector, ascending, comparer);
        }

        /// <summary>
        /// Sortiert die Liste in-place nach dem angegebenen Key‑Selector (z. B. x => x.Name).
        /// </summary>
        public static void SortByName<T>(this IList<T> list, Func<T, string> nameSelector, bool ascending = true, IComparer<string> comparer = null)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (nameSelector == null) throw new ArgumentNullException(nameof(nameSelector));
            comparer = comparer ?? StringComparer.OrdinalIgnoreCase;

            // OrderBy erzeugt stabile Sortierung; dann in-place überschreiben
            var ordered = ascending
                ? list.OrderBy(nameSelector, comparer ?? StringComparer.OrdinalIgnoreCase).ToList()
                : list.OrderByDescending(nameSelector, comparer ?? StringComparer.OrdinalIgnoreCase).ToList();

            for (int i = 0; i < ordered.Count; i++)
                list[i] = ordered[i];
        }

        // Erstellt oder liest aus Cache einen Func<object,string> der den "Name"-Wert liefert
        private static Func<T, string> CreateOrGetNameSelector<T>()
        {
            var type = typeof(T);
            var selectorObj = _nameSelectorCache.GetOrAdd(type, t =>
            {
                // Suche Property first (case-insensitive), dann Field
                var props = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                var prop = props.FirstOrDefault(p => string.Equals(p.Name, "Name", StringComparison.OrdinalIgnoreCase) && p.CanRead);
                if (prop != null)
                {
                    return new Func<object, string>(o =>
                    {
                        var v = prop.GetValue(o, null);
                        return v?.ToString();
                    });
                }

                var fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public);
                var field = fields.FirstOrDefault(f => string.Equals(f.Name, "Name", StringComparison.OrdinalIgnoreCase));
                if (field != null)
                {
                    return new Func<object, string>(o =>
                    {
                        var v = field.GetValue(o);
                        return v?.ToString();
                    });
                }

                // kein Member gefunden -> Exception beim ersten Aufruf
                return new Func<object, string>(o => throw new InvalidOperationException($"Typ '{t.FullName}' hat kein öffentliches lesbares Member 'Name'."));
            });

            return new Func<T, string>(x => selectorObj(x));
        }
    }
}