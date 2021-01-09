using Marte.Exploracao.Dominio.Contrato;
using Marte.Exploracao.Dominio.ObjetoDeValor;
using System;
using System.Collections.Generic;

namespace Marte.Exploracao.Dominio.Entidade
{
    public class Sonda
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public Planalto Planalto { get; private set; }
        public Posicao PosicaoAtual { get; private set; }
        public DirecaoCardinal DirecaoCardinalAtual { get; private set; }

        public IEspecificacaoDeNegocio EspecificacaoDeNegocio { get; private set; }
        private readonly IDictionary<DirecaoMovimento, Action> movimentosExploratorio;
        private readonly IDictionary<DirecaoCardinal, Action> direcaoSentidoHorario;
        private readonly IDictionary<DirecaoCardinal, Action> direcaoSentidoAntiHorario;

        private Sonda()
        {
            movimentosExploratorio = new Dictionary<DirecaoMovimento, Action>
            {
                {DirecaoMovimento.Direita, () => direcaoSentidoHorario[DirecaoCardinalAtual].Invoke()},
                {DirecaoMovimento.Esqueda, () => direcaoSentidoAntiHorario[DirecaoCardinalAtual].Invoke()}
            };

            direcaoSentidoHorario = new Dictionary<DirecaoCardinal, Action>
            {
                {DirecaoCardinal.Norte, () => DirecaoCardinalAtual = DirecaoCardinal.Leste},
                {DirecaoCardinal.Leste, () => DirecaoCardinalAtual = DirecaoCardinal.Sul},
                {DirecaoCardinal.Sul, () => DirecaoCardinalAtual = DirecaoCardinal.Oeste},
                {DirecaoCardinal.Oeste, () => DirecaoCardinalAtual = DirecaoCardinal.Norte}
            };

            direcaoSentidoAntiHorario = new Dictionary<DirecaoCardinal, Action>
            {
                {DirecaoCardinal.Norte, () => DirecaoCardinalAtual = DirecaoCardinal.Oeste},
                {DirecaoCardinal.Oeste, () => DirecaoCardinalAtual = DirecaoCardinal.Sul},
                {DirecaoCardinal.Sul, () => DirecaoCardinalAtual = DirecaoCardinal.Leste},
                {DirecaoCardinal.Leste, () => DirecaoCardinalAtual = DirecaoCardinal.Norte}
            };
        }

        public Sonda(IEspecificacaoDeNegocio especificacaoDeNegocio, string nome) : this()
        {
            if (especificacaoDeNegocio == null)
                especificacaoDeNegocio = new EspecificacaoDeNegocio();

            EspecificacaoDeNegocio = especificacaoDeNegocio;

            if (string.IsNullOrWhiteSpace(nome))
                EspecificacaoDeNegocio.Adicionar(new RegraDeNegocio("O nome da sonda não foi informado."));

            Nome = nome;
        }

        public void Explorar(Planalto planalto)
        {
            if (planalto == null)
            {
                EspecificacaoDeNegocio.Adicionar(new RegraDeNegocio("O planalto a ser explorado não foi informado."));
            }

            Planalto = planalto;
        }

        public void IniciarEm(Posicao posicaoDesejada, DirecaoCardinal direcaoCardinalAtual)
        {
            if (Planalto == null)
            {
                return;
            }

            if (posicaoDesejada == null)
            {
                EspecificacaoDeNegocio.Adicionar(new RegraDeNegocio("A posição inicial da sonda não foi informada."));
                return;
            }

            if (posicaoDesejada.X > Planalto.EixoX() || posicaoDesejada.Y > Planalto.EixoY())
            {
                EspecificacaoDeNegocio.Adicionar(new RegraDeNegocio("Posição fora da faixa (Malha do Planalto) para exploração."));
                return;
            }

            PosicaoAtual = posicaoDesejada;
            DirecaoCardinalAtual = direcaoCardinalAtual;
        }

        public void Vire(DirecaoMovimento movimento)
        {
            movimentosExploratorio[movimento].Invoke();
        }

        public void Move(IMovimento movimento)
        {
            PosicaoAtual = movimento.Executar(this);
        }

        public bool HouveViolacao()
        {
            return EspecificacaoDeNegocio.HouveViolacao();
        }
    }
}

