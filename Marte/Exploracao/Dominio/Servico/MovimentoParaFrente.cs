using Marte.Exploracao.Dominio.Contrato;
using Marte.Exploracao.Dominio.Entidade;
using Marte.Exploracao.Dominio.ObjetoDeValor;

namespace Marte.Exploracao.Dominio.Servico
{
    public class MovimentoParaFrente : IMovimento
    {
        private readonly ICorretorDaProximaPosicaoDoMovimento _corretorDaProximaPosicaoDoMovimento;

        public MovimentoParaFrente(ICorretorDaProximaPosicaoDoMovimento corretorDaProximaPosicaoDoMovimento)
        {
            _corretorDaProximaPosicaoDoMovimento = corretorDaProximaPosicaoDoMovimento;
        }

        public Posicao Executar(Sonda sonda)
        {
            return _corretorDaProximaPosicaoDoMovimento.Executar(sonda.PosicaoAtual, sonda.DirecaoCardinalAtual);
        }
    }
}


