using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace telegram_bot_app
{
    public class Program
    {
        private static TelegramBotClient bot { get; set; }
        static async Task Main(string[] args)
        {
            using var cts = new CancellationTokenSource();
            bot = new TelegramBotClient(
                "8524557401:AAGNrdWFFYjiTD1J6lQqfjKi6kAA0Mu5zHc",
                cancellationToken: cts.Token);
            bot.OnMessage += OnMessage;
            Console.WriteLine($"Bot started ...");
            Console.ReadLine();
            cts.Cancel(); // stop the bot
        }

        private static async Task OnMessage(Message msg, UpdateType type)
        {
            var chatId = msg.Chat.Id;

            if (msg.Text.StartsWith("/add"))
            {
                string task = msg.Text.Replace("/add", "").Trim();

                if (task == "")
                {
                    await bot.SendMessage(chatId, "Write task after /add");
                    return;
                }

                File.AppendAllText("tasks.txt", task + Environment.NewLine);

                await bot.SendMessage(chatId, "Task added!");
            }
            else if (msg.Text == "/list")
            {
                if (!File.Exists("tasks.txt"))
                {
                    await bot.SendMessage(chatId, "No tasks yet.");
                    return;
                }

                string[] tasks = File.ReadAllLines("tasks.txt");

                if (tasks.Length == 0)
                {
                    await bot.SendMessage(chatId, "No tasks.");
                    return;
                }

                string result = "";

                for (int i = 0; i < tasks.Length; i++)
                {
                    result += (i + 1) + ". " + tasks[i] + Environment.NewLine;
                }

                await bot.SendMessage(chatId, result);
            }
            else if (msg.Text.StartsWith("/remove"))
            {
                if (!File.Exists("tasks.txt"))
                {
                    await bot.SendMessage(chatId, "No tasks.");
                    return;
                }

                string numberText = msg.Text.Replace("/remove", "").Trim();

                if (!int.TryParse(numberText, out int number))
                {
                    await bot.SendMessage(chatId, "Write correct number.");
                    return;
                }

                string[] tasks = File.ReadAllLines("tasks.txt");

                if (number <= 0 || number > tasks.Length)
                {
                    await bot.SendMessage(chatId, "No task with this number.");
                    return;
                }

                List<string> newTasks = new List<string>();

                for (int i = 0; i < tasks.Length; i++)
                {
                    if (i != number - 1)
                    {
                        newTasks.Add(tasks[i]);
                    }
                }

                File.WriteAllLines("tasks.txt", newTasks);

                await bot.SendMessage(chatId, "Task removed!");
            }
            else
            {
                await bot.SendMessage(chatId, "Commands: /add /list /remove");
            }
        }
    }
}
