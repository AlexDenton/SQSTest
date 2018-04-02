using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Extensions.NETCore.Setup;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQSTest
{
    public class SQSHelper
    {
        private readonly IAmazonSQS _AmazonSQSClient;

        private const string _QueueName = "DentonQueue";

        public SQSHelper(AWSOptions options)
        {
            _AmazonSQSClient = options.CreateServiceClient<IAmazonSQS>();
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
            };

            return (await _AmazonSQSClient.GetQueueUrlAsync(request));
        }

        public async Task<SendMessageResponse> SendSQSMessage(string queueUrl, string body)
        {
            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = body
            };

            return await _AmazonSQSClient.SendMessageAsync(sendMessageRequest);
        }

        public async Task<ReceiveMessageResponse> ReceiveSQSMessage(string queueUrl)
        {
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = queueUrl,
            };

            return await _AmazonSQSClient.ReceiveMessageAsync(receiveMessageRequest);
        }

        public async Task<DeleteMessageResponse> DeleteSQSMessage(string queueUrl, string receiptHandle)
        {
            var deleteMessageRequest = new DeleteMessageRequest
            {
                QueueUrl = queueUrl,
                ReceiptHandle = receiptHandle
            };

            return await _AmazonSQSClient.DeleteMessageAsync(deleteMessageRequest);
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