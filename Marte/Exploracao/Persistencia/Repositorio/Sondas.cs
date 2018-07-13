using Marte.Exploracao.Dominio.Entidade;
using Marte.Exploracao.Dominio.Repositorio;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Marte.Exploracao.Persistencia.Repositorio
{
    public class Sondas : ISondas
    {
        private IMongoDatabase BancoDeDados;

        public Sondas(IMongoDatabase bancoDeDados)
        {
            BancoDeDados = bancoDeDados ?? throw new ArgumentException("A conexão com o banco de dados não foi informada.");
        }

        public Sonda ObterPorNome(string nome)
        {
            try
            {
                return Todas().AsQueryable().Where(onde => onde.Nome.Equals(nome)).FirstOrDefault(); ;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Gravar(Sonda sonda)
        {
            try
            {
                if (sonda.EspecificacaoDeNegocio.HouveViolacao())
                    return;

                var sondaExiste = ObterPorNome(sonda.Nome);
                if (sondaExiste == null)
                {
                    Todas().InsertOne(sonda);
                }
                else
                {
                    Expression<Func<Sonda, bool>> filtro = filtra => filtra.Id.Equals(sonda.Id);
                    Todas().ReplaceOne(filtro, sonda);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Sonda> ObterTodas()
        {
            try
            {
                return Todas().AsQueryable().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private IMongoCollection<Sonda> Todas()
        {
            return BancoDeDados.GetCollection<Sonda>("Sonda");
        }
    }
}
