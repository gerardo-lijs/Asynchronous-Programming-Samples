using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncDesktopDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(StartNumberTextBox.Text, out int start);
            int.TryParse(EndNumberTextBox.Text, out int end);
            if (start == 0 || end == 0) return;

            ResultTextBlock.Text = "";
            int result = GetPrimesCount(start, end);
            ResultTextBlock.Text = $"{result} prime numbers between {start} and {end}";
        }

        private int GetPrimesCount(int start, int count)
        {
            return ParallelEnumerable.Range(start, count).Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0));
        }
    }
}
