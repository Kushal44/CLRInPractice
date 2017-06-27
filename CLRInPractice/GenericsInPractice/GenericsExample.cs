﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
