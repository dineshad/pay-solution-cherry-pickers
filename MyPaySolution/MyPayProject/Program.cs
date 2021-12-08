using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Globalization;
namespace MyPayProject
{
    class Program
    {
        /// <summary>
        /// Calls class to import the CSV file and the clalss to write the created payrecords to to new CSV file snd optionslly to the console. 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            List<PayRecord> PayrecordList = CsvImporter.ImportPayRecords("C:/Users/User/source/repos/MyPaySolution/MyPayProject/Import/employee-payroll-data.csv");
            
            string path = "C:/Users/User/source/repos/MyPaySolution/MyPayProject/Export/";            
            string fillename = $"{DateTime.Now.Ticks}-records";
            string file = $"{path}{fillename}.csv";
            PayRecordWriter.Write(file, PayrecordList,true); 
        }
    }
}
