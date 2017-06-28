using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceInPractice
{
    //An example to show basic usage of Interface
    //An example to show Interface and Inheritance
    //An example to show implementing multiple Interfaces
    //An example to show External implementation of interface.
    //An example to show use of Generics in Interface. Please look into GenericsInPractice for that.
    public class InterfaceExample
    {
        public static void InterfaceWithInheritanceImplementationForIDisposable()
        {
            using(var managedPtr = new ManagedWrapper())
            {
                //Unmanaged code is passed managedPtr.Ptr
            }

            using(var derived = new DerivedManagedWrapper())
            {
                ExternalInterfaceImplementation eimi = new ExternalInterfaceImplementation();
                //Can NOT call eimi.Version.
                IVersioning iVer = eimi;
                Console.WriteLine(iVer.Version);
            }
        }

        public static void ExternalInterfaceImplementationMethod()
        {

        }
    }

    #region Interface and Inheritance

    internal class ManagedWrapper : IDisposable
    {
        public ManagedWrapper()
        {
            Marshal.WriteInt32(_ptr, 0);
        }

        ~ManagedWrapper()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <remarks>
        /// This method uses template method pattern as derived class override Dispose(bool) to change the step in disposing algorithm.
        /// </remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="isDisposing"><c>true</c> to release both managed and unmanaged resources. This is the case when it is called from user code.
        /// <c>false</c> to release only unmanaged resources. This is the case when it is called from Finalizer from GC.</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                //No managed object to dispose
            }

            if (_ptr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_ptr);
                _ptr = IntPtr.Zero;
            }
        }

        public IntPtr Ptr { get { return _ptr; } }

        private IntPtr _ptr = Marshal.AllocHGlobal(1024);
    }

    internal class DerivedManagedWrapper : ManagedWrapper
    {
        public DerivedManagedWrapper():base()
        {
            _stream = new FileStream("Input.dat", FileMode.Open);
            if (_stream.Length == 0)
                throw new TypeInitializationException(typeof(DerivedManagedWrapper).FullName, null);
        }

        protected override void Dispose(bool isDisposing)
        {
            //If dispose is called from Dispose() not Finalizer, dispose fileStream as well.
            if (isDisposing)
            {
                _stream.Close();
            }
            base.Dispose(isDisposing);
        }

        private Stream _stream;
    }

    #endregion

    #region Interface and "new" Keyword

    internal interface IVersioning
    {
        int Version { get; }
    }

    internal class BaseClass : IVersioning
    {
        /// <summary>
        /// Implementation of an Interface method/property. It is marked as virtual and sealed. So can't override in derived class.
        /// If it is marked virtual explicitly, then derived class can override it.
        /// NOTE: sealed for a type member is used with override keyword when overriding base class virtual method.
        /// </summary>
        public int Version
        {
            get { return 1; }
        }
    }

    internal class DerivedClass : BaseClass, IVersioning
    {
        /// <summary>
        /// "new" keyword added to avoid warning. 
        /// This keyword tell the compiler that the method has not relationship with similar member existing in base class.
        /// </summary>
        new public int Version
        {
            get { return 1; }
        }
    }

    #endregion

    public class ExternalInterfaceImplementation : IVersioning
    {
        /// <summary>
        /// Cannot specify accessibility with Explicit Interface Method Implementation.
        /// It prevents the instance of class from calling interface method as it is NOT considered part of object model 
        /// </summary>
        int IVersioning.Version
        {
            get { return 1; }
        }
    }

}
