using System;
using System.Threading.Tasks;

namespace SQSTest
{
    public class SQSQueueSender
    {
        private SQSHelper _SQSHelper;

        private readonly TimeSpan _SendDelay = TimeSpan.FromMilliseconds(1);

        public SQSQueueSender(SQSHelper sqsHelper)
        {
            _SQSHelper = sqsHelper;
        }

        public async Task Run()
        {
            var queueUrlResponse = await _SQSHelper.GetQueueUrl();

            for (var i = 0; i < 10; i++)
            {
                await _SQSHelper.SendSQSMessage(queueUrlResponse.QueueUrl, i.ToString());
                await Task.Delay(_SendDelay);
            }
        }
    }
}