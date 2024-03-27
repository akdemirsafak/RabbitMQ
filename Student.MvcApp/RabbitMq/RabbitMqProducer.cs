using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Student.MvcApp.RabbitMq;

public class RabbitMqProducer : IRabbitMqProducer
{
    private readonly IConfiguration _configuration;

    public RabbitMqProducer(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendMessage<T>(T message) //generic yapılarda queue name burada alınabilir.
    {
        var factory = new ConnectionFactory()
        {
            HostName = _configuration.GetSection("RabbitMQ:HostName").Value

        };
        var connection = factory.CreateConnection();

        using var channel = connection.CreateModel();
        channel.QueueDeclare("student", exclusive: false, autoDelete: false);
        //kuyruğu tanımlıyoruz varsa bağlanır yoksa o queue'yu oluşturur.
        //exclusive: default olarak true gelir.Özel olması durumudur ve portalda görünmeyeceği anlamına gelir.Güvenlik açığı oluşturabilir örnek amaçlı false yaptık.
        //autodelete: default olarak true gelir. Queue'da mesaj kalmadığı zaman, iş bittiğinde, connection olmadığı zaman queue'yi kaldırır. Yer kaplamaması amacıyla yapılır.

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange: "", routingKey: "student", body: body);

    }
}
