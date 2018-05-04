using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundamentalsInPractice
{
    internal struct ValueType
    {
        //Value types cannot have parameterless constructor.
        //public ValueType()
        //{

        //}

        //Cannot have instance property or field in struct.
        //int m_x = 0;

        public ValueType(int x, int y)
        {
            m_x = x;
            m_y = y;
        }

        public int m_x;
        public int m_y;
    }

    public class UsingValueType
    {
        public static void ValueTypeUsingDefaultParameterlessConstructor()
        {
            ValueType valueType = new ValueType();
            valueType.m_x = 5;
            valueType.m_y = 5;
        }

        public static void ValueTypeUsingParameterConstructor()
        {
            ValueType valueType = new ValueType(10, 10);
        }
    }

}
