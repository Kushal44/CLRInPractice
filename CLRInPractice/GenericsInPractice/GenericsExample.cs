using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using Newtonsoft.Json;

namespace GenericsInPractice
{
    /// <summary>
    /// Generics is used to define "of" relationship. Like List of T (type parameter), Dictionary etc.
    /// It is also used by the "Provider" class to instantiate a object and provider it for usage
    /// For example Activator.CreateInstance of T.
    /// 
    /// Usually we create a class which provides the functionality. Like Patient.Save()
    /// Generics is meant for "Algorithm Reusability". Generic class is about the functionality which is common across disparate objects.
    /// For example, Generic List can store object of any type, say, Repository.Save(T), where T can be any object.
    /// </summary>
    /// <remarks>
    /// The example here shows the use of Generics in deserialization of JSON to strongly typed objects. 
    /// </remarks>
    public class GenericsExample
    {
        public static void DisplayPerson()
        {
            var person = PersonProvider();
            Console.WriteLine(person.ToString());
        }

        private static Person PersonProvider()
        {
            var inputJSON = "{}";
            var person = Read<Person>(inputJSON);
            return person;
        }

        private static T Read<T>(string jsonString) where T : class
        {
            var data = default(T);
            data = JsonConvert.DeserializeObject<T>(jsonString);
            return data;
        }
    }

    internal class Base
    {
        public virtual void M<T1, T2>()
            where T1: struct
            where T2: class
        {
        }
    }

    internal sealed class Derived : Base
    {
        //You can not change the constraints in the override method, can change the name of type parameter.
        //public override void M<T3, T4>()
        //    where T3: EventArgs
        //    where T4: class
        //{
        //}
    }
}
