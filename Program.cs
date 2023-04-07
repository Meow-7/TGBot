using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using Telegram.Bot;
using HtmlAgilityPack;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bots.Http;
using Telegram.Bot.Extensions;
using Telegram.Bot.Exceptions;
using Telegram.Bots.Extensions.Polling;
using System.Net.Http;
using Telegram.Bot.Types.ReplyMarkups;
using System.Net.Mail;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bots.Types.Inline;
using System.Text.RegularExpressions;
class Program
{
    

    static void Main(string[] args)
    {
        var bottoken = "6169969843:AAE0nHtGc0AwgrSOfYjpMzZWO8uOQHV_vbw";
        var botClient = new TelegramBotClient(bottoken);
      
        botClient.StartReceiving(Update, Error);
        Console.ReadLine();
        
    }

    private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
    {
        Console.WriteLine($"Error: {arg2.Message}");
        return Task.CompletedTask;
    }


    async static Task Update(ITelegramBotClient botClient, Update update , CancellationToken token)
    {
        
        var message = update.Message;
        var url = "https://zaxid.net/vtrati_rosiyan_u_viyni_proti_ukrayini_n1537635";
        var url2 = "https://index.minfin.com.ua/ua/russian-invading/casualties/";
        var httpClient = new HttpClient();
        var html = await httpClient.GetStringAsync(url);
        var html2 = await httpClient.GetStringAsync(url2);
        var htmlDocument = new HtmlDocument();
        var htmlDocument2 = new HtmlDocument();
        htmlDocument.LoadHtml(html);
        htmlDocument2.LoadHtml(html2);








        if (update.CallbackQuery != null)
        {
            var callbackQuery = update.CallbackQuery;
            switch (callbackQuery.Data)
            {
                case "button_clicked3":
                  

                    var keyboard4 = new InlineKeyboardMarkup(new[]
{
    new[]
    {
        InlineKeyboardButton.WithCallbackData("\U0001F4E9 Отримати статистику на пошту", "button4"),
        InlineKeyboardButton.WithCallbackData("\U0001F4AF Деяка статистика у процентах", "button44")
    }
});

                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "/all - загальна статистика\n/afv - Втрати бойових броньованих машин\n/airdef - Втрати засобів протиповітряної оборони\n/artillery_systems - Втрати артилерійських систем\n/cruise_missile - Втрати крилатих ракет\n/drones - Втрати безпілотників оперативно-тактичного рівня\n/helicopters - Втрати гелікоптерів\n/mlrs - Втрати реактивних систем залпового вогню\n/personnel - Втрати особового складу\n/ships - Втрати катерів/кораблів\n/spec_equip - Втрати спеціальної техніки\n/tanks - Втрати танків\n" +
                "/vehicles_fueltanks - Втрати автомобільної техніки і паливних цистерн\n/warplanes - Втрати літаків\n", replyMarkup: keyboard4);
                    break;

                case "button4":
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "\U0000270F Напишіть свій e-mail");
                    break;

                case "button44":
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "/afv_perc - Втрати бойових броньованих машин у %\n/helicopters_perc - Втрати гелікоптерів у %\n/personnel_perc - Втрати особового складу у %\n/tanks_perc - Втрати танків у %\n/warplanes_perc - Втрати літаків у %");
                    break;
            }
            return;
        }

        if (message == null)
        {
            return;
        }

        if (message.Text.ToLower().Contains("/start"))
        {
            var button3 = new InlineKeyboardButton("h")
            {
                Text = "\U0001F441 Переглянути команди",
                CallbackData = "button_clicked3"
            };

            var commands = new BotCommand[]
{
    new BotCommand { Command = "start", Description = "На початок \U0001F519" },
    new BotCommand { Command = "help", Description = "Як користуватися ботом? \U00002753" }
};

            await botClient.SetMyCommandsAsync(commands);



            var keyboard3 = new InlineKeyboardMarkup(button3);
          
            await botClient.SendTextMessageAsync(message.Chat.Id, "\U0001F44B Привіт! Я - бот котрий надасть актуальну статистику про ворожі втрати", replyMarkup:keyboard3);
        }

        if (message.Text == "/all")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Загальні втрати країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, currul.InnerHtml.Replace("<li>", "").Replace("</li>", "\n"));
                    break;
                }
            }
        }

        if (message.Text == "/tanks")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'танків')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати танків країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, li.InnerHtml.Replace("<li>", "").Replace("</li>", "\n").Replace("танків", "Втрати танків"));
                    break;
                }
            }

        }

        if (message.Text == "/mlrs")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'реактивних')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати реактивних систем залпового вогню країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, li.InnerHtml.Replace("<li>", "").Replace("</li>", "\n").Replace("реактивних", "Втрати реактивних"));
                    break;
                }
            }

        }

        if (message.Text == "/afv")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'бойових')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати бойових броньованих машин країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, li.InnerHtml.Replace("<li>", "").Replace("</li>", "\n").Replace("бойових", "Втрати бойових"));
                    break;
                }
            }

        }

        if (message.Text == "/warplanes")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'літаків')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати літаків країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, li.InnerHtml.Replace("<li>", "").Replace("</li>", "\n").Replace("літаків", "Втрати літаків"));
                    break;
                }
            }

        }

        if (message.Text == "/helicopters")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'гелікоптерів')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати гелікоптерів країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, li.InnerHtml.Replace("<li>", "").Replace("</li>", "\n").Replace("гелікоптерів", "Втрати гелікоптерів"));
                    break;
                }
            }

        }

        if (message.Text == "/vehicles_fueltanks")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'цистерн')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати автомобільної техніки і паливних цистерн країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, li.InnerHtml.Replace("<li>", "").Replace("</li>", "\n").Replace("автомобільної", "Втрати автомобільної"));
                    break;
                }
            }

        }

        if (message.Text == "/cruise_missile")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'крилатих')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати крилатих ракет країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, li.InnerHtml.Replace("<li>", "").Replace("</li>", "\n").Replace("крилатих", "Втрати крилатих"));
                    break;
                }
            }

        }

        if (message.Text == "/personnel")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'особового')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати особового складу країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, li.InnerHtml.Replace("<li>", "").Replace("</li>", "\n").Replace("особового", "Втрати особового"));
                    break;
                }
            }

        }

        if (message.Text == "/airdef")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'протиповітряної')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати засобів протиповітряної оборони країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, li.InnerHtml.Replace("<li>", "").Replace("</li>", "\n").Replace("засобів", "Втрати засобів"));
                    break;
                }
            }

        }

        if (message.Text == "/ships")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'кораблів')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати катерів/кораблів країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, li.InnerHtml.Replace("<li>", "").Replace("</li>", "\n").Replace("катерів", "Втрати катерів"));
                    break;
                }
            }

        }

        if (message.Text == "/spec_equip")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'спеціальної')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати спеціальної техніки країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, li.InnerHtml.Replace("<li>", "").Replace("</li>", "\n").Replace("спеціальної", "Втрати спеціальної"));
                    break;
                }
            }

        }

        if (message.Text == "/artillery_systems")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'артилерійських')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати артилерійських систем країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, li.InnerHtml.Replace("<li>", "").Replace("</li>", "\n").Replace("артилерійських", "Втрати артилерійських"));
                    break;
                }
            }

        }

        if (message.Text == "/drones")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'безпілотників')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати безпілотників оперативно-тактичного рівня країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, li.InnerHtml.Replace("<li>", "").Replace("</li>", "\n").Replace("безпілотників", "Втрати безпілотників"));
                    break;
                }
            }

        }

        //if (message.Type != MessageType.Text)
        //    return;

        if (message.Text.Contains("@") || message.Text.Contains("."))
        {
            MailAddress from = new MailAddress("smplml23@gmail.com");
            MailAddress to = new MailAddress(message.Text);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Статистика ворожих втрат за " + DateTime.Today.ToString("d");
            m.IsBodyHtml = true;
            m.Body = "<h3>Втрати ворога</h3>";
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    m.Body = "Загальні втрати країни-оккупанта станом на " + DateTime.Today.ToString("d") + ": <br>" + currul.InnerHtml.Replace("<li>", "\n").Replace("</li>", "<br>");
                    break;
                }
            }
            //m.Attachments.Add(new Attachment("C:\\Users\\Olexii\\source\\repos\\WindowsFormsApp2\\WindowsFormsApp2\\bin\\Debug\\ListOfTeachers.json"));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("smplml23@gmail.com", "ayrmsynnlqjkwcpz");
            smtp.EnableSsl = true;
            smtp.Send(m);
            await botClient.SendTextMessageAsync(message.Chat.Id, "\U0001F4EC Можете перевірити свій e-mail!");
        }

        if (message.Text == "/help")
        {

            await botClient.SendTextMessageAsync(message.Chat.Id, "1. Після відправки команди /start з'явиться вітання\n" +
               "2. Натисніть на кнопку 'Переглянути команди' і виберіть команду, що цікавить\n" + "3. При бажанні можете отримати статистику на пошту\n" +
               "4. Для цього клікніть на 'Отримати статистику на пошту' і відправте свій e-mail");

        }

       
        if (message.Text == "/tanks_perc")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'танків')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати танків країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, Math.Round(((float.Parse(Regex.Match(li.InnerHtml, @"(\d+)\s+(\d+)").Value)) / 13500) * 100, 2) + "%\n" + li.InnerHtml + "\nвійська РФ: 3300\nна зберіганні додатково 10200");
                    break;
                }
            }

        }

        if (message.Text == "/afv_perc")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'ББМ')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати бойових броньованих машин країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, Math.Round(((float.Parse(Regex.Match(li.InnerHtml, @"(\d+)\s+(\d+)").Value)) / 29260) * 100, 2) + "%\n" + li.InnerHtml + "\nвійська РФ: 13760\nна зберіганні додатково 15500");
                    break;
                }
            }

        }

        if (message.Text == "/personnel_perc")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'особового')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати особового складу країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, Math.Round(((float.Parse(Regex.Match(li.InnerHtml, @"\d+").Value)) / 900000) * 100, 2) + "%\n" + li.InnerHtml + "\nвійська РФ: 900000");
                    break;
                }
            }

        }

        if (message.Text == "/warplanes_perc")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'літаків')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати літаків країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, Math.Round(((float.Parse(Regex.Match(li.InnerHtml, @"\d+").Value)) / 1380) * 100, 2) + "%\n" + li.InnerHtml + "\nвійська РФ: 1380");
                    break;
                }
            }

        }

        if (message.Text == "/helicopters_perc")
        {
            var uls = htmlDocument.DocumentNode.Descendants("ul");
            foreach (var currul in uls)
            {
                if (currul.InnerHtml.Contains("артилерійських"))
                {
                    var li = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(text(), 'гелікоптерів')]");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Втрати гелікоптерів країни-оккупанта станом на " + DateTime.Today.ToString("d") + ":");
                    await botClient.SendTextMessageAsync(message.Chat.Id, Math.Round(((float.Parse(Regex.Match(li.InnerHtml, @"\d+").Value)) / 960) * 100, 2) + "%\n" + li.InnerHtml + "\nвійська РФ: 960");
                    break;
                }
            }

        }





    }




}

