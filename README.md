# Month Year Folder Organizer

## English

### Overview

Month Year Folder Organizer is a simple desktop tool that automatically organizes all files from a selected folder and its subfolders into a new structured directory.

The software scans all files and moves them into subfolders organized by their creation date, following the format:

```
YYYY/MM
```

For example:
```
Organized
 ┣ 2024
 ┃ ┣ 01
 ┃ ┃ ┣ photo1.jpg
 ┃ ┃ ┗ document1.pdf
 ┃ ┗ 02
 ┃    ┗ report.docx
 ┗ 2025
    ┗ 03
       ┗ image.png
```

Each file is placed into the corresponding year/month folder based on its creation date, helping you easily keep track of when your files were created.

---

### How It Works

1. Select the folder you want to organize.  
2. The app scans all files and subfolders.  
3. Files are copied (or moved, depending on configuration) into a new directory structure grouped by creation year and month.

---

### Technologies

- C# (.NET 8 / WPF)
- Windows Presentation Foundation (WPF) for the interface
- System.IO for filesystem management

---

### How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/MonthYearFolderOrganizer.git
   ```
2. Open the solution in Visual Studio 2022 or later.
3. Build and run the WPF project.
4. Choose a source folder and let the app do the rest.

---

### Output Example

```
Original Folder: C:\Users\John\Downloads
Organized Output: C:\Users\John\Downloads_Organized
```

---

## Português

### Visão Geral

O Month Year Folder Organizer é uma ferramenta de desktop que organiza automaticamente todos os arquivos de uma pasta e suas subpastas em uma nova estrutura organizada.

O software analisa os arquivos e os distribui em subpastas conforme sua data de criação, seguindo o formato:

```
YYYY/MM
```

Por exemplo:
```
Organizado
 ┣ 2024
 ┃ ┣ 01
 ┃ ┃ ┣ foto1.jpg
 ┃ ┃ ┗ documento1.pdf
 ┃ ┗ 02
 ┃    ┗ relatorio.docx
 ┗ 2025
    ┗ 03
       ┗ imagem.png
```

Cada arquivo é movido para a pasta correspondente ao ano e mês de criação, facilitando a organização cronológica dos seus arquivos.

---

### Como Funciona

1. Selecione a pasta que deseja organizar.  
2. O aplicativo analisa todos os arquivos e subpastas.  
3. Os arquivos são copiados (ou movidos) para uma nova estrutura organizada por ano/mês de criação.

---

### Tecnologias Utilizadas

- C# (.NET 8 / WPF)
- Windows Presentation Foundation (WPF) para a interface
- System.IO para manipulação de arquivos e diretórios

---

### Como Executar

1. Clone o repositório:
   ```bash
   git clone https://github.com/seuusuario/MonthYearFolderOrganizer.git
   ```
2. Abra a solução no Visual Studio 2022 ou superior.  
3. Compile e execute o projeto WPF.  
4. Escolha a pasta de origem e deixe o aplicativo organizar tudo para você.

---

### Exemplo de Saída

```
Pasta Original: C:\Users\Ranniery\Downloads
Pasta Organizada: C:\Users\Ranniery\Downloads_Organized
```

---

### Autor

Desenvolvido por Ranniery Dias  
2025
