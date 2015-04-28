using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.WinForms
{
    public abstract class ChainLinkHeadPresenterBase<TResult> : TerminalPresenterBase,
        IChainLinkHeadPresenter<TResult>
    {
        public ChainLinkHeadPresenterBase(ITerminalView view, String title)
            : base(view, title)
        {
        }

        Object IChainLinkPresenter.Result
        {
            get
            {
                return this.Result;
            }
        }

        public TResult Result
        {
            get;
            protected set;
        }

        public ITerminalPresenter Next
        {
            get;
            protected set;
        }
        
        public virtual IChainLinkMiddlePresenter<TNextResult, TResult> ContinueWith<TNextResult>(IChainLinkMiddlePresenter<TNextResult, TResult> nextElement)
        {
            if (nextElement == null)
                throw new ArgumentNullException();

            nextElement.ParentLink = this;
            this.Next = nextElement;

            return nextElement;
        }

    }
}
