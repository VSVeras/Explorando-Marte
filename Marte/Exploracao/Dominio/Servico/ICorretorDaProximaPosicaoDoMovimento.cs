using Marte.Exploracao.Dominio.ObjetoDeValor;

namespace Marte.Exploracao.Dominio.Servico
{
    public interface ICorretorDaProximaPosicaoDoMovimento
    {
        Posicao Executar(Posicao posicaoAtual, DirecaoCardinal direcaoCardinalAtual);
    }
}
