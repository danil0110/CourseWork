using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWork
{
    public class Equation
    {
        private static int iterations;
        public static string HalfDivide(double a, double b, int[] odds)
        {
            iterations = 0;

            double f_a, f_b, f_c, x_c;

            f_a = EquationFunction(odds, a);
            f_b = EquationFunction(odds, b);

            double sigma = 0.001;

            while (Math.Abs(b - a) > sigma)
            {
                iterations++;
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

            return $"x = {(a + b) / 2}\nf(x) = {EquationFunction(odds, (a + b) / 2)}\nКоличество итераций: {iterations}";
        }

        public static string Newton(double x0, int[] odds)
        {
            iterations = 0;

            double x1 = 0;
            int[] f_der = Derivative(odds);
            double sigma = 0.001;

            while (Math.Abs(x1 - x0) > sigma)
            {
                iterations++;
                x0 = x1;
                x1 = x0 - EquationFunction(odds, x0) / EquationFunction(f_der, x0);
            }
            return $"x = {x1}\nf(x) = {EquationFunction(odds, x1)}\nКоличество итераций: {iterations}";
        }

        public static string Secant(double x, double xLast, int[] odds)
        {
            iterations = 0;

            double xGrandLast, sigma = 0.001;

            while (Math.Abs(x - xLast) > sigma)
            {
                iterations++;
                xGrandLast = xLast;
                xLast = x;
                x -= EquationFunction(odds, xLast) * (xLast - xGrandLast) / (EquationFunction(odds, xLast) - EquationFunction(odds, xGrandLast));
            }
            return $"x = {x}\nf(x) = {EquationFunction(odds, x)}\nКоличество итераций: {iterations}";
        }

        public static string RootSegments(int[] odds, ref bool flag_answer)
        {
            string result = "";
            double a = -10000;
            while (a < 10000)
            {
                if (Equation.EquationFunction(odds, a) * Equation.EquationFunction(odds, a + 0.2) < 0)
                {
                    result += $"[{a}, {a + 0.2}]\n";
                    flag_answer = true;
                }

                a += 0.2;
            }

            if (flag_answer == false)
                return "Нет корней.";
            else
                return result;
        }

        public static double EquationFunction(int[] func, double num)
        {
            double result = 0;
            for (int i = 0; i < func.Length; i++)
            {
                result += func[i] * Math.Pow(num, func.Length - i - 1);
            }

            return result;
        }

        private static int[] Derivative(int[] func)
        {
            int[] result = new int[func.Length - 1];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = func[i] * (func.Length - i - 1);
            }

            return result;
        }

        public static string OutputEquation(int[] odds)
        {
            string result = "";
            if (odds[0] > 0)
                if (odds[0] == 1)
                    result += $"x^{odds.Length - 1}";
                else
                    result += $"{odds[0]}x^{odds.Length - 1}";
            else
                if (odds[0] == -1)
                    result += $"-x^{odds.Length - 1}";
                else
                    result += $"{odds[0]}x^{odds.Length - 1}";

            for (int i = 1; i < odds.Length - 1; i++)
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

            if (odds[odds.Length - 1] > 0)
                result += $"+{odds[odds.Length - 1]}";
            else
                result += $"{odds[odds.Length - 1]}";

            result += "=0";
            return result;
        }
    }
}
