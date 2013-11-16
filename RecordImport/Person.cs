using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RecordImport.BinarySearchTree;
using RecordImport.Common;

namespace RecordImport
{
    public class Person : IComparable<Person>, ISortable
    {
        private List<string> Values { get; set; }
        private List<string> Properties { get; set; }
        public List<KeyValuePair<string, string>> Data { get; private set; }
        public List<string> SortingProperties { get; set; }

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
            if (propertyKeyValue.Equals(null))
                return false;
            return propertyKeyValue.Value.ToUpper() == value.ToUpper();
        }

        public string GetValueByProperty(string property)
        {
            var propertyKeyValue = Data.FirstOrDefault(p => p.Key == property);
            return propertyKeyValue.Equals(null) ? null : propertyKeyValue.Value;
        }
        

        private bool CompareGreaterThanProperties(Person targetObject, string propertyElement)
        {
            return GetValueByProperty(propertyElement).CompareTo(targetObject.GetValueByProperty(propertyElement)) > 0;
        }

        private bool CompareEqualProperties(Person targetObject, string propertyElement)
        {
            return Equals(GetValueByProperty(propertyElement), targetObject.GetValueByProperty(propertyElement));
        }

        public int CompareTo(Person targetOBject)
        {
            if (targetOBject == null)
                return 1;

            bool targetGreaterThan = CheckSelfGreaterThan(targetOBject);

            bool targetEqual = CheckSelfEqualTo(targetOBject);

            if (targetGreaterThan)
            {
                return 1;
            }
            if (targetEqual)
            {
                return 0;
            }

            return -1;

            //bool surnameIsGreater = CompareGreaterThanProperties(targetOBject, PersonElements.Surname);
            //bool firstNameIsGreater = CompareGreaterThanProperties(targetOBject, PersonElements.FirstName);
            //bool ageIsGreater = CompareGreaterThanProperties(targetOBject, PersonElements.Age);



            //bool surnameIsEqual = CompareEqualProperties(targetOBject, PersonElements.Surname);
            //bool firstNameIsEqual = CompareEqualProperties(targetOBject, PersonElements.FirstName);
            //bool ageIsEqual = CompareEqualProperties(targetOBject, PersonElements.Age);


            //if (surnameIsEqual &&
            //    firstNameIsEqual &&
            //    ageIsEqual)
            //    return 0;

            //if (surnameIsGreater)
            //    return 1;

            //if (surnameIsEqual &&
            //    firstNameIsGreater)
            //    return 1;

            //if (surnameIsEqual &&
            //    firstNameIsEqual &&
            //    ageIsGreater)
            //    return 1;
        }

        private bool CheckSelfGreaterThan(Person targetOBject)
        {
            bool isGreater;
            bool isEqual;
            int sortingIndex = 0;
            do
            {
                isEqual = CompareEqualProperties(targetOBject, SortingProperties[sortingIndex]);
                if (!isEqual)
                {
                    isGreater = CompareGreaterThanProperties(targetOBject, SortingProperties[sortingIndex]);
                    return isGreater;
                }

                sortingIndex++;
            } while (isEqual && sortingIndex < SortingProperties.Count);
            return false;
        }

        private bool CheckSelfEqualTo(Person targetOBject)
        {
            if (SortingProperties.Select(sortingProperty => CompareEqualProperties(targetOBject, sortingProperty)).Any(isEqual => !isEqual))
            {
                return false;
            }
            return true;
        }

        public void SetSortingProperties(List<string> sortingProperties)
        {
            this.SortingProperties = sortingProperties;
        }
    }
}