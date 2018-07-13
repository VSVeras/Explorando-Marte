using Marte.Exploracao.Dominio.Entidade;

namespace Marte.Exploracao.Dominio.Contrato
{
    public interface IMovimento
    {
        void Executar(Sonda sonda);
    }

}
