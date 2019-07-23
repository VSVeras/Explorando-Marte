using Marte.Exploracao.Dominio.Contrato;
using Marte.Exploracao.Dominio.Entidade;
using Marte.Exploracao.Dominio.ObjetoDeValor;
using Marte.Exploracao.Dominio.Servico;
using Marte.Exploracao.Persistencia.Contratos;
using Marte.Exploracao.Persistencia.Repositorio;
using MongoDB.Driver;
using System;


namespace Marte.CamadaAnticorrupcao
{
    public class ExploradorDePlanalto
    {
        private readonly IConexaoComOBanco conexaoComOBanco;
        private readonly IMongoDatabase db;
        private readonly IEspecificacaoDeNegocio especificacaoDeNegocio;

        private Coordenada coordenada;
        private Posicao posicaoInicioalDaSonda;
        private DirecaoCardinal direcaoCardinalInicioalDaSonda;
        private IMovimento movimentoSempreParaFrente;
        private string[] serieDeInstrucoesIndicandoParaASondaComoElaDeveraExplorarOPlanalto;
        private string resultado;
        private int numeroDeLinhasNaMensagemEnviadaParaControlarAsSondas = 5;

        public ExploradorDePlanalto(IConexaoComOBanco conexaoComOBanco, IMongoDatabase db, IEspecificacaoDeNegocio especificacaoDeNegocio)
        {
            this.conexaoComOBanco = conexaoComOBanco;
            this.db = db;
            this.especificacaoDeNegocio = especificacaoDeNegocio;
        }

        public string Iniciar(string mensagem)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(mensagem))
                    throw new Exception("Mensagem inválida.");

                string[] separadores = new string[] { "\n" };
                string[] linhas = mensagem.Split(separadores, StringSplitOptions.None);

                if (linhas.Length < numeroDeLinhasNaMensagemEnviadaParaControlarAsSondas)
                    throw new Exception($"Mensagem inválida, só contém {linhas.Length} linha(s).");

                ObterDadosInstrucoesPassadasPeloOperadorDaNasa(linhas);

                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ObterDadosInstrucoesPassadasPeloOperadorDaNasa(string[] linhas)
        {
            var sondaNumero = 1;
            var contardorDeLinhas = 1;

            foreach (var linha in linhas)
            {
                switch (contardorDeLinhas)
                {
                    case 1:
                        ObterCoordenadaDoPontoSuperiorDireitoDaMalhaDoPlanalto(linha);
                        break;
                    case 4:
                    case 2:
                        ObterPosicaoInicialDaSonda(linha);
                        break;
                    case 5:
                    case 3:
                        ObterSerieDeInstrucoesIndicandoParaASondaComoElaDeveraExplorarOPlanalto(linha);
                        break;
                }

                if (contardorDeLinhas == 3 || contardorDeLinhas == 5)
                {
                    ExecutarExploracao(sondaNumero);
                    sondaNumero++;
                }

                contardorDeLinhas++;
            }
        }

        private void ExecutarExploracao(int sondaNumero)
        {
            Sondas sondas = new Sondas(db);

            Planalto planalto = new Planalto();
            planalto.Criar(coordenada);

            movimentoSempreParaFrente = new MovimentoParaFrente();

            var nomeDaSonda = $"Mark {sondaNumero}";

            Sonda sonda = ObterSonda(sondas, nomeDaSonda);

            sonda.Explorar(planalto);

            sonda.IniciarEm(posicaoInicioalDaSonda, direcaoCardinalInicioalDaSonda);

            ExecutarInstrucaoDeMovimentoNaSonda(sonda, movimentoSempreParaFrente);

            sondas.Gravar(sonda);

            sondas = null;

            var direcao = sonda.DirecaoCardinalAtual.ToString().ToUpper().Substring(0, 1).Replace("O", "W").Replace("L", "E");

            if (sondaNumero > 1)
                resultado += "-";

            resultado += $"{sonda.PosicaoAtual.X} {sonda.PosicaoAtual.Y} {direcao}";
        }

        private Sonda ObterSonda(Sondas sondas, string nomeDaSonda)
        {
            Sonda sonda = sondas.ObterPorNome(nomeDaSonda);

            if (sonda == null)
                sonda = new Sonda(especificacaoDeNegocio, nomeDaSonda);

            return sonda;
        }

