using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.WinForms
{
    public enum APlusBState
    {
        EnterA,
        EnterB,
        Result
    }

    public class StepActions
    {
        public Action UpdateUI { get; set; }

        public Func<ITerminalPresenter> Do1 { get; set; }

        public Func<ITerminalPresenter> Do2 { get; set; }
    }

    public class APlusBPresenter : TerminalPresenterBase
    {
        private Int32 a, b;
        private APlusBState state;
        private String error = null;

        private Dictionary<APlusBState, StepActions> stateActions;

        private void InitializeStateActions()
        {
            this.stateActions = new Dictionary<APlusBState, StepActions>();

            this.stateActions.Add(APlusBState.EnterA,
                new StepActions()
                {
                    UpdateUI = () =>
                    {
                        this.view.Title = this.error ?? "Enter A";
                        this.view.Input = this.a.ToString();
                        this.view.Button1_Text = "Confirm A";
                        this.view.Button2_Text = "Exit";
                    },
                    Do1 = () => // Confirm A
                    {
                        if (!Int32.TryParse(this.view.Input, out this.a))
                        {
                            this.error = "A is in incorrect format. Enter A again";
                            return this;
                        }

                        this.error = null;                     
                        this.state = APlusBState.EnterB;

                        return this;
                    },
                    Do2 = () => // Exit
                    {
                        this.Reset();

                        return this.Parent;
                    }
                });
            
            this.stateActions.Add(APlusBState.EnterB,
                new StepActions()
                {
                    UpdateUI = () =>
                    {
                        this.view.Title = this.error ?? "Enter B";
                        this.view.Input = this.b.ToString();
                        this.view.Button1_Text = "Confirm B";
                        this.view.Button2_Text = "Back to A";
                    },
                    Do1 = () => // Confirm B
                    {
                        if (!Int32.TryParse(this.view.Input, out this.b))
                        {
                            this.error = "B is in incorrect format. Enter B again";
                            return this;
                        }

                        this.error = null;                     
                        this.state = APlusBState.Result;

                        return this;
                    },
                    Do2 = () => // Back to a
                    {
                        this.state = APlusBState.EnterA;

                        return this;
                    }
                });

            this.stateActions.Add(APlusBState.Result,
                new StepActions()
                {
                    UpdateUI = () =>
                    {
                        this.view.Title = String.Format("The result of {0} + {1}", this.a, this.b);
                        this.view.Output = (this.a + this.b).ToString();
                        this.view.Button1_Text = "Exit";
                        this.view.Button2_Text = "Back";
                    },
                    Do1 = () => // Exit
                    {
                        this.Reset();

                        return this.Parent;
                    },
                    Do2 = () => // Back to B
                    {
                        this.state = APlusBState.EnterB;

                        return this;
                    }
                });
        }

        public APlusBPresenter(ITerminalView view) : base(view, "A + B")
        {
            this.InitializeStateActions();
            this.Reset();
        }

        public override void UpdateUI()
        {
            this.view.Clear();

            this.stateActions[this.state].UpdateUI();
        }

        public override ITerminalPresenter this[int index]
        {
            get { throw new NotImplementedException(); }
        }

        public override ITerminalPresenter Do1()
        {
            var nextPresenter = this.stateActions[this.state].Do1();

            nextPresenter.UpdateUI();

            return nextPresenter;
        }

        public override ITerminalPresenter Do2()
        {
            var nextPresenter = this.stateActions[this.state].Do2();

            nextPresenter.UpdateUI();

            return nextPresenter;
        }

        public override void Reset()
        {
            this.state = APlusBState.EnterA;
            this.a = 0;
            this.b = 0;
            this.error = null;
        }
    }
}
