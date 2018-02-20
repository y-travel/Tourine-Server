using System.Linq;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Tourine.ServiceInterfaces.Persons;

namespace Tourine.ServiceInterfaces
{
    public class TourineBotCmdService
    {
        private IDbConnectionFactory ConnectionFactory { get; set; }
        public TourineBotCmdService(IDbConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        public string BuyerTeamList()
        {
            return "";
        }

        public bool IsRegistered(Message message)
        {
            using (var db =ConnectionFactory.OpenDbConnection())
            {
                return db.Exists<Person>(x => x.ChatId == message.Chat.Id);
            }
        }

        public TourineCmdStatus Register(Message message)
        {
            if (message.Type != MessageType.ContactMessage)
                return TourineCmdStatus.ContactTypeError;
            
            using (var db = ConnectionFactory.OpenDbConnection())
            {
                if (!db.Exists<Person>(x => x.SocialNumber == message.Contact.PhoneNumber))
                    return TourineCmdStatus.NumberError;
                var persons = db.Select<Person>().Where(x => x.SocialNumber == message.Contact.PhoneNumber);
                if (persons.Count() > 1)
                    return TourineCmdStatus.MultipleNumber;
                var person = persons.ToArray()[0];
                person.ChatId = message.Chat.Id;
                db.Update(person);
                return TourineCmdStatus.Registered;
            }
        }

        public PersonType RegisteredAs(Message message)
        {
            using (var db = ConnectionFactory.OpenDbConnection())
            {
                return db.Single<Person>(x => x.ChatId == message.Chat.Id).Type;
            }
        }
    }
}
