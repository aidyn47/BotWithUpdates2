
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
namespace telegramBot1
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var botClient = new TelegramBotClient(token: "5397233464:AAFXgvdDiz55DxMP7HRc2-jEtnbz0qTKi2I");
            using var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            botClient.StartReceiving(
                HandleUpdatesAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token);

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Начал прослушку @{me.Username}");
            Console.ReadLine();

            
            cts.Cancel();

        }

        static async Task HandleUpdatesAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandleMessage(botClient, update.Message, cancellationToken);
                return;
            }

            if (update.Type == UpdateType.CallbackQuery)
            {
                await HandleCallbackQuery(botClient, update.CallbackQuery);
                return;
            }

        }


        static async Task HandleMessage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            var chatId = message.Chat.Id;

            switch (message.Text)
            {
                case "/start":
                    InlineKeyboardMarkup keyboard = (new[]
                {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Одежда","/kategory"),
                InlineKeyboardButton.WithCallbackData("Другое", "@"),
            },

        });
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Что хотите купить? ", replyMarkup: keyboard);
                    return;
                    break;

                case "/kategory":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Категория: \n" + "\n" +
                                                                      "верхняя одежда - /outerwear\n" +
                                                                      "футболки - /tshirts\n" +
                                                                      "брюки - /pants\n" +
                                                                      "обувь - /shoes"); return; break;
                    
                case "/outwear":
                    InlineKeyboardMarkup keyboard2 = (new[]
                {
            new[]
            {
                InlineKeyboardButton.WithUrl("магазин1","https://www.instagram.com/aidyn_neofishal/"),
                InlineKeyboardButton.WithUrl("магазин2", "https://www.instagram.com/aidyn_neofishal/"),
            },

        });
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Верхняя одежда:", replyMarkup: keyboard2);

                    return;
                    break;
                case "/tshirts":
                    InlineKeyboardMarkup keyboard3 = (new[]
                {
            new[]
            {
                InlineKeyboardButton.WithUrl("магазин3","https://www.instagram.com/aidyn_neofishal/"),
                InlineKeyboardButton.WithUrl("магазин4", "https://www.instagram.com/aidyn_neofishal/"),
            },

        });
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Футболки:", replyMarkup: keyboard3);

                    return;
                    break;
                case "/pants":
                    InlineKeyboardMarkup keyboard4 = (new[]
                {
            new[]
            {
                InlineKeyboardButton.WithUrl("магазин5","https://www.instagram.com/aidyn_neofishal/"),
                InlineKeyboardButton.WithUrl("магазин6", "https://www.instagram.com/aidyn_neofishal/"),
            },

        });
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Брюки:" , replyMarkup: keyboard4);

                    return;
                    break;
                case "/shoes":
                    InlineKeyboardMarkup keyboard5 = (new[]
                {
            new[]
            {
                InlineKeyboardButton.WithUrl("магазин7","https://www.instagram.com/aidyn_neofishal/"),
                InlineKeyboardButton.WithUrl("магазин8", "https://www.instagram.com/aidyn_neofishal/"),
            },

        });
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Магазины обуви:", replyMarkup: keyboard5);

                    return;
                    break;
                case "help":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "все команды: \n" +
                                                                      "\n" +
                                                                      "/menu\n" +
                                                                      "/kategory\n" +
                                                                      "/photo\n" +
                                                                      "/contact\n" +
                                                                      "/location\n" +
                                                                      "/video\n" +
                                                                      "/album");
                    break;
                case "ввести код товара":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Код:");
                    break;
                case "1234":
                    InlineKeyboardMarkup keyboard6 = (new[]
                {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("S", "Вы выбрали S"),
                InlineKeyboardButton.WithCallbackData("M", "Вы выбрали M"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("L", "Вы выбрали L"),
                InlineKeyboardButton.WithCallbackData("XL", "Вы выбрали XL"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("XXL", "Вы выбрали XXL"),
            },
        });
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Размеры: ", replyMarkup: keyboard6);
                    return;
                    break;
                case "/photo":
                    await botClient.SendPhotoAsync(
                    chatId: chatId,
                    photo: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
                    caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
                    parseMode: ParseMode.Html,
                    cancellationToken: cancellationToken);
                    break;
                case "/contact":
                    await botClient.SendContactAsync(
                    chatId: chatId,
                    phoneNumber: "+996709622093",
                    firstName: "Aidyn",
                    vCard: "BEGIN:VCARD\n" +
                           "VERSION:3.0\n" +
                           "N:Solo;Han\n" +
                           "ORG:Scruffy-looking nerf herder\n" +
                           "TEL;TYPE=voice,work,pref:+1234567890\n" +
                           "mametovaidyn2003@gmail.com\n" +
                           "END:VCARD",
                    cancellationToken: cancellationToken);
                    break;
                case "/location":
                    await botClient.SendVenueAsync(
                    chatId: chatId,
                    latitude: 50.0840172f,
                    longitude: 14.418288f,
                    title: "Bishkek",
                    address: "Kolmo",
                    cancellationToken: cancellationToken);
                    break;
                case "/video":
                    await botClient.SendVideoAsync(
                    chatId: chatId,
                    video: "https://raw.githubusercontent.com/TelegramBots/book/master/src/docs/video-countdown.mp4",
                    thumb: "https://raw.githubusercontent.com/TelegramBots/book/master/src/2/docs/thumb-clock.jpg",
                    supportsStreaming: true,
                    cancellationToken: cancellationToken);
                    break;
                case "/album":
                    Message[] messages = await botClient.SendMediaGroupAsync(
                    chatId: chatId,
                    media: new IAlbumInputMedia[]
                    {
                new InputMediaPhoto("https://cdn.pixabay.com/photo/2017/06/20/19/22/fuchs-2424369_640.jpg"),
                new InputMediaPhoto("https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg"),
                    },
                    cancellationToken: cancellationToken);
                    break;
                /*case "/menu":
                    ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                                            {
                    new KeyboardButton[] { "/start", "/kategory" },
                    new KeyboardButton[] { "ввести код товара", "help" }
                })
                    {
                        ResizeKeyboard = true
                    };
                    ReplyKeyboardMarkup keyboard7 = replyKeyboardMarkup;
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Choose:", replyMarkup: keyboard7);
                    return;
                    break;*/
            }
            return;
        }


        static async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, callbackQuery.Data);

        }

        static Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessag = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Ошибка телеграм АПИ: \n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessag);
            return Task.CompletedTask;
        }

    }



}