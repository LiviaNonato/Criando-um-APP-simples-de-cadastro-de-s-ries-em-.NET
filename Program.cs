using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "0":
                        ReiniciarPrograma();
                        break;
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine(":( COMANDO INVÁLIDO!");
                        // throw new ArgumentOutOfRangeException();
                        break;
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.Clear();
            Console.WriteLine("Obrigado por utilizar nossos serviços!");
            Console.ReadLine();
        }

        private static void ReiniciarPrograma()
        {
            var lista = repositorio.Lista();
            if (lista.Count > 0)
            {
                foreach (var serie in lista)
                {
                    repositorio.Exclui(serie.retornaId());
                }
            }

            Console.Clear();
        }


        private static void ExcluirSerie()
        {
            Console.Write("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            repositorio.Exclui(indiceSerie);
        }

        private static void VisualizarSerie()
        {
            Console.Clear();
            Console.Write("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie);
            Console.ReadLine();
        }

        private static void AtualizarSerie()
        {
            Console.Clear();
            Console.Write("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
            // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("\t{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.Write("\nDigite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o Ano de Início da Série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();

            Serie atualizaSerie = new Serie(id: indiceSerie,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Atualiza(indiceSerie, atualizaSerie);
        }

        private static void ListarSeries()
        {
            Console.Clear();

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }

            Console.WriteLine("Listar séries:");
            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();

                Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
            }
            Console.ReadLine();
        }

        private static void InserirSerie()
        {
            Console.Clear();
            Console.WriteLine("Inserir nova série");

            // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
            // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("\t{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.Write("\nDigite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o Ano de Início da Série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Insere(novaSerie);
            Console.Clear();
        }

        private static string ObterOpcaoUsuario()
        {
            Console.Clear();
            var lista = repositorio.Lista();

            Console.WriteLine("\nDIO Séries a seu dispor!!!");
            Console.WriteLine("\nInforme a opção desejada:");

            Console.WriteLine("\t0 - Reiniciar programa");

            if (lista.Count > 0)
                Console.WriteLine("\t1 - Listar séries {0}", (lista.Count > 0) ? "(" + lista.Count + ")" : null);

            Console.WriteLine("\t2 - Inserir nova série");

            if (lista.Count > 0)
            {
                Console.WriteLine("\t3 - Atualizar série");
                Console.WriteLine("\t4 - Excluir série");
                Console.WriteLine("\t5 - Visualizar série");
            }

            Console.WriteLine("\tC - Limpar Tela");
            Console.WriteLine("\tX - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}