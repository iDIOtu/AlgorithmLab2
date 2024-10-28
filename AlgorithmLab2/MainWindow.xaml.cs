using System.Windows;

namespace AlgorithmLab2
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new HanoiTowers()); // Загружаем первую страницу по умолчанию
        }

        private void ButtonToPage1_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HanoiTowers()); // Переход на первую страницу
        }

        private void ButtonToPage2_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TSquare());
        }

        private void ButtonToPage3_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Graphicsxaml());
        }
    }
}