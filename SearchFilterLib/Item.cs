using System.Collections.Generic;

namespace SearchFilterLib
{
    public partial class SearchData
    {
        private class Item
        {
            public string Text;
            public HashSet<string> Strings;
        }
    }
}
