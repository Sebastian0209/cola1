using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using System;

namespace cola1
{

    
    class Program
    {
        static async Task Main(string[] args)
        {
            const string ServiceBusConnectionString = "<Endpoint=sb://csrsi410.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=AUVeZb+wLwaVqKNImMwqo+2lP5I8gYrIB6OiH8QrjrY=>";
            const string QueueName = "<qprueba>";
            IQueueClient queueClient;
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            string messageBody = $"Message prueba de computacion movil";
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            // Write the body of the message to the console.
            //Console.WriteLine($"Sending message: {messageBody}");

            // Send the message to the queue.
            await queueClient.SendAsync(message);

//Console.WriteLine("Hello World!");
        }
    }
}
