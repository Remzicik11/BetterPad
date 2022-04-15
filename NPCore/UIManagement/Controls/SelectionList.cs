using System;
using System.Collections.Generic;
using System.Text;

namespace NPCore.UIControls
{
    public class SelectionList : IContent
    {
        public int Selection { get; set; }
        
        public List<SelectionItem> Items { get; set; } = new List<SelectionItem>();

        public class SelectionItem
        {
            public string Name { get; set; }
            public Action Action;
        }
    }
}
