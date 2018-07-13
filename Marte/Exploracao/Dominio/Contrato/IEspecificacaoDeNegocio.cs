using Marte.Exploracao.Dominio.ObjetoDeValor;
using System.Collections.Generic;

namespace Marte.Exploracao.Dominio.Contrato
{
    public interface IEspecificacaoDeNegocio
    {
        IEnumerable<RegraDeNegocio> RegrasDeNegocio { get; }

        void Adicionar(RegraDeNegocio RegraDeNegocio);
        bool Contem(RegraDeNegocio RegraDeNegocio);
        bool HouveViolacao();
    }
}
