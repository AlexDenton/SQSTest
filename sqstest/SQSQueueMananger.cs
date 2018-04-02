using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Extensions.NETCore.Setup;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQSTest
{
    public class SQSQueueMananger
    {
        private readonly IAmazonSQS _AmazonSQSClient;

        private readonly string _QueueName;

        private readonly string _QueueUrl;

        public SQSQueueMananger(
            AWSOptions options,
            string queueName)
        {
            _AmazonSQSClient = options.CreateServiceClient<IAmazonSQS>();
            _QueueName = queueName;
            _QueueUrl = GetQueueUrl().Result.QueueUrl;
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

            return await _AmazonSQSClient.GetQueueUrlAsync(request);
        }

        public async Task<SendMessageResponse> SendSQSMessage(string body)
        {
            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = _QueueUrl,
                MessageBody = body
            };

            return await _AmazonSQSClient.SendMessageAsync(sendMessageRequest);
        }

        public async Task<ReceiveMessageResponse> ReceiveSQSMessage()
        {
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = _QueueUrl,
                MaxNumberOfMessages = 10,
                WaitTimeSeconds = 5,
            };

            return await _AmazonSQSClient.ReceiveMessageAsync(receiveMessageRequest);
        }

        public async Task<DeleteMessageResponse> DeleteSQSMessage(string receiptHandle)
        {
            var deleteMessageRequest = new DeleteMessageRequest
            {
                QueueUrl = _QueueUrl,
                ReceiptHandle = receiptHandle
            };

            return await _AmazonSQSClient.DeleteMessageAsync(deleteMessageRequest);
        }
    }
}