using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using Renci.SshNet;

namespace V2ray_Installer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string sip = string.Empty;
            string suser = string.Empty;
            string sport = string.Empty;
            string puser = string.Empty;
            string spass = string.Empty;
            string ppass = string.Empty;
            string pport = string.Empty;
            
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome To V2ray Installer ...");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine();
                Console.WriteLine("Please Enter Server Ip Address :");
                sip = Console.ReadLine();
                if (sip != "")
                    break;
            }
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine();
                Console.WriteLine("Please Enter Server Port (Default is 22) :");
                sport = Console.ReadLine();
                if (sport != "")
                    break;
            }
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine();
                Console.WriteLine("Please Enter Server UserName (Default is root) :");
                suser = Console.ReadLine();
                if (suser != "")
                    break;
            }
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine();
                Console.WriteLine("Please Enter Server PassWord :");
                spass = Console.ReadLine();
                if (spass != "")
                    break;
            }

            try
            {

                var passwordConnectionInfo = new PasswordConnectionInfo(sip, Convert.ToInt32(sport), suser, spass);
                passwordConnectionInfo.Timeout = TimeSpan.FromSeconds(10000.0);
                using (SshClient sshClient = new SshClient(passwordConnectionInfo))
                {
                    try
                    {

                        sshClient.Connect();

                        bool isConnected = sshClient.IsConnected;

                        if (isConnected)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine();
                            Console.WriteLine("Login Is Successful");
                            while (true)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine();
                                Console.WriteLine("Please Enter Panel UserName :");
                                puser = Console.ReadLine();
                                if (puser != "")
                                    break;
                            }
                            while (true)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine();
                                Console.WriteLine("Please Enter Panel PassWord :");
                                ppass = Console.ReadLine();
                                if (ppass != "")
                                    break;
                            }
                            while (true)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine();
                                Console.WriteLine("Please Enter Panel Port (Default is 54321) :");
                                pport = Console.ReadLine();
                                if (pport != "")
                                    break;
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine();
                            Console.WriteLine("Installing ...");
                            SshCommand sc = sshClient.RunCommand("sudo wget https://raw.githubusercontent.com/rgoogoonani/installv2ray/main/MainInstall.sh && sudo chmod +x MainInstall.sh && sudo ./MainInstall.sh "+puser+" "+ppass+" "+pport);
                            
                            string answer1 = sc.Result;

                            if (answer1.Contains("安装完成，面板已启动"))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine();
                                Console.WriteLine("The Installation Was Successful.");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine();
                                Console.WriteLine("Panel UserName : "+puser);
                                Console.WriteLine();
                                Console.WriteLine("Panel PassWord : "+ppass);
                                var uri = "http://" + sip + ":" + pport;
                                var psi = new System.Diagnostics.ProcessStartInfo();
                                psi.UseShellExecute = true;
                                psi.FileName = uri;
                                System.Diagnostics.Process.Start(psi);
                                
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("The Installation Was Not Successful.");
                            }
                            sshClient.Disconnect();
                            sshClient.Dispose();
                            
                            
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("UserName Or PassWord Is InCorect");
                        }
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Press Enter to Exit ...");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                    catch
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Error to Connect server");
                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Press Enter to Exit ...");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
            catch
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error to Connect server");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press Enter to Exit ...");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
