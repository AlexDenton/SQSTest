﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQSTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();

            Console.WriteLine("Hello World!");
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
