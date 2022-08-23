using Marte.Exploracao.Persistencia.Contratos;
using MongoDB.Driver;

namespace Marte.Exploracao.Persistencia.BancoDeDados
{
    public class ProvedorDeAcesso
    {
        public IMongoDatabase Criar(IConexaoComOBanco conexaoComOBanco)
        {
            IMongoClient client = new MongoClient(conexaoComOBanco.Obter());
            IMongoDatabase database = client.GetDatabase("Marte");

            //if (!BsonClassMap.IsClassMapRegistered(typeof(Sonda)))
            //{
            //    BsonClassMap.RegisterClassMap<Sonda>(cm =>
            //    {
            //        cm.AutoMap();
            //        cm.SetIgnoreExtraElements(true);
            //        cm.UnmapMember(m => m.EspecificacaoDeNegocio);
            //    });
            //}

            return database;
        }
    }
}
