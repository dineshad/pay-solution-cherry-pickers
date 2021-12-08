using System;
using System.Collections.Generic;
using System.Text;

namespace MyPayProject
{
    public class WorkingHolidayPayRecord : PayRecord
    {
        /// <summary>
        /// Constructor for WorkingHolidayPayRecord
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hours"></param>
        /// <param name="rates"></param>
        /// <param name="visa"></param>
        /// <param name="yearToDate"></param>
        public WorkingHolidayPayRecord(int id, double[] hours, double[] rates, int visa, int yearToDate) : base(id, hours, rates)
        {
            Visa = visa;
            YearToDate = yearToDate + Convert.ToInt32(Gross);
        }
        public int Visa { get; private set; }
        public int YearToDate { get; private set; }
        
        /// <summary>
        /// Sets Tax
        /// </summary>
        public override double Tax
        {
            get
            {
                return TaxCalculator.CalculateWorkingHolidayTax(Gross,YearToDate);
            }
        }
        /// <summary>
        /// Creates the output to print on the console.
        /// </summary>
        /// <returns>string</returns>
        public override string GetDetails()
        {
            return $"------- EMPLOYEE: {Id} ------------\nGROSS:\t${Gross:0,0.00}\nTAX:\t${Tax:0,0.00}\nNet:\t${Net:0,0.00}\nVisa:\t{Visa:0,0}\nYTD:\t${YearToDate:0.00}\n\n";
        }
    }
}
