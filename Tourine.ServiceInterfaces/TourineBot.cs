using System;
using System.Linq;
using ServiceStack;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;

namespace Tourine.ServiceInterfaces
{
    public class TourineBot
    {
        private readonly string _machineName = "\r\n(" + System.Environment.MachineName + ")";

        private readonly TelegramBotClient _bot;
        public readonly TourineBotCmdService CmdService;
        public TourineBot(TourineBotCmdService cmdService,string telegramToken)
        {
            CmdService = cmdService;
            _bot = new TelegramBotClient(telegramToken);
            _bot.OnMessage += BotOnMessageReceived;
            _bot.OnMessageEdited += BotOnMessageReceived;
            _bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            _bot.OnInlineQuery += BotOnInlineQueryReceived;
            _bot.OnInlineResultChosen += BotOnChosenInlineResultReceived;
            _bot.OnReceiveError += BotOnReceiveError;
            _bot.StartReceiving();
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
            var finalKeyboard = new ReplyKeyboardMarkup();
            finalKeyboard.Keyboard =
                new[]
                {
                    new KeyboardButton[]
                    {
                        new KeyboardButton("مشخصات_سفر_من"),
                        new KeyboardButton("اعضای_تیم")
                    },

                    new KeyboardButton[]
                    {
                        new KeyboardButton("گزارش_کلی_تور")
                    },

                    new KeyboardButton[]
                    {
                        new KeyboardButton("ریز_گزارش_تور")
                    }
                };
            finalKeyboard.ResizeKeyboard = true;

            var message = messageEventArgs.Message;
            if (message.Type == MessageType.ContactMessage)
            {
                if (!CmdService.IsRegistered(message))
                {
                    var res = CmdService.Register(message);
                    switch (res)
                    {
                        case TourineCmdStatus.ContactTypeError:
                            break;
                        case TourineCmdStatus.NumberError:
                            await _bot.SendTextMessageAsync(
                                                        message.Chat.Id,
                                                        message.Contact.FirstName + " " + message.Contact.LastName + "\r\n" + " عزیز، شماره شما در سامانه تورینه موجود نمی باشد " + "\r\n" +
                                                        _machineName,
                                                        replyMarkup: new ReplyKeyboardRemove());
                            break;
                        case TourineCmdStatus.MultipleNumber:
                            break;
                        case TourineCmdStatus.Registered:

                            await _bot.SendTextMessageAsync(
                                message.Chat.Id,
                                "شماره شما با موفقیت در سامانه تورینه ثبت شد" +
                                _machineName,
                                replyMarkup: finalKeyboard);

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    await _bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "شماره شما قبلا در سامانه تورینه ثبت شده است" + _machineName,
                        replyMarkup: new ReplyKeyboardRemove());
                }
            }
            if (!CmdService.IsRegistered(message))
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
                requestNumber.ResizeKeyboard = true;
                requestNumber.OneTimeKeyboard = true;

                await _bot.SendTextMessageAsync(
                    message.Chat.Id,
                    message.Chat.FirstName + " عزیز " + "\r\n" +
                    "ربات اطلاع رسان تورینه ورود شما را به جمع خانواده تورینه خوش آمد می گوید" + _machineName);

                await _bot.SendTextMessageAsync(
                    message.Chat.Id,
                    "برای احراز هویت، شماره تلفن خود را انتخاب کنید" + _machineName,
                    replyMarkup: requestNumber);
            }

            if (message == null || message.Type == MessageType.TextMessage)
            {
                switch (Enumerable.First(message.Text.Split(' ')))
                {

                    case "مشخصات_سفر_من":
                        await _bot.SendTextMessageAsync(
                            message.Chat.Id,
                            "مشخصات سفر شما به شرح زیر می باشد " + "\r\n" +
                            _machineName);
                        break;
                    case "اعضای_تیم":
                        await _bot.SendTextMessageAsync(
                            message.Chat.Id,
                            "مشخصات تیم شما به شرح زیر می باشد " + "\r\n" +
                            _machineName);
                        break;
                    case "گزارش_کلی_تور":
                        await _bot.SendTextMessageAsync(
                            message.Chat.Id,
                            "گزارش کلی تور به شرح زیر می باشد " + "\r\n" +
                            _machineName);
                        break;

                    case "ریز_گزارش_تور":
                        await _bot.SendTextMessageAsync(
                            message.Chat.Id,
                            "ریز گزارش تور به شرح زیر می باشد " + "\r\n" +
                            _machineName);
                        break;

                    default:
                        if (CmdService.IsRegistered(message))
                        {

                            await _bot.SendTextMessageAsync(
                                message.Chat.Id,
                                "ربات تورینه در خدمت شماست..." +
                                _machineName,
                                replyMarkup: finalKeyboard);

                            var role = CmdService.RegisteredAs(message);
                            if (role.Is(PersonType.Passenger))
                            {
                            }
                            if (role.Is(PersonType.Leader))
                            {
                            }
                        }

                        break;
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
