namespace Marte.Exploracao.Dominio.ObjetoDeValor
{
    public class Planalto
    {
        public Coordenada Malha { get; private set; }

        public void Criar(Coordenada malha)
        {
            this.Malha = malha;
        }

        public int EixoX() { return Malha.X; }
        public int EixoY() { return Malha.Y; }
    }
}
