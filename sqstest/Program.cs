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
            Console.WriteLine($"Profile: {options.Profile}");
            var client = options.CreateServiceClient<IAmazonSQS>();
            Console.WriteLine($"ServiceURL: {client.Config.DetermineServiceURL()}");

            //var credentials = await options.Credentials.GetCredentialsAsync();
            //Console.WriteLine($"Access Key: {credentials.AccessKey}");

            //var sqsHelper = new SQSHelper();
            //var createQueueResponse = await sqsHelper.CreateSQSQueue();
            //var queueUrlResponse = await sqsHelper.GetQueueUrl();
            //var sendMessageResponse = await sqsHelper.SendSQSMessage(queueUrlResponse.QueueUrl);
            //var receiveMessageResponse = await sqsHelper.ReceiveSQSMessage(queueUrlResponse.QueueUrl);
            //sqsHelper.ProcessReceiveMessageResponse(receiveMessageResponse);
        }
    }
}
