using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AlgorithmLab2
{
    /// <summary>
    /// Логика взаимодействия для TSquare.xaml
    /// </summary>
    public partial class TSquare : Page
    {
        public TSquare()
        {
            InitializeComponent();
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            FractalCanvas.Children.Clear();
            DepthInput.IsEnabled = false;
            StartButton.IsEnabled = false;

            if (int.TryParse(DepthInput.Text, out int depth) && depth > 0)
            {
                DrawFractalOutline(415, 250, Enumerable.Range(0, depth).Sum(i => 300.0 /Math.Pow(2, i)));
                await DrawTSquare(415, 250, 300, depth);
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное значение глубины.");
            }

            DepthInput.IsEnabled = true;
            StartButton.IsEnabled = true;
        }

        private async Task DrawTSquare(double centerX, double centerY, double size, int depth)
        {
            if (depth == 0) return;

            Rectangle square = new Rectangle
            {
                Width = size,
                Height = size,
                Fill = Brushes.Black,
                Stroke = Brushes.Black,
                StrokeThickness = 1 
            };

            Canvas.SetLeft(square, centerX - size / 2);
            Canvas.SetTop(square, centerY - size / 2);
            FractalCanvas.Children.Add(square);

            double newSize = size / 2;

            await Task.Delay(0);
            await DrawTSquare(centerX - size / 2, centerY - size / 2, newSize, depth - 1); // Верхний левый
            //await Task.Delay(1);
            await DrawTSquare(centerX - size / 2, centerY + size / 2, newSize, depth - 1); // Нижний левый
            //await Task.Delay(1);
            await DrawTSquare(centerX + size / 2, centerY - size / 2, newSize, depth - 1); // Верхний правый
            //await Task.Delay(1);
            await DrawTSquare(centerX + size / 2, centerY + size / 2, newSize, depth - 1); // Нижний правый 
        }

        private void DrawFractalOutline(double centerX, double centerY, double size)
        {
            Rectangle outline = new Rectangle
            {
                Width = size,
                Height = size,
                Fill = Brushes.Transparent,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            Canvas.SetLeft(outline, centerX - size / 2);
            Canvas.SetTop(outline, centerY - size / 2);
            FractalCanvas.Children.Add(outline);
        }
    }
}