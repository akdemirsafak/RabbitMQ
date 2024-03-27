//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");



using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory= new ConnectionFactory
{
    HostName = "localhost"
};
var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare("student",exclusive:false,autoDelete:false); //queue consumer(burası) tarafında da oluşturulabilir.

var consumer = new EventingBasicConsumer(channel);

//consumer.Received += (model, eventArgs) =>{}; //Rabbitmq mesajı yolladığı anda bir event ile yakalamamız lazım. Consumer'da Received ile yakalarız.

consumer.Received += Receiver; //Receiver bir delegate olarak atandı.Bu event için Receiver adındaki methodu delege ediyoruz.
channel.BasicConsume(queue:"student",consumer:consumer);

Console.ReadLine();


void Receiver(object model,BasicDeliverEventArgs args) 
{
    var body= args.Body.ToArray(); //Byte dizisine dönüştürdük.
    var message=Encoding.UTF8.GetString(body);
    Console.WriteLine($" Received : {message}");
}