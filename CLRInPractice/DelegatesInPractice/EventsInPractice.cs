using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesInPractice
{
    public class EventsInPractice
    {
        public static void Perform()
        {
            var mailManager = new MailManager();
            var fax = new Fax(mailManager);
            mailManager.SimulateNewEmail("Modi", "Trump", "H1-B");
        }

        public static void PerfromUnregisterNonListener()
        {
            var mailManager = new MailManager();
            var fax = new Fax(mailManager);

            fax.Unregister(mailManager);

            //Again...Doesn't cause any exception.
            fax.Unregister(mailManager);
        }

        public static void PerformWithEmailWithOverrideForEvent()
        {
            var mm = new EmailManager();

            var fax = new Fax(mm);
            mm.SimulateNewEmail(@"RSS", @"Modi", @"Build Temple");
        }
    }
    
    //Define a type that holds the info to be sent to receiver.
    public class NewMailEventArgs : EventArgs
    {
        public NewMailEventArgs(string from, string to, string subject)
        {
            From = from;
            To = to;
            Subject = subject;
        }

        public string From { get; private set; }
        public string To { get; private set; }
        public string Subject { get; private set; }
    }

    internal class MailManager
    {
        //Create an event member 
        //This result in create a private delegate field initialized to null.
        //Even though it is public, it cannot be accessed from outside.
        //Only add_NewMail and remove_newMail can be accessed from outside.
        //Check IL code ILDSM...
        public event EventHandler<NewMailEventArgs> NewMail;

        //Create a method responsible for raising the event.
        //It should not called from outside. 
        //Also marked virtual for derived class to override it.
        protected virtual void OnNewMail(NewMailEventArgs args)
        {
            var temp = NewMail;
            if (temp != null)
            {
                temp(this, args);
            }
        }

        public void SimulateNewEmail(string from, string to, string subject)
        {
            OnNewMail(new NewMailEventArgs(from, to, subject));
        }
    }

    internal class EmailManager: MailManager
    {
        //Don't do anything. Don't want to inform anyone on new email.
        protected override void OnNewMail(NewMailEventArgs args)
        {
            Console.WriteLine("Top Secret. Can't disclose.");
        }
    }

    internal sealed class Fax
    {
        public Fax(MailManager mm)
        {
            mm.NewMail += mm_NewMail;
        }

        /// <summary>
        /// Removing the non existing listener doesn't throw exception.
        /// </summary>
        public void Unregister(MailManager mm)
        {
            mm.NewMail -= mm_NewMail;
        }

        void mm_NewMail(object sender, NewMailEventArgs e)
        {
            Console.WriteLine("From: {0}, To: {1}, Subject: {2}", 
                               e.From, 
                               e.To, 
                               e.Subject);
        }
    }

}
