using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQSTest
{
    public class SQSHelper
    {
        private readonly AmazonSQSClient _AmazonSQSClient;

        private const string _QueueName = "DentonQueue";

        public SQSHelper()
        {
            _AmazonSQSClient = GetSQSClient();
        }

        public static AmazonSQSClient GetSQSClient()
        {
            var sqsConfig = new AmazonSQSConfig
            {
                ServiceURL = ""
            };

            return new AmazonSQSClient(sqsConfig);
        }

        public async Task<CreateQueueResponse> CreateSQSQueue()
        {
            var createQueueRequest = new CreateQueueRequest
            {
                QueueName = _QueueName,
                Attributes = new Dictionary<string, string>
                {
                    {
                        QueueAttributeName.VisibilityTimeout, "10"
                    }
                }
            };

            return await _AmazonSQSClient.CreateQueueAsync(createQueueRequest);
        }

        public async Task<GetQueueUrlResponse> GetQueueUrl()
        {
            var request = new GetQueueUrlRequest
            {
                QueueName = _QueueName,
                QueueOwnerAWSAccountId = ""
            };

            return (await _AmazonSQSClient.GetQueueUrlAsync(request));
        }

        public async Task<SendMessageResponse> SendSQSMessage(string queueUrl)
        {
            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = "Hello there"
            };

            return await _AmazonSQSClient.SendMessageAsync(sendMessageRequest);
        }

        public async Task<ReceiveMessageResponse> ReceiveSQSMessage(string queueUrl)
        {
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = queueUrl
            };

            return await _AmazonSQSClient.ReceiveMessageAsync(receiveMessageRequest);
        }

        public void ProcessReceiveMessageResponse(ReceiveMessageResponse response)
        {
            foreach (var message in response.Messages)
            {
                Console.WriteLine(message.Body);
            }
        }
    }
}