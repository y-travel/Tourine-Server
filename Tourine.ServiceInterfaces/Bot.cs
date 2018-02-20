using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Properties;

namespace Tourine.ServiceInterfaces
{
    public abstract class TelegramCommand
    {
        public PersonType PersonType { get; set; }
        public List<KeyboardButton> KeyboardButtons { get; set; }
        public string Text { get; set; }
        public MessageType Type { get; set; }

        public Func<string> Execute { get; set; }

        public Func<Message, bool> CanExecute { get; set; }

        protected TelegramCommand(MessageType type, string text)
        {
            Text = text;
            Type = type;
        }
    }

    public class TourineCommand : TelegramCommand
    {
        public TourineCommand(MessageType type, string text) : base(type, text)
        {
        }
    }

    public class Invoker
    {
        public void Execute(Message msg, TelegramCommand cmd)
        {
            //conditional execution
            if (cmd.CanExecute(msg))
                cmd.Execute();
        }
    }

    public class CommandProvider
    {
        public List<Tuple<PersonType, TelegramCommand>> TelegramCommands { get; set; } = new List<Tuple<PersonType, TelegramCommand>>();
        public void Init()
        {
            CreateLeaderCommands();

        }

        public void CreateLeaderCommands()
        {
            var cmd = new TourineCommand(MessageType.ContactMessage, Resources.ShowPassengerList);
            cmd.CanExecute = msg => cmd.Text == msg.Text && cmd.Type == msg.Type;
            cmd.Execute = () =>
            {
                System.Diagnostics.Debug.Write("I'm a leader and I'm in Iraq");
                return "";
            };
            TelegramCommands.Add(Tuple.Create(PersonType.Leader, (TelegramCommand)cmd));
        }

        public void CreatePassengerCommands()
        {
            var cmd = new TourineCommand(MessageType.ContactMessage, Resources.ShowPassengerList);
            cmd.CanExecute = msg => cmd.Text == msg.Text && cmd.Type == msg.Type;
            cmd.Execute = () =>
            {
                System.Diagnostics.Debug.Write("I'm a passenger and I'm in Iran");
                return "";
            };
            TelegramCommands.Add(Tuple.Create(PersonType.Passenger, (TelegramCommand)cmd));
        }
    }

    public class Client
    {
        public void Main()
        {
            var provider = new CommandProvider();
            var invoker = new Invoker();

            while (true)
            {
                //get message from bot
                var msg = new Message();
                //get person info from session
                var person = new Person();
                provider.TelegramCommands.ForEach(x =>
                {
                    invoker.Execute(msg, x.Item2);
                });
            }


        }
    }
}
