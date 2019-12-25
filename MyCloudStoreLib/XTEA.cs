using System;
using System.Collections.Generic;
using System.Text;

namespace MyCloudStoreLib
{
	public static class XTEA
	{
		private static void Prepare(string key, string ivs, uint[] uintkey, uint[] uintiv)
		{
			// kljuc, koji je string bilo koje duzine, hashujemo da bi dobili kljuc duzine 128 bita (16 bajta)
			byte[] keyhash = MD5.Hash(key);
			// vektor inicijalizacije 
			byte[] iv = Encoding.ASCII.GetBytes(ivs);

			// pretvorimo oba iz byte[] u uint[] jer xtea to ocekuje
			for (int i = 0; i < 4; i++)
			{
				uintkey[i] = 0;
				for (int j = 0; j < 4; j++)
					uintkey[i] += (uint)keyhash[i * 4 + j] << (j * 8);
			}
			for (int i = 0; i < 2; i++)
			{
				uintiv[i] = 0;
				for (int j = 0; j < 4; j++)
					uintiv[i] += (uint)iv[i * 4 + j] << (j * 8);
			}
		}

		public static byte[] Encrypt(byte[] input, string key, string ivs)
		{
			uint[] uintkey = new uint[4];
			uint[] uintiv = new uint[2];
			Prepare(key, ivs, uintkey, uintiv);

			long size = input.LongLength; // duzina ulaza
			long numBlocks = size / 8; // broj celih blokova ulaza
			long remainder = size % 8; // broj bajtova na kraju koji ne cine ceo blok
			if (remainder > 0) // ukoliko ima ostatka u zadnjem bloku, povecavamo broj blokova za 1
				numBlocks++;

			// dodajemo jos jedan blok koji ce sadrzati velicinu originalnog ulaza u bajtovima
			byte[] output = new byte[numBlocks * 8 + 8]; 
			Array.Copy(input, output, size);
			if (remainder > 0) // dopunjujemo zadnji blok nulama
				for (int i = 0; i < 8 - remainder; i++)
					output[size + i] = 0;

			// ofb mod: u prvom bloku se kriptuje vektor inicijalizacije
			// u svakom sledecem se kriptuje izlaz prethodnog
			// izlaz svakog se xor-uje sa odgovarajucim blokom ulaznog podatka (kopiranog u output) i salje na izlaz
			for (long i = 0; i < numBlocks; i++)
			{
				EncryptBlock(uintiv, uintkey);
				for (int j = 0; j < 8; j++)
					output[i * 8 + j] ^= (byte)(uintiv[j / 4] >> ((3 - (j % 4)) * 8));
			}

			byte[] sizeBytes = BitConverter.GetBytes(size);
			for (int i = 0; i < 8; i++)
				output[8 * numBlocks + i] = sizeBytes[i];

			return output;
		}

		public static byte[] Decrypt(byte[] input, string key, string ivs)
		{
			uint[] uintkey = new uint[4];
			uint[] uintiv = new uint[2];
			Prepare(key, ivs, uintkey, uintiv);

			long size = input.LongLength;
			// oduzimamo jedan blok, to je onaj gde cuvamo duzinu originalnog fajla
			// njega ne treba dekriptovati
			long numBlocks = size / 8 - 1;

			byte[] output = new byte[numBlocks * 8];
			Array.Copy(input, output, size - 8);

			for (long i = 0; i < numBlocks; i++)
			{
				EncryptBlock(uintiv, uintkey);
				for (int j = 0; j < 8; j++)
					output[i * 8 + j] ^= (byte)(uintiv[j / 4] >> ((3 - (j % 4)) * 8));
			}

			// izvucemo duzinu originalnog fajla i kopiramo dekriptovan izlaz
			// u novi byte[] te duzine
			byte[] origSizeB = new byte[8];
			for (int i = 0; i < 8; i++)
				origSizeB[i] = input[size - 8 + i];
			long origSize = BitConverter.ToInt64(origSizeB, 0);
			byte[] outputOrigSize = new byte[origSize];
			Array.Copy(output, outputOrigSize, origSize);


			return outputOrigSize;
		}

		// XTEA algortiam za kriptovanje jednog bloka
		// OFB mod se oslanja na simetricnost XOR operacije, tako da je potreban samo algoritam kriptovanja
		private static void EncryptBlock(uint[] block, uint[] key)
		{
			const int num_rounds = 64;
			uint v0 = block[0];
			uint v1 = block[1];
			uint delta = 0x9e3779b9;
			uint sum = 0;

			for (int i = 0; i < num_rounds; i++)
			{
				v0 += (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (sum + key[sum & 3]); // ((v1 << 4) xor (v2 >> 5) + v1) xor (sum + key[sum and 3])
				sum += delta;
				v1 += (((v0 << 4) ^ (v0 >> 5)) + v0) ^ (sum + key[(sum >> 11) & 3]); // ((v0 << 4) xor (v0 >> 5) + v1) xor (sum + key[(sum >> 11) and 3])
			}
			block[0] = v0;
			block[1] = v1;
		}
	}
}
