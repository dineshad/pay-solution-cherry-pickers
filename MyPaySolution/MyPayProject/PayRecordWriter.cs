using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper;
using System.IO;
using System.Globalization;

namespace MyPayProject
{
    public static class PayRecordWriter
    {
        /// <summary>
        /// Write to the CSV file and optionally to the console.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="records"></param>
        /// <param name="writeToConsole"></param>
        public static void Write(string file, List<PayRecord> records, bool writeToConsole = true)
        {
            using (var writer = new StreamWriter(file))
            using (var writeCSV = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                foreach (PayRecord record in records)
                {
                    writeCSV.WriteField(record.Id);
                    writeCSV.WriteField(string.Format("{0:0.00}",record.Gross));
                    writeCSV.WriteField(string.Format("{0:0.00}",record.Tax));
                    writeCSV.WriteField(string.Format("{0:0.00}",record.Net));
                    writeCSV.NextRecord();
                }
            }

            if (writeToConsole == true)
            {
                foreach (PayRecord record in records)
                {
                    Console.Write(record.GetDetails());
                }
            }
        }
    }
}
