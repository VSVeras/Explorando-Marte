using Marte.Exploracao.Persistencia.Contratos;
using System.Configuration;

namespace Marte.PreocupacoesTransversal.Exploracao.Persistencia.BancoDeDados
{
    public class ConexaoComOBanco : IConexaoComOBanco
    {
        public string Obter()
        {
            return ConfigurationManager.AppSettings["ConfiguracaoDaConexaoComOBanco"];
        }
    }
}
