using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RecordImport
{
    public class Import
    {
        public static string FilePath { get; private set; }
        public static string PropertyDelimiter { get; private set; }

        public Import(string filePath, string propertyDelimiter)
        {
            FilePath = filePath;
            PropertyDelimiter = propertyDelimiter;
        }

        public string[] GetAllRecordFileLines()
        {
            string[] allLines;
            using (var streamReader = new StreamReader(FilePath))
                allLines = streamReader.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            return allLines;
        }

        public string[] GetRecordProperties(string[] allLines, int lineIndex)
        {
            return allLines[lineIndex].Split(new[] { PropertyDelimiter },StringSplitOptions.RemoveEmptyEntries);
        }

        

        public IEnumerable<Person> LoadAllRecordsIntoObjects(string[] allLines, string[] properties)
        {
            List<Person> allRecords = new List<Person>();
            for (int index = 1; index < allLines.Length; index++)
            {
                var values = GetRecordProperties(allLines, index);
                var recordObject = new Person(properties, values);
                allRecords.Add(recordObject);
            }
            return allRecords;
        }
    }
}
