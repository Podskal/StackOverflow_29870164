using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.WinForms
{
    public abstract class ChainLinkMiddlePresenterBase<TResult, TParentResult> : TerminalPresenterBase,
        IChainLinkMiddlePresenter<TResult, TParentResult>
    {
        public ChainLinkMiddlePresenterBase(ITerminalView view, String title)
            : base(view, title)
        {

        }

        public IChainLinkPresenter<TParentResult> ParentLink
        {
            get;
            set;
        }

        public IChainLinkMiddlePresenter<TNextResult, TResult> ContinueWith<TNextResult>(IChainLinkMiddlePresenter<TNextResult, TResult> nextElement)
        {
            if (nextElement == null)
                throw new ArgumentNullException();

            nextElement.ParentLink = this;
            this.Next = nextElement;

            return nextElement;
        }

        public ITerminalPresenter EndWith(ITerminalPresenter end)
        {
            if (end == null)
                throw new ArgumentNullException();

            this.Next = end;

            return end;
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
    }
}
