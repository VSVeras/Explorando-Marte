using Marte.Exploracao.Dominio.Contrato;
using Marte.Exploracao.Dominio.Entidade;
using Marte.Exploracao.Dominio.ObjetoDeValor;
using Marte.Exploracao.Dominio.Servico;
using Marte.Exploracao.Persistencia.BancoDeDados;
using Marte.Exploracao.Persistencia.Contratos;
using Marte.Exploracao.Persistencia.Repositorio;
using Marte.PreocupacoesTransversal.Exploracao.Persistencia.BancoDeDados;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;

namespace Marte.Testes.Integracao.Exploracao.Dominio.Entidade
{
    [TestClass]
    public class SondaTeste
    {
        private Planalto planalto;
        private Sonda sonda;
        private IMovimento movimentoSempreParaFrente;
        private IConexaoComOBanco conexaoComOBanco;
        private IMongoDatabase db;
        private IEspecificacaoDeNegocio especificacaoDeNegocio;
        private ICorretorDaProximaPosicaoDoMovimento corretorDaProximaPosicaoDoMovimento;

        private string nomeDaSonda = "Mark 1";

        [TestInitialize]
        public void Iniciar()
        {
            especificacaoDeNegocio = new EspecificacaoDeNegocio();
            corretorDaProximaPosicaoDoMovimento = new CorretorDaProximaPosicaoDoMovimento();

            var coordenada = new Coordenada(5, 5);
            planalto = new Planalto();
            planalto.Criar(coordenada);

            movimentoSempreParaFrente = new MovimentoParaFrente(corretorDaProximaPosicaoDoMovimento);

            conexaoComOBanco = new ConexaoComOBanco();
            db = new ProvedorDeAcesso().Criar(conexaoComOBanco);
        }

        [TestMethod]
        public void Deve_gravar_uma_sonda()
        {
            Sondas sondas = new Sondas(db);
            sonda = new Sonda(especificacaoDeNegocio, nomeDaSonda);

            sondas.Gravar(sonda);

            Assert.IsFalse(sonda.HouveViolacao());
        }

        [TestMethod]
        public void Deve_fazer_a_exploracao_com_a_sonda_iniciando_em_12N_com_a_serie_de_instruncoes_LMLMLMLMM_e_gravar_os_dados_no_banco()
        {
            var posicaoDesejada = new Posicao(1, 2);
            Sondas sondas = new Sondas(db);
            sonda = new Sonda(especificacaoDeNegocio, nomeDaSonda);
            sonda.Explorar(planalto);
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

            sondas.Gravar(sonda);

            var posicaoEsperada = new Posicao(1, 3);
            Assert.AreEqual(posicaoEsperada, sonda.PosicaoAtual);
            Assert.AreEqual(nomeDaSonda, sonda.Nome);
            Assert.AreEqual(DirecaoCardinal.Norte, sonda.DirecaoCardinalAtual);
        }

        [TestCleanup]
        public void Finalizar()
        {
            db = null;
        }
    }
}