        private void ExecutarInstrucaoDeMovimentoNaSonda(Sonda sonda, IMovimento movimentoSempreParaFrente)
        {
            for (int contador = 0; contador < serieDeInstrucoesIndicandoParaASondaComoElaDeveraExplorarOPlanalto.Length; contador++)
            {
                var instrucao = serieDeInstrucoesIndicandoParaASondaComoElaDeveraExplorarOPlanalto[contador];

                switch (instrucao)
                {
                    case "L":
                        sonda.Vire(DirecaoMovimento.Esqueda);
                        break;
                    case "R":
                        sonda.Vire(DirecaoMovimento.Direita);
                        break;
                    case "M":
                        sonda.Move(movimentoSempreParaFrente);
                        break;
                }
            }
        }

        private void ObterSerieDeInstrucoesIndicandoParaASondaComoElaDeveraExplorarOPlanalto(string linha)
        {
            if (linha.Length < 1)
                throw new Exception($"Mensagem inválida, série de instruções indicando para a sonda como ela deverá explorar o planalto só contém {linha.Length} caracter(s).");

            var quantidadeDeCaracteres = linha.Length;
            serieDeInstrucoesIndicandoParaASondaComoElaDeveraExplorarOPlanalto = new string[quantidadeDeCaracteres];
            for (int contador = 0; contador < linha.Length; contador++)
            {
                serieDeInstrucoesIndicandoParaASondaComoElaDeveraExplorarOPlanalto[contador] = linha[contador].ToString();
            }
        }

        private void ObterPosicaoInicialDaSonda(string linha)
        {
            var caracteres = ObterCaracteresSeparadosPorEspaco(linha);

            if (caracteres.Length <= 2)
                throw new Exception($"Mensagem inválida, posição inicial só contém {caracteres.Length} caracter(s).");


            int[] numeros = new int[2];
            string[] letras = new string[1];

            int contador = 0;
            foreach (var item in caracteres)
            {
                int numero = 0;
                char letrar = ' ';

                switch (contador)
                {
                    case 0:
                    case 1:
                        if (!ENumero(item, out numero))
                            throw new Exception("Mensagem inválida, posição inicial não contém valores númericos.");

                        numeros[contador] = numero;
                        break;
                    case 2:
                        letrar = Convert.ToChar(item);
                        if (!char.IsLetter(letrar))
                            throw new Exception("Mensagem inválida, posição inicial não contém caracter.");

                        letras[0] = item;
                        break;
                }
                contador++;
            }

            posicaoInicioalDaSonda = new Posicao(numeros[0], numeros[1]);
            direcaoCardinalInicioalDaSonda = ObterDirecaoCardinal(letras[0]);
        }

        private DirecaoCardinal ObterDirecaoCardinal(string letra)
        {
            DirecaoCardinal direcaoCardinal = DirecaoCardinal.Norte;

            switch (letra)
            {
                case "E":
                    direcaoCardinal = DirecaoCardinal.Leste;
                    break;
                case "S":
                    direcaoCardinal = DirecaoCardinal.Sul;
                    break;
                case "W":
                    direcaoCardinal = DirecaoCardinal.Oeste;
                    break;
                default:
                    break;
            }

            return direcaoCardinal;
        }

        public void ObterCoordenadaDoPontoSuperiorDireitoDaMalhaDoPlanalto(string linha)
        {
            var caracteres = ObterCaracteresSeparadosPorEspaco(linha);

            if (caracteres.Length <= 1)
                throw new Exception($"Mensagem inválida, coordenada do ponto superior-direito da malha do planalto só contém {caracteres.Length} caracter(s).");

            int[] numeros = new int[2];
            int contador = 0;
            foreach (var item in caracteres)
            {
                int numero = 0;

                if (!ENumero(item, out numero))
                    throw new Exception("Mensagem inválida, coordenada do ponto superior-direito da malha do planalto não contém valores númericos.");

                numeros[contador] = numero;
                contador++;
            }

            coordenada = new Coordenada(numeros[0], numeros[1]);
        }

        private bool ENumero(string item, out int numero)
        {
            return (Int32.TryParse(item, out numero));
        }

        private string[] ObterCaracteresSeparadosPorEspaco(string linha)
        {
            return linha.Split(' ');
        }
    }
}
