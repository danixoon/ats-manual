using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using ATSManual.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.InteropServices;
using System.IO.Pipes;
using System.Threading;

namespace ATSManual
{
    static class Program
    {
        public static event EventHandler<string> DbChanged = delegate { };
        private static bool isDbInit = false;

        private static async void InitDb()
        {
            if (isDbInit) return;
            isDbInit = true;
            while (true)
            {
                var cmd = new Process()
                {
                    StartInfo = {
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = "cmd.exe"
                }
                };


                cmd.Start();

                cmd.StandardInput.WriteLine("SQLLocalDb start Test\nSQLLocalDb info Test");
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();

                var output = await cmd.StandardOutput.ReadToEndAsync();

                var url = Regex.Match(output, "np:\\\\.+query").ToString();

                Properties.Settings.Default["ATSManualDBConnectionString"] = $"Data Source={url};Initial Catalog=ATS_MANUAL;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                DbChanged(null, url);

                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }


        public async static Task WriteConfig(Dictionary<string, string> conf)
        {
            using (FileStream stream = new FileStream("./config.ini", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
            {
                StreamWriter writer = new StreamWriter(stream);
                foreach (var item in conf)
                    await writer.WriteLineAsync($"{item.Key}={item.Value}");

                writer.Close();
            }

            Console.Write("yep");
        }


        public static Dictionary<string, string> ReadConfig()
        {
            var separator = '=';
            var conf = new Dictionary<string, string>();

            using (FileStream stream = new FileStream("./config.ini", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
            {
                StreamReader reader = new StreamReader(stream);

                var text = reader.ReadToEnd().Trim();
                if (text.Length == 0) return conf;

                var content = text.Split('\n');

                foreach (var line in content)
                {
                    var splitted = line.Split(separator);
                    if (splitted.Length == 0) continue;
                    conf[splitted[0].ToUpper()] = splitted.Length == 1 ? "" : string.Join(separator.ToString(), splitted.Skip(1));
                }



                return conf;
            }
        }

        private const int SW_SHOWMAXIMIZED = 3;
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var proc = Process.GetProcessesByName("ATSManual");

            if (proc.Count() > 1)
            {
                using (NamedPipeClientStream client = new NamedPipeClientStream("ats-manual-command"))
                {
                    client.Connect(1000);
                    using (StreamWriter wr = new StreamWriter(client))
                    {
                        wr.WriteLine("tray_show");
                        wr.WriteLine("exit");
                        wr.Flush();
                    }
                }

                Exit();
                return;
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form = new ManualForm();

            if (true || App.IsProd)
            {
                StartLogging();

                var conf = ReadConfig();
                if (conf.ContainsKey("DATABASE_CONNECTION_STRING"))
                    Properties.Settings.Default["ATSManualDBConnectionString"] = conf["DATABASE_CONNECTION_STRING"];

                App.config = conf;

                if (conf.Count > 0)
                    Logging.Logger.Log($"Приложение запущено с параметрами: {string.Join(", ", conf.Select(p => $"{p.Key}={p.Value}"))}", Logging.Logger.MessageType.System);
                else
                    Logging.Logger.Log("Приложение запущено без параметров", Logging.Logger.MessageType.System);
            }


            StartServer();



            Application.Run(Form);
            Logging.Logger.Log($"Приложение штатно закрыто", Logging.Logger.MessageType.System);

            Exit();
        }

        static void Exit()
        {
            ClosedTokenSource.Cancel();
            Application.Exit();
        }

        public static bool ItIsTime()
        {
            var now = DateTime.UtcNow;
            //return true;
            return now.Day == 21 && now.Month == 12;
        }

        public static async void StartEgg()
        {
            while (true)
            {
                try
                {
                    if (ItIsTime())
                    {
                        Form.Text = "ДМБ 2021 (!) Это просто Бомба";
                        if (!App.config.ContainsKey("EGG"))
                        {
                            App.config["EGG"] = "yeah";
                            await WriteConfig(App.config);
                        }
                        Form.EnsureEgg();
                    }
                    else
                    {
                        if (App.config.ContainsKey("EGG"))
                        {
                            Form.EnsureEgg();
                            App.config.Remove("EGG");
                            await WriteConfig(App.config);
                        }
                    }
                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
                catch (Exception ex)
                {
                    // p0huy
                }
            }
        }

        public static async void StartBackup()
        {
            while (true)
            {
                try
                {
                    await App.store.App.RunTask("export", ItIsTime() ? "Запланированный дембель" : $"Запланированное экспортирование", Task.Run(() =>
                    {
                        var now = DateTime.Now;
                        string minutes = (now.Minute - now.Minute % 30).ToString();

                        minutes = minutes.Length == 1 ? $"0{minutes}" : minutes;



                        string file = $"Экспорт {now.ToShortDateString()}_{now.Hour}.{minutes}";
                        string dir = $"{Application.StartupPath}\\выгрузка";
                        var path = $"{dir}\\{file}.xlsx";

                        Directory.CreateDirectory(dir);

                        Import.Importer.ExportExcelFile(path);
                    }));
                }
                catch (Exception e)
                {
                    MessageBox.Show("Произошла ошибка при запланированном экспорте: " + e.Message, "Ошибка");
                    Logging.Logger.Log("Ошибка запланированного экспорта: " + e.Message, Logging.Logger.MessageType.Error);
                }

                await Task.Delay(TimeSpan.FromMinutes(30));
            }
        }

        static void StartLogging()
        {
            FileStream ostrm;
            StreamWriter writer;

            try
            {
                ostrm = new FileStream("./logs.log", FileMode.Append);
                writer = new StreamWriter(ostrm);
                writer.AutoFlush = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно произвести логгирование: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logging.Logger.Log($"Невозможно произвести логгирование: " + ex.Message, Logging.Logger.MessageType.Error);
                return;
            }

            Console.SetOut(writer);
            Console.SetError(writer);
        }

        public static ManualForm Form { get; private set; }
        public static CancellationTokenSource ClosedTokenSource { get; private set; } = new CancellationTokenSource();

        static Task StartServer()
        {
            //var proc = Process.GetCurrentProcess();
            return Task.Factory.StartNew(async () =>
            {
                //try
                //{

                while (true)
                {
                    //Logging.Logger.Log($"Создание сервера");
                    using (NamedPipeServerStream server = new NamedPipeServerStream("ats-manual-command"))
                    {
                        //while (!server.IsConnected)
                        //{
                        //server.BeginWaitForConnection((r) =>
                        //{

                        //}, null);
                        //Logging.Logger.Log($"Ожидание соединения..");
                        server.WaitForConnection();
                        //Logging.Logger.Log($"Соединение.");

                        using (StreamReader reader = new StreamReader(server))
                        {
                            string temp;
                            do
                            {
                                temp = await reader.ReadLineAsync();
                                switch (temp)
                                {
                                    case "tray_show":
                                        Form.ShowFromTray();
                                        break;
                                }
                                //Logging.Logger.Log($"Сообщение: {temp}");
                            } while (temp != "exit");
                        }

                        //Logging.Logger.Log($"Чтение завершено.");
                    }
                    //}
                }

            }, ClosedTokenSource.Token);


        }
    }
}
