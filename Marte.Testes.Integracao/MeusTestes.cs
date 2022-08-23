using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;

namespace Marte.Testes.Integracao
{

    [TestClass]
    public class MeusTestes
    {
        [TestMethod]
        public void Testa()
        {
            IMongoClient client = new MongoClient("mongodb://localhost");
            IMongoDatabase database = client.GetDatabase("MeusTestes");

            //if (!BsonClassMap.IsClassMapRegistered(typeof(Xpto)))
            //{
            //    BsonClassMap.RegisterClassMap<Xpto>(cm =>
            //    {
            //        //cm.AutoMap();
            //        cm.SetIgnoreExtraElements(true);
            //        cm.SetIsRootClass(true);
            //        cm.MapIdMember(m => m.Id);
            //        cm.MapMember(m => m.Nome);
            //    });
            //}

            IMongoCollection<Xpto> xptos = database.GetCollection<Xpto>("Xpto");
            var xpto = new Xpto(Guid.NewGuid(), "Oxiiiiiiiiiiiiii");
            xptos.InsertOne(xpto);

            Assert.IsTrue(true);
        }
    }
}
