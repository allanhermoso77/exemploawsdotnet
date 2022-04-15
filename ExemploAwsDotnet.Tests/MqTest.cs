using Amazon;
using ExemploAwsDotnet.ServiceModel;
using NUnit.Framework;
using ServiceStack.Messaging;
using ServiceStack.Aws.Sqs;

namespace ExemploAwsDotnet.Tests
{
    public class MqTest
    {
        private SqsConnectionFactory sqsFactory;
        SqsQueueManager sqsQueueManager;
        private readonly SqsMqMessageFactory MqFactory;
        public MqTest()
        {
            sqsFactory = new SqsConnectionFactory(
                "<AWS ACCESS KEY>",
                "<AWS SECRET KEY>",
                RegionEndpoint.USEast1);

            sqsQueueManager = new SqsQueueManager(sqsFactory) {
                DisableBuffering = true,
            };
            
            MqFactory = new SqsMqMessageFactory(sqsQueueManager);
            
            //Delete all ProjetoStart MQ's
            //sqsQueueManager.PurgeQueues(QueueNames<ProjetoStart>.AllQueueNames);
        }

        [Test] // requires running Host MQ Server project
        public void Can_send_Request_Reply_message()
        {
            using (var mqClient = MqFactory.CreateMessageQueueClient())
            {
                var replyToMq = mqClient.GetTempQueueName();

                mqClient.Publish(new Message<ProjetoStart>(new ProjetoStart { Name = "MQ Worker" })
                {
                    ReplyTo = replyToMq,
                });

                var responseMsg = mqClient.Get<ProjetoStartResponse>(replyToMq);
                mqClient.Ack(responseMsg);
                Assert.That(responseMsg.GetBody().Result, Is.EqualTo("ProjetoStart, MQ Worker!"));
            }
        }
    }
}