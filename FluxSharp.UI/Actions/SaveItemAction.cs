using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxSharp.Actions
{
    public class SaveItemAction
    {
        public SaveItemAction(string id, string text)
        {
            Id = id;
            Text = text;
        }
        
        public string Id { get; private set; }
        public string Text { get; private set; }
    }

    public class EditItemAction
    {
        public EditItemAction(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}
