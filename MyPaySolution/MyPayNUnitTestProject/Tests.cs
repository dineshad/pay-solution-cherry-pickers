using System;
using NUnit.Framework;
using MyPayProject;
using System.IO;
using System.Collections.Generic;

namespace MyPayNUnitTestProject
{
    public class Tests
    {
        private List<PayRecord> _records;
        private double[] grossArr = {652,418,2202,1104,1797.45};
        private double[] taxArr = {182.45,133.76,754.91,165.60};
        private double[] netArr = {469.55,284.24,1447.09,938.40,1200.31 };
        [SetUp]
        public void Setup()
        {
            _records = CsvImporter.ImportPayRecords("C:/Users/User/source/repos/MyPaySolution/MyPayProject/Import/employee-payroll-data.csv");
        }
        [Test]
        public  void TestImport()
        {
            CollectionAssert.IsNotEmpty(_records);
            Assert.AreEqual(5, _records.Count);
        }
        [Test]
        public void TestGross()
        {
           for (int i=0; i < grossArr.Length; i++)
           {
                Assert.AreEqual(grossArr[i], Math.Round(_records[i].Gross, 2));
           }
        }
        [Test]
        public void TestTax()
        {
            for (int i = 0; i < taxArr.Length; i++)
            {
                Assert.AreEqual(taxArr[i], Math.Round(_records[i].Tax, 2));
            }
        }
        [Test]
        public void TestNet()
        {
            for (int i = 0; i < netArr.Length; i++)
            {
                Assert.AreEqual(netArr[i], Math.Round(_records[i].Net, 2));
            }
        }
        [Test]
        public void TestExport()
        {
            string path = "C:/Users/User/source/repos/MyPaySolution/MyPayProject/Export/";
            string fillename = $"{DateTime.Now.Ticks}-records";
            string file = $"{path}{fillename}.csv";
            PayRecordWriter.Write(file,_records,writeToConsole:false);
            bool exists = File.Exists(Path.GetFullPath(file));
            Assert.IsTrue(exists);
        }
    }
}