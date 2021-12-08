using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Globalization;



namespace MyPayProject
{
    
    public class CsvImporter
    {
        /// <summary>
        /// Opens the specified file by name using a StreamReader class from the reusable System.IO component
        /// Iterates through each line(row) in the file. 
        /// Determines when all the rows for an employee have been read and instantiates a new instance of a resident or working holiday pay record, as appropriate.
        /// Add each PayRecord to a list
        /// </summary>
        /// <param name="file"></param>
        /// <returns>List<PayRecord></returns>
        public static List<PayRecord> ImportPayRecords(string file)
        {          
            List<PayRecord> payRecords = new List<PayRecord>();
            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                List<int> IdList = new List<int>();
                Dictionary<int, List<double>> hoursWorkedDic = new Dictionary<int, List<double>>();
                Dictionary<int, List<double>> hourlyRateDic = new Dictionary<int, List<double>>();
                Dictionary<int, string> visasDic = new Dictionary<int, string>();
                Dictionary<int, string> yearToDatesDic = new Dictionary<int, string>();

                while (csv.Read())
                {
                    string textId = csv.GetField(0);
                    string textHours = csv.GetField(1);
                    string textRate = csv.GetField(2);
                    string textVisa = csv.GetField(3);
                    string textYearToDate = csv.GetField(4);

                    // do something with the record.
                    int id = int.Parse(textId);
                    double hours = double.Parse(textHours);
                    double rate = double.Parse(textRate);
                    if (!IdList.Contains(id))
                    { 
                        IdList.Add(id);
                    }
                    visasDic.TryAdd(id, textVisa);
                    yearToDatesDic.TryAdd(id, textYearToDate);
                    TryAddHoursWorked(id, hours, hoursWorkedDic);
                    TryAddHourlyRate(id, rate, hourlyRateDic);  
                }
                foreach(int IdNumber in IdList)
                {
                    double[] EmpHoursArr = hoursWorkedDic[IdNumber].ToArray();
                    double[] EmpRatesArr = hourlyRateDic[IdNumber].ToArray();
                    payRecords.Add(CreatePayRecord(IdNumber, EmpHoursArr, EmpRatesArr, visasDic[IdNumber], yearToDatesDic[IdNumber]));
                }
                


            }            
            return payRecords;
        }
        /// <summary>
        /// Creates a PayRecord for each employee by calling either ResidentPayRecord or WorkingHolidayPayRecord class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hours"></param>
        /// <param name="rates"></param>
        /// <param name="visa"></param>
        /// <param name="yearToDate"></param>
        /// <returns></returns>
        public static PayRecord CreatePayRecord(int id, double[] hours, double[] rates, string visa, string yearToDate)
        {
            if (visa == "" || yearToDate == "")
            {
                ResidentPayRecord residentPayRecord = new ResidentPayRecord(id, hours, rates);
                return residentPayRecord;
            }
            else
            {
                int VisaCategory;
                Int32.TryParse(visa, out VisaCategory);
                int YearToDateValue;
                Int32.TryParse(yearToDate, out YearToDateValue);
                WorkingHolidayPayRecord workingHolidayPayRecord = new WorkingHolidayPayRecord(id, hours, rates, VisaCategory, YearToDateValue);
                return workingHolidayPayRecord;
            }
        }
        /// <summary>
        /// Add a new hour item by creating a new hours list or adding to the existing hour list of each employee.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hours"></param>
        /// <param name="hoursWorkedDic"></param>
        private static void TryAddHoursWorked(int id, double hours, Dictionary<int,List<double>> hoursWorkedDic)
        {
            List<double> hoursList;
            if (hoursWorkedDic.TryGetValue(id,out hoursList))
            {
                hoursList.Add(hours);
            }
            else
            {
                hoursList = new List<double>();
                hoursList.Add(hours);
                hoursWorkedDic.Add(id, hoursList);
            }            
        }
        /// <summary>
        /// Add a new rate item by creating a new rates list or adding to the existing rates list of each employee.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rates"></param>
        /// <param name="hourlyRateDic"></param>
        private static void TryAddHourlyRate(int id, double rates, Dictionary<int, List<double>> hourlyRateDic)
        {
            List<double> ratesList;
            if (hourlyRateDic.TryGetValue(id, out ratesList))
            {
                ratesList.Add(rates);
            }
            else
            {
                ratesList = new List<double>();
                ratesList.Add(rates);
                hourlyRateDic.Add(id, ratesList);
            }
        }
    }
}



