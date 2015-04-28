using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.WinForms
{
    /// <summary>
    /// Presenter whose sole goal is to allow user to select some other option and press next
    /// </summary>
    public class SelectOptionPresenter : TerminalPresenterBase
    {
        private IList<KeyValuePair<String, ITerminalPresenter>> options;
        private ITerminalPresenter selected;
        private String title;

        public SelectOptionPresenter(ITerminalView view,
            String title, 
            IList<KeyValuePair<String, ITerminalPresenter>> options)
            : base(view)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            this.title = title;

            this.options = options;

            foreach (var item in options)
            {
                item.Value.Parent = this;
            }
        }

        public override void UpdateUI()
        {
            this.view.Clear();

            this.view.Button1_Text = "Confirm selection";
            this.view.Button2_Text = "Go back";
            this.view.Title = title;
            this.view.SelectionItems = options
                .Select(opt => opt.Key);
        }

        public override ITerminalPresenter this[int index]
        {
            get
            {
                this.selected = this.options[index].Value;
            
                return this;
            }
        }

        public override ITerminalPresenter Do1()
        {
            return this.ConfirmSelection();
        }

        public override ITerminalPresenter Do2()
        {
            return this.GoBack();
        }

        public ITerminalPresenter ConfirmSelection()
        {
            if (selected == null)
            {
                return this;
            }

            this.selected.UpdateUI();
            return this.selected;
        }
        
        public ITerminalPresenter GoBack()
        {
            this.Parent.UpdateUI();
            return this.Parent;
        }
    }
}
