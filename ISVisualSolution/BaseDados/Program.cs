using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;

namespace BaseDados
{

    class Program
    {
        public static string conn = Properties.Settings.Default.ConnectionString;
        public static string[] topics = { "news" };
        static void Main(string[] args)
        {
            Console.Write("Escreva um ip: ");
            string ip = Console.ReadLine();
            ip.Trim();
            ip = "127.0.0.1";
            Console.WriteLine("Seu ip é: " + ip);
            MqttClient mClient = new MqttClient(ip);
            mClient.Connect(Guid.NewGuid().ToString());
            if (!mClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }
            else
            {
                Console.WriteLine("Broker OK...");
            }
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
            mClient.Subscribe(topics, qosLevels);

            mClient.MqttMsgPublishReceived += MClient_MqttMsgPublishReceived;
          
            Console.ReadKey();
        }

        private static void MClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string msg = Encoding.UTF8.GetString(e.Message);
            Console.WriteLine($"Topico:{e.Topic}|Msg:{msg}");
            dynamic response=JsonConvert.DeserializeObject(msg);
            Console.WriteLine("json: " + response);
            
        }
    }
}
