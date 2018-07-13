namespace Marte.Exploracao.Dominio.ObjetoDeValor
{
    public class Coordenada
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Coordenada(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            var coordenada = obj as Coordenada;
            return coordenada != null &&
                   X == coordenada.X &&
                   Y == coordenada.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}

