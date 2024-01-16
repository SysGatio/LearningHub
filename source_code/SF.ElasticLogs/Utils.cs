using System.Text;
using Newtonsoft.Json;

namespace SF.ElasticLogs
{
    public static class Utils
    {
        public static class Constantes
        {
            public const string FilaLogAuditoria = "LogAuditoria";
            public const string FilaLogOperacao = "LogOperacao";

            public const string ErroSistemaNaoAutorizado = "LogExcecao";
        }

        public static byte[] SerializarLog<T>(T log)
        {
            var json = JsonConvert.SerializeObject(log);
            var resultado = Encoding.UTF8.GetBytes(json);

            return resultado;
        }


    }
}
