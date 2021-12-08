using System;
using System.Collections.Generic;
using System.Text;

namespace MyPayProject
{
    public abstract class PayRecord
    {
        protected double[] _hours;
        protected double[] _rates;

        /// <summary>
        /// Constructor for when creating pay records for residents.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hours"></param>
        /// <param name="rates"></param>
        public PayRecord(int id, double[] hours, double[] rates)
        {
            _hours = hours;
            _rates = rates;
            Id = id;
        }
        /// <summary>
        /// Constructor for when creating pay records for Working Holiday Payrecords
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hours"></param>
        /// <param name="rates"></param>
        /// <param name="visa"></param>
        /// <param name="yearToDate"></param>
        public PayRecord(int id, double[] hours, double[] rates, int visa, int yearToDate)
        {
            _hours = hours;
            _rates = rates;
            Id = id;
           
        }
        /// <summary>
        /// Getting and Setting Id
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Getting and Setting Gross
        /// </summary>
        public double Gross
        {
            get
            {
                double Hours = 0.0;
                double Rates = 0.0;
                double Gross = 0.0;
                for (int i = 0; i < _hours.Length; i++)
                {
                    Hours = _hours[i];
                    Rates = _rates[i];
                    Gross += Hours * Rates;
                }
                return Gross;
            }
        }
        /// <summary>
        /// Tax will be set in child classes
        /// </summary>
        public abstract double Tax { get; }
        /// <summary>
        /// Getting and Setting Net Value
        /// </summary>
        public double Net
        {
            get 
            {
                return Gross - Tax;
            }
        }
        /// <summary>
        /// Returns the details to be displayed on the console.
        /// </summary>
        /// <returns></returns>
        public virtual string GetDetails()
        {
            
            return $"------- EMPLOYEE: {Id} ------------\nGROSS:\t${Gross:0,0.00}\nTAX:\t${Tax:0,0.00}\nNet:\t${Net:0,0.00}\n\n";
        }
        
    }
}
