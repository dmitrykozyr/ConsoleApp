using MVP.Presenters;
using MVP.Views.Interfaces;
using System.Windows.Forms;

namespace MVP
{
    public partial class FormCalculator : Form, ICalculator
    {
        public string NumberOne { get => txtNumberOne.Text; set => txtNumberOne.Text = value; }
        public string NumberTwo { get => txtNumberTwo.Text; set => txtNumberTwo.Text = value; }
        public string Result { get => txtResult.Text; set => txtResult.Text = value; }

        private CalculatorPresenter presenter;

        public FormCalculator()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            presenter = new CalculatorPresenter(this);
            presenter.Sum();
        }

        private void btnSub_Click(object sender, System.EventArgs e)
        {
            presenter = new CalculatorPresenter(this);
            presenter.Sub();
        }

        private void btnMul_Click(object sender, System.EventArgs e)
        {
            presenter = new CalculatorPresenter(this);
            presenter.Mul();
        }

        private void btnDiv_Click(object sender, System.EventArgs e)
        {
            presenter = new CalculatorPresenter(this);
            presenter.Div();
        }
    }
}
