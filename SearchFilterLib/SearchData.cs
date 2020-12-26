using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SearchFilterLib
{
    public partial class SearchData
    {
        private ConcurrentBag<SearchItem> _items = new ConcurrentBag<SearchItem>();
        public SearchData(string[] strings)
        {
            Parallel.For(0, strings.Length, (i, state) =>
            {
                string text = strings[i];
                _items.Add(new SearchItem() { Text = text, Strings = GetParts(text) });
            });
            Console.WriteLine(_items.Count);
        }

        private HashSet<string> GetParts(string text)
        {
            HashSet<string> result = new HashSet<string>();
            StringBuilder x = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                char cha = text[i];
                if (Char.IsUpper(cha))
                {
                    if (x.Length > 0)
                    {
                        x.Clear();
                    }
                }
                x.Append(cha);
                result.Add(x.ToString().ToLower());
            }
            return result;
        }

        public string[] Search(string searchText)
        {
            List<SearchItem> findedItems = new List<SearchItem>(_items);
            List<string>[] allVarians = GenerateFindTextVariants(searchText.ToLower());
            for (int i = 0; i < findedItems.Count; i++)
            {
                SearchItem item = findedItems[i];

                bool ok = false;
                foreach (var variants in allVarians)
                {
                    ok = false;
                    foreach (var variant in variants)
                    {
                        if (item.Strings.Contains(variant))
                        {
                            ok = true;
                            break;
                        }
                    }
                    if (!ok)
                    {
                        break;
                    }
                }
                if (!ok)
                {
                    findedItems.RemoveAt(i);
                    i--;
                }
            }
            return findedItems.Select(x => x.Text).ToArray();
        }

        private List<string>[] GenerateFindTextVariants(string text)
        {
            List<string>[] variants = new List<string>[text.Length];
                for (int i = 0; i < text.Length; i++)
                {
                    string cha = text[i].ToString();
                    variants[i] = new List<string>();
                    if (i > 0)
                    {
                        foreach (var variant in variants[i-1])
                        {
                            variants[i].Add(variant + cha);
                        }
                    }
                    variants[i].Add(cha);
                }
            return variants;
        }
    }
}
