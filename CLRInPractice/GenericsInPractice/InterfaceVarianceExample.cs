using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsInPractice
{
    class InterfaceVarianceExample
    {
        public static void GenericCovarianceWithInterface()
        {
            List<Giraffe> giraffes = new List<Giraffe>();

            //Assignment compatibility
            IEnumerable<Giraffe> lstGiraffe = giraffes;

            //IEnumerable is Covariant in nature, assignment is not reversed. It uses "out" for the type parameter. 
            //Because IEnumerable only returns elements NOT taking any input. 
            IEnumerable<Mammal> lstMammal = giraffes;
        }

        public static void GenericContravarianceWithInterface()
        {
            IEqualityComparer<Mammal> comparer = new MammalComparer();

            comparer = new AnimalComparer();

            //****Does NOT work****
            //comparer = new GiraffeComparer();

            //****Does NOT work****
            //Dictionary<Mammal, string> dictionary = new Dictionary<Mammal, string>(new GiraffeComparer());


            //Dictionary use comparer passed to constructor to compare TKEY type parameter.

            //It's okay to use AnimalComparer for comparing Mammal
            //But you CANNOT use GiraffeComparer to compare Animal because Animal is higher than Giraffe.
            Dictionary<Mammal, string>  dictionary = new Dictionary<Mammal, string>(new AnimalComparer());

        }
    }

    public class AnimalComparer : IEqualityComparer<Animal>
    {
        public bool Equals(Animal x, Animal y)
        {
            return true;
        }

        public int GetHashCode(Animal obj)
        {
            throw new NotImplementedException();
        }
    }

    public class MammalComparer : IEqualityComparer<Mammal>
    {

        public bool Equals(Mammal x, Mammal y)
        {
            return true;
        }

        public int GetHashCode(Mammal obj)
        {
            throw new NotImplementedException();
        }
    }

    public class GiraffeComparer : IEqualityComparer<Giraffe>
    {

        public bool Equals(Giraffe x, Giraffe y)
        {
            return true;
        }

        public int GetHashCode(Giraffe obj)
        {
            throw new NotImplementedException();
        }
    }
}
