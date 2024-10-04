using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace ApiRabbitMq.RabbitMQ;

public class RabbitMQProducer : IRabbitMQProducer
{
    public void SendProductMessage<T>(T message)
    {
        //Definição do servidor Rabbit MQ : usamos uma imagem Docker
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        //Cria uma conexão RabbitMQ usando uma factory
        var connection = factory.CreateConnection();
        //Cria um channel com sessão e model
        using var channel = connection.CreateModel();
        //declara a fila(queue) a seguir o nome e propriedades
        channel.QueueDeclare("product", exclusive: false);
        //Serializa a mensagem
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        //Põe os dados na fila : product
        channel.BasicPublish(exchange: "", routingKey: "product", body: body);
    }
}
