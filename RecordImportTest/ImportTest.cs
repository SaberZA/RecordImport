using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecordImport;
using RecordImport.BinarySearchTree;
using RecordImport.Common;

namespace RecordImportTest
{
    [TestClass]
    public class ImportTest
    {
        private const string _filePath = @"C:\Apps\RecordImport\records.txt";
        private Import Import = new Import(_filePath,",");

        [TestMethod]
        public void CanFindRecordsFile_ShouldReturnTrue()
        {
            Assert.IsTrue(File.Exists(_filePath));
        }

        [TestMethod]
        public void CanReadDataFromRecordsFile_GivenRecordsFile_ShouldReturnOver150Records()
        {
            //---------------Set up test pack-------------------
            
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var allLines = Import.GetAllRecordFileLines();
            //---------------Test Result -----------------------
            Assert.IsTrue(allLines.Length > 150);
        }

        [TestMethod]
        public void CanGetPropertiesFromRecordsFile_GivenRecordsFile_ShouldReturnListOfStringProperties()
        {
            //---------------Set up test pack-------------------
            var allLines = Import.GetAllRecordFileLines();
            //---------------Assert Precondition----------------
            Assert.IsTrue(allLines.Length > 0);
            //---------------Execute Test ----------------------
            var properties = Import.GetRecordProperties(allLines, 0);
            
            //---------------Test Result -----------------------
            Assert.AreEqual(PersonElements.FirstName.ToUpper(),properties[0].ToUpper());
            Assert.AreEqual(PersonElements.Surname.ToUpper(),properties[1].ToUpper());
            Assert.AreEqual(PersonElements.Age.ToUpper(),properties[2].ToUpper());
        }
        
        [TestMethod]
        public void CanCreateRecordsObjectFromRecordsText_GivenRecordsFile_ShouldReturnListOfRecordObjects()
        {
            //---------------Set up test pack-------------------
            var allLines = Import.GetAllRecordFileLines();
            //---------------Assert Precondition----------------
            Assert.IsTrue(allLines.Length > 0);
            //---------------Execute Test ----------------------
            var properties = Import.GetRecordProperties(allLines, 0);
            var allRecords = Import.LoadAllRecordsIntoObjects(allLines, properties);
            //---------------Test Result -----------------------
            var recordMaureenBlades44 = allRecords.FirstOrDefault(p => p.Has(PersonElements.FirstName, "Maureen") 
                && p.Has(PersonElements.Surname, "Blades") 
                && p.Has(PersonElements.Age, "44"));
            Assert.IsTrue(recordMaureenBlades44 != null);
            Assert.AreEqual("Blades",recordMaureenBlades44.GetValueByProperty(PersonElements.Surname));
            Assert.AreEqual("44", recordMaureenBlades44.GetValueByProperty(PersonElements.Age));
        }

        [Ignore] //TODO Van Der Merwe, Steven 15 Nov 2013: Ignored Test - BubbleSort taking too long
        [TestMethod]
        public void CanSortAllRecordsWithCompareTo_GivenRecordsFile_ShouldReturnBubbleSortedList()
        {
            //---------------Set up test pack-------------------
            var allLines = Import.GetAllRecordFileLines();
            //---------------Assert Precondition----------------
            Assert.IsTrue(allLines.Length > 0);
            //---------------Execute Test ----------------------
            var properties = Import.GetRecordProperties(allLines, 0);
            var allRecords = Import.LoadAllRecordsIntoObjects(allLines, properties);

            var sortedRecords = ApplyBubbleSort(allRecords).ToList();
            var greatestPerson = sortedRecords[sortedRecords.Count - 1];
            var lowestPerson = sortedRecords[0];
            
            //---------------Test Result -----------------------
            
            Assert.Fail("Test Not Yet Implemented");
        }

        [TestMethod]
        public void CanStoreRecordsInBinarySearchTree_GivenRecordsFile_ShouldReturnPersonsBinaryTree()
        {
            //---------------Set up test pack-------------------
            var allLines = Import.GetAllRecordFileLines();
            //---------------Assert Precondition----------------
            Assert.IsTrue(allLines.Length > 0);
            //---------------Execute Test ----------------------
            var properties = Import.GetRecordProperties(allLines, 0);
            var allRecords = Import.LoadAllRecordsIntoObjects(allLines, properties);

            BinaryTree<Person> personsTree = new BinaryTree<Person>(allRecords.ToArray());
            //---------------Test Result -----------------------
            Assert.IsTrue(personsTree.getSize() == 49998);
        }

        [TestMethod]
        public void ApplyBubbleSort_GivenRandomNumbers_ShouldReturnSortedNumbers()
        {
            //---------------Set up test pack-------------------
            int[] list = {4, 3, 5, 9, 12, 1, 7, 8, 11};
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            ApplyBubbleSort(list);
            //---------------Test Result -----------------------
            foreach (int i in list)
                Debug.Write(i + ", ");

            Assert.IsTrue(list[0] == 1);
            Assert.IsTrue(list[8] == 12);
        }

        

        private IEnumerable<Person> ApplyBubbleSort(IEnumerable<Person> allRecords)
        {
            bool needNextPass = true;

            var enumerableRecords = allRecords as IList<Person> ?? allRecords.ToList();
            for (int k = 1; k < enumerableRecords.Count && needNextPass; k++)
            {
                needNextPass = false;
                for (int i = 0; i < enumerableRecords.Count - k; i++)
                {
                    if (enumerableRecords[i].CompareTo(enumerableRecords[i+1]) > 0)
                    {
                        Person temp = enumerableRecords[i];
                        enumerableRecords[i] = enumerableRecords[i + 1];
                        enumerableRecords[i + 1] = temp;

                        needNextPass = true;
                    }
                }
            }
            return enumerableRecords;
        }

        private IEnumerable<int> ApplyBubbleSort(IEnumerable<int> allRecords)
        {
            bool needNextPass = true;

            var enumerableRecords = allRecords as IList<int> ?? allRecords.ToList();
            for (int k = 1; k < enumerableRecords.Count && needNextPass; k++)
            {
                needNextPass = false;
                for (int i = 0; i < enumerableRecords.Count - k; i++)
                {
                    if (enumerableRecords[i].CompareTo(enumerableRecords[i + 1]) > 0)
                    {
                        int temp = enumerableRecords[i];
                        enumerableRecords[i] = enumerableRecords[i + 1];
                        enumerableRecords[i + 1] = temp;

                        needNextPass = true;
                    }
                }
            }
            return enumerableRecords;
        }
    }
}
