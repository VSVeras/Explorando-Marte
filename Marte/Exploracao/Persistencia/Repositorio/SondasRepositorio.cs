using Marte.Exploracao.Dominio.Entidade;
using Marte.Exploracao.Dominio.Repositorio;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Marte.Exploracao.Persistencia.Repositorio
{
    public class SondasRepositorio : ISondasRepositorio
    {
        private readonly IMongoDatabase BancoDeDados;

        public SondasRepositorio(IMongoDatabase bancoDeDados)
        {
            BancoDeDados = bancoDeDados ?? throw new ArgumentException("A conexão com o banco de dados não foi informada.");
        }

        public Sonda ObterPorNome(string nome)
        {
            try
            {
                return Colecao().AsQueryable()
                    .Where(onde => onde.Nome.Equals(nome)).FirstOrDefault(); ;
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
                if (sonda.HouveViolacao())
                    return;

                var sondaExiste = ObterPorNome(sonda.Nome);
                if (sondaExiste == null)
                {
                    Colecao().InsertOne(sonda);
                }
                else
                {
                    Expression<Func<Sonda, bool>> filtro = colecao => colecao.Id.Equals(sonda.Id);
                    Colecao().ReplaceOne(filtro, sonda);
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
                return Colecao().AsQueryable().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private IMongoCollection<Sonda> Colecao()
        {
            return BancoDeDados.GetCollection<Sonda>("Sonda");
        }
    }
}
