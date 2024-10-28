using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AlgorithmLab2
{
    public partial class TSquare : Page
    {
        private int _delay = 0;
        public TSquare()
        {
            InitializeComponent();
            DrawFractalOutline(440, 250, 525);
            DrawTSquare(440, 250, 300, 3);
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            DepthInput.IsEnabled = false;
            StartButton.IsEnabled = false;
            CheckButton.IsEnabled = true;

            if (int.TryParse(DepthInput.Text, out int depth) && depth > 0 && depth < 11)
            {
                FractalCanvas.Children.Clear();
                _delay = 1;
                DrawFractalOutline(440, 250, Enumerable.Range(0, depth).Sum(i => 300.0 /Math.Pow(2, i)));
                await DrawTSquare(440, 250, 300, depth);
            }
            else if (depth >10)
            {
                MessageBox.Show("Maximum depth value: 10");
            }
            else
            {
                MessageBox.Show("Please enter a valid depth value.");
            }

            DepthInput.IsEnabled = true;
            StartButton.IsEnabled = true;
            CheckButton.IsEnabled = false;
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

            await Task.Delay(_delay);
            await DrawTSquare(centerX - size / 2, centerY - size / 2, newSize, depth - 1); // Верхний левый
            await DrawTSquare(centerX - size / 2, centerY + size / 2, newSize, depth - 1); // Нижний левый
            await DrawTSquare(centerX + size / 2, centerY - size / 2, newSize, depth - 1); // Верхний правый
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

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            _delay = 0;
        }
    }
}