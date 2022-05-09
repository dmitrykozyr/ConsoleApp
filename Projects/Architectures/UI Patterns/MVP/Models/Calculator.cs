using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
