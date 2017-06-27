using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace BusinessLayer
{
    //[JsonObject(MemberSerialization.OptIn)]
    //[JsonObject]
    [Serializable]
    public class Person
    {
        public Guid SSN { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public int Age { get; set; }

        public override string ToString()
        {
            OnPersonArrived();
            return string.Format("SSN: {0}, First:{1}, Last:{2}, Age:{3}",
                SSN, FirstName,
                LastName, Age);
        }

        public event EventHandler PersonArrived;

        protected virtual void OnPersonArrived()
        {
            var temp = PersonArrived;
            if (temp != null)
            {
                temp(this, null);
            }
        }
    }

    [JsonObject]
    public class PersonV2
    {
        public Guid SSN { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        
        public string LastName { get; set; }

        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("SSN: {0}, First:{1}, Middle:{2}, Last:{3}, Age:{4}",
                SSN, FirstName, MiddleName,
                LastName, Age);
        }

        [OnDeserialized]
        private void OnDeserialzed(StreamingContext context)
        {
            if (string.IsNullOrEmpty(MiddleName))
            {
                MiddleName = "Dhanush";
            }
        }
    }

    [JsonObject]
    public class PersonV3
    {
        public Guid SSN { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("SSN: {0}, First:{1}, Last:{2}, Age:{3}",
                SSN, First_Name,
                Last_Name, Age);
        }
    }

    [JsonObject]
    public class PersonV4
    {
        private Guid SSN { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("SSN: {0}, First:{1}, Last:{2}, Age:{3}",
                SSN, FirstName,
                LastName, Age);
        }
    }

    [JsonObject]
    public class PersonV5
    {
        public Guid SSN { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("SSN: {0}, First:{1}, Last:{2}, Age:{3}",
                SSN, FirstName,
                LastName, Age);
        }

        public bool IsOlderThan(int age)
        {
            return Age > age;
        }
    }

    public class Patient : Person
    {
        public List<string> MedicalHistory;

        public override string ToString()
        {
            return string.Format("SSN: {0}, First:{1}, Last:{2}, Age:{3}, History: {4}",
                SSN, FirstName,
                LastName, Age,
                string.Concat(MedicalHistory[0], MedicalHistory[1], MedicalHistory[2]));
        }
    }
}
