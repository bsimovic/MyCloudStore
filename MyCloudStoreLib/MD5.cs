using System;
using System.Text;

namespace MyCloudStoreLib
{
	public static class MD5
	{
		private static uint LeftRotate(uint x, int c)
		{
			return ((x << c) | (x >> (32 - c)));
		}

		public static byte[] Hash(byte[] input)
		{
			int[] s = 
			{
				7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22,
				5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20,
				4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23,
				6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21 ,6, 10, 15, 21
			};
			
			// izracunati sinusi
			uint[] K =
			{
				0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee,
				0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501,
				0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be,
				0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821,
				0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa,
				0xd62f105d, 0x02441453, 0xd8a1e681, 0xe7d3fbc8,
				0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed,
				0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a,
				0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c,
				0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70,
				0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x04881d05,
				0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665,
				0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039,
				0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1,
				0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1,
				0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391
			};

			uint a0 = 0x67452301, b0 = 0xefcdab89, c0 = 0x98badcfe, d0 = 0x10325476;

			// preprocesiranje
			long size = input.LongLength;		// duzina ulaza u bajtovima
			long msgsize = size + 1;			// duzina poruke, prvi dodatan bajt
			long zerobytes = msgsize % 64;		// sledi racunica za broj bajtova "0000 0000" koji treba dodati
			if (zerobytes > 56)
				zerobytes = 120 - zerobytes;
			else if (zerobytes < 56)
				zerobytes = 56 - zerobytes;
			else
				zerobytes = 0;
			msgsize += zerobytes + 8;			// plus 8 bajta za duzinu ulaza


			byte[] msg = new byte[msgsize];
			Array.Copy(input, msg, size);			// kopiramo ulazni niz
			msg[size] = 0x80;						// dodajemo bajt sa jedinicom (1000 0000)
			for (long i = 0; i < zerobytes; i++)	// dodajemo nule
				msg[size + 1 + i] = 0;
			byte[] sizeBytes = BitConverter.GetBytes(size * 8);		// izdvajamo bajtove iz size, nema potrebe za "size mod 2^64" jer je long sam po sebi 64 bita
			for (int i = 0; i < 8; i++)
				msg[size + 1 + zerobytes + i] = sizeBytes[i];   // dodajemo na kraj poruke

			long numChunks = msgsize / 64;						// broj chunkova
			for (long chunk = 0; chunk < numChunks; chunk++)	// za svaki chunk...
			{
				uint[] M = new uint[16];        // delimo chunk na 16 dela od po 32 bita (4 bajta --- uint)
				for (int i = 0; i < 16; i++)
				{
					M[i] = 0;
					for (int j = 0; j < 4; j++)
						M[i] += (uint)msg[chunk * 64 + i * 4 + j] << (j * 8);
				}

				uint A = a0, B = b0, C = c0, D = d0;

				for (uint i = 0; i < 64; i++)
				{
					uint F, g;
					if ((0 <= i) && (i <= 15))
					{
						F = (B & C) | (~B & D);
						g = i;
					}
					else if ((16 <= i) && (i <= 31))
					{
						F = (D & B) | (~D & C);
						g = (5 * i + 1) % 16;
					}
					else if ((32 <= i) && (i <= 47))
					{
						F = B ^ C ^ D;
						g = (3 * i + 5) % 16;
					}
					else
					{
						F = C ^ (B | ~D);
						g = (7 * i) % 16;
					}

					F += A + K[i] + M[g];
					A = D;
					D = C;
					C = B;
					B += LeftRotate(F, s[i]);
				}

				a0 += A;
				b0 += B;
				c0 += C;
				d0 += D;
			}

			byte[] a0b = BitConverter.GetBytes(a0);
			byte[] b0b = BitConverter.GetBytes(b0);
			byte[] c0b = BitConverter.GetBytes(c0);
			byte[] d0b = BitConverter.GetBytes(d0);

			byte[] output = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				int t = i / 4;
				switch (t)
				{
					case 0: output[i] = a0b[i % 4]; break;
					case 1: output[i] = b0b[i % 4]; break;
					case 2: output[i] = c0b[i % 4]; break;
					case 3: output[i] = d0b[i % 4]; break;
				}
			}

			return output;
		}


		public static byte[] Hash(string input)
		{
			return Hash(Encoding.ASCII.GetBytes(input));
		}

		public static string HashString(byte[] input)
		{
			byte[] hash = Hash(input);
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < 16; i++)
				sb.Append(String.Format("{0:x2}", hash[i]));

			return sb.ToString();
		}

		public static string HashString(string input)
		{
			return HashString(Encoding.ASCII.GetBytes(input));
		}
	}
}
