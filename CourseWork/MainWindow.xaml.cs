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
        int number, choice = 0;
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
            for (int i = 0; i < number; i++)
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
                if (i == number - 1)
                    unknown[i].Content = " = 0";
                gd.Children.Add(unknown[i]);
                marginchik += 72;
                boxik += 72;
            }
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

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            choice = 1; // метод половинного деления
            info.Content = "Отрезок [a, b]";
            label_input1.Content = "a =";
            label_input1_Copy.Content = "b =";
            input1.Visibility = Visibility.Visible;
            input1_Copy.Visibility = Visibility.Visible;
            input1.Text = "";
            input1_Copy.Text = "";
        }

        private void radioButton_Copy_Checked(object sender, RoutedEventArgs e)
        {
            choice = 2; // метод Ньютона
            info.Content = "Начальное приближение x0.";
            label_input1.Content = "x0 =";
            label_input1_Copy.Content = "";
            input1.Visibility = Visibility.Visible;
            input1_Copy.Visibility = Visibility.Hidden;
            input1.Text = "";
            input1_Copy.Text = "";
        }

        private void rootSegments_Click(object sender, RoutedEventArgs e)
        {
            odds = new int[number];

            for (int i = 0; i < number; i++)
                odds[i] = Convert.ToInt32(oddBoxes[i].Text);

            string result = "";
            bool flag = false;
            double a = -10000;
            while (a < 10000)
            {
                if (EquationFunction(odds, a) * EquationFunction(odds, a + 0.2) < 0)
                {
                    result += $"[{a}, {a + 0.2}]\n";
                    flag = true;
                }

                a += 0.2;
            }

            if (flag == false)
            {
                segments.Content = "Нет корней.";
            }
            else
                segments.Content = result;
        }

        private void radioButton_Copy1_Checked(object sender, RoutedEventArgs e)
        {
            choice = 3; // метод секущих
            info.Content = "Первое и второе приближение x0, x1.";
            label_input1.Content = "x0 =";
            label_input1_Copy.Content = "x1 =";
            input1.Visibility = Visibility.Visible;
            input1_Copy.Visibility = Visibility.Visible;
            input1.Text = "";
            input1_Copy.Text = "";
        }

        private void HalfDivide()
        {
            double a = Convert.ToDouble(input1.Text), b = Convert.ToDouble(input1_Copy.Text), f_a, f_b, f_c, x_c;
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

            answer.Content = $"x = {(a + b) / 2}\nf(x) = {EquationFunction(odds, (a + b) / 2)}";
        }

        private void Newton()
        {
            double x0 = Convert.ToDouble(input1.Text), x1 = 0;
            int[] f_der = Derivative(odds);
            double sigma = 0.001;

            while (Math.Abs(x1 - x0) > sigma)
            {
                x0 = x1;
                x1 = x0 - EquationFunction(odds, x0) / EquationFunction(f_der, x0);
            }
            answer.Content = $"x = {x1}\nf(x) = {EquationFunction(odds, x1)}";
        }

        private void Secant()
        {
            double x = Convert.ToDouble(input1.Text), xLast = Convert.ToDouble(input1_Copy.Text), xGrandLast, sigma = 0.001;

            while (Math.Abs(x - xLast) > sigma)
            {
                xGrandLast = xLast;
                xLast = x;
                x -= EquationFunction(odds, xLast) * (xLast - xGrandLast) / (EquationFunction(odds, xLast) - EquationFunction(odds, xGrandLast));
            }
            answer.Content = $"x = {x}\nf(x) = {EquationFunction(odds, x)}";
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (choice == 1)
                HalfDivide();
            else if (choice == 2)
                Newton();
            else if (choice == 3)
                Secant();
        }

        private int[] Derivative(int[] func)
        {
            int[] result = new int[func.Length - 1];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = func[i] * (func.Length - i - 1);
            }

            return result;
        }

    }
}
