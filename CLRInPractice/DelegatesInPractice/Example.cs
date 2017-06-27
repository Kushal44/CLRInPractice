using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DelegatesInPractice
{
    //delegate can be declare outside the class because CLR creates the class ReportFeedback derived from MulticastDelegate.
    //MulitcastDelegate derives Delegate class.
    //internal class ReportFeedback : MulticastDelegate
    //{ }

    //string s = "ABC"; //A namespace cannot contain member field or method.

    internal delegate void ReportFeedback(int inputValue);

    public class Presenter
    {
        public void PerformForNone()
        {
            DoubleIt bo = new DoubleIt();
            bo.DoIt(10, null);
        }
        
        public void PerformForConsole()
        {
            ReportFeedback rfb = new ReportFeedback(ReportOnConsole);
            DoubleIt bo = new DoubleIt();
            bo.DoIt(10, rfb);
        }

        public void PerformForDialog()
        {
            ReportFeedback rfb = new ReportFeedback(ReportOnDialog);
            DoubleIt bo = new DoubleIt();
            bo.DoIt(100, rfb);
        }

        public void PerformForAll()
        {
            ReportFeedback rfb1 = new ReportFeedback(ReportOnConsole);
            ReportFeedback rfb2 = new ReportFeedback(ReportOnDialog);
            
            //Delegate.Combine combines null and rfb1.
            //Now both rfbChain and rfb1 point to same delegate object. Please note invocation list (_invocationList) is null so far.
            //When rfbChain and rfb2 are combined, _invocationList has items pointing to rfb1 and rfb2. Now, rfbChain points to delegate object having that _invocationlist. 

            ReportFeedback rfbChain = null;
            rfbChain = (ReportFeedback)Delegate.Combine(rfbChain, rfb1);
            rfbChain = (ReportFeedback)Delegate.Combine(rfbChain, rfb2);

            DoubleIt bo = new DoubleIt();
            bo.DoIt(100, rfbChain);
        }

        public void FancyPerformForAll()
        {
            ReportFeedback rfb1 = new ReportFeedback(ReportOnConsole);
            ReportFeedback rfb2 = new ReportFeedback(ReportOnDialog);

            ReportFeedback rfbChain = null;
            rfbChain += rfb1;
            rfbChain += rfb2;

            DoubleIt bo = new DoubleIt();
            bo.DoIt(100, rfbChain);
        }

        public void PerformForAllUsingInvocationList()
        {
            ReportFeedback rfb1 = new ReportFeedback(ReportOnConsole);
            ReportFeedback rfb2 = new ReportFeedback(ReportOnDialog);

            ReportFeedback rfbChain = null;
            rfbChain += rfb1;
            rfbChain += rfb2;

            Delegate[] delegates = rfbChain.GetInvocationList();
            DoubleIt bo = new DoubleIt();

            foreach(ReportFeedback del in delegates)
            {
                bo.DoIt(100, del);
            }
        }

        public void PerformUsingSyntacticSugar()
        {
            DoubleIt bo = new DoubleIt();
            bo.DoIt(6, ReportOnDialog); //No needs to construct a delegate object.

            //No need to define a callback method, use lambda expression instead.
            bo.DoIt(8, (val) => Console.WriteLine(val)); 
        }

        private void ReportOnDialog(int value)
        {
            MessageBox.Show(string.Format("{0}", value));
        }

        private static void ReportOnConsole(int value)
        {
            Console.WriteLine(value);
        }
       
    }


    /// <summary>
    /// This class shows how lambda expression uses the local variables of a function.
    /// Look into IL code to see how a separate nested class is created by CLR to 
    /// pass the local variables to anonymous method.
    /// </summary>
    public sealed class AClass
    {
        public static void UsingLocalVariables(int numToDo)
        {
            int[] squares = new int[numToDo];
            AutoResetEvent done = new AutoResetEvent(false);

            for (int i = 0; i < squares.Length; i++)
            {
                ThreadPool.QueueUserWorkItem(obj => {
                    int num = (int)obj;
                    squares[num] = num * num;
                    if (Interlocked.Decrement(ref numToDo) == 0)
                        done.Set();
                }, i);
            }

            done.WaitOne();

            for (int i = 0; i < squares.Length; i++)
            {
                Console.WriteLine("Index {0}, Square={1}", i, squares[i]);
            }
        }

        //This method does NOT use lambda to create a callback. So a separate 
        //class is created to pass data structure having values.
        public static void UsingLocalVariablesWithoutLambda(int numToDo)
        {
            int[] squares = new int[numToDo];
            AutoResetEvent done = new AutoResetEvent(false);

            for (int i = 0; i < squares.Length; i++)
            {
                ThreadPool.QueueUserWorkItem(WaitCallback, 
                                            new LocalVarDataStructure() { 
                                            done = done,
                                            NumToDo = i,
                                            Squares = squares,
                                            CurrentIndex = i
                });
            }

            done.WaitOne();

            for (int i = 0; i < squares.Length; i++)
            {
                Console.WriteLine("Index {0}, Square={1}", i, squares[i]);
            }
        }

        private static void WaitCallback(object obj)
        {
            LocalVarDataStructure localVar = (LocalVarDataStructure)obj;
            localVar.Squares[localVar.CurrentIndex] = localVar.NumToDo * localVar.NumToDo;
            int k = localVar.NumToDo;
            if (Interlocked.Decrement(ref k) == 0)
                localVar.done.Set();
        }

        public class LocalVarDataStructure
        {
            public AutoResetEvent done { get; set; }

            public int NumToDo { get; set; }

            public int[] Squares { get; set; }

            public int CurrentIndex { get; set; }
        }
    }

    //Business Logic class. These classes are the jewels of an application. Both UI and database 
    //should be plug-in to these, no direct dependency on it.
    internal class DoubleIt
    {
        public void DoIt(int inputValue, ReportFeedback feedback)
        {
            if (feedback != null)
            {
                feedback.Invoke(inputValue * 2);
            }
        }
    }
}
