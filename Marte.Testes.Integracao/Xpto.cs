using System;

namespace Marte.Testes.Integracao
{
    public class Xpto
    {
        public Xpto(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
    }
}
