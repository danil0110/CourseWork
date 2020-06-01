using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWork
{
    // Класс, який містить всі методи, необхідні для розв'язання нелінійних алгебраїчних рівнянь
    public class Equation
    {
        private static int iterations;

        // Метод половинного ділення
        public static string HalfDivide(double a, double b, int[] odds)
        {
            iterations = 0; // кількість ітерацій

            double f_a, f_b, f_c, x_c;

            f_a = EquationFunction(odds, a); // значення функції у точці "a"
            f_b = EquationFunction(odds, b); // значення функції у точці "b"

            double sigma = 0.001;

            while (Math.Abs(b - a) > sigma)
            {
                iterations++;
                x_c = (a + b) / 2;
                f_c = EquationFunction(odds, x_c);
                if (f_a * f_c <= 0) // якщо знак відрізнається
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

            return $"x = {Math.Round((a + b) / 2, 3)}\nf(x) = {Math.Round(EquationFunction(odds, (a + b) / 2), 3)}\nКоличество итераций: {iterations}";
        }

        // Метод Ньютона
        public static string Newton(double x0, int[] odds)
        {
            iterations = 0; // кількість ітерацій

            double x1 = 0;
            int[] f_der = Derivative(odds); // похідна від функції
            double sigma = 0.001;

            while (Math.Abs(x1 - x0) > sigma)
            {
                iterations++;
                x0 = x1;
                x1 = x0 - EquationFunction(odds, x0) / EquationFunction(f_der, x0);
            }
            return $"x = {Math.Round(x1, 3)}\nf(x) = {Math.Round(EquationFunction(odds, x1), 3)}\nКоличество итераций: {iterations}";
        }

        // Метод січних
        public static string Secant(double x, double xLast, int[] odds)
        {
            iterations = 0; // кількість ітерацій

            double xGrandLast, sigma = 0.001;

            while (Math.Abs(x - xLast) > sigma)
            {
                iterations++;
                xGrandLast = xLast; // найпопередніша точка
                xLast = x;
                x -= EquationFunction(odds, xLast) * (xLast - xGrandLast) / (EquationFunction(odds, xLast) - EquationFunction(odds, xGrandLast));
            }
            return $"x = {Math.Round(x, 3)}\nf(x) = {Math.Round(EquationFunction(odds, x), 3)}\nКоличество итераций: {iterations}";
        }

        // Відділення коренів на відрізки
        public static string RootSegments(int[] odds, ref bool flag_answer)
        {
            string result = "";
            double a = -10000;
            while (a < 10000) // дивимося відрізки від -10000 до 10000 із кроком 0.2
            {
                if (Equation.EquationFunction(odds, a) * Equation.EquationFunction(odds, a + 0.2) < 0) // якщо на кінцях відрізка знак значення функціїї розрізняється
                {
                    result += $"[{Math.Round(a, 5)}, {Math.Round(a + 0.2, 5)}]\n"; // вивести цей відрізок
                    flag_answer = true; // корінь існує
                }

                a += 0.2;
            }

            if (flag_answer == false)
                return "Нет корней.";
            else
                return result;
        }
        
        // Значення функції у заданій точці
        public static double EquationFunction(int[] func, double num)
        {
            double result = 0;
            for (int i = 0; i < func.Length; i++)
            {
                result += func[i] * Math.Pow(num, func.Length - i - 1);
            }

            return result;
        }

        // Похідна від функції
        private static int[] Derivative(int[] func)
        {
            int[] result = new int[func.Length - 1]; // похідна
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = func[i] * (func.Length - i - 1); // множення коефіцієнта на показник степені змінної
            }

            return result;
        }

        // Виведення рівняння у вигляді рядка (для текстового файлу)
        public static string OutputEquation(int[] odds)
        {
            string result = "";
            if (odds[0] > 0) // перший член рівняння, багато перевірок для виведення замість "1x" чи "-1x" просто "x" чи "-x"
                if (odds[0] == 1)
                    result += $"x^{odds.Length - 1}";
                else
                    result += $"{odds[0]}x^{odds.Length - 1}";
            else
                if (odds[0] == -1)
                    result += $"-x^{odds.Length - 1}";
                else
                    result += $"{odds[0]}x^{odds.Length - 1}";

            for (int i = 1; i < odds.Length - 1; i++) // виведення всіх інших членів рівняння 
            {
                if (odds[i] > 0)
                    if (odds[i] == 1)
                        result += $"+x^{odds.Length - i - 1}";
                    else
                        result += $"+{odds[i]}x^{odds.Length - i - 1}";
                else
                    if (odds[i] == -1)
                        result += $"-x^{odds.Length - i - 1}";
                    else
                        result += $"{odds[i]}x^{odds.Length - i - 1}";
            }

            if (odds[odds.Length - 1] > 0) // виведення вільного члену рівняння
                result += $"+{odds[odds.Length - 1]}";
            else
                result += $"{odds[odds.Length - 1]}";

            result += "=0"; // дописати в кінці рядка "=0"
            return result;
        }
    }
}