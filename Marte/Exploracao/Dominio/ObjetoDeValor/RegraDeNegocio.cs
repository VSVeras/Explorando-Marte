using System.Collections.Generic;

namespace Marte.Exploracao.Dominio.ObjetoDeValor
{
    public class RegraDeNegocio
    {
        public string Informacao { get; private set; }

        public RegraDeNegocio(string mensagem)
        {
            Informacao = mensagem;
        }

        public override bool Equals(object obj)
        {
            var negocio = obj as RegraDeNegocio;
            return negocio != null &&
                   Informacao == negocio.Informacao;
        }

        public override int GetHashCode()
        {
            var hashCode = 1087701368;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Informacao);
            return hashCode;
        }
    }
}

