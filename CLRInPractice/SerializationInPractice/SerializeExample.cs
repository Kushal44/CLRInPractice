using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BusinessLayer;

namespace SerializationInPractice
{
    public class SerializeExample
    {
        #region Basic Serialization
        //Basic serialization using Binary formatter.
        //public static void SerializeUsingBinaryFormatter()
        //{
        //    List<string> friends = new List<string>() { "Abhi", "Misra", "MJ", "Tak", "Maa", "Pal" };
        //    var serializedStream = Serialize(friends);
        //    serializedStream.Position = 0;

        //    IEnumerable<string> result = (List<string>)Deserialize(serializedStream);

        //    foreach (var r in result)
        //    {
        //        Console.WriteLine("{0}", r);
        //    }
        //}

        //public static void SerializeUsingJsonFormatter()
        //{
        //    var patient = new Person() {
        //        SSN = Guid.NewGuid(),
        //        FirstName = "FName",
        //        LastName = "LName",
        //        Age = 25
        //        //MedicalHistory =  new List<string>() {"Disease1", "Disease2", "Disease3"}
        //    };

        //    var jsonStr = JsonSerializer(patient);

        //    Console.WriteLine(jsonStr);

        //    var deserializedObject = JsonConvert.DeserializeObject<Person>(jsonStr);
        //    deserializedObject.PersonArrived += PersonArrivedHandler;
        //    Console.WriteLine(deserializedObject.ToString());
        //}

        private static Stream Serialize(Object objectGraph)
        {
            using (FileStream fs = new FileStream("Input.dat", FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, objectGraph);
                return fs;
            }
        }

        private static Object Deserialize(Stream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
        }

        private static void PersonArrivedHandler(object sender, EventArgs e)
        {
            Console.WriteLine("Person Arrived.");
        }

        //Basic serialization using JSON formatter
        private static string JsonSerializer(Object objectGraph)
        {
            var jsonString = JsonConvert.SerializeObject(objectGraph);
            return jsonString;
        }

        ////Serialization when new version has a new field.
        //public static void SerializeUsingJsonFormatterWithNewField()
        //{
        //    var patient = new Patient() {
        //        SSN = Guid.NewGuid(),
        //        FirstName = "FName",
        //        LastName = "LName",
        //        Age = 25,
        //        MedicalHistory = new List<string>() { "Disease1", "Disease2", "Disease3" }
        //    };

        //    var jsonStr = JsonSerializer(patient);

        //    Console.WriteLine(jsonStr);

        //    var deserializedObject = JsonConvert.DeserializeObject<PersonV2>(jsonStr);
        //    Console.WriteLine(deserializedObject.ToString());
        //}

        ////Serialization when new version deletes an existing field.
        //public static void SerializeUsingJsonFormatterWithDeleteField()
        //{
        //    var personV2 = new PersonV2() {
        //        SSN = Guid.NewGuid(),
        //        FirstName = "FName",
        //        MiddleName = "MName",
        //        LastName = "LName",
        //        Age = 25
        //    };

        //    var jsonStr = JsonSerializer(personV2);

        //    Console.WriteLine(jsonStr);

        //    var deserializedObject = JsonConvert.DeserializeObject<Person>(jsonStr);
        //    Console.WriteLine(deserializedObject.ToString());
        //}

        ////Serialization when new version change the name of the field

        //public static void SerializeUsingJsonFormatterWithChangeName()
        //{
        //    var person = new Person() {
        //        SSN = Guid.NewGuid(),
        //        FirstName = "FName",
        //        LastName = "LName",
        //        Age = 25
        //    };

        //    var jsonStr = JsonSerializer(person);
        //    Console.WriteLine(jsonStr);

        //    var deserializedObject = JsonConvert.DeserializeObject<PersonV3>(jsonStr);
        //    Console.WriteLine(deserializedObject.ToString());
        //}

        ////Serialization when access modifier of a field is changed from public to private.

        //public static void SerializeUsingJsonFormatterWithAccessModifierChange()
        //{
        //    var patient = new Person() {
        //        SSN = Guid.NewGuid(),
        //        FirstName = "FName",
        //        LastName = "LName",
        //        Age = 25
        //    };

        //    var jsonStr = JsonSerializer(patient);
        //    Console.WriteLine(jsonStr);

        //    var deserializedObject = JsonConvert.DeserializeObject<PersonV4>(jsonStr);
        //    Console.WriteLine(deserializedObject.ToString());
        //}

        ////Serialization when method is added to the type.

        //public static void SerializeUsingJsonFormatterWithNewMethodAdded()
        //{
        //    var patient = new Person() {
        //        SSN = Guid.NewGuid(),
        //        FirstName = "FName",
        //        LastName = "LName",
        //        Age = 25
        //    };

        //    var jsonStr = JsonSerializer(patient);
        //    Console.WriteLine(jsonStr);

        //    var deserializedObject = JsonConvert.DeserializeObject<PersonV5>(jsonStr);
        //    Console.WriteLine(deserializedObject.ToString());
        //    Console.WriteLine("Is Older: {0}", deserializedObject.IsOlderThan(15));
        //}

        #endregion

        #region Deserialize Type From Assembly Not Loaded In AppDomain        
        /// <summary>
        /// Binary formatter stores the Assembly identity which declares the 
        /// Type to be serialized.
        /// It loads the needed assembly to AppDomain before deserialization 
        /// even if assembly is NOT added as Reference in project.
        /// </summary>
        public static void PersistSerializedObjectGraph()
        {
            //****Serialize****//
            //var person = new Person() {
            //    SSN = Guid.NewGuid(),
            //    FirstName = "FName",
            //    LastName = "LName",
            //    Age = 25
            //};

            //Serialize(person);

            
            //****Deserialize****//
            using (var fs = new FileStream("Input.dat", FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                var obj = formatter.Deserialize(fs);
                Console.WriteLine(obj.ToString());
            }
        }

        public static void PersistSerializedObjectGraphInJSON()
        {
            //var person = new Person() {
            //    SSN = Guid.NewGuid(),
            //    FirstName = "FName",
            //    LastName = "LName",
            //    Age = 25
            //};

            //var jsonStr = JsonSerializer(person);
            //File.WriteAllText("Input.json", jsonStr);


            var person = JsonConvert.DeserializeObject<Person>(
                            File.ReadAllText("Input.json"));
            Console.WriteLine(person.ToString());
        }

        #endregion
    }
}
