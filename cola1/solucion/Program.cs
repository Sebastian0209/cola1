using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
namespace Consumidor
{
    class Program
    {
        static IQueueClient queueClient;
        static async Task Main(string[] args)
        {
            string ServiceBusConnectionString = "Endpoint=sb://serviciobus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=l/xeKXQ3iPYHJbFxCB6FOO3LWmgvr/SMyC8l2gUy38k=";
            string QueueName = "qprueba";

            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
            Console.ReadKey();
            await queueClient.CloseAsync();


        }
        static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {

            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");


            await queueClient.CompleteAsync(message.SystemProperties.LockToken);


        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
