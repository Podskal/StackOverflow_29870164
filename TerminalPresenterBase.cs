using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.WinForms
{
    /// <summary>
    /// Base class for all presenters
    /// </summary>
    public abstract class TerminalPresenterBase : ITerminalPresenter
    {
        protected ITerminalView view;
        private String title;

        public TerminalPresenterBase(ITerminalView view, String title)
        {
            if (view == null) 
                throw new ArgumentNullException("view");

            this.view = view;
            this.title = title;
            this.Parent = this;
        }

        public String Title
        {
            get;
            protected set;
        }

        public abstract void UpdateUI();

        public abstract ITerminalPresenter this[int index]
        {
            get;
        }

        public abstract ITerminalPresenter Do1();
        public abstract ITerminalPresenter Do2();

        public virtual ITerminalPresenter Parent
        {
            get;
            set;
        }

        public virtual void Reset()
        {
            this.UpdateUI();
        }
    }
}
