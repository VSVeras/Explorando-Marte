using Marte.Exploracao.Dominio.Contrato;
using System.Collections.Generic;
using System.Linq;

namespace Marte.Exploracao.Dominio.ObjetoDeValor
{
    public class EspecificacaoDeNegocio : IEspecificacaoDeNegocio
    {
        private readonly IList<RegraDeNegocio> _regrasDeNegocio;

        public IEnumerable<RegraDeNegocio> RegrasDeNegocio { get { return _regrasDeNegocio; } }

        public EspecificacaoDeNegocio()
        {
            _regrasDeNegocio = new List<RegraDeNegocio>();
        }

        public bool Contem(RegraDeNegocio regraDeNegocio)
        {
            return _regrasDeNegocio.Contains(regraDeNegocio);
        }

        public bool HouveViolacao()
        {
            return _regrasDeNegocio.Any();
        }

        public void Adicionar(RegraDeNegocio regraDeNegocio)
        {
            if (!Contem(regraDeNegocio))
                _regrasDeNegocio.Add(regraDeNegocio);
        }

        public override bool Equals(object obj)
        {
            var negocio = obj as EspecificacaoDeNegocio;
            return negocio != null &&
                   EqualityComparer<IList<RegraDeNegocio>>.Default.Equals(_regrasDeNegocio, negocio._regrasDeNegocio);
        }

        public override int GetHashCode()
        {
            return -539705838 + EqualityComparer<IList<RegraDeNegocio>>.Default.GetHashCode(_regrasDeNegocio);
        }
    }
}

