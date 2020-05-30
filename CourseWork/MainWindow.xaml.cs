using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        bool flag_answer;

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

        private void rootSegments_Click(object sender, RoutedEventArgs e)
        {
            flag_answer = false;
            odds = new int[number];
            for (int i = 0; i < number; i++)
                odds[i] = Convert.ToInt32(oddBoxes[i].Text);

            segments.Content = Equation.RootSegments(odds, ref flag_answer);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "";
            saveFile.DefaultExt = ".txt";
            saveFile.Filter = "Test files|*.txt";
            if (saveFile.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(saveFile.FileName, false))
                {
                    sw.WriteLine(Equation.OutputEquation(odds));
                    sw.WriteLine(answer.Content);
                    sw.Close();
                }
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (flag_answer)
            {
                odds = new int[number];
                for (int i = 0; i < number; i++)
                    odds[i] = Convert.ToInt32(oddBoxes[i].Text);

                if (choice == 1)
                    answer.Content = Equation.HalfDivide(Convert.ToDouble(input1.Text), Convert.ToDouble(input1_Copy.Text), odds);
                else if (choice == 2)
                    answer.Content = Equation.Newton(Convert.ToDouble(input1.Text), odds);
                else if (choice == 3)
                    answer.Content = Equation.Secant(Convert.ToDouble(input1.Text), Convert.ToDouble(input1_Copy.Text), odds);
            }
            else
                answer.Content = "Нет корней.";
            
        }
    }
}