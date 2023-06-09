using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Determinant
{
    public class Matrix
    {
        readonly int N;
        readonly double[,] Data;

        public Matrix(int n)
        {
            N = n;
            Data = new double[N, N];
        }

        public double this[int x, int y]
        {
            get => Data[x, y];
            set => Data[x, y] = value;
        }

        Matrix Exclude(int row, int col)
        {
            Matrix res = new Matrix(N);

            for (int i = 0; i < N - 1; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    res[i, j] = i < row ? this[i, j] : this[i + 1, j];
                }
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N - 1; j++)
                {
                    res[i, j] = j < col ? res[i, j] : res[i, j + 1];
                }
            }

            Matrix resized = new Matrix(N - 1);

            for (int i = 0; i < N - 1; i++)
            {
                for (int j = 0; j < N - 1; j++)
                {
                    resized[i, j] = res[i, j];
                }
            }

            return resized;
        }
        public double CalculateDeterminant()
        {
            if (N == 1) return this[0, 0];
            if (N == 2) return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];

            double res = 0;

            for (int j = 0; j < N; j++)
            {
                res += (j % 2 == 1 ? 1 : -1) * this[1, j] * Exclude(1, j).CalculateDeterminant();
            }

            return res;
        }
        public string CalculateDeterminantSteps()
        {
            if (N == 1) return $"{this[0, 0]} = {this[0, 0]}";

            if (N == 2)
            {
                double a = this[0, 0];
                double b = this[0, 1];
                double c = this[1, 0];
                double d = this[1, 1];
                double det = a * d - b * c;

                return $"{a} * {d} - {b} * {c} = {det}";
            }

            string steps = "";
            int sign = 1;

            for (int j = 0; j < N; j++)
            {
                Matrix minor = Exclude(0, j);
                double det = minor.CalculateDeterminant();
                steps += $"{this[0, j]} * ({sign} * {det}) + ";
                sign = -sign;
            }

            steps = steps.Substring(0, steps.Length - 3); // remove the last " + "
            double result = 0;

            for (int j = 0; j < N; j++)
            {
                Matrix minor = Exclude(0, j);
                double det = minor.CalculateDeterminant();
                result += this[0, j] * sign * det;
                sign = -sign;
            }

            steps += $"= {result}";

            return steps;
        }
        Matrix Transpose()
        {
            Matrix res = new Matrix(N);

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    res[j, i] = this[i, j];
                }
            }

            return res;
        }
        public Matrix Invert(out double det)
        {
            det = CalculateDeterminant();
            if (det == 0) return null;

            Matrix res = new Matrix(N);

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    res[i, j] = ((i + j) % 2 == 1 ? -1 : 1) * Exclude(i, j).CalculateDeterminant() / det;
                }
            }

            return res.Transpose();
        }
    }
}





