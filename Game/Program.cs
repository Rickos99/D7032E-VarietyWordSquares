using Game.Core.Communication;
using Game.Core.Network;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Game.UI.Console.Menus;
using System.Threading;
using Game.Core.IO;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestClientHostConnection();
            //TestTimedInput();
            //TestTimeoutInput();

            //ReadAllSettings();
            //ReadSetting("Language.UI");
            //AddUpdateAppSettings("Player.Humans", "3");
            //AddUpdateAppSettings("Player.Bots", "0");

            //new Game(args);

            ReadLine(5);

            var HCMenu = GetHostClientMenu();
            HCMenu.Show();
        }

        private static ConsoleMenu GetHostClientMenu()
        {
            var header = "Welcome to VarietyWordSquares";
            var menuChoices = new List<MenuChoice>()
            {
                new MenuChoice("H", "Host a game", () => new GameHost()),
                new MenuChoice("C", "Connect to a game", () => new GameClient()),
                new MenuChoice("Test", "Test Host", () => TestHost())
            };
            return new ConsoleMenu(header, menuChoices);
        }

        static void TestHost()
        {
            var host = new Host(5500);
            host.Start();

            Console.WriteLine("Waiting for incoming connections");

            var client = host.WaitForIncomingConnection();
            while (true)
            {
                Console.Write("Enter a question to send: ");
                Host.SendMessageToClient(new OpenQuestion(Console.ReadLine()), client);

                var msg = Host.ReadMessageFromClient(client);
                Console.WriteLine($"Recieved: {msg.Content}");
                Console.Write("Answer: ");
            }
        }

        static void ReadLine(int secondsTimeout)
        {
            try
            {
                Console.WriteLine($"Please enter your name within the next {secondsTimeout} seconds.");
                string name = TimedConsoleReader.ReadLine(secondsTimeout*1000);
                Console.WriteLine("Hello, {0}!", name);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("Sorry, you waited too long.");
            }
        }

        //static void ReadAllSettings()
        //{
        //    try
        //    {
        //        var appSettings = ConfigurationManager.AppSettings;

        //        if (appSettings.Count == 0)
        //        {
        //            Console.WriteLine("AppSettings is empty.");
        //        }
        //        else
        //        {
        //            foreach (var key in appSettings.AllKeys)
        //            {
        //                Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
        //            }
        //        }
        //    }
        //    catch (ConfigurationErrorsException)
        //    {
        //        Console.WriteLine("Error reading app settings");
        //    }
        //}

        //static void ReadSetting(string key)
        //{
        //    try
        //    {
        //        var appSettings = ConfigurationManager.AppSettings;
        //        string result = appSettings[key] ?? "Not Found";
        //        Console.WriteLine(result);
        //    }
        //    catch (ConfigurationErrorsException)
        //    {
        //        Console.WriteLine("Error reading app settings");
        //    }
        //}

        //static void AddUpdateAppSettings(string key, string value)
        //{
        //    try
        //    {
        //        var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //        var settings = configFile.AppSettings.Settings;
        //        if (settings[key] == null)
        //        {
        //            settings.Add(key, value);
        //        }
        //        else
        //        {
        //            settings[key].Value = value;
        //        }
        //        configFile.Save(ConfigurationSaveMode.Modified);
        //        ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        //    }
        //    catch (ConfigurationErrorsException)
        //    {
        //        Console.WriteLine("Error writing app settings");
        //    }
        //}

        //public static void TestTimeoutInput()
        //{
        //    var alphabet = "abcdefghijklmnopqrstuvwxyz";
        //    var timeIsOut = false;
        //    var seconds = 10;
        //    Task.Run(() =>
        //    {
        //        Task.Delay(seconds * 1000).Wait();
        //        timeIsOut = true;
        //    });

        //    var pressedKeys = new Queue<ConsoleKeyInfo>();
        //    Console.WriteLine("Write some text...");
        //    while (!timeIsOut)
        //    {
        //        var key = Console.ReadKey(true);
        //        if (alphabet.Contains(key.KeyChar))
        //        {
        //            Console.Write(key.KeyChar);
        //        }
        //        else
        //        {
        //            switch (key.Key)
        //            {
        //                case ConsoleKey.Backspace:
        //                case ConsoleKey.Delete:
        //                    Console.SetCursorPosition(Console.CursorLeft-2, Console.CursorTop);
        //                    Console.Write(' ');
        //                    break;
        //                case ConsoleKey.LeftArrow:
        //                    Console.SetCursorPosition(Console.CursorLeft--, Console.CursorTop);
        //                    break;
        //                case ConsoleKey.RightArrow:
        //                    Console.SetCursorPosition(Console.CursorLeft++, Console.CursorTop);
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //        //pressedKeys.Enqueue(key);
        //    }

        //    Console.WriteLine("Time is out");
        //    Console.ReadKey();
        //}

        //public static void TestTimedInput()
        //{
        //    var delay = 10;

        //    var input = Task.Run(() =>
        //    {
        //        return Console.ReadLine();
        //    });
        //    var timer = Task.Run(() =>
        //    {
        //        for (var i = 10; i > 0; i--)
        //        {
        //            Console.Write("\r");
        //            Console.Write("{0} seconds left...{1}", i, i == 1 ? "\n" : "");
        //            Task.Delay(1000).Wait();
        //        }
        //    });

        //    Task.WaitAny(input, timer);
        //    var x = input.Result;
        //    Console.ReadLine();
        //}

        //public static void TestClientHostConnection()
        //{
        //    Console.WriteLine("Choose a option: \n\t [H] Create host \n\t [C] Create client");
        //    bool createHost = Console.ReadLine().ToUpper() == "H";

        //    if (createHost)
        //    {
        //        var host = new Host(5500);
        //        host.Start();

        //        var client = host.WaitForIncomingConnection();
        //        while (true)
        //        {
        //            var msg = host.ReadMessageFromClient(client);

        //            Console.WriteLine($"Recieved: {msg.Content}");
        //            Console.Write("Answer: ");
        //            host.SendMessageToClient(new InformationMessage(Console.ReadLine()), client);
        //        }
        //    }
        //    else
        //    {
        //        var client = new Client(5500);
        //        client.OpenConnection();

        //        while (true)
        //        {
        //            Console.Write("Type a question: ");
        //            var msg = new OpenQuestion(Console.ReadLine());
        //            client.SendMessage(msg);
        //            Console.WriteLine("Question has been sent!");
        //            Console.WriteLine("Waiting for answer...");
        //            var answer = client.ReadMessage();
        //            Console.WriteLine($"Answer: {answer.Content}");
        //        }
        //    }

        //}

        //public static void TestNetPackets()
        //{
        //    // Prepare
        //    IMessage msg = new ClosedQuestion("Is this working as you expected?", new List<Choice>() {
        //        new Choice("1", "Yes"),
        //        new Choice("2", "No"),
        //    });

        //    //IMessage msg = new OpenQuestion("What is your name");

        //    // JSON Serializer
        //    //var opts = new JsonSerializerSettings()
        //    //{
        //    //    TypeNameHandling = TypeNameHandling.All
        //    //};
        //    //var msgSent = JsonConvert.SerializeObject(msgToSend, opts);
        //    //var msgRecieved = (MessageBase)JsonConvert.DeserializeObject(msgSent, opts);

        //    // XML Serializer
        //    //var msgSent = Serialize(msg);
        //    //var msgRecieved = Deserialize(msgSent, typeof(ClosedQuestion));

        //    // NetPackets
        //    var packet = new NetPacket<IMessage>(msg);
        //    var packetSerialized = JsonSerializer.Serialize(packet);
        //    var packetDeserialized = JsonSerializer.Deserialize<NetPacket<IMessage>>(packetSerialized);
        //    var message = MessageMapper.From(packetDeserialized);
        //}
    }
}