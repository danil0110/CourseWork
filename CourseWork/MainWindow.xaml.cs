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

namespace CourseWork
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            gd.Children.Clear();
            int number = Convert.ToInt32(stepen.Text) + 1;
            int[] odds = new int[number];
            Label[] unknown = new Label[number];
            TextBox[] oddBoxes = new TextBox[number];
            int marginchik = 70, boxik = 43;
            for (int i = 0; i < number - 1; i++)
            {
                oddBoxes[i] = new TextBox();
                oddBoxes[i].Name = $"textBox{i + 1}";
                oddBoxes[i].Margin = new Thickness(boxik, 73, 0, 0);
                oddBoxes[i].Visibility = Visibility.Visible;
                oddBoxes[i].Width = 27;
                oddBoxes[i].HorizontalAlignment = HorizontalAlignment.Left;
                oddBoxes[i].VerticalAlignment = VerticalAlignment.Top;
                gd.Children.Add(oddBoxes[i]);
                unknown[i] = new Label();
                unknown[i].Name = $"label{i + 1}";
                unknown[i].Margin = new Thickness(marginchik,70,0,0);
                unknown[i].Visibility = Visibility.Visible;
                if (number - i - 1 != 1)
                    unknown[i].Content = $"x^{number - i - 1}";
                else
                    unknown[i].Content = "x";
                gd.Children.Add(unknown[i]);
                marginchik += 72;
                boxik += 72;
            }
            oddBoxes[number - 1] = new TextBox();
            oddBoxes[number - 1].Name = $"textBox{number}";
            oddBoxes[number - 1].Margin = new Thickness(boxik, 73, 0, 0);
            oddBoxes[number - 1].Visibility = Visibility.Visible;
            oddBoxes[number - 1].Width = 27;
            oddBoxes[number - 1].HorizontalAlignment = HorizontalAlignment.Left;
            oddBoxes[number - 1].VerticalAlignment = VerticalAlignment.Top;
            gd.Children.Add(oddBoxes[number - 1]);
        }
    }
}
