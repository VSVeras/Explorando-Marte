using Marte.Exploracao.Dominio.Contrato;
using Marte.Exploracao.Dominio.Entidade;
using Marte.Exploracao.Dominio.ObjetoDeValor;

namespace Marte.Exploracao.Dominio.Servico
{
    public class MovimentoParaFrente : IMovimento
    {
        public void Executar(Sonda sonda)
        {
            switch (sonda.DirecaoCardinalAtual)
            {
                case DirecaoCardinal.Norte:
                    sonda.PosicaoAtual = new Posicao(sonda.PosicaoAtual.X, sonda.PosicaoAtual.Y + 1);
                    break;
                case DirecaoCardinal.Leste:
                    sonda.PosicaoAtual = new Posicao(sonda.PosicaoAtual.X + 1, sonda.PosicaoAtual.Y);
                    break;
                case DirecaoCardinal.Sul:
                    sonda.PosicaoAtual = new Posicao(sonda.PosicaoAtual.X, sonda.PosicaoAtual.Y - 1);
                    break;
                case DirecaoCardinal.Oeste:
                    sonda.PosicaoAtual = new Posicao(sonda.PosicaoAtual.X - 1, sonda.PosicaoAtual.Y);
                    break;
            }
        }
    }
}

