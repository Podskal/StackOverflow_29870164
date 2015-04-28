using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.WinForms
{
    /// <summary>
    /// Represents terminal presenter that UI can operate upon.
    /// </summary>
    public interface IGlobalTerminalPresenter
    {
        void UpdateUI();

        void Do1();

        void Do2();

        Int32 SelectedIndex
        {
            get;
            set;
        }

        void Reset();
    }

    /// <summary>
    /// Represents internal terminal presenter that is used inside IGlobalTerminalPresenter.
    /// </summary>
    public interface ITerminalPresenter
    {
        void UpdateUI();

        ITerminalPresenter this[Int32 index]
        {
            get;
        }

        ITerminalPresenter Do1();

        ITerminalPresenter Do2();

        ITerminalPresenter Parent
        {
            get;
            set;
        }

        void Reset();
    }
}
