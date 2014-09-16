using System;
using System.Security.Cryptography.X509Certificates;
using BehaviorTree.Synchronous;

namespace RunBehaviors
{
    public class SpammerComputer
    {
        public bool IntrudersDetected;

        public DateTime AdminsBirthday;

        public string[] SpamOrders =
        {
            "foo@gmail.com",
            "bar@gmail.com",
            "qwer@gmail.com",
            "asdf@gmail.com",
            "john.smith@hotmail.com",
        };
    }

    public interface IMessageSender
    {
        void Send(string address, string message);
    }

    public class Emailer : IMessageSender
    {
        public void Send(string address, string message)
        {
            Console.WriteLine("e-mail to {0}: {1}", address, message);
        }

        public void Spam(string email, int amount)
        {
            Console.WriteLine("Spamming address {0} with {1} emails...", email, amount);
        }
    }

    public class IsSpammerSafe : IBehavior<Emailer, SpammerComputer>
    {
        public static IsSpammerSafe Create()
        {
            return new IsSpammerSafe();
        }

        public BehaviorStatus Execute(Emailer target, SpammerComputer ctx)
        {
            return ctx.IntrudersDetected ? BehaviorStatus.Failure : BehaviorStatus.Success;
        }
    }

    public class IsAdminsBirthday : IBehavior<Emailer, SpammerComputer>
    {
        public static IsAdminsBirthday Create() { return new IsAdminsBirthday(); }

        public BehaviorStatus Execute(Emailer target, SpammerComputer ctx)
        {
            return ctx.AdminsBirthday.Date.Equals(DateTime.Today.Date) ? BehaviorStatus.Success : BehaviorStatus.Failure;
        }
    }

    public class SendMessage : IBehavior<IMessageSender, object>
    {
        private string _address;
        private string _msg;

        public static SendMessage Create(string address, string msg)
        {
            return new SendMessage
            {
                _address = address,
                _msg = msg,
            };
        }

        public BehaviorStatus Execute(IMessageSender target, object ctx)
        {
            target.Send(_address, _msg);
            return BehaviorStatus.Success;
        }
    }

    class Program
    {
        public static BehaviorStatus SendSpamEmails(Emailer emailer, SpammerComputer ctx)
        {
            if (ctx.SpamOrders.Length == 0) return BehaviorStatus.Failure;
            foreach (var order in ctx.SpamOrders)
                emailer.Spam(order, 1000);
            return BehaviorStatus.Success;
        }

        static void Main(string[] args)
        {
            var spammerBehavior = Selector.Create(
                Sequence.Create(
                    Inverter.Create(IsSpammerSafe.Create()),
                    SendMessage.Create("admin@spambot.org", "The spambot is compromised!")
                ),
                Sequence.Create(
                    IsAdminsBirthday.Create(),
                    SendMessage.Create("admin@spambot.org", "Happy birthday Admin!")
                ),
                Do.Create<Emailer, SpammerComputer>(SendSpamEmails)
            );

            var emailer = new Emailer();

            var spammer1 = new SpammerComputer
            {
                IntrudersDetected = false,
                AdminsBirthday = DateTime.Today,
            };

            var spammer2 = new SpammerComputer
            {
                IntrudersDetected = true,
                AdminsBirthday = new DateTime(1978, 12, 23),
            };

            var spammer3 = new SpammerComputer
            {
                IntrudersDetected = false,
                AdminsBirthday = new DateTime(1978, 12, 23),
            };

            var spammer4 = new SpammerComputer
            {
                IntrudersDetected = false,
                AdminsBirthday = new DateTime(1978, 12, 23),
                SpamOrders = new string[]{},
            };

            Console.WriteLine("Spammer 1 behavior:");
            BehaviorStatus status = spammerBehavior.Execute(emailer, spammer1);
            if (status == BehaviorStatus.Failure) Console.WriteLine("Spammer 1 behavior failed");

            Console.WriteLine("\n\rSpammer 2 behavior:");
            status = spammerBehavior.Execute(emailer, spammer2);
            if (status == BehaviorStatus.Failure) Console.WriteLine("Spammer 2 behavior failed");

            Console.WriteLine("\n\rSpammer 3 behavior:");
            status = spammerBehavior.Execute(emailer, spammer3);
            if (status == BehaviorStatus.Failure) Console.WriteLine("Spammer 3 behavior failed");

            Console.WriteLine("\n\rSpammer 4 behavior:");
            status = spammerBehavior.Execute(emailer, spammer4);
            if (status == BehaviorStatus.Failure) Console.WriteLine("Spammer 4 behavior failed");

            Console.ReadLine();
        }
    }
}
