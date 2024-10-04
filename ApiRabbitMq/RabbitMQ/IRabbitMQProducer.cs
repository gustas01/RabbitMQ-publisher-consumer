namespace ApiRabbitMq.RabbitMQ
{
    public interface IRabbitMQProducer
    {
        void SendProductMessage<T>(T message);
    }
}
