using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MothYearFolderOrganizerFront
{
    public class Organizer
    {
        public event Action<string> StatusUpdated;
        private string _exePath = Assembly.GetExecutingAssembly().Location;
        public string Origin, Destination;

        public void Organize()
        {
            if (Origin == null)
            {
                Origin = Path.GetDirectoryName(_exePath);  // Caminho da pasta com as imagens
            }

            Destination = $@"{Origin}\Imagens Organizadas"; // Caminho onde serão salvas as pastas organizadas

            if (!Directory.Exists(Origin))
            {
                StatusUpdated.Invoke("A pasta de origem não existe.");
                return;
            }

            var files = Directory.GetFiles(Origin, "*.*", SearchOption.AllDirectories);
            StatusUpdated.Invoke($"Foram encontrados {files.Length} na pasta");
            var filesDict = SearchFiles(files);
            CopiarArquivosEmPastas(filesDict, files);
        }

        public Dictionary<string, List<string>> SearchFiles(string[] files)
        {
            var arquivosPorData = new Dictionary<string, List<string>>();
            StatusUpdated.Invoke("**********************************************");
            StatusUpdated.Invoke("Varrendo os arquivos...");
            int i = 1;
            foreach (var arquivo in files)
            {
                StatusUpdated.Invoke($"Abrindo o arquivo {i} de {files.Length}...");
                StatusUpdated.Invoke($"{files.Length - i} arquivos restantes");
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
                    StatusUpdated.Invoke($"Erro ao acessar arquivo {arquivo}: {ex.Message}");
                }
                i++;
            }
            return arquivosPorData;
        }

        public void CopiarArquivosEmPastas(Dictionary<string, List<string>> filesByDate, string[] files)
        {
            StatusUpdated.Invoke("**********************************************");
            StatusUpdated.Invoke("Copiando os arquivos...");
            int i = 1;
            foreach (var par in filesByDate)
            {
                string destinationFolder = Path.Combine(Destination, par.Key);
                Directory.CreateDirectory(destinationFolder);

                foreach (var filePath in par.Value)
                {
                    try
                    {
                        StatusUpdated.Invoke($"Copiando o arquivo {i} de {files.Length}...");
                        StatusUpdated.Invoke($"{files.Length - i} arquivos restantes");
                        string fileName = Path.GetFileName(filePath);
                        string finalDestination = Path.Combine(destinationFolder, fileName);

                        if (!File.Exists(finalDestination))
                        {
                            File.Copy(filePath, finalDestination);
                        }
                        else
                        {
                            int index = 1;
                            string newDestination;
                            do
                            {
                                string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                                string extension = Path.GetExtension(fileName);
                                newDestination = Path.Combine(destinationFolder, $"{nameWithoutExtension}_{index}{extension}");
                                index++;
                            }
                            while (File.Exists(newDestination));

                            File.Copy(filePath, newDestination);
                        }
                    }
                    catch (Exception ex)
                    {
                        StatusUpdated.Invoke($"Erro ao copiar o arquivo {filePath}: {ex.Message}");
                    }
                    i++;
                }
            }
            StatusUpdated.Invoke("Arquivos organizados com sucesso.");
        }
    }
}
