using Marte.Exploracao.Dominio.ObjetoDeValor;
using System;

namespace Marte.Exploracao.Dominio.Servico
{
    public class CorretorDaProximaPosicaoDoMovimento : ICorretorDaProximaPosicaoDoMovimento
    {
        public Posicao Executar(Posicao posicaoAtual, DirecaoCardinal direcaoCardinalAtual)
        {
            switch (direcaoCardinalAtual)
            {
                case DirecaoCardinal.Norte:
                    return new Posicao(posicaoAtual.X, posicaoAtual.Y + 1);
                case DirecaoCardinal.Leste:
                    return new Posicao(posicaoAtual.X + 1, posicaoAtual.Y);
                case DirecaoCardinal.Sul:
                    return new Posicao(posicaoAtual.X, posicaoAtual.Y - 1);
                case DirecaoCardinal.Oeste:
                    return new Posicao(posicaoAtual.X - 1, posicaoAtual.Y);
                default:
                    throw new ArgumentException("Não foi possivel executar a movimentação.");
            }
        }
    }
}


