namespace SF.ElasticLogs.Modelos
{
    public class RabbitMqConfig
    {
        private const string DefaultHostName = "b-b4596503-11e1-4b96-a33b-fd3b84759d5f.mq.sa-east-1.amazonaws.com";

        public string HostName => DefaultHostName;
        public string? UserName { get; set; }
        public string? Password { get; set; }
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