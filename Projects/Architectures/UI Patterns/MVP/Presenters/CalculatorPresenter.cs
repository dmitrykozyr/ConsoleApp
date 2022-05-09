using MVP.Models;
using MVP.Views.Interfaces;
using System;
using System.Windows.Forms;

namespace MVP.Presenters
{
    public class CalculatorPresenter
    {
        Calculator _calculator = new Calculator();
        private ICalculator _calculatorView;

        public CalculatorPresenter(ICalculator calculatorView)
        {
            _calculatorView = calculatorView;
        }

        public void ModelViewConnection()
        {
            _calculator.NumberOne = Convert.ToDouble(_calculatorView.NumberOne);
            _calculator.NumberTwo = Convert.ToDouble(_calculatorView.NumberTwo);
        }

        public void Sum()
        {
            ModelViewConnection();
            _calculatorView.Result = _calculator.CalculateSumation().ToString();
        }

        public void Sub()
        {
            ModelViewConnection();
            _calculatorView.Result = _calculator.CalculateSubstraction().ToString();
        }

        public void Mul()
        {
            ModelViewConnection();
            _calculatorView.Result = _calculator.CalculateMultiplication().ToString();
        }

        public void Div()
        {
            ModelViewConnection();

            if (Convert.ToDouble(_calculatorView.NumberTwo) == 0)
            {
                MessageBox.Show("Can't divide on 0");
            }
            else
            {
                _calculatorView.Result = _calculator.CalculateDiviton().ToString();
            }
        }
    }
}
