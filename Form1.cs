using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test.WinForms
{
    public partial class MainForm : Form,
        ITerminalView
    {
        private IGlobalTerminalPresenter globalTerminalPresenter;

        public MainForm()
        {
            InitializeComponent();

            var nextLevelPresenters = new KeyValuePair<String, ITerminalPresenter>[]
            {
                new KeyValuePair<String, ITerminalPresenter>(
                    "A plus B", 
                    new APlusBPresenter(this)),
                new KeyValuePair<String, ITerminalPresenter>(
                    "Just empty selector", 
                    new SelectOptionPresenter(this, 
                        "Selector with no selection choices", 
                        Enumerable
                            .Empty<KeyValuePair<String, ITerminalPresenter>>()
                            .ToArray()))
            };

            var topPresenter = new SelectOptionPresenter(this, "Select the option and press the confirm button",  nextLevelPresenters);

            this.globalTerminalPresenter = new GlobalTerminalPresenter(topPresenter);
        }

        #region Event handlers

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var senderComboBox = (ComboBox)sender;

            this.globalTerminalPresenter.SelectedIndex = senderComboBox.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.globalTerminalPresenter.Do1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.globalTerminalPresenter.Do2();
        }

        #endregion


        #region ITerminalView implementation

        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public String Button1_Text
        {
            get { return this.button1.Text; }
            set { this.button1.Text = value; }
        }

        public String Button2_Text
        {
            get { return this.button2.Text; }
            set { this.button2.Text = value; }
        }

        public string Input
        {
            get { return this.textBox_Input.Text; }
            set { this.textBox_Input.Text = value; }
        }

        public string Output
        {
            get { return this.textBox_Output.Text; }
            set { this.textBox_Output.Text = value; }
        }

        public IEnumerable<string> SelectionItems
        {
            get { return this.comboBox.Items.Cast<String>(); }
            set
            { 
                this.comboBox.Items.Clear();

                if (value == null)
                    return;

                foreach (var item in value)
                {
                    this.comboBox.Items.Add(item);
                }
            }
        }

        public void Clear()
        {
            this.comboBox.SelectedIndex = -1;
            this.Title = String.Empty;
            this.Input = String.Empty;
            this.Output = String.Empty;
            this.SelectionItems = null;
        }

        #endregion
    } 
}
