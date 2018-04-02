using System;
using System.Threading.Tasks;

namespace SQSTest
{
    public class SQSQueueReceiver
    {
        private readonly SQSQueueMananger _sqsQueueMananger;

        private readonly TimeSpan _PollDelay = TimeSpan.FromMilliseconds(1);

        public SQSQueueReceiver(SQSQueueMananger sqsQueueMananger)
        {
            _sqsQueueMananger = sqsQueueMananger;
        }

        public async Task Run()
        {
            while (true)
            {
                var receiveMessageResponse = await _sqsQueueMananger.ReceiveSQSMessage();

                foreach (var message in receiveMessageResponse.Messages)
                {
                    Console.Write($"{message.Body}, ");
                    var deleteMessageResponse = await _sqsQueueMananger.DeleteSQSMessage(message.ReceiptHandle);
                }

                Console.WriteLine();

                await Task.Delay(_PollDelay);
            }
        }

    }
}