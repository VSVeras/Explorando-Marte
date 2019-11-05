using Marte.Exploracao.Dominio.Contrato;
using Marte.Exploracao.Dominio.Entidade;

namespace Marte.Exploracao.Dominio.Servico
{
    public class MovimentoParaFrente : IMovimento
    {
        private readonly ICorretorDaProximaPosicaoDoMovimento _correcaoDaProximaPosicao;

        public MovimentoParaFrente(ICorretorDaProximaPosicaoDoMovimento correcaoDaProximaPosicao)
        {
            _correcaoDaProximaPosicao = correcaoDaProximaPosicao;
        }

        public void Executar(Sonda sonda)
        {
            sonda.PosicaoAtual = _correcaoDaProximaPosicao.Executar(sonda.PosicaoAtual, sonda.DirecaoCardinalAtual);
        }
    }
}


