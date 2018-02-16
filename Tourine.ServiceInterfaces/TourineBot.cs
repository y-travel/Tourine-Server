using System.Linq;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Tours;
using Service = Tourine.ServiceInterfaces.Services.Service;

namespace Tourine.ServiceInterfaces
{
    public class TourineBot
    {
        private readonly string _machineName = "\r\n(" + System.Environment.MachineName + ")";
        private IDbConnectionFactory ConnectionFactory { get; set; }

        private readonly TelegramBotClient _bot = new TelegramBotClient("546328065:AAE0wviYKqxhAGCW7HAIlKU3LVlcvw7Ogbc");

        public TourineBot(IDbConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
            _bot.OnMessage += BotOnMessageReceived;
            _bot.OnMessageEdited += BotOnMessageReceived;
            _bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            _bot.OnInlineQuery += BotOnInlineQueryReceived;
            _bot.OnInlineResultChosen += BotOnChosenInlineResultReceived;
            _bot.OnReceiveError += BotOnReceiveError;
            //var me = _bot.GetMeAsync().Result;
            _bot.StartReceiving();
            //Bot.StopReceiving();
        }

        public void Stop()
        {
            if (_bot.IsReceiving)
            {
                _bot?.StopReceiving();
            }
        }

        public bool Start()
        {
            if (!_bot.IsReceiving)
            {
                _bot.StartReceiving();
            }
            return true;
        }

        public void Send(string str, string chatId)
        {
            str += _machineName;
            _bot.SendTextMessageAsync(
               chatId,
               str,
               replyMarkup: new ReplyKeyboardRemove());
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {

            string msg;
            var message = messageEventArgs.Message;
            if (message == null || message.Type == MessageType.TextMessage)
            {
                switch (Enumerable.First(message.Text.Split(' ')))
                {
                    // send inline keyboard
                    case "/start":
                        using (var db = ConnectionFactory.OpenDbConnection())
                        {
                            if (db.Exists<Person>(x => x.ChatId == message.Chat.Id))
                            {
                                var requestReplyKeyboard = new ReplyKeyboardMarkup();
                                var person = db.Single<Person>(x => x.ChatId == message.Chat.Id);
                                if (person.Type.Is(PersonType.Customer))
                                {
                                    msg = "سلام " + person.Name + " " + person.Family +
                                          " شما به عنوان مشتری وارد سیستم شدید";
                                }
                                else if (person.Type.Is(PersonType.Leader))
                                {
                                    msg = "سلام " + person.Name + " " + person.Family +
                                          " شما به عنوان لیدر وارد سیستم شدید";
                                    requestReplyKeyboard = new ReplyKeyboardMarkup(new[]
                                    {
                                        new KeyboardButton("اعضای سفر من")
                                    });
                                }
                                else
                                {
                                    msg = "سلام " + person.Name + " " + person.Family +
                                          " شما به عنوان مسافر وارد سیستم شدید";
                                    requestReplyKeyboard = new ReplyKeyboardMarkup(new[]
                                    {
                                        new KeyboardButton("مشخصات_سفر_من")
                                    });
                                }

                                await _bot.SendTextMessageAsync(
                                    person.ChatId,
                                    msg + _machineName,
                                    replyMarkup: requestReplyKeyboard);
                            }
                            else
                            {
                                var requestNumber = new ReplyKeyboardMarkup
                                {
                                    Keyboard = new[]
                                    {
                                        new[]
                                        {
                                            new KeyboardButton("اجازه دسترسی به شماره ام را می دهم")
                                            {
                                                RequestContact = true
                                            }
                                        }
                                    }
                                };

                                await _bot.SendTextMessageAsync(
                                    message.Chat.Id,
                                    "برای احراز هویت، شماره تلفن خود را انتخاب کنید" + _machineName,
                                    replyMarkup: requestNumber);
                            }
                        }
                        break;

                    case "مشخصات_سفر_من":
                        break;
                }
            }
            if (message.Type == MessageType.ContactMessage)
            {
                using (var db = ConnectionFactory.OpenDbConnection())
                {
                    var person = db.Single<Person>(x => x.SocialNumber == message.Contact.PhoneNumber);
                    if (person != null)
                    {
                        person.ChatId = message.Chat.Id;
                        db.Update(person);

                        await _bot.SendTextMessageAsync(
                            message.Chat.Id,
                            person.Name + " " + person.Family + "\r\n" + " عزیز، شماره شما در سامانه تورینه ثبت شد " +
                            _machineName,
                            replyMarkup: new ReplyKeyboardRemove());
                    }
                    else
                    {
                        await _bot.SendTextMessageAsync(
                            message.Chat.Id,
                            message.Contact.FirstName + " " + message.Contact.FirstName + "\r\n" + " عزیز، شماره شما در سامانه تورینه موجود نمی باشد " + "\r\n" +
                            _machineName,
                            replyMarkup: new ReplyKeyboardRemove());
                    }
                }
            }
        }
        private async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            await _bot.AnswerCallbackQueryAsync(
                callbackQueryEventArgs.CallbackQuery.Id,
                $"Received {callbackQueryEventArgs.CallbackQuery.Data}");
        }

        private async void BotOnInlineQueryReceived(object sender, InlineQueryEventArgs inlineQueryEventArgs)
        {
            System.Diagnostics.Debug.WriteLine($"Received inline query from: {inlineQueryEventArgs.InlineQuery.From.Id}");

            InlineQueryResult[] results = {
                        new InlineQueryResultLocation
                        {
                            Id = "1",
                            Latitude = 40.7058316f, // displayed result
                            Longitude = -74.2581888f,
                            Title = "New York",
                            InputMessageContent = new InputLocationMessageContent // message if result is selected
                            {
                                Latitude = 40.7058316f,
                                Longitude = -74.2581888f,
                            }
                        },

                        new InlineQueryResultLocation
                        {
                            Id = "2",
                            Longitude = 52.507629f, // displayed result
                            Latitude = 13.1449577f,
                            Title = "Berlin",
                            InputMessageContent = new InputLocationMessageContent // message if result is selected
                            {
                                Longitude = 52.507629f,
                                Latitude = 13.1449577f
                            }
                        }
                    };

            await _bot.AnswerInlineQueryAsync(
                inlineQueryEventArgs.InlineQuery.Id,
                results,
                isPersonal: true,
                cacheTime: 0);
        }

        private void BotOnChosenInlineResultReceived(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
        {
            System.Diagnostics.Debug.WriteLine($"Received inline result: {chosenInlineResultEventArgs.ChosenInlineResult.ResultId}");
        }

        private void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            System.Diagnostics.Debug.WriteLine("Received error: {0} — {1}",
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message);
        }

    }
}
public class _services
{
    public string destination { get; set; }
    public string agency { get; set; }
    public string date { get; set; }
    public string type { get; set; }
    public string status { get; set; }
}

public class PersonService
{
    public Service Service { get; set; }
    public Tour Tour { get; set; }
    //public Agency Agency { get; set; }
}