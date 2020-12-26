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
        private ConcurrentBag<Item> _items = new ConcurrentBag<Item>();
        public SearchData(string[] strings)
        {
            Parallel.For(0, strings.Length, (i, state) =>
            {
                string text = strings[i];
                _items.Add(new Item() { Text = text, Strings = GetParts(text) });
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
            List<Item> findedItems = new List<Item>(_items);
            for (int i = 0; i < findedItems.Count; i++)
            {
                Item item = findedItems[i];
                List<string> variants = new List<string>();
                foreach (char c in searchText)
                {
                    string newCahr = c.ToString();

                    for (int i1 = 0; i1 < variants.Count; i1++)
                    {
                        variants[i1] += newCahr;
                    }
                    variants.Add(newCahr);

                    bool ok = false;
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
                        item.Deleted = true;
                        break;
                    }
                }
                if (item.Deleted)
                {
                    findedItems.RemoveAt(i);
                    i--;
                }
            }
            return findedItems.Select(x => x.Text).ToArray();
        }
    }
}
