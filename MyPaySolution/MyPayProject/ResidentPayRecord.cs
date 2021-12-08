using System;
using System.Collections.Generic;
using System.Text;

namespace MyPayProject
{
    public class ResidentPayRecord : PayRecord
    {
        public ResidentPayRecord(int id, double[] hours, double[] rates) :base(id,hours,rates)
        {

        }
        /// <summary>
        /// Sets Tax for Resident Pay Record
        /// </summary>
        public override double Tax
        {
            get
            {
                return TaxCalculator.CalculateResidentialTax(Gross);
            }
        } 
       
    }
}
