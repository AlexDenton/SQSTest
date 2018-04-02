using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;

namespace SQSTest
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

            Run().Wait();
        }

        public static async Task Run()
        {
            var options = Configuration.GetAWSOptions();
            var sqsQueueMananger = new SQSQueueMananger(options, "DentonQueue");

            var sqsQueueSender = new SQSQueueSender(sqsQueueMananger);
            var senderTask = sqsQueueSender.Run();

            var sqsQueueReceiver = new SQSQueueReceiver(sqsQueueMananger);
            var receiverTask = sqsQueueReceiver.Run();

            await Task.WhenAll(
                senderTask,
                receiverTask);
        }
    }
}
