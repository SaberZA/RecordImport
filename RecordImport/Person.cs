using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RecordImport.Common;

namespace RecordImport
{
    public class Person : IComparable<Person>
    {
        private List<string> Values { get; set; }
        private List<string> Properties { get; set; }
        public List<KeyValuePair<string, string>> Data { get; private set; }

        public Person(IEnumerable<string> properties, IEnumerable<string> values)
        {
            Properties = properties.ToList();
            Values = values.ToList();
            Data = new List<KeyValuePair<string, string>>();
            
            for (int index = 0; index < Properties.Count; index++)
            {
                var property = Properties[index];
                var value = Values[index];
                Data.Add(new KeyValuePair<string, string>(property, value));
            }
        }

        public bool Has(string property, string value)
        {
            var propertyKeyValue = Data.FirstOrDefault(p => p.Key == property);

            return propertyKeyValue.Value.ToUpper() == value.ToUpper();
        }

        public string GetValueByProperty(string property)
        {
            var propertyKeyValue = Data.FirstOrDefault(p => p.Key == property);

            return propertyKeyValue.Value;
        }
        

        private bool CompareGreaterThanProperties(Person targetObject, string propertyElement)
        {
            return GetValueByProperty(propertyElement).CompareTo(targetObject.GetValueByProperty(propertyElement)) > 0;
        }

        private bool CompareEqualProperties(Person targetObject, string propertyElement)
        {
            return Equals(GetValueByProperty(propertyElement), targetObject.GetValueByProperty(propertyElement));
        }

        public int CompareTo(Person other)
        {
            if (other == null)
                return 1;

            bool surnameIsGreater = CompareGreaterThanProperties(other, PersonElements.Surname);
            bool firstNameIsGreater = CompareGreaterThanProperties(other, PersonElements.FirstName);
            bool ageIsGreater = CompareGreaterThanProperties(other, PersonElements.Age);

            bool surnameIsEqual = CompareEqualProperties(other, PersonElements.Surname);
            bool firstNameIsEqual = CompareEqualProperties(other, PersonElements.FirstName);
            bool ageIsEqual = CompareEqualProperties(other, PersonElements.Age);


            if (surnameIsEqual &&
                firstNameIsEqual &&
                ageIsEqual)
                return 0;

            if (surnameIsGreater)
                return 1;

            if (surnameIsEqual &&
                firstNameIsGreater)
                return 1;

            if (surnameIsEqual &&
                firstNameIsEqual &&
                ageIsGreater)
                return 1;

            return -1;
        }
    }
}