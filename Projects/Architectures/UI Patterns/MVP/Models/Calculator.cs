namespace MVP.Models
{
    public class Calculator
    {
        public double NumberOne { get; set; }

        public double NumberTwo { get; set; }

        public double CalculateSumation()
        {
            return NumberOne + NumberTwo;
        }

        public double CalculateSubstraction()
        {
            return NumberOne - NumberTwo;
        }

        public double CalculateMultiplication()
        {
            return NumberOne * NumberTwo;
        }

        public double CalculateDiviton()
        {
            return NumberOne / NumberTwo;
        }
    }
}
