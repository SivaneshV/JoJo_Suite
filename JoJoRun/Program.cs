using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Activities.XamlIntegration;
using System.IO;
using System.Threading;
using System.Configuration;
using JoJoSuite.Business.Lib;

namespace JoJoSuite.Business.Run
{
    class Program
    {
        static void Main(string[] args)
        {
            //args: 0-botId, 1-schedule/test, 2-userId

            Console.WriteLine("R2rRun: starting robot.");

            int botId = 0;
            int userId = 0;

            r2rLib r2rLib = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);

            if (args.Length >= 1)
            {
                Int32.TryParse(args[0], out botId);

                if (botId > 0)
                {
                    r2rBot bot = r2rLib.GetBot(botId);

                    r2rUser user = new r2rUser();
                    user.Id = 0;

                    if (bot.Id > 0)
                    {
                        if (bot.XAML.Contains("CountTracker") == false)
                        {
                            Console.WriteLine("R2rRun: bot:" + bot.Title);
                            Console.WriteLine("R2rRun: TRANSACTION-TRACKER IS MANDATORY FOR ALL BOTS.");
                            Console.WriteLine("R2rRun: Please add TransactionTracker to the bot.");
                        }
                        else
                        {
                            Console.WriteLine("R2rRun: running:" + bot.Title);

                            try
                            {
                                r2rBotRun run1 = new r2rBotRun();

                                run1.Bot = bot;
                                run1.DateRun = run1.TimeStart = run1.TimeEnd = DateTime.Now;
                                run1.TestRun = false;

                                if (args.Length >= 2)
                                {
                                    run1.TestRun = (args[1] == "1");
                                }
                                if (args.Length >= 3)
                                {
                                    Int32.TryParse(args[2], out userId);

                                    if (userId > 0)
                                    {
                                        user = r2rLib.GetUser(userId);
                                    }

                                }

                                run1.User = user;

                                run1.Id = r2rLib.AddBotRun(run1);

                                //221205
                                string sXaml = bot.XAML;

                                sXaml = sXaml.Replace("221205", run1.Id.ToString());

                                using (var stream = StringToStream(sXaml))
                                {
                                    Console.WriteLine("R2rRun: bot start time:" + run1.TimeStart.ToShortTimeString());
                                    Activity workflowActivity = (Activity)ActivityXamlServices.Load(stream);
                                    WorkflowInvoker.Invoke(workflowActivity);
                                    Console.WriteLine("R2rRun: finished running.");
                                    r2rLib.UpdateBotRunEndTime(run1, DateTime.Now);
                                    Console.WriteLine("R2rRun: bot end time:" + run1.TimeEnd.ToShortTimeString());
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("R2rRun: Bot not found with Id: " + botId);
                }
            }
            else
            {
                Console.WriteLine("R2rRun: file not supplied.");
            }

            Console.WriteLine("R2rRun: execution completed, closing.");
            Thread.Sleep(5000);
        }

        public static Stream StringToStream(string text)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
