using System;
using System.Linq;
using System.Threading.Tasks;

namespace SQSTest
{
    public class SQSQueueReceiver
    {
        private readonly SQSHelper _SQSHelper;

        private readonly TimeSpan _PollDelay = TimeSpan.FromMilliseconds(1);

        public SQSQueueReceiver(SQSHelper sqsHelper)
        {
            _SQSHelper = sqsHelper;
        }

        public async Task Run()
        {
            var queueUrlResponse = await _SQSHelper.GetQueueUrl();

            while (true)
            {
                var receiveMessageResponse = await _SQSHelper.ReceiveSQSMessage(queueUrlResponse.QueueUrl);

                foreach (var message in receiveMessageResponse.Messages)
                {
                    Console.Write($"{message.Body}, ");
                    var deleteMessageResponse = await _SQSHelper.DeleteSQSMessage(queueUrlResponse.QueueUrl, message.ReceiptHandle);
                }

                Console.WriteLine();

                await Task.Delay(_PollDelay);
            }
        }

    }
}