using System;
using System.Linq;
using ServiceStack;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Tourine.ServiceInterfaces.Bot;

namespace Tourine.ServiceInterfaces.Common
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

        public void Send(string str, long chatId)
        {
            str += _machineName;
            _bot.SendTextMessageAsync(
                chatId,
                str,
                replyMarkup: new ReplyKeyboardRemove());
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var finalKeyboard = new ReplyKeyboardMarkup
            {
                Keyboard = new[]
                {
                    new[]
                    {
                        new KeyboardButton("سفر_من")
                    },

                    new[]
                    {
                        new KeyboardButton("گزارش_افراد_زیر_5_سال"),
                        new KeyboardButton("ریز_گزارش_تور")
                    },
                },
                ResizeKeyboard = true
            };

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
                var leaderId = Guid.Parse("070d5d34-2723-4e48-bfe4-b07838e480f1");
                switch (Enumerable.First(message.Text.Split(' ')))
                {

                    case "سفر_من":
                        //@TODO: get buyer id from session
                        var buyerId = Guid.Parse("0cdf3854-efa5-4cca-b659-921a9309c60b");

                        var buyerLastTour = CmdService.GetBuyerLastTour(buyerId);
                        var buyerLastTeam = CmdService.GetBuyerLastTeam(buyerId);
                        var teamServices = CmdService.GetTeamPersonAndServices(buyerLastTeam.TourId,buyerLastTeam.Id);
                        string strMyTeam = "";
                        for (var i = 0; i < teamServices.Count; i++)
                        {
                            strMyTeam += (i + 1).ToString("00") + ". " + teamServices[i].serviceSum.GetEmojis();
                            strMyTeam += teamServices[i].Family + "," + teamServices[i].Name;
                            strMyTeam += "\r\n";
                        }
                        await _bot.SendTextMessageAsync(
                            message.Chat.Id,
                            "مشخصات سفر شما به شرح زیر می باشد " + "\r\n" +buyerLastTour.TourDetail.StartDate.ToString("yyyy/MM/dd") + "\r\n" 
                            + buyerLastTour.TourDetail.Destination.Name +"\r\n" +strMyTeam+
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
    }

    public static class StringExtensions
    {
        public static string GetLeftToRight(this string str) => '\u200e' + str;
    }
}