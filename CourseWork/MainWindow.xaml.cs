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
        Label[] unknown;
        TextBox[] oddBoxes;
        int number;
        int[] odds;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            gd.Children.Clear();
            number = Convert.ToInt32(stepen.Text) + 1;
            unknown = new Label[number];
            oddBoxes = new TextBox[number];
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

        private void halfDivide_Click(object sender, RoutedEventArgs e)
        {
            double a = -10, b = -3, f_a, f_b, f_c, x_c;
            odds = new int[number];

            for (int i = 0; i < number; i++)
                odds[i] = Convert.ToInt32(oddBoxes[i].Text);

            f_a = EquationFunction(odds, a);
            f_b = EquationFunction(odds, b);

            double sigma = 0.001;

            while (Math.Abs(b - a) > sigma)
            {
                x_c = (a + b) / 2;
                f_c = EquationFunction(odds, x_c);
                if (f_a * f_c <= 0)
                {
                    b = x_c;
                    f_b = f_c;
                }
                else
                {
                    a = x_c;
                    f_a = f_c;
                }

            }

            answer.Content = $"x = {(a + b) / 2}";

        }

        private double EquationFunction(int[] func, double num)
        {
            double result = 0;
            for (int i = 0; i < func.Length; i++)
            {
                result += func[i] * Math.Pow(num, func.Length - i - 1);
            }

            return result;
        }

    }
}
