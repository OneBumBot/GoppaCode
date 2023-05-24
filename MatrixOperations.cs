using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoppaCodes
{
    internal static class MatrixOperations
    {
        internal static LinearGoppaCode LinearGoppaCode
        {
            get => default;
            set
            {
            }
        }

        public static int[,] Transpose(int[,] A)
        {
            int w = A.GetLength(0);
            int h = A.GetLength(1);

            int[,] result = new int[h, w];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j, i] = A[i, j];
                }
            }

            return result;
        }

        public static int[,] AddEA(int[,] A)
        {
            int[,] AE = new int[A.GetUpperBound(0) + 2 + A.GetUpperBound(1),
                    A.GetUpperBound(1) + 1];
            for (int i = 0; i < A.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < A.GetUpperBound(1) + 1; j++)
                    AE[i, j] = A[i, j];
            }

            for (int i = 0; i < AE.GetUpperBound(1) + 1; i++)
            {
                AE[i + A.GetUpperBound(0) + 1, i] = 1;
            }

            return AE;
        }

        public static List<int[]> FindKernel(int[,] A, int str)
        {
            List<int[]> kernel = new();
            int height = A.GetUpperBound(0) + 1;
            int width = A.GetUpperBound(1) + 1;
            int zeroEl;

            for (int i = 0; i < width; i++)
            {
                zeroEl = 0;
                for (int j = 0; j < str; j++)
                {
                    if (A[j, i] != 0) break;
                    zeroEl++;
                }

                if (zeroEl == str)
                {
                    var el = new int[height - str];
                    var k = 0;
                    for (int j = str; j < height; j++)
                    {
                        el[k] = A[j, i];
                        k++;
                    }
                    kernel.Add(el);
                }
            }
            return kernel;
        }

        public static int[,] GaussGF2(int[,] A, int str)
        {
            int height = A.GetUpperBound(0) + 1, width = A.GetUpperBound(1) + 1;
            int cell_min = 0;

            for (int i = 0; i < width; i++)
            {
                int min = 2;
                for (int j = 0; j < str; j++)
                {
                    if (A[j, i] != 0 && A[j, i] == 1)
                    {
                        min = A[j, i];
                        cell_min = j;
                        break;
                    }
                }

                if (min != 2)
                {
                    if (min == -1)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            A[j, i] *= -1;
                        }
                    }

                    for (int j = i + 1; j < width; j++)
                    {
                        if (A[cell_min, j] != 0)
                        {
                            for (int k = 0; k < height; k++)
                            {
                                A[k, j] ^= A[k, i];
                            }
                        }
                    }
                }
            }
            return A;
        }

        public static int[,] ConvertAndTransposeBits(int[,] Matrix, int numOfBits)
        {
            int height = Matrix.GetLength(0);
            int width = Matrix.GetLength(1);
            int[,] H = new int[height * numOfBits, width];
            for (int row = 0; row < height; row++)
            {
                for (int cell = 0; cell < width; cell++)
                {
                    var bits =GlobalFunc.ConvertToBits(Matrix[row, cell], numOfBits);
                    for (int j = 0; j < numOfBits; j++)
                    {
                        H[row * 4 + j, cell] = bits[j];
                    }
                }
            }

            return H;
        }

    }
}
