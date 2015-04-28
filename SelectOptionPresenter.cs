using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.WinForms
{
    /// <summary>
    /// Presenter whose sole goal is to allow user to select some other option with single button click
    /// </summary>
    public class SelectOptionPresenter : TerminalPresenterBase
    {
#warning Options can be set or can be not set resulting in inconsistent state - consider some workaround for real app.

        private IList<KeyValuePair<String, ITerminalPresenter>> options;
        private ITerminalPresenter selected;

        /// <summary>
        /// Bad constructor - uses null options.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="title"></param>
        public SelectOptionPresenter(ITerminalView view,
            String title)
            : this(view, title, null)
        {
        }

        public SelectOptionPresenter(ITerminalView view,
            String title,
            IList<KeyValuePair<String, ITerminalPresenter>> options)
            : base(view, title)
        {
            this.Options = options;
        }

        /// <summary>
        /// Bad setter - allows bad values.
        /// </summary>
        public IList<KeyValuePair<String, ITerminalPresenter>> Options
        {
            get
            {
                return this.options;
            }
            set
            {
                this.options = value;

                if (value == null)
                    return;

                foreach (var item in value)
                {
                    item.Value.Parent = this;
                }
            }  
            
        }

        public override void UpdateUI()
        {
            this.view.Clear();

            this.view.Button1_Text = this.options[0].Key;
            this.view.Button2_Text = this.options[1].Key;
            this.view.Title = this.Title;
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
            return this.options[0].Value;
        }

        public override ITerminalPresenter Do2()
        {
            return this.options[1].Value;
        }
    }
}
