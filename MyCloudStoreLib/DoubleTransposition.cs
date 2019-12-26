using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace MyCloudStoreLib
{
    public static class DoubleTransposition
    {
        // uzmi prva 4 bajta iz hash-a za prvi kljuc i druga 4 za drugi
        // dva niza brojeva 0-7 bazirano na hash-u
        private static byte[][] GetKeysFromHash(byte[] hash)
        {
            byte[][] keys = new byte[2][];
            keys[0] = new byte[4];
            keys[1] = new byte[4];
            Array.Copy(hash, 0, keys[0], 0, 4);
            Array.Copy(hash, 4, keys[1], 0, 4);

            byte max1, max2;
            for (byte i = 0; i < 4; i++)
            {
                max1 = keys[0].Max();
                max2 = keys[1].Max();
                keys[0][Array.IndexOf(keys[0], max1)] = i;
                keys[1][Array.IndexOf(keys[1], max2)] = i;
            }

            return keys;
        }

        public static byte[] Encrypt(byte[] input, string key, string ivs)
        {
            byte[][] keys = GetKeysFromHash(MD5.Hash(key));
            byte[] ivhash = MD5.Hash(ivs);

            long size = input.LongLength;
            long numBlocks = size / 16;
            long remainder = size % 16;
            if (remainder > 0)
                numBlocks++;

            // dodajemo jos 8 bajta koji ce sadrziti duzinu originalnog fajla
            byte[] output = new byte[numBlocks * 16 + 8];
            Array.Copy(input, output, size);
            if (remainder > 0)
                for (int i = 0; i < 16 - remainder; i++)
                    output[size + i] = 0;

            for (long i = 0; i < numBlocks; i++)
            {
                EncryptBlock(ivhash, keys[0], keys[1]);
                for (int j = 0; j < 16; j++)
                    output[i * 16 + j] ^= ivhash[j];
            }

            byte[] sizeBytes = BitConverter.GetBytes(size);
            for (int i = 0; i < 8; i++)
                output[16 * numBlocks + i] = sizeBytes[i];

            return output;
        }

        public static byte[] Decrypt(byte[] input, string key, string ivs)
        {
            byte[][] keys = GetKeysFromHash(MD5.Hash(key));
            byte[] ivhash = MD5.Hash(ivs);

            // oduzimamo 8 od duzine, u zadnja 8 bajta je duzina originalnog fajla
            // njega ne treba dekriptovati
            long size = input.LongLength - 8;
            long numBlocks = size / 16;

            byte[] output = new byte[size];
            Array.Copy(input, output, size);

            for (long i = 0; i < numBlocks; i++)
            {
                EncryptBlock(ivhash, keys[0], keys[1]);
                for (int j = 0; j < 16; j++)
                    output[i * 16 + j] ^= ivhash[j];
            }

            byte[] origSizeB = new byte[8];
            for (int i = 0; i < 8; i++)
                origSizeB[i] = input[size + i];
            ulong origSize = BitConverter.ToUInt64(origSizeB, 0);
            byte[] outputOrigSize = new byte[origSize];
            for (ulong i = 0; i < origSize; i++)
                outputOrigSize[i] = output[i];

            return outputOrigSize;
        }

        // zameni mesta dvema vrstama
        private static void SwapRows(byte[,] mat, int a, int b)
        {
            byte tmp;
            for (int i = 0; i < 4; i++)
            {
                tmp = mat[a, i];
                mat[a, i] = mat[b, i];
                mat[b, i] = tmp;
            }
        }

        // zameni mesta dvema kolonama
        private static void SwapColumns(byte[,] mat, int a, int b)
        {
            byte tmp;
            for (int i = 0; i < 4; i++)
            {
                tmp = mat[i, a];
                mat[i, a] = mat[i, b];
                mat[i, b] = tmp;
            }
        }

        // racunamo da je jedan blok byte[16] 
        // a kljucevi byte[4] koji sadrze brojeve 0-3 razlicitim rasporedima
        private static void EncryptBlock(byte[] input, byte[] key1, byte[] key2)
        {
            // rasporedjivanje u matrici
            byte[,] mat = new byte[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    mat[i, j] = input[i * 4 + j];

            byte swapind;
            byte tmp;
            // zamena vrsta
            for (byte i = 0; i < 4; i++)
            {
               swapind = (byte)Array.IndexOf(key1, i);
               if (i != swapind)
               {
                    SwapRows(mat, i, swapind);
                    tmp = key1[i];
                    key1[i] = key1[swapind];
                    key1[swapind] = tmp;
                }
            }    

            // zamena kolona
            for (byte i = 0; i < 4; i++)
            {
                swapind = (byte)Array.IndexOf(key2, i);
                if (i != swapind)
               {
                    SwapColumns(mat, i, swapind);
                    tmp = key2[i];
                    key2[i] = key2[swapind];
                    key2[swapind] = tmp;
                }
            }

            // vrati matricu u niz
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                   input[i * 4 + j] = mat[i, j];
        }
    }
}
