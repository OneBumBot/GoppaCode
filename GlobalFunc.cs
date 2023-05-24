using System.Collections;

namespace GoppaCodes
{
    internal static  class GlobalFunc
    {
        public static EncForm EncForm
        {
            get => default;
            set
            {
            }
        }

        public static DecForm DecForm
        {
            get => default;
            set
            {
            }
        }

        public static int[] ConvertBitsString(string binString)
        {
            int[] bits = new int[binString.Length];
            bits = binString.Select(n => Int32.Parse((n.ToString()))).ToArray();
            return bits;
        }

        public static int[] ConvertToBits(int val, int numOfBits)
        {
            var b = new BitArray(new int[] { val }); // превращаем число в последовательность True и False
            bool[] bits = new bool[b.Length]; //Инициализируем массив для хранения бит
            b.CopyTo(bits, 0); //копируем полученные биты в массив
            int[] bitsValues = bits.Select(bit => (int)(bit ? 1 : 0)).ToArray(); //Превращаем True и False в 1 и 0
            return bitsValues[..numOfBits];
        }

        public static int[] ConvertToBits(string text)
        {
            string strBits = string.Join("", text.Select(n => Convert.ToString(n, 2).PadLeft(8, '0')));
            int[] bits = new int[strBits.Length];
            bits = strBits.Select(n => Int32.Parse(n.ToString())).ToArray();
            return bits;
        }

        public static byte[] GetBytesFromBinaryString(string input)
        {
            int numOfBytes = input.Length / 8;
            byte[] bytes = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; ++i)
            {
                bytes[i] = Convert.ToByte(input.Substring(8 * i, 8), 2);
            }
            return bytes;
        }

    }
}
