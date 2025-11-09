using System.Reflection;

namespace MonthYearFolderOrganizer
{
    public class Organizer
    {
        public event Action<string>? StatusUpdated;
        static string exePath = Assembly.GetExecutingAssembly().Location;
        static string origem = Path.GetDirectoryName(exePath);  // Caminho da pasta com as imagens
        static string destino = @"{origem}\Imagens Organizadas"; // Caminho onde serão salvas as pastas organizadas

        public static void Organize()
        {
            if (!Directory.Exists(origem))
            {
                Console.WriteLine("A pasta de origem não existe.");
                return;
            }

            var arquivos = Directory.GetFiles(origem, "*.*", SearchOption.AllDirectories);
            var arquivosDict = SearchFiles(arquivos);

            Console.WriteLine($"Foram encontrados {arquivos.Length} na pasta");
        }

        public static Dictionary<string, List<string>> SearchFiles(string[] arquivos)
        {
            var arquivosPorData = new Dictionary<string, List<string>>();

            int i = 1;
            foreach (var arquivo in arquivos)
            {
                Console.Clear();
                Console.WriteLine("**********************************************");
                Console.WriteLine("Varrendo os arquivos...");
                Console.WriteLine($"Abrindo o arquivo {i} de {arquivos.Length}...");
                Console.WriteLine($"{arquivos.Length - i} arquivos restantes");
                try
                {
                    var dataCriacao = File.GetCreationTime(arquivo);
                    var dataMod = File.GetLastWriteTime(arquivo);

                    var olderDate = dataCriacao > dataMod ? dataMod : dataCriacao;

                    string chaveAnoMes = olderDate.ToString("yyyy-MM");

                    if (!arquivosPorData.ContainsKey(chaveAnoMes))
                    {
                        arquivosPorData[chaveAnoMes] = new List<string>();
                    }

                    arquivosPorData[chaveAnoMes].Add(arquivo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao acessar arquivo {arquivo}: {ex.Message}");
                }
                i++;
            }

            return arquivosPorData;
        }

        public void CopiarArquivosEmPastas(Dictionary<string, List<string>> arquivosPorData, string[] arquivos)
        {
            int i = 1;
            foreach (var par in arquivosPorData)
            {
                Console.Clear();
                Console.WriteLine("**********************************************");
                Console.WriteLine("Copiando os arquivos...");
                Console.WriteLine($"Abrindo o arquivo {i} de {arquivos.Length}...");
                Console.WriteLine($"{arquivos.Length - i} arquivos restantes");
                string pastaDestino = Path.Combine(destino, par.Key);
                Directory.CreateDirectory(pastaDestino);

                foreach (var caminhoArquivo in par.Value)
                {
                    try
                    {
                        string nomeArquivo = Path.GetFileName(caminhoArquivo);
                        string destinoFinal = Path.Combine(pastaDestino, nomeArquivo);

                        // Evita sobrescrever se já existir
                        if (!File.Exists(destinoFinal))
                        {
                            File.Copy(caminhoArquivo, destinoFinal);
                        }
                        else
                        {
                            // Renomeia adicionando um sufixo numérico
                            int contador = 1;
                            string novoDestino;
                            do
                            {
                                string nomeSemExtensao = Path.GetFileNameWithoutExtension(nomeArquivo);
                                string extensao = Path.GetExtension(nomeArquivo);
                                novoDestino = Path.Combine(pastaDestino, $"{nomeSemExtensao}_{contador}{extensao}");
                                contador++;
                            }
                            while (File.Exists(novoDestino));

                            File.Copy(caminhoArquivo, novoDestino);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao copiar o arquivo {caminhoArquivo}: {ex.Message}");
                    }
                    i++;
                }
            }
            Console.WriteLine("Arquivos organizados com sucesso.");
        }
    }
}
