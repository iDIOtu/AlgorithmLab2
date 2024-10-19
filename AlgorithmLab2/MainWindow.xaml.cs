using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AlgorithmLab2
{
    public partial class MainWindow : Window
    {
        private const int TowerCount = 3;
        private const int DiskHeight = 19;
        private const int DiskWidthIncrement = 15;
        private readonly Stack<int>[] towers = new Stack<int>[TowerCount];
        private Rectangle[,] diskRectangles;
        private int animationSpeed = 5;
        private CancellationTokenSource cancellationTokenSource;
        Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            InitializeTowers();

            // Initialize disks on application start
            if (int.TryParse(diskCountTextBox.Text, out int diskCount) && diskCount > 0)
            {
                InitializeDisks(diskCount);
            }
        }

        private void InitializeTowers()
        {
            for (int i = 0; i < TowerCount; i++)
            {
                towers[i] = new Stack<int>();
            }
        }

        private void DiskCountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(diskCountTextBox.Text, out int diskCount) && diskCount > 0)
            {
                InitializeTowers();
                InitializeDisks(diskCount);
            }
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            startButton.IsEnabled = false;
            diskCountTextBox.IsEnabled = false;
            cancelButton.IsEnabled = true; // Enable Cancel button

            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            if (int.TryParse(diskCountTextBox.Text, out int diskCount) && diskCount > 0 && diskCount <= 1000)
            {
                RemoveDisks();
                InitializeTowers();
                InitializeDisks(diskCount);
                try
                {
                    await MoveDisks(diskCount, 0, 1, 2, token);
                }
                catch (OperationCanceledException) { };
            }
            else
            {
                MessageBox.Show("Please enter a valid number of disks.");
            }

            startButton.IsEnabled = true;
            diskCountTextBox.IsEnabled = true;
            cancelButton.IsEnabled = false; // Disable Cancel button after completion
        }

        private void InitializeDisks(int diskCount)
        {
            RemoveDisks();

            diskRectangles = new Rectangle[diskCount, TowerCount];

            for (int i = 0; i < diskCount; i++)
            {
                int width = DiskWidthIncrement * (diskCount - i);
                var rect = new Rectangle
                {
                    Width = width,
                    Height = DiskHeight,
                    Fill = new SolidColorBrush(Colors.Blue),
                    Stroke = new SolidColorBrush(Colors.Black),
                    StrokeThickness = 1
                };

                double leftPosition = 247.5 - width / 2;
                double topPosition = 650 - (i + 1) * DiskHeight;

                Canvas.SetLeft(rect, leftPosition);
                Canvas.SetTop(rect, topPosition);

                Panel.SetZIndex(rect, 2);

                canvas.Children.Add(rect);

                towers[0].Push(i);
                diskRectangles[i, 0] = rect;
            }
        }

        private void RemoveDisks()
        {
            var disks = canvas.Children.OfType<Rectangle>().Where(r => r.Fill is SolidColorBrush brush && brush.Color == Colors.Blue).ToList();
            foreach (var disk in disks)
            {
                canvas.Children.Remove(disk);
            }
        }

        private async Task MoveDisks(int n, int start, int temp, int end, CancellationToken token)
        {
            if (n > 0)
            {
                await MoveDisks(n - 1, start, end, temp, token);
                await MoveDisk(start, end, token);
                await MoveDisks(n - 1, temp, start, end, token);
            }
        }

        private async Task MoveDisk(int from, int to, CancellationToken token)
        {
            if (towers[from].Count == 0) return;

            token.ThrowIfCancellationRequested(); // Check for cancellation

            int disk = towers[from].Pop();
            towers[to].Push(disk);

            Rectangle rect = diskRectangles[disk, from];
            diskRectangles[disk, to] = rect;

            double towerCenterX;
            switch (to)
            {
                case 0: towerCenterX = 247.5; break;
                case 1: towerCenterX = 597.5; break;
                case 2: towerCenterX = 947.5; break;
                default: towerCenterX = 250; break;
            }

            double targetX = towerCenterX - rect.Width / 2;
            double targetY = 650 - towers[to].Count * DiskHeight;

            await AnimateDisk(rect, targetX, targetY);
        }

        private async Task AnimateDisk(Rectangle rect, double targetX, double targetY)
        {
            double currentX = Canvas.GetLeft(rect);
            double currentY = Canvas.GetTop(rect);

            while (Math.Abs(currentX - targetX) > 1 || Math.Abs(currentY - targetY) > 1)
            {

                if (Math.Abs(currentX - targetX) > 1)
                {
                    currentX += (targetX - currentX) / (100.0 / animationSpeed); ;
                    Canvas.SetLeft(rect, currentX);
                }

                if (Math.Abs(currentY - targetY) > 1)
                {
                    currentY += (targetY - currentY) / (100.0 / animationSpeed);
                    Canvas.SetTop(rect, currentY);
                }

                int flag = animationSpeed;

                if (animationSpeed == 100)
                    flag = 101;

                await Task.Delay(100 / flag); // Adjust delay based on speed
            }

            Canvas.SetLeft(rect, targetX);
            Canvas.SetTop(rect, targetY);
        }

        private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            animationSpeed = (int)speedSlider.Value;

            if (speedValueTextBlock != null)

                speedValueTextBlock.Text = animationSpeed.ToString();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Reset everything when Cancel is pressed
            cancellationTokenSource?.Cancel();
            RemoveDisks();
            InitializeTowers();

            if (int.TryParse(diskCountTextBox.Text, out int diskCount) && diskCount > 0)
            {
                InitializeDisks(diskCount); // Reinitialize disks on the first tower
            }

             // Request cancellation
        }
    }
}