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
        public TourineBot(TourineBotCmdService cmdService, string telegramToken)
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
                        new KeyboardButton("گزارش_افراد_زیر_5_سال")
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
                var leaderId = Guid.Parse("cdaf2353-b68d-4f0f-8056-5e37e51d70aa");
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

                    case "گزارش_افراد_زیر_5_سال":

                        if (CmdService.IsLeader(message.Chat.Id))//@TODO: ugly - should get from sessin
                        {
                            var lastTour = CmdService.GetLeaderLastRunningTour(leaderId);
                            var tourUnder5ServiceCount = CmdService.GetTourServiceCountForUnder5(lastTour.Id);
                            var under5ServiceList = CmdService.GetTourUnder5PersonListAndService(tourUnder5ServiceCount.Tour.Id);

                            string strU5 = "";
                            for (var i = 0; i < under5ServiceList.Count; i++)
                            {
                                strU5 += (i + 1).ToString("00") + ". " + under5ServiceList[i].serviceSum.GetEmojis();
                                strU5 += under5ServiceList[i].Family + "," + under5ServiceList[i].Name;
                                strU5 += "\r\n";
                            }
                            string msg = "گزارش افراد زیر 5 سال به شرح زیر می باشد: " + "\r\n\r\n" +
                                tourUnder5ServiceCount.GetTelegramView() + "\r\n" + "لیست افراد" + "\r\n" +
                                strU5 + _machineName;

                            await _bot.SendTextMessageAsync(
                                message.Chat.Id, msg);

                        }
                        else
                        {
                            await _bot.SendTextMessageAsync(
                                message.Chat.Id, "شما در هیچ توری به عوان سرگروه نمی باشید");
                        }

                        break;

                    case "ریز_گزارش_تور":
                        if (CmdService.IsLeader(message.Chat.Id)) //@TODO: ugly - should get from sessin
                        {
                            //@TODO: leader id is PersonId in session
                            var lastTourAllPerson = CmdService.GetLeaderLastRunningTour(leaderId);
                            var tourAllPersonCount = CmdService.GetTourServiceCount(lastTourAllPerson.Id);
                            var personList = CmdService.GetTourPersonList(tourAllPersonCount.Tour.Id);

                            string str = "";
                            for (var i = 0; i < personList.Count; i++)
                            {
                                str += (i + 1).ToString("00") + ". " + personList[i].GetTelegramView();
                                str += personList[i].IsLeader ? "(سرگروه)" : " ";
                                str += "\r\n";
                            }
                            await _bot.SendTextMessageAsync(
                                message.Chat.Id,
                                "ریز گزارش تور به شرح زیر می باشد: " + "\r\n\r\n" +
                                tourAllPersonCount.GetTelegramView() + "\r\n" + "لیست افراد" + "\r\n" + str +
                                _machineName);
                        }
                        else
                        {
                            await _bot.SendTextMessageAsync(
                                message.Chat.Id, "شما در هیچ توری به عوان سرگروه نمی باشید");
                        }
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

public static class StringExtensions
{
    public static string GetLeftToRight(this string str) => '\u200e' + str;
}