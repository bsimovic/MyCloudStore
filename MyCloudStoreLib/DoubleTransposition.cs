using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace MyCloudStoreLib
{
    public static class DoubleTransposition
    {
        // pretvori niz karaktera u niz brojeva gledajuci relativni redosled u abecedi
        private static int[] GetKeyFromString(String key)
        {
            int keyLength = key.Length;
            int[] ret = new int[keyLength];
            List<byte> bytes = new List<byte>(Encoding.ASCII.GetBytes(key));
            bytes.Sort();

            byte min;
            for (int i = 0; i < keyLength; i++)
            {
                min = bytes.Min();
                ret[min] = i;
                bytes.Remove(min);
            }
            
            return ret;
        }

        // zameni mesta dvema vrstama
        private static void SwapRows(byte[,] mat, int a, int b)
        {
            int lenRow = mat.GetLength(0);
            byte tmp;
            for (int i = 0; i < lenRow; i++)
            {
                tmp = mat[a, i];
                mat[a, i] = mat[b, i];
                mat[b, i] = tmp;
            }
        }

        // zameni mesta dvema kolonama
        private static void SwapColumns(byte[,] mat, int a, int b)
        {
            int lenCol = mat.GetLength(1);
            byte tmp;
            for (int i = 0; i < lenCol; i++)
            {
                tmp = mat[i, a];
                mat[i, a] = mat[i, b];
                mat[i, b] = tmp;
            }
        }

        public static byte[] Encrypt(byte[] input, string key1, string key2)
        {
            byte[] output = new byte[input.Length];
            int[] numKey1 = GetKeyFromString(key1);
            int[] numKey2 = GetKeyFromString(key2);
            int lenKey1 = numKey1.Length;
            int lenKey2 = numKey2.Length;

            // rasporedjivanje u matrici
            byte[,] mat = new byte[lenKey1, lenKey2];
            for (int i = 0; i < lenKey1; i++)
                for (int j = 0; j < lenKey2; j++)
                    mat[i, j] = input[i * lenKey2 + j];

            
            int swapInd;
            int tmp;
            // zamena vrsta
            for (int i = 0; i < lenKey1; i++)
            {
                swapInd = Array.IndexOf(numKey1, i);
                if (i != swapInd)
                {
                    SwapRows(mat, i, swapInd);
                    tmp = numKey1[i];
                    numKey1[i] = numKey1[swapInd];
                    numKey1[swapInd] = tmp;
                }
            }

            // zamena kolona
            for (int i = 0; i < lenKey2; i++)
            {
                swapInd = Array.IndexOf(numKey2, i);
                if (i != swapInd)
                {
                    SwapColumns(mat, i, swapInd);
                    tmp = numKey2[i];
                    numKey2[i] = numKey2[swapInd];
                    numKey2[swapInd] = tmp;
                }
            }

            // smesti matricu u output niz
            for (int i = 0; i < lenKey1; i++)
                for (int j = 0; j < lenKey2; j++)
                    output[i * lenKey2 + j] = mat[i, j];

            return output;
        }
    }
}
