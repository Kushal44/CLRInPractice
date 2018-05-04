using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsInPractice
{
    internal sealed class Node<T>
    {
        public T m_data;
        public Node<T> m_next;

        public Node(T data)
            : this(data, null)
        { }

        public Node(T data, Node<T> next)
        {
            m_data = data;
            m_next = next;
        }

        public override string ToString()
        {
            return m_data.ToString() + ((m_next != null) ? m_next.ToString() : string.Empty);
        }
    }


    public abstract class Node
    {
        protected Node m_next;

        public Node(Node next)
        {
            m_next = next;
        }
    }

    internal sealed class TypedNode<T> : Node
    {
        public T m_data;

        public TypedNode(T data) :
            this(data, null)
        { }

        public TypedNode(T data, Node next) :
            base(next)
        {
            m_data = data;
        }

        public override string ToString()
        {
            return m_data.ToString() + ((m_next != null) ? m_next.ToString() : string.Empty);
        }
    }


    public static class LinkedList
    {
        public static void SameType()
        {
            Node<char> head = new Node<char>('C');
            Node<char> second = new Node<char>('B', head);
            Node<char> first = new Node<char>('A', second);
            Console.WriteLine(first.ToString());
        }

        public static void DifferentTypes()
        {
            TypedNode<char> head = new TypedNode<char>('.');
            TypedNode<DateTime> second = new TypedNode<DateTime>(DateTime.Now, head);
            TypedNode<string> first = new TypedNode<string>("Today is ", second);
            Console.Write(first.ToString());
        }
    }
}
