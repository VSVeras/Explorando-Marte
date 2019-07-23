using Marte.Exploracao.Dominio.Contrato;
using Marte.Exploracao.Dominio.Entidade;
using Marte.Exploracao.Dominio.ObjetoDeValor;
using Marte.Exploracao.Dominio.Servico;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Marte.Testes.Unidade.Exploracao.Dominio.ObjetoDeValor
{
    [TestClass]
    public class PlanaltoTeste
    {
        private Planalto planalto;
        private Sonda sonda;
        private IMovimento movimentoSempreParaFrente;
        private IEspecificacaoDeNegocio especificacaoDeNegocio;

        [TestInitialize]
        public void Iniciar()
        {
            especificacaoDeNegocio = new EspecificacaoDeNegocio();
            var coordenada = new Coordenada(5, 5);
            planalto = new Planalto();
            planalto.Criar(coordenada);

            movimentoSempreParaFrente = new MovimentoParaFrente();

            sonda = new Sonda(especificacaoDeNegocio, "Mark I");

            sonda.Explorar(planalto);
        }

        [TestMethod]
        public void Deve_criar_uma_malha_de_navegacao_no_planalto_dado_uma_coordenada_de_um_ponto_superior_direita()
        {
            var coordenadaEsperada = new Coordenada(5, 5);

            Assert.AreEqual(coordenadaEsperada, planalto.Malha);
        }

        [TestMethod]
        public void Deve_informar_que_houve_uma_quebra_na_regra_de_negocio_na_qual_o_eixo_desejado_esta_fora_da_malha_para_exploracao()
        {
            var regraDeNegocio = new RegraDeNegocio("Posição fora da faixa (Malha do Planalto) para exploração.");

            var posicaoDesejada = new Posicao(6, 2);
            var posicaoEsperada = new Posicao(0, 2);
            sonda.IniciarEm(posicaoDesejada, DirecaoCardinal.Norte);

            Assert.IsTrue(sonda.HouveViolacao());
            Assert.IsTrue(sonda.EspecificacaoDeNegocio.Contem(regraDeNegocio));
        }
    }

}
