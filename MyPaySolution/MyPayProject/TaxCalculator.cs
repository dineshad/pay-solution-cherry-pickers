using System;
using System.Collections.Generic;
using System.Text;

namespace MyPayProject
{
    static class TaxCalculator
    {
        /// <summary>
        /// Calculates Residentia lTax
        /// </summary>
        /// <param name="Gross"></param>
        /// <returns>double Tax</returns>
        public static double CalculateResidentialTax(double Gross)
        {
            var TaxBands = new[]
            {
                new[] {-1, 72, 0.19,0.19 },
                new[] {72, 361, 0.2342,3.213 },
                new[] { 361, 932, 0.3477, 44.2476 },
                new[] { 932, 1380, 0.345, 41.7311 },
                new[] { 1380, 3111, 0.39, 103.8657 },
                new[] { 3111, 999999, 0.47, 352.7888 },
            };            
            foreach ( var Band in TaxBands)
            {
                if (Gross > Band[0] && Gross <= Band[1])
                {
                    double A = Band[2];
                    double B = Band[3];
                    double Tax = A * Gross - B;
                    return Tax;
                }               
            }
            return 0.0;
        }
        /// <summary>
        /// Calculates Working Holiday Tax
        /// </summary>
        /// <param name="Gross"></param>
        /// <param name="YearToDate"></param>
        /// <returns>double Tax</returns>
        public static double CalculateWorkingHolidayTax(double Gross, double YearToDate)
        {
            double TotalGrossAmount = YearToDate + Gross;
            var TaxRates = new[]
            {
                new[] {-1, 37000, 0.15},
                new[] { 37000, 90000, 0.32},
                new[] { 90000, 180000, 0.37},
                new[] { 180000, 9999999, 0.45},
            };
            foreach (var TaxRate in TaxRates)
            {
                if (TotalGrossAmount > TaxRate[0] && TotalGrossAmount <= TaxRate[1])
                {
                    double Rate = TaxRate[2];
                    double Tax = Gross * Rate;
                    return Tax;
                }
            }
            return 0.0;
        }
    }
}

