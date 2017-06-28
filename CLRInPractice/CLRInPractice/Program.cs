using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelegatesInPractice;
using GenericsInPractice;
using InterfaceInPractice;
using ReflectionInPractice;
using SerializationInPractice;

namespace CLRInPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Delegate In Practice

            //Presenter p = new Presenter();
            //p.PerformUsingSyntacticSugar();

            //AClass.UsingLocalVariablesInCallback(10);

            //AClass.UsingLocalVariablesWithoutLambda(10);

            //GenericsExample.DisplayPerson();

            #endregion

            #region Event In Practice

            //EventsInPractice.Perform();
            //EventsInPractice.PerfromUnregisterNonListener();

            //EventsInPractice.PerformWithEmailWithOverrideForEvent();

            #endregion

            #region Reflection In Practice

            //ReflectionExample.ShowPublicTypes();

            //ReflectionExample.CreatingDynamicExtensibleApplication();

            //ReflectionExample.CallingMembersOfTypeUsingReflection();

            //ReflectionExample.CallingMembersOfTypeUsingDynamic();
            #endregion

            #region Serialization In Practice

            //SerializeExample.SerializeUsingBinaryFormatter();

            //SerializeExample.SerializeUsingJsonFormatter();

            //SerializeExample.SerializeUsingJsonFormatterWithNewField();

            //SerializeExample.SerializeUsingJsonFormatterWithDeleteField();

            //SerializeExample.SerializeUsingJsonFormatterWithAccessModifierChange();

            //SerializeExample.SerializeUsingJsonFormatterWithNewMethodAdded();

            //SerializeExample.PersistSerializedObjectGraph();

            //SerializeExample.PersistSerializedObjectGraphInJSON();
            #endregion

            #region Interface In Practice

            //InterfaceExample.InterfaceWithInheritanceImplementationForIDisposable();
            //InterfaceExample.ExternalInterfaceImplementationMethod();

            #endregion

            Console.Read();
        }
    }
}
