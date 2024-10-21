using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Client
    {
        static void Main(string[] args)
        {
            var firstComand = Console.ReadLine();
            String comand = firstComand.Split(' ')[0];
            if (comand == "server")
            {
                Console.WriteLine("Соединение установлено");
                String socket = firstComand.Split(' ')[1];
                String ip = socket.Split(':')[0];
                int port = Convert.ToInt32(socket.Split(':')[1]);
                TcpClient tcpClient = new TcpClient(ip, port);
                StreamWriter writer = new StreamWriter(tcpClient.GetStream());
                StreamReader reader = new StreamReader(tcpClient.GetStream());
                //sWriter.WriteLine("server");
                //sWriter.Flush();
                while (true){
                    try
                    {
                        var secondComand = Console.ReadLine();
                        if (secondComand.Split(' ')[0] == "upload")
                        {
                            var file_path = secondComand.Split(' ')[1];                          
                            byte[] bytes = File.ReadAllBytes(file_path);

                            writer.WriteLine("upload");
                            writer.Flush();

                            writer.WriteLine(bytes.Length.ToString());
                            writer.Flush();

                            writer.WriteLine(file_path);
                            writer.Flush();           
                            
                            tcpClient.Client.SendFile(file_path);
                            Console.WriteLine("Файл отправлен");
                        }
                        else if(secondComand.Split(' ')[0] == "download")
                        {
                            String directotyPath = secondComand.Split(' ')[1];   
                            writer.WriteLine("download");
                            writer.Flush();
                            /*foreach (int countFile in countFiles)
                            {
                                String countFiles = reader.ReadLine();
                                String filename = reader.ReadLine();
                                String filePath = Path.Combine(directotyPath, filename);
                                using (StreamWriter writer2 = new StreamWriter(filePath)) {
                                    writer2.Write(reader.Read());
                                    writer2.Flush();
                                }
                            
                            }
                        */
                            Console.WriteLine("Все файлы записаны на диск");
                        }
                        else
                        {
                            Console.WriteLine("Неверная команда");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("Неверная команда");
            }
        }
        void GetFiles()
        {

        }
    }
}
