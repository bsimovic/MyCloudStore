using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCloudStoreLib;

namespace MyCloudStoreDebug
{
	class Program
	{
		static void Main(string[] args)
		{
			byte[] f = File.ReadAllBytes("C:\\izvestaj.pdf");
			byte[] o = XTEA.Encrypt(f, "branko", MD5.HashString(f));
			File.WriteAllBytes("D:\\crypted.xtea", o);
			byte[] enc = File.ReadAllBytes("D:\\crypted.xtea");
			byte[] dec = XTEA.Decrypt(enc, "brankox", MD5.HashString(f));
			File.WriteAllBytes("D:\\decrpyted.pdf", dec);


		}
	}
}
