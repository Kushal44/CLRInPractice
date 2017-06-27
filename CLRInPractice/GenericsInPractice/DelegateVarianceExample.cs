using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsInPractice
{
    public class Animal
    {
        public string Category {get; protected set;}
    }

    public class Mammal : Animal
    {
        public Mammal()
        {
            base.Category = "Mammal";
        }
    }

    public class Reptile : Animal
    {
        public Reptile()
        {
            base.Category = "Reptile";
        }
    }

    public class Giraffe : Mammal
    {
        public Giraffe()
        {
            base.Category = "Giraffe";
        }
    }

    public class Snake : Giraffe
    {
        public Snake()
        {
            base.Category = "Snake";
        }
    }

    public class DelegateVarianceExample
    {
        public static void GenericVarianceWithClass()
        {
            //Assignment Compatibility
            var animal = new Mammal();

            //Classes are invariant.
            //List<Mammal> mammals = giraffes;
        }


        #region Delegate Contravariance

        public static void GenericContraVarianceWithDelegates()
        {
            //Assignment Compatibility
            Action<Mammal> action1 = MammalAction;

            //Contravariance because Action is takes "in" type parameter. so assignment is reversed.
            action1 = AnimalAction;

            //Take a close look at the DoAnimalThing method which is making call to Action.
            //So it cannot call any child of base class (Mammal) to be passed as parameter to Action.
            //If we pass "Giraffe" then it CANNOT pass Mammal object when calling Action.
            DoAnimalThing(AnimalAction);

            //DoAnimalThing(GiraffeAction); This is invalid because "in" type parameter.
        }

        private static void MammalAction(Mammal m)
        {
            Console.WriteLine(m.Category);
        }

        private static void GiraffeAction(Giraffe g)
        {
            Console.WriteLine(g.Category);
        }

        private static void AnimalAction(Animal a)
        {
            Console.WriteLine(a.Category);
        }

        private static void DoAnimalThing(Action<Mammal> onFinish)
        {
            onFinish(new Mammal());
        }

        #endregion

        #region Delegate Covariance

        public static void GenericCovarianceWithDelegates()
        {
            //Assignment Compatibility
            Func<Mammal> func1 = MammalFunc;

            //Covariance because of "out" type parameter.
            func1 = ProvideGirrafe;

            //Take a close look at the MammalProvider. It can return object of type Mammal or any of it's derived class.
            //func1 = ProvideAnimal;
        }

        private static Mammal MammalFunc()
        {
            return new Mammal();
        }

        private static Giraffe ProvideGirrafe()
        {
            return new Giraffe();
        }

        private static Animal ProvideAnimal()
        {
            return new Animal();
        }

        private static Mammal MammalProvider(Func<Mammal> provider)
        {
            var mammal =  provider();
            return mammal;
        }

        #endregion
    }
}
