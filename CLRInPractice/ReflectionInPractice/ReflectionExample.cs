using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;

namespace ReflectionInPractice
{
    public class ReflectionExample
    {
        public static void ShowPublicTypes()
        {
            string dataAssembly = "System.Data, version=4.0.0.0, " +
                                    "culture=neutral, PublicKeyToken=b77a5c561934e089";
            LoadAssemblyToShowPublicTypes(dataAssembly);
        }

        private static void LoadAssemblyToShowPublicTypes(string assembly)
        {
            //Assembly.Load needs a full qualified name for assembly to load.
            //It looks for assembly in GAC, then application base directory
            //Assembly.LoadFrom allows to specify the path of assembly to load. 
            Assembly a = Assembly.Load(assembly);

            foreach(Type t in a.ExportedTypes)
            {
                Console.WriteLine(t.FullName);
            }
        }

        /// <summary>
        /// It explains the way to create dynamically extensible applications. 
        ///1. Plug-ins implement the Interface. 
        ///2. Types implementing the interface are loaded using reflection.
        ///3. Then Types are initialized using reflection.
        ///4. Explicit casting to Interface to make the object strongly typed.
        ///5. Now any public member of object can be called.
        /// </summary>
        public static void CreatingDynamicExtensibleApplication()
        {
            string plugInDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            var pluInAssemblies = Directory.EnumerateFiles(plugInDir, "Business*.dll");
           
            var plugInTypes = from file in pluInAssemblies
                              let assembly = Assembly.LoadFrom(file)
                              from t in assembly.ExportedTypes
                              where t.IsClass && typeof(ICricketTeam).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo())
                              select t;

            foreach (Type t in plugInTypes)
            {
                var team = (ICricketTeam)Activator.CreateInstance(t);

                foreach (var player in team.Players)
                {
                    Console.Write("{0} ", player);
                }
                Console.WriteLine();
                Console.WriteLine("**********************************");

                team.Play();
                team.DoCommercials();
            }

        }

        /// <summary>
        /// It explains the use of reflection to discover and invoke the type's member. Unlike
        /// example above it is NOT type safe.
        /// 
        /// System.Object
        ///     -> System.Reflection.MemberInfo
        ///         -> System.TypeInfo (Nested types)
        ///         -> System.FieldInfo
        ///         -> System.MethodBase
        ///             -> System.Reflection.ConstructionInfo
        ///             -> System.Reflection.MethodInfo
        ///         -> System.Reflection.PropertyInfo
        ///         -> System.Reflection.EventInfo
        /// </summary>
        public static void CallingMembersOfTypeUsingReflection()
        {
            Assembly assm = Assembly.LoadFrom(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), 
                                                "BusinessLayer.dll"));
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach(Assembly a in assemblies)
            {
                Console.WriteLine(a.FullName);
            }
                
            foreach(Type t in assm.ExportedTypes)
            {
                if (t.IsClass)
                {
                    string typeName = t.FullName;

                    //****Invoke constructor to create the initialize object.****
                    ConstructorInfo ctor = t.GetTypeInfo().DeclaredConstructors.First();
                    Object obj = ctor.Invoke(null);

                    //****Use Activator.CreateInstance to initialize the object.**** 
                    //Object obj = Activator.CreateInstance(t);


                    //GetDeclaredProperty
                    PropertyInfo prop = t.GetTypeInfo().GetDeclaredProperty("BoardMembers");
                    if (prop.CanRead)
                    {
                        var boardMembers = (IEnumerable<string>)prop.GetValue(obj);
                    }


                    //GetDeclaredMethod
                    MethodInfo methodInfo = t.GetTypeInfo().GetDeclaredMethod("PublishNewRules");
                    methodInfo.Invoke(obj, null);


                    //GetDeclaredEvent
                    //EventHandler added to event.
                    EventInfo ei = t.GetTypeInfo().GetDeclaredEvent("OnNewScheduleRelease");
                    EventHandler<CricketScheduleEventArgs> eh = 
                        new EventHandler<CricketScheduleEventArgs>(NewScheduleHandler);
                    ei.AddEventHandler(obj, eh);

                    //Calling method having parameters.
                    Object[] args = new Object[] { 2017 };
                    methodInfo = t.GetTypeInfo().GetDeclaredMethod("PublishSchedule");
                    methodInfo.Invoke(obj, args);
                }
            }
        }

        /// <summary>
        /// This methods shows calling the instance method using the object
        /// of type "dynamic".
        /// You don't need GetDeclaredMethod for that.
        /// </summary>
        public static void CallingMembersOfTypeUsingDynamic()
        {
            Assembly assm = Assembly.LoadFrom(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                                               "BusinessLayer.dll"));
            Type t = assm.ExportedTypes.First();

            if (t.IsClass)
            {
                //dynamic obj = Activator.CreateInstance(t);
                //obj.PublishNewRules();

                //obj.PublishSchedule(2017);

                var obj = Activator.CreateInstance<IccBoard>();
                obj.PublishNewRules();
            }
        }

        private static void NewScheduleHandler(object sender, CricketScheduleEventArgs eventArgs)
        {
            Console.WriteLine(eventArgs.TestMatchSchedule);
            Console.WriteLine(eventArgs.OdiSchdeule);
            Console.WriteLine(eventArgs.T20Schedule);
        }
    }
}
