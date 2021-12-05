using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;
using System.Net.Sockets;
using System.Net;

namespace TMSProject
{
    class Program
    {
        static public bool runServer = true;
        static public TMSDBInteraction tmsdb;

        static void Main(string[] args)
        {
            // <--------------- Start Testing CarrierUpdateSystem
            // Files
            string csvFileIn = "../../Carriers.csv";
            string csvFileOut = "../../Carriersnew.csv";
            CarrierUpdateSystemCommunication.UpdateCSV(CarrierUpdateSystemCommunication.ReadCSV(csvFileIn), csvFileOut);
            // <--------------- End Testing CarrierUpdateSystem

            tmsdb = new TMSDBInteraction();
            Thread serverThread = new Thread(new ThreadStart(StartServer));
            Thread stopPoller = new Thread(new ThreadStart(StopServer));
            
            serverThread.Start();
            stopPoller.Start();

            serverThread.Join();
            stopPoller.Join();
        }

        static public void StartServer()
        {
            TcpListener server = null;
            string serverMessage;
            IPAddress serverIP;
            int serverPort;

            try
            {
                serverIP = IPAddress.Parse(ConfigurationManager.AppSettings.Get("serverIP"));
                serverPort = Int32.Parse(ConfigurationManager.AppSettings.Get("serverPort"));
                //Create a new TCP listener with the provided IP and port
                server = new TcpListener(serverIP, serverPort);

                //Start the server up
                server.Start();
                Logger.WriteLog("Server Started");

                while (runServer)
                {
                    if (server.Pending())
                    {
                        TcpClient client = server.AcceptTcpClient();

                        serverMessage = "Connected";
                        Logger.WriteLog(serverMessage);

                        ParameterizedThreadStart ts = new ParameterizedThreadStart(ProcessClient);
                        Thread clientThread = new Thread(ts);
                        clientThread.Start(client);
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
            }
            catch (Exception ex)
            {
                serverMessage = ex.Message;
                Logger.WriteLog(serverMessage);
            }
            finally
            {
                if (server != null)
                {
                    server.Stop();
                }
            }
        }

        static public void ProcessClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            string serverMessage;
            try
            {
                //Create buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                //Create the stream object to read and write
                NetworkStream stream = client.GetStream();

                int i = 0;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = Encoding.ASCII.GetString(bytes, 0, i);
                    byte[] msg = null;//This will be used to send the information back

                    serverMessage = " : Received: " + data;
                    Logger.WriteLog(serverMessage);

                    msg = Encoding.ASCII.GetBytes(tmsdb.Command(data));

                    Logger.WriteLog(" : Response: " + Encoding.ASCII.GetString(msg));

                    stream.Write(msg, 0, msg.Length);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
            }
            finally
            {
                client.Close();
            }
        }


        static public void StopServer()
        {
            while(runServer)
            {
                char key = (char)Console.Read();

                if(key == 'x' || key == 'X')
                {
                    runServer = false;
                    Logger.WriteLog("Server stopping...");
                }
                Thread.Sleep(25);
            }
            
        }
    }
}
