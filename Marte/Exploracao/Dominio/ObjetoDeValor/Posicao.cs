namespace Marte.Exploracao.Dominio.ObjetoDeValor
{
    public class Posicao
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Posicao(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            var posicao = obj as Posicao;
            return posicao != null &&
                   X == posicao.X &&
                   Y == posicao.Y;
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

