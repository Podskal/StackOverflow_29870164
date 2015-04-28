using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.WinForms
{
    /// <summary>
    /// This represents your UI in technology-independent manner
    /// </summary>
    public interface ITerminalView
    {
        String Title { get; set; }
        String Input { get; set; }
        String Output { get; set; }
        String Button1_Text { get; set; }
        String Button2_Text { get; set; }
        IEnumerable<String> SelectionItems { get; set; }
        void Clear();
    }
}
