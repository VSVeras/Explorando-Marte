using Marte.Exploracao.Dominio.Entidade;
using System.Collections.Generic;

namespace Marte.Exploracao.Dominio.Repositorio
{
    public interface ISondasRepositorio
    {
        Sonda ObterPorNome(string nome);
        List<Sonda> ObterTodas();
        void Gravar(Sonda sonda);
    }
}
