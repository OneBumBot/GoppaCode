using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoppaCodes
{
    internal class LinearGoppaCode
    {
        public DecForm DecForm
        {
            get => default;
            set
            {
            }
        }

        public EncForm EncForm
        {
            get => default;
            set
            {
            }
        }

        public static string GenerateErrors(string encMsg, int numOfErros)
        {
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder(encMsg);
            for(int i = 0; i < numOfErros; i++)
            {
                int pos = rnd.Next(0, encMsg.Length);
                int val = Int32.Parse(sb[pos].ToString());
                sb[pos] = Char.Parse((val^1).ToString());
            }
            return sb.ToString();
        } 

        public static string Decode(string encodedMsg, GF field, int[] coef, int[] prims)
        {
            Polynomial goppa = new Polynomial(field, coef);
            var HOld = GeneratHMatrix(goppa, field, prims);
            var G = GenerateGMatrix(HOld, field);
            List<int> infoPos = FindInfoVectorPos(G);
            string decoded = "";
            int[] bits = GlobalFunc.ConvertBitsString(encodedMsg);
            Polynomial G2X = CreateSqrGoppa(goppa);
            int[,] H = GeneratHMatrix(G2X, field, prims);
            int[] trueBits = new int[infoPos.Count];
            int blockSize = H.GetLength(1);
            int blocks = bits.Length / blockSize;
            for (int i = 0; i < blocks; i++)
            {
                var block = bits[(i * blockSize)..((i + 1) * blockSize)];
                int[] S = CalcSyndromes(H, G2X, block, field);
                int zeroEl = 0;
                for (int j = 0; j < S.Length; j++)
                {
                    if (S[j] == 0) zeroEl++;
                }

                if (zeroEl == S.Length)
                {
                    trueBits = new int[infoPos.Count];
                    for (int j = 0; j < trueBits.Length; j++)
                    {
                        trueBits[j] = block[infoPos[j]];
                    }
                    foreach (var bit in trueBits)
                    {
                        decoded += bit.ToString();
                    }
                    continue;
                }
                Polynomial locator = CalcLocatorPoly(S, field, G2X.Degree);
                List<int> roots = FindRoots(locator, field);
                int s = locator.Degree;
                if(roots.Count == s)
                    for (int j = 0; j < roots.Count; j++)
                    {
                        block[Array.IndexOf(field.Primitives[2..], field.Inverse(roots[j])) - 1] ^= 1;
                    }
                trueBits = new int[infoPos.Count];
                for (int j = 0; j < trueBits.Length; j++)
                {
                    trueBits[j] = block[infoPos[j]];
                }
                foreach (var bit in trueBits)
                {
                    decoded += bit.ToString();
                }
            }
            return decoded;
        }

        public static (string,int[], int[]) Encode(string message, GF field)
        {
            int[] prim;
            int[,] H;
            List<int[]> G;
            int blockSize;
            Polynomial Gx;
            while (true)
            {
                (Gx, prim) = GenerateGoppaPolynomial(32, field);
                H = GeneratHMatrix(Gx, field, prim);
                G = GenerateGMatrix(H, field);
                blockSize = G.Count;
                if (G.Count != 0 && TestGoppaPoly(Gx, prim)) break;
            }

            int[] messageBits =GlobalFunc.ConvertToBits(message);
            int iter = messageBits.Length / blockSize;
            int iterQ = messageBits.Length % blockSize;
            string encMsg = "";
            int[] msgPart, newMsg;
            for (int i = 0; i < iter; i++) // Итератор по частям сообщения
            {
                msgPart = messageBits[(i * blockSize)..((i + 1) * blockSize)];

                newMsg = new int[G[0].Length];
                for (int j = 0; j < G[0].Length; j++) //Идём по столбцам матрица
                {
                    for (int k = 0; k < msgPart.Length; k++) // Для каждого катого элемента в стобце
                    {
                        newMsg[j] = GF.AddOrSubtract(field.Multiply(msgPart[k], G[k][j]), newMsg[j]);
                    }
                }
                foreach (var bit in newMsg)
                {
                    encMsg += bit.ToString();
                }

            }

            msgPart = messageBits[(messageBits.Length - iterQ)..];
            newMsg = new int[G[0].Length];
            for (int j = 0; j < G[0].Length; j++) //Идём по столбцам матрица
            {
                for (int k = 0; k < msgPart.Length; k++) // Для каждого катого элемента в стобце
                {
                    newMsg[j] = GF.AddOrSubtract(field.Multiply(msgPart[k], G[k][j]), newMsg[j]);
                }
            }

            foreach (var bit in newMsg)
            {
                encMsg += bit.ToString();
            }

            return (encMsg, Gx.Coefficients, prim);
        }

        private static List<int> FindInfoVectorPos(List<int[]> G)
        {

            List<int> pos = new List<int>();
            for (int i = 0; i < G[0].Length; i++)
            {
                int nonZeroEl = 0;
                for (int j = 0; j < G.Count; j++)
                {
                    if (nonZeroEl > 1) break;
                    if (G[j][i] == 1) nonZeroEl++;
                }

                if (nonZeroEl == 1) pos.Add(i);

            }

            return pos;
        }

        private static int[] CalcSyndromes(int[,] H, Polynomial G2x, int[] encodedMsgBits, GF field)
        {
            int degree = G2x.Degree * 2;
            H = MatrixOperations.Transpose(H);
            int[] bits = encodedMsgBits;
            int blockSize = H.GetLength(0);
            int[] newMsg = new int[H.GetLength(1)];
            for (int j = 0; j < H.GetLength(1); j++) //Идём по столбцам матрица
            {
                for (int k = 0; k < blockSize; k++) // Для каждого катого элемента в стобце
                {
                    newMsg[j] = GF.AddOrSubtract(field.Multiply(bits[k], H[k, j]), newMsg[j]);
                }
            }

            return newMsg;
        }

        private static Polynomial CalcLocatorPoly(int[] syndromes, GF field, int degree)
        {
            Polynomial locator;
            Polynomial locatorOld;

            locator = new Polynomial(field, new int[] { 1 });
            locatorOld = new Polynomial(field, new int[] { 1 });

            int L = 0;

            for (int r = 0; r < degree; r++)
            {
                int k = r + L;
                int delta = syndromes[k];
                for (int j = 1; j < locator.Coefficients.Length; j++)
                {
                    delta = GF.AddOrSubtract(delta, field.Multiply(locator.GetCoefficient(j), syndromes[k - j]));
                }

                locatorOld = locatorOld.MultiplyByMonomial(1, 1);
                if (delta != 0)
                {
                    if (locatorOld.Coefficients.Length > locator.Coefficients.Length)
                    {
                        Polynomial locatorNew = locatorOld.MultiplyByMonomial(0, delta);
                        locatorOld = locator.MultiplyByMonomial(0, field.Inverse(delta));
                        locator = locatorNew;
                    }

                    locator = locator.AddOrSubtract(locatorOld.MultiplyByMonomial(0, delta));
                }
            }
            return locator;
        }

        private static List<int> FindRoots(Polynomial locatorPoly, GF field)
        {
            List<int> roots = new List<int> { };

            foreach (var prim in field.Primitives[1..])
            {
                if (locatorPoly.EvaluateAt(prim) == 0) roots.Add(prim);
            }

            return roots;
        }

        private static Polynomial CreateSqrGoppa(Polynomial Goppa)
        {
            return Goppa.Multiply(Goppa);
        }

        private static List<int[]> GenerateGMatrix(int[,] H, GF field)
        {
            H = MatrixOperations.ConvertAndTransposeBits(H, (int)Math.Log2(field.Size));
            int[,] B;

            int str = H.GetLength(0);
            B = MatrixOperations.AddEA(H);

            B = MatrixOperations.GaussGF2(B, str);
            List<int[]> G = MatrixOperations.FindKernel(B, str);
            return G;
        }

        private static int[,] GeneratHMatrix(Polynomial Gx, GF field, int[] prim)
        {
            int[,] H = new int[Gx.Degree, prim.Length];
            for (int i = 0; i < H.GetLength(0); i++)
            {
                for (int j = 0; j < H.GetLength(1); j++)
                {
                    var Eval = Gx.EvaluateAt(prim[j]);
                    var invEval = field.Inverse(Eval);
                    int pow = 1;
                    for (int p = 0; p < i; p++)
                    {
                        pow = field.Multiply(pow, prim[j]);
                    }
                    var mult = field.Multiply(invEval, pow);
                    H[i, j] = mult;
                }
            }

            return H;
        }

        private static (Polynomial, int[]) GenerateGoppaPolynomial(int degree, GF field)
        {
            Polynomial goppa;
            var primitives = field.Primitives[2..];
            int[] left, right;
            int size = field.Size;
            Random rand = new Random();
            size = primitives.Length;
            int r = rand.Next(0, size - 1);
            goppa = new Polynomial(field, new int[] { 1, primitives[r] });
            left = primitives[..r];
            right = primitives[(r + 1)..];
            primitives = left.Concat(right).ToArray();
            for (int i = 0; i < degree - 1; i++)
            {
                size = primitives.Length;
                r = rand.Next(0, size - 1);
                goppa = goppa.Multiply(new Polynomial(field, new[] { 1, primitives[r] }));
                left = primitives[..r];
                right = primitives[(r + 1)..];
                primitives = left.Concat(right).ToArray();
            }
            return (goppa, primitives);
        }
        private static bool TestGoppaPoly(Polynomial goppa, int[] prim)
        {
            for (int i = 3; i < prim.Length; i++)
            {
                if (goppa.EvaluateAt(prim[i]) == 0)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
