using Marte.Exploracao.Dominio.Contrato;
using Marte.Exploracao.Dominio.Entidade;

namespace Marte.Exploracao.Dominio.Servico
{
    public class MovimentoParaFrente : IMovimento
    {
        private readonly ICorretorDaProximaPosicaoDoMovimento _corretorDaProximaPosicaoDoMovimento;

        public MovimentoParaFrente(ICorretorDaProximaPosicaoDoMovimento corretorDaProximaPosicaoDoMovimento)
        {
            _corretorDaProximaPosicaoDoMovimento = corretorDaProximaPosicaoDoMovimento;
        }

        public void Executar(Sonda sonda)
        {
            sonda.PosicaoAtual = _corretorDaProximaPosicaoDoMovimento.Executar(sonda.PosicaoAtual, sonda.DirecaoCardinalAtual);
        }
    }
}


