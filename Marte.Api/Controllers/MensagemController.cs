using Marte.CamadaAnticorrupcao;
using Marte.Exploracao.Dominio.Contrato;
using Marte.Exploracao.Dominio.ObjetoDeValor;
using Marte.Exploracao.Persistencia.BancoDeDados;
using Marte.Exploracao.Persistencia.Contratos;
using Marte.PreocupacoesTransversal.Exploracao.Persistencia.BancoDeDados;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Marte.Api.Controllers
{
    public class MensagemController : ApiController
    {
        private readonly IConexaoComOBanco conexaoComOBanco;
        private readonly IMongoDatabase db;
        private readonly IEspecificacaoDeNegocio especificacaoDeNegocio;
        private readonly ExploradorDePlanalto explorador;

        public MensagemController()
        {
            conexaoComOBanco = new ConexaoComOBanco();
            db = new ProvedorDeAcesso().Criar(conexaoComOBanco);
            especificacaoDeNegocio = new EspecificacaoDeNegocio();
            explorador = new ExploradorDePlanalto(conexaoComOBanco, db, especificacaoDeNegocio);
        }

        [HttpPost]
        [Route("api/Mensagem")]
        public Task<HttpResponseMessage> Post([FromBody]dynamic body)
        {
            HttpResponseMessage ResponseMessage;

            try
            {
                var conteudo = (string)body.conteudo;
                var retorno = explorador.Iniciar(conteudo);

                if (especificacaoDeNegocio.HouveViolacao())
                    ResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, new { errors = especificacaoDeNegocio.RegrasDeNegocio.ToList() });
                else
                    ResponseMessage = Request.CreateResponse(HttpStatusCode.Created, retorno);
            }
            catch (Exception error)
            {
                ResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, new { errors = error.Message });
            }

            return Task.FromResult<HttpResponseMessage>(ResponseMessage);
        }
    }
}
