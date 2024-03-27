namespace Student.MvcApp.RabbitMq;

public interface IRabbitMqProducer //Publisher da denilebilir.
{
    void SendMessage<T>(T message);
}
