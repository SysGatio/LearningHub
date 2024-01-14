using System;
using System.Collections.Generic;
using RabbitMQ.Client;
using SF.ElasticLogs.Autorizacao;
using SF.ElasticLogs.Modelos;
using SF.ElasticLogs.Servicos;

namespace SF.ElasticLogs
{
    public static class ElasticLog
    {
        private static readonly RabbitMqService RabbitMqService = new RabbitMqService(new RabbitMqConfig());

        /// <summary>
        /// Envia log de auditoria para a fila de auditoria do RabbitMQ.
        /// O sistema que enviar o log deve estar cadastrado na lista de sistemas permitidos, autorizado pela COTEC.
        /// </summary>
        /// <param name="logAuditoria">Log de auditoria a ser enviado.</param>
        /// <param name="parametros">Parâmetros contendo as credenciais e o ID do sistema para acesso ao RabbitMQ.</param>
        public static void GerarLogAuditoria(LogAuditoria logAuditoria, ParametrosLog parametros)
        {
            VerificarSistemaPermitido(parametros.SistemaId);

            EnviarLogAuditoria(logAuditoria, parametros);
        }

        /// <summary>
        /// Envia uma lista de logs de auditoria para a fila de auditoria do RabbitMQ.
        /// Cada log da lista é serializado e enviado individualmente.
        /// O sistema que enviar o log deve estar cadastrado na lista de sistemas permitidos, autorizado pela COTEC.
        /// </summary>
        /// <param name="logsAuditoria">Coleção de logs de auditoria a serem enviados.</param>
        /// <param name="parametros">Parâmetros contendo as credenciais e o ID do sistema para acesso ao RabbitMQ.</param>
        public static void GerarLogAuditoria(IEnumerable<LogAuditoria> logsAuditoria, ParametrosLog parametros)
        {
            VerificarSistemaPermitido(parametros.SistemaId);

            foreach (var logAuditoria in logsAuditoria)
            {
                EnviarLogAuditoria(logAuditoria, parametros);
            }
        }

        /// <summary>
        /// Envia um log de operação para a fila de operação do RabbitMQ.
        /// O sistema que enviar o log deve estar cadastrado na lista de sistemas permitidos, autorizado pela COTEC.
        /// </summary>
        /// <param name="logOperacao">Log de operação a ser enviado.</param>
        /// <param name="parametros">Parâmetros contendo as credenciais e o ID do sistema para acesso ao RabbitMQ.</param>
        public static void GerarLogOperacao(LogOperacao logOperacao, ParametrosLog parametros)
        {
            VerificarSistemaPermitido(parametros.SistemaId);

            EnviarLogOperacao(logOperacao, parametros);
        }

        /// <summary>
        /// Envia uma lista de logs de operação para a fila de operação do RabbitMQ.
        /// Cada log da lista é serializado e enviado individualmente.
        /// O sistema que enviar o log deve estar cadastrado na lista de sistemas permitidos, autorizado pela COTEC.
        /// </summary>
        /// <param name="logsOperacao">Coleção de logs de operação a serem enviados.</param>
        /// <param name="parametros">Parâmetros contendo as credenciais e o ID do sistema para acesso ao RabbitMQ.</param>
        public static void GerarLogOperacao(IEnumerable<LogOperacao> logsOperacao, ParametrosLog parametros)
        {
            VerificarSistemaPermitido(parametros.SistemaId);

            foreach (var logOperacao in logsOperacao)
            {
                
                EnviarLog(Utils.SerializarLog(logOperacao),
                    parametros.UsuarioRabbitMq,
                    parametros.SenhaRabbitMq,
                    Utils.Constantes.FilaLogOperacao);
            }
        }

        #region Auxiliares

        private static void VerificarSistemaPermitido(Guid sistemaId)
        {
            if (!SistemasPermitidos.SistemaEhValido(sistemaId))
            {
                throw new UnauthorizedAccessException(Utils.Constantes.ErroSistemaNaoAutorizado);
            }
        }

        private static void EnviarLog(byte[] corpo, string usuarioRabbitMq, string senhaRabbitMq, string nomeFila)
        {
            using var channel = RabbitMqService
                .ConfigurarConexao(usuarioRabbitMq, senhaRabbitMq)
                .CreateConnection()
                .CreateModel();

            channel.BasicPublish(exchange: "", routingKey: nomeFila, basicProperties: null, body: corpo);
        }

        private static void EnviarLogAuditoria(LogAuditoria logAuditoria, ParametrosLog parametros)
        {
            EnviarLog(Utils.SerializarLog(logAuditoria),
                parametros.UsuarioRabbitMq,
                parametros.SenhaRabbitMq,
                Utils.Constantes.FilaLogAuditoria);
        }

        private static void EnviarLogOperacao(LogOperacao logOperacao, ParametrosLog parametros)
        {
            EnviarLog(Utils.SerializarLog(logOperacao),
                parametros.UsuarioRabbitMq,
                parametros.SenhaRabbitMq,
                Utils.Constantes.FilaLogOperacao);
        }
        #endregion

    }
}
