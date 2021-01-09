using Marte.Exploracao.Dominio.Entidade;
using Marte.Exploracao.Dominio.ObjetoDeValor;

namespace Marte.Exploracao.Dominio.Contrato
{
    public interface IMovimento
    {
        Posicao Executar(Sonda sonda);
    }

}
