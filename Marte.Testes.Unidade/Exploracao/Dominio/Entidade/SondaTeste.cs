using Marte.Exploracao.Dominio.Contrato;
using Marte.Exploracao.Dominio.Entidade;
using Marte.Exploracao.Dominio.ObjetoDeValor;
using Marte.Exploracao.Dominio.Servico;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marte.Testes.Unidade.Exploracao.Dominio.Entidade
{
    [TestClass]
    public class SondaTeste
    {
        private Sonda sonda;
        private IMovimento movimentoSempreParaFrente;
        private IEspecificacaoDeNegocio especificacaoDeNegocio;
        private ICorretorDaProximaPosicaoDoMovimento corretorDaProximaPosicaoDoMovimento;

        [TestInitialize]
        public void Iniciar()
        {
            especificacaoDeNegocio = new EspecificacaoDeNegocio();
            corretorDaProximaPosicaoDoMovimento = new CorretorDaProximaPosicaoDoMovimento();
            movimentoSempreParaFrente = new MovimentoParaFrente(corretorDaProximaPosicaoDoMovimento);

            var coordenada = new Coordenada(5, 5);
            var planalto = new Planalto();
            planalto.Criar(coordenada);
            sonda = new Sonda(especificacaoDeNegocio, "Mark I");
            sonda.Explorar(planalto);
        }

        [TestMethod]
        public void Deve_iniciar_a_exploracao_em_uma_coordenada_inicial()
        {
            var posicaoDesejada = new Posicao(1, 2);

            sonda.IniciarEm(posicaoDesejada, DirecaoCardinal.Norte);

            var posicaoEsperada = new Posicao(1, 2);
            Assert.AreEqual(posicaoEsperada, sonda.PosicaoAtual);
        }

        [TestMethod]
        public void Deve_fazer_a_exploracao_virando_e_movendo_a_sonda_para_uma_nova_coordenada()
        {
            var posicaoDesejada = new Posicao(1, 2);
            sonda.IniciarEm(posicaoDesejada, DirecaoCardinal.Norte);
            sonda.Vire(DirecaoMovimento.Esqueda);

            sonda.Move(movimentoSempreParaFrente);

            var posicaoEsperada = new Posicao(0, 2);
            Assert.AreEqual(posicaoEsperada, sonda.PosicaoAtual);
            Assert.AreEqual(DirecaoCardinal.Oeste, sonda.DirecaoCardinalAtual);
        }


        [TestMethod]
        public void Deve_fazer_a_exploracao_com_a_sonda_iniciando_em_12N_com_a_serie_de_instruncoes_LMLMLMLMM()
        {
            var posicaoDesejada = new Posicao(1, 2);
            sonda.IniciarEm(posicaoDesejada, DirecaoCardinal.Norte);
            sonda.Vire(DirecaoMovimento.Esqueda);
            sonda.Move(movimentoSempreParaFrente);
            sonda.Vire(DirecaoMovimento.Esqueda);
            sonda.Move(movimentoSempreParaFrente);
            sonda.Vire(DirecaoMovimento.Esqueda);
            sonda.Move(movimentoSempreParaFrente);
            sonda.Vire(DirecaoMovimento.Esqueda);
            sonda.Move(movimentoSempreParaFrente);

            sonda.Move(movimentoSempreParaFrente);

            var posicaoEsperada = new Posicao(1, 3);
            Assert.IsFalse(sonda.HouveViolacao());
            Assert.AreEqual(posicaoEsperada, sonda.PosicaoAtual);
            Assert.AreEqual(DirecaoCardinal.Norte, sonda.DirecaoCardinalAtual);
        }

        public void Deve_fazer_a_exploracao_com_a_sonda_iniciando_em_33L_com_a_serie_de_instruncoes_MMRMMRMRRM()
        {
            var posicaoDesejada = new Posicao(3, 3);
            sonda.IniciarEm(posicaoDesejada, DirecaoCardinal.Leste);
            sonda.Move(movimentoSempreParaFrente);
            sonda.Move(movimentoSempreParaFrente);
            sonda.Vire(DirecaoMovimento.Direita);
            sonda.Move(movimentoSempreParaFrente);
            sonda.Move(movimentoSempreParaFrente);
            sonda.Vire(DirecaoMovimento.Direita);
            sonda.Move(movimentoSempreParaFrente);
            sonda.Vire(DirecaoMovimento.Direita);
            sonda.Vire(DirecaoMovimento.Direita);

            sonda.Move(movimentoSempreParaFrente);

            var posicaoEsperada = new Posicao(5, 1);
            Assert.IsFalse(sonda.HouveViolacao());
            Assert.AreEqual(posicaoEsperada, sonda.PosicaoAtual);
            Assert.AreEqual(DirecaoCardinal.Leste, sonda.DirecaoCardinalAtual);
        }
    }

}
