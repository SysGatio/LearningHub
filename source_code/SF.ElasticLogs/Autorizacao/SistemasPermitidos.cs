using System;
using System.Reflection;

namespace SF.ElasticLogs.Autorizacao
{
    public class SistemasPermitidos
    {
        #region App01
        public const string App01DevA = "73a2302a-0d99-4be4-80c0-621dda93acf2";
        public const string App01DevB = "dfcff023-fdea-477d-8371-52cab55851ec";
        #endregion

        #region App02
        public const string App02DevA = "3a5a6a92-052a-4193-a4e2-33b464a26d48";
        #endregion

        public static bool SistemaEhValido(Guid sistemaId)
        {
            var campos = typeof(SistemasPermitidos).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var campo in campos)
            {
                if (Guid.TryParse(campo.GetValue(null)?.ToString(), out var guidValor) && guidValor == sistemaId)
                {
                    return true;
                }
            }

            return false;
        }
    }
}