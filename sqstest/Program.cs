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
            Configuration = new ConfigurationBuilder().Build();
            var options = Configuration.GetAWSOptions();
            Console.WriteLine(options.Profile);

            //Run().Wait();
        }

        public static async Task Run()
        {
            var sqsClient = SQSHelper.GetSQSClient();
            var sqsHelper = new SQSHelper();
            var createQueueResponse = await sqsHelper.CreateSQSQueue();
            var queueUrlResponse = await sqsHelper.GetQueueUrl();
            var sendMessageResponse = await sqsHelper.SendSQSMessage(queueUrlResponse.QueueUrl);
            var receiveMessageResponse = await sqsHelper.ReceiveSQSMessage(queueUrlResponse.QueueUrl);
            await sqsHelper.ProcessReceiveMessageResponse(receiveMessageResponse);
        }
    }
}
