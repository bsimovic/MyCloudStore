using System;
using System.Collections.Generic;
using System.Text;

namespace MyCloudStoreLib
{
	public static class XTEA
	{
		public static void Encrypt(byte[] input, string key)
		{
			throw new NotImplementedException();
		}

		private static void EncryptBlock(uint[] block, uint[] key)
		{
			if (block.Length != 2)
				throw new Exception("Block size needs to be 64.");
			if (key.Length != 4)
				throw new Exception("Key size needs to be 128 bits.");

			byte num_rounds = 64;
			uint v0 = block[0];
			uint v1 = block[1];
			uint delta = 0x9e3779b9; // 2,654,435,769 ???
			uint sum = 0;

			for (byte i = 0; i < num_rounds; i++)
			{
				v0 += (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (sum + key[sum & 3]); // ((v1 << 4) xor (v2 >> 5) + v1) xor (sum + key[sum and 3])
				sum += delta;
				v1 += (((v0 << 4) ^ (v0 >> 5)) + v0) ^ (sum + key[(sum >> 11) & 3]); // ((v0 << 4) xor (v0 >> 5) + v1) xor (sum + key[(sum >> 11) and 3])
			}
			block[0] = v0;
			block[1] = v1;
		}

		private static void DecryptBlock(uint[] block, uint[] key)
		{
			if (block.Length != 2)
				throw new Exception("Block size needs to be 64.");
			if (key.Length != 4)
				throw new Exception("Key size needs to be 128 bits.");

			const byte num_rounds = 64;
			uint v0 = block[0];
			uint v1 = block[1];
			uint delta = 0x9e3779b9;
			uint sum = delta * num_rounds;

			for (byte i = 0; i < num_rounds; i++)
			{
				v1 -= (((v0 << 4) ^ (v0 >> 5)) + v0) ^ (sum + key[(sum >> 11) & 3]);
				sum -= delta;
				v0 -= (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (sum + key[sum & 3]);
			}
			block[0] = v0;
			block[1] = v1;
		}
	}
}
