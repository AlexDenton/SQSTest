using System;
using System.Threading.Tasks;

namespace SQSTest
{
    public class SQSQueueSender
    {
        private SQSQueueMananger _sqsQueueMananger;

        private readonly TimeSpan _SendDelay = TimeSpan.FromMilliseconds(1);

        public SQSQueueSender(SQSQueueMananger sqsQueueMananger)
        {
            _sqsQueueMananger = sqsQueueMananger;
        }

        public async Task Run()
        {
            for (var i = 0; i < 10; i++)
            {
                await _sqsQueueMananger.SendSQSMessage(i.ToString());
                await Task.Delay(_SendDelay);
            }
        }
    }
}