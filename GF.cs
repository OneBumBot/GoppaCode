using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoppaCodes
{
    internal sealed class GF
    {
        public int Size => size;
        public int GeneratorBase => generatorBase;
        public int[] Primitives => primitives;

        private int[] primitives;
        private int[] expTable;
        private int[] logTable;
        private Polynomial zero;
        private Polynomial one;
        private readonly int size;
        private readonly int primitive;
        private readonly int generatorBase;
        private bool initialized = false;

        public GF(int primitive, int size, int genBase)
        {
            this.primitive = primitive;
            this.size = size;
            this.generatorBase = genBase;

            Initialize();

        }

        public void Initialize()
        {
            expTable = new int[size];
            logTable = new int[size];
            primitives = new int[size];
            int x = 1;

            for (int i = 0; i < size; i++)
            {
                expTable[i] = x;
                x <<= 1;
                if (x >= size)
                {
                    x ^= primitive;
                    x &= size - 1;
                }
            }

            for (int i = 0; i < size - 1; i++)
                logTable[expTable[i]] = i;

            logTable[0] = 0;
            zero = new Polynomial(this, new int[] { 0 });
            one = new Polynomial(this, new int[] { 1 });

            initialized = true;
            primitives[0] = 0;
            primitives[1] = 1;
            for (int i = 2; i < size; i++)
            {
                primitives[i] = Exp(i - 1);
            }
        }

        private void CheckInit()
        {
            if (initialized == false)
                Initialize();
        }

        internal Polynomial Zero
        {
            get
            {
                CheckInit();
                return zero;
            }
        }
        internal Polynomial One
        {
            get
            {
                CheckInit();
                return one;
            }
        }

        internal Polynomial BuildMonomial(int degree, int coefficient)
        {
            CheckInit();

            if (degree < 0)
                throw new ArgumentException();

            if (coefficient == 0)
                return zero;

            var coefficients = new int[degree + 1];
            coefficients[0] = coefficient;

            return new Polynomial(this, coefficients);
        }

        static internal int AddOrSubtract(int a, int b)
        {
            return a ^ b;
        }

        internal int Exp(int a)
        {
            CheckInit();
            return expTable[a];
        }

        internal int Log(int a)
        {
            CheckInit();

            return logTable[a];
        }

        internal int Inverse(int a)
        {
            CheckInit();

            if (a == 0)
                throw new ArithmeticException();

            return expTable[size - logTable[a] - 1];
        }

        internal int Multiply(int a, int b)
        {
            CheckInit();

            if (a == 0 || b == 0)
                return 0;

            return expTable[(logTable[a] + logTable[b]) % (size - 1)];
        }

        override public string ToString() => $"GF(0x{primitive.ToString("X")},{size})";

        internal LinearGoppaCode LinearGoppaCode
        {
            get => default;
            set
            {
            }
        }
    }

}
