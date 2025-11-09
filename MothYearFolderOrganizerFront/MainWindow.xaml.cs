using System;
using System.Windows;
using System.Windows.Forms;

namespace MothYearFolderOrganizerFront
{
    public partial class MainWindow : Window
    {
        Organizer _organizer = new Organizer();
        public string Output { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            _organizer.StatusUpdated += OnStatusUpdated;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _organizer.Organize();
        }

        private void OnStatusUpdated(string mensagem)
        {
            Dispatcher.Invoke(() =>
            {
                Status.Text += mensagem + Environment.NewLine;
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Selecione uma pasta";
                dialog.ShowNewFolderButton = true;

                var result = dialog.ShowDialog();

                _organizer.Origin = dialog.SelectedPath;
            }
        }
    }
}
