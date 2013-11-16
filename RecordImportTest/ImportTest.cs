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
        
        [TestMethod]
        public void CanSortRecordsIntoTree_GivenRecordsFile_ShouldReturnSortedTree()
        {
            //---------------Set up test pack-------------------
            var allLines = Import.GetAllRecordFileLines();
            //---------------Assert Precondition----------------
            Assert.IsTrue(allLines.Length > 0);
            //---------------Execute Test ----------------------
            var properties = Import.GetRecordProperties(allLines, 0);
            var allRecords = Import.LoadAllRecordsIntoObjects(allLines, properties);

            var personsTree = new BinaryTree<Person>(allRecords.ToArray());
            //personsTree.SortingProperties = new List<string>() {PersonElements.Surname, PersonElements.FirstName};
            //BinaryTree<Person> sortedTree = personsTree.Sort();
            //---------------Test Result -----------------------
            var sortedPersonList = new List<Person>();
            var treeIterator = personsTree.iterator();
            while (treeIterator.MoveNext())
            {
                var person = treeIterator.Current;
                sortedPersonList.Add(person);
            }
            Assert.AreEqual("Zunkel", sortedPersonList[49997].GetValueByProperty("Surname"));
            Assert.AreEqual("Nj", sortedPersonList[49997].GetValueByProperty("FirstName"));
            Assert.AreEqual("45", sortedPersonList[49997].GetValueByProperty("Age"));

            Assert.AreEqual("Zunkel", sortedPersonList[49988].GetValueByProperty("Surname"));
            Assert.AreEqual("Aindrea", sortedPersonList[49988].GetValueByProperty("FirstName"));
            Assert.AreEqual("69", sortedPersonList[49988].GetValueByProperty("Age"));

            Assert.AreEqual("Zissem", sortedPersonList[49987].GetValueByProperty("Surname"));
            Assert.AreEqual("Jori", sortedPersonList[49987].GetValueByProperty("FirstName"));
            Assert.AreEqual("56", sortedPersonList[49987].GetValueByProperty("Age"));
        }

        [TestMethod]
        public void CanSortRecordsBySurnameFirstName_GivenRecordsFile_ShouldReturnDuplicateKeyException()
        {
            //---------------Set up test pack-------------------
            var allLines = Import.GetAllRecordFileLines();
            //---------------Assert Precondition----------------
            Assert.IsTrue(allLines.Length > 0);
            //---------------Execute Test ----------------------
            var properties = Import.GetRecordProperties(allLines, 0);
            var allRecords = Import.LoadAllRecordsIntoObjects(allLines, properties);

            var personsTree = new BinaryTree<Person>(allRecords.ToArray());
            personsTree.SortingProperties = new List<string>() {PersonElements.Surname, PersonElements.FirstName};
            bool didRaiseException = false;
            try
            {
                BinaryTree<Person> sortedTree = personsTree.Sort();
            }
            catch (DuplicateKeyException exception)
            {
                didRaiseException = true;
                Debug.WriteLine(exception.Message);
            }
            //---------------Test Result -----------------------
            Assert.AreEqual(true, didRaiseException);
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

            var personsTree = new BinaryTree<Person>(allRecords.ToArray());
            //---------------Test Result -----------------------
            Assert.IsTrue(personsTree.getSize() == 49998);
        }
    }
}
