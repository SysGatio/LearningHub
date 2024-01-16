namespace SF.ElasticLogs.Modelos
{
    public class RabbitMqConfig
    {
        private const string DefaultHostName = "b-7d2f1b03-8e25-4a35-abe8-fb0a36db0619.mq.us-east-1.amazonaws.com";

        public string HostName => DefaultHostName;

        public string VirtualHost => "/";
        public int Port => 5671;

        public SslConfig Ssl { get; } = new SslConfig
        {
            Enabled = true,
            ServerName = DefaultHostName
        };
    }

    public class SslConfig
    {
        public bool Enabled { get; set; }
        public string? ServerName { get; set; }
    }
}