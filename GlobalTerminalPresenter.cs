using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.WinForms
{
    /// <summary>
    /// Represents terminal presenter to use inside GUI. It handles current ISpecificTerminalPresenter inside itself.
    /// </summary>
    public class GlobalTerminalPresenter : IGlobalTerminalPresenter
    {
        #region Fields

        private ITerminalPresenter current;
        private Int32 selectedIndex;

        #endregion
        

        #region Constructors

        public GlobalTerminalPresenter(ITerminalPresenter mainPresenter)
        {
            if (mainPresenter == null)
                throw new ArgumentNullException("mainPresenter");

            this.current = mainPresenter;

            this.UpdateUI();
        }

        #endregion

        public void UpdateUI()
        {
            this.current.UpdateUI();
        }

        public void Do1()
        {
            this.current = this.current.Do1();
        }

        public void Do2()
        {
            this.current = this.current.Do2();
        }

        public Int32 SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }
            set
            {
                this.selectedIndex = value;

                if (value == -1)
                    return;

                this.current = this.current[value];
            }
        }

        public void Reset()
        {
            this.current.Reset();
        }
    }
}
