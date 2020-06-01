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
    public partial class MainWindow : Window
    {
        Label[] unknown;        // масив написів для змінних "x^5", "x^4", ...
        TextBox[] oddBoxes;     // масив комірок для коефіцієнтів
        int number, choice = 0; // степінь рівняння, вибір методу розв'язання
        int[] odds;             // масив коефіцієнтів рівняння
        bool flag_answer;       // існування коренів

        // Створення вікна програми
        public MainWindow()
        {
            InitializeComponent();
        }

        // Кнопка зчитування степені рівняння
        private void size_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                number = Convert.ToInt32(stepen.Text) + 1;
                if (number <= 0)
                {
                    MessageBox.Show("Некорректная степень уравнения!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (number > 21)
                {
                    MessageBox.Show("Максимальная степень 20!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    number = 21;
                    stepen.Text = "20";
                }
            }
            catch
            {
                MessageBox.Show("Некорректно введены данные!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            gd.Children.Clear(); // видалення всіх написів із змінними та комірок для коефіцієнтів
            unknown = new Label[number];
            oddBoxes = new TextBox[number];
            int marginchik = 50, boxik = 23;
            for (int i = 0; i < number; i++)
            {
                oddBoxes[i] = new TextBox();
                oddBoxes[i].Name = $"textBox{i + 1}";
                oddBoxes[i].Margin = new Thickness(boxik, 53, 0, 0);
                oddBoxes[i].Visibility = Visibility.Visible;
                oddBoxes[i].Width = 27;
                oddBoxes[i].HorizontalAlignment = HorizontalAlignment.Left;
                oddBoxes[i].VerticalAlignment = VerticalAlignment.Top;
                gd.Children.Add(oddBoxes[i]);
                unknown[i] = new Label();
                unknown[i].Name = $"label{i + 1}";
                unknown[i].Margin = new Thickness(marginchik,50,0,0);
                unknown[i].Visibility = Visibility.Visible;
                if (number - i - 1 != 1)
                    unknown[i].Content = $"x^{number - i - 1}";
                else
                    unknown[i].Content = "x";
                if (i == number - 1)
                    unknown[i].Content = " = 0";
                gd.Children.Add(unknown[i]);

                if (unknown.Length > 8)
                {
                    myWindow.Width = 600 + 70*(unknown.Length - 7);
                }
                else
                {
                    myWindow.Width = 650;
                }

                marginchik += 72;
                boxik += 72;
            }
        }

        // Обрати метод половинного ділення
        private void radioButton_HalfDivide_Checked(object sender, RoutedEventArgs e)
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

        // Обрати метод Ньютона
        private void radioButton_Newton_Checked(object sender, RoutedEventArgs e)
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

        // Обрати метод січних
        private void radioButton_Secant_Checked(object sender, RoutedEventArgs e)
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

        // Кнопка для відділення коренів рівняння на відрізки
        private void rootSegments_Click(object sender, RoutedEventArgs e)
        {
            flag_answer = false;
            odds = new int[number];
            for (int i = 0; i < number; i++)
            {
                try
                {
                    odds[i] = Convert.ToInt32(oddBoxes[i].Text);
                }
                catch
                {
                    MessageBox.Show("Некорректно введены данные!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            segments.Content = Equation.RootSegments(odds, ref flag_answer);
        }

        // Кнопка для збереження результатів у текстовий файл
        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (answerBox.Text == "")
            {
                MessageBox.Show("Результат отсутствует!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "";
            saveFile.DefaultExt = ".txt";
            saveFile.Filter = "Text files|*.txt";
            if (saveFile.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(saveFile.FileName, false))
                {
                    sw.WriteLine(Equation.OutputEquation(odds));
                    sw.WriteLine(answerBox.Text);
                    sw.Close();
                }
            }
        }

        // Кнопка для отримання додаткової інформації щодо кожного з методів
        private void help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Метод половинного деления: на отрезке должен быть только 1 корень!\nМетод Ньютона: приближение должно быть отличное от 0!\nМетод секущих: приближения должны быть отличны от 0!", "Информация", MessageBoxButton.OK, MessageBoxImage.Question);
        }

        // Кнопка для розв'язання рівняння
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            odds = new int[number];
            for (int i = 0; i < number; i++)
            {
                try
                {
                    odds[i] = Convert.ToInt32(oddBoxes[i].Text);
                }
                catch
                {
                    MessageBox.Show("Некорректно введены данные!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            double a = -10000;
            while (a < 10000)
            {
                if (Equation.EquationFunction(odds, a) * Equation.EquationFunction(odds, a + 0.2) < 0)
                    flag_answer = true;
                a += 0.2;
            }

            if (flag_answer)
            {
                double num1 = 0, num2 = 0;
                if (choice == 1)
                {
                    try
                    {
                        num1 = Convert.ToDouble(input1.Text);
                        num2 = Convert.ToDouble(input1_Copy.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Некорректно введены данные!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    answerBox.Text = Equation.HalfDivide(num1, num2, odds);
                }
                    
                else if (choice == 2)
                {
                    try
                    {
                        num1 = Convert.ToDouble(input1.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Некорректно введены данные!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    answerBox.Text = Equation.Newton(num1, odds);
                }
                else if (choice == 3)
                {
                    try
                    {
                        num1 = Convert.ToDouble(input1.Text);
                        num2 = Convert.ToDouble(input1_Copy.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Некорректно введены данные!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    answerBox.Text = Equation.Secant(num1, num2, odds);
                }
            }
            else
                answerBox.Text = "Нет корней.";
            
        }
    }
}